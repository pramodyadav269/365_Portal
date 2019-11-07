var app = angular.module('MasterPage', []);

// CONTROLLER FUNCTIONS
app.controller("DefaultController", function ($scope, $rootScope, DataService) {
    objDs = DataService;
    objDs.DS_GetUserTopics();

    $scope.ActiveContainer = "Topic";
    $scope.NotificationText = "Notifications";

    $scope.GetModulesByTopic = function (topicId) {
        $scope.ActiveContainer = "Module";
        $scope.SelectedTopic = $rootScope.Topics.filter(function (v) {
            return topicId == v.TopicId;
        })[0];
        objDs.DS_GetModulesByTopic(topicId);
    }

    $scope.GetContentsByModule = function (topicId, moduleId) {
        $scope.ActiveContainer = "Content";
        $scope.SelectedModule = $rootScope.Module.UnlockedItems.filter(function (v) {
            return moduleId == v.ModuleID;
        })[0];
        objDs.DS_GetContentsByModule(topicId, moduleId);
    }

    $scope.ViewContent = function (topicId, moduleId, contentId, moduleName, type) {
        $scope.ContentGoBackText = moduleName;
        $scope.SelectedContent = $rootScope.Content.UnlockedItems.filter(function (v) {
            return contentId == v.ContentID;
        })[0];

        objDs.DS_GetContentDetails(topicId, moduleId, contentId);
        if (type.toLowerCase() == 'survey') {
            $scope.ActiveContainer = "ContentSurveyView";
        }
        else if (type.toLowerCase() == 'flashcard') {
            $scope.BeginFlashcard();
        }
        else if (type.toLowerCase() == 'finalquiz') {
            $scope.ActiveContainer = "ContentQuizView";
        }
        else {
            $scope.ActiveContainer = "ContentView";
            //Unlock Next Content
            objDs.DS_UpdateContent(topicId, moduleId, contentId);
        }
    }

    // Module Completed...
    $scope.UpdateContent = function (topicId, moduleId, contentIddd) {
        objDs.DS_UpdateContent(topicId, moduleId, contentIddd);
        $scope.GetModulesByTopic(topicId);
    }

    $scope.FlashcardPreviousClicked = function (index, total) {
        if (index == 0) {
            $scope.ShowFlashcardIntro();
        }
        else {
            $scope.ShowFlashcardSlides();
            $scope.CurrIndex = index - 1;
        }
    }

    $scope.SkipFlashcard = function (topicId, moduleId, contentId) {
        //Unlock Next Content
        objDs.DS_UpdateContent(topicId, moduleId, contentId);
    }

    $scope.FlashcardNextClicked = function (index, total) {
        if ((index + 1) == total) {
            $scope.ShowFlashcardQuiz();
        }
        else {
            $scope.ShowFlashcardSlides();
            $scope.CurrIndex = index + 1;
        }
    }

    $scope.FlashcardQuestionPrevioustClicked = function (index, total) {
        if (index == 0) {
            $scope.ShowFlashcardSlides();
        }
        else {
            $scope.ShowFlashcardQuiz();
            $scope.CurrIndex = index - 1;
        }
    }

    $scope.FlashcardQuestionNextClicked = function (topicId, moduleId, contentId,index, total) {
        if ((index + 1) == total) {
            $scope.ShowFinalQuizIntro();
            //Unlock Next Content
            objDs.DS_UpdateContent(topicId, moduleId, contentId);
        }
        else {
            $scope.ShowFlashcardQuiz();
            $scope.CurrIndex = index + 1;
        }
    }

    $scope.ShowFlashcardIntro = function () {
        $scope.ActiveContainer = "ContentFlashcardView";
        $scope.ActiveSubContainer = "FlashcardIntro";
    }

    $scope.BeginFlashcard = function () {
        $scope.ActiveContainer = "ContentFlashcardView";
       // $scope.ActiveSubContainer = "BeginFlashcard";
        $scope.ActiveSubContainer = "FlashcardIntro";
    }

    $scope.ShowFlashcardSlides = function () {
        $scope.ActiveContainer = "ContentFlashcardView";
        $scope.ActiveSubContainer = "FlashcardSlides";
        $scope.CurrIndex = 0;
    }

    $scope.GetSelectedValues = function (items) {
        var ids = "";
        for (var i = 0; i < items.length; i++) {
            if (items[i].IsSelected) {
                ids += items[i].AnswerID + ",";
            }
        }
        ids = ids.replace(/,\s*$/, "");
        return ids;
    }

    $scope.SubmitAnswers = function () {
        var cloneObj = $rootScope.SpecialContents;
        var questionList = [];

        $.each(cloneObj.Questions, function (key, question) {
            if (question.QuestionTypeID != '1') {
                // Single Selection
                questionList.push({
                    "QuestionID": question.QuestionID,
                    "AnswerIDs": question.Value_Text,
                    "FilePath": question.FilePath,// Base64
                    "Value_Text": question.QuestionTypeID == '8' ? GetFormattedDate(question.Value_Text) : question.Value_Text
                });
            }
            else {
                //Multiple Selection
                questionList.push({
                    "QuestionID": question.QuestionID,
                    "AnswerIDs": $scope.GetSelectedValues(question.AnswerOptions),
                    "FilePath": question.FilePath,// Base64
                    "Value_Text": question.Value_Text
                });
            }
        });

        var requestParams = {
            TopicID: cloneObj.TopicID
            , ModuleID: cloneObj.ModuleID
            , ContentID: cloneObj.ContentID
            , SurveyID: cloneObj.SurveyID
            , ContentType: cloneObj.Type
            , Questions: questionList
        };
        objDs.DS_SubmitAnswers(requestParams);
    }

    $scope.RetakeTest = function (topicId, moduleId, contentId, surveyId) {
        objDs.DS_RetakeTest(topicId, moduleId, contentId, surveyId);
    }

    $scope.GetFormattedDate = function (date) {
        return date.split("/").reverse().join("-");
    }

    $scope.ShowFlashcardQuiz = function () {
        $scope.ActiveContainer = "ContentFlashcardView";
        $scope.ActiveSubContainer = "FlashcardQuiz";
        $scope.CurrIndex = 0;
    }

    $scope.ShowFinalQuizIntro = function () {
        $scope.ActiveContainer = "ContentQuizView";
    }

    $scope.GoBack = function (prevPage) {
        $scope.ActiveContainer = prevPage;
        if (prevPage == 'Content')
            $("#dvVideoRating").hide();
    }

    $scope.RateVideo = function (topicId, moduleId, contentId, rating) {
        //Save Rating
        $("#dvVideoRating").hide();
        objDs.DS_RateContent(topicId, moduleId, contentId, rating);
        $scope.GoBack('Content');
    }
});

//COMMON SERVICE OPERATIONS
app.service("DataService", function ($http, $rootScope, $compile) {
    var ds = this;

    ds.DS_GetUserTopics = function (userId) {
        //var RequestParams = { UserID: userId };
        var requestParams = { contact_name: "Scott", company_name: "HP" };
        $http({
            method: "POST",
            url: "../api/Trainning/GetTopicsByUser",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: JSON.stringify(requestParams),

        }).then(function success(response) {
            var responseData = response.data;
            $rootScope.Topics = responseData.Data;
        });
    }

    ds.DS_GetModulesByTopic = function (topicId) {

        // var requestParams = { TopicId: topicId };
        //topicId = 25;
        var requestParams = { TopicID: topicId };
        $http({
            method: "POST",
            url: "../api/Trainning/GetModulesByTopic",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: requestParams,
        }).then(function success(response) {
            var responseData = response.data;
            $rootScope.Module = responseData;
        });

    }

    ds.DS_GetContentsByModule = function (topicId, moduleId) {

        var requestParams = { TopicID: topicId, ModuleID: moduleId };
        $http({
            method: "POST",
            url: "../api/Trainning/GetContentsByModule",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: requestParams,
        }).then(function success(response) {
            var responseData = response.data;
            $rootScope.Content = responseData;
        });
    }

    ds.DS_GetContentDetails = function (topicId, moduleId, contentId) {

        var requestParams = { TopicID: topicId, ModuleID: moduleId, ContentID: contentId };
        $http({
            method: "POST",
            url: "../api/Trainning/GetContentDetails",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: requestParams,
        }).then(function success(response) {
            var responseData = response.data;
            $rootScope.SpecialContents = responseData;
            if ($rootScope.SpecialContents.Type == 'CONTENT') {
                if ($rootScope.SpecialContents.DocType == 'VIDEO') {
                    $("#divVideo").html('<video id="vdVideoPlayer" onclick="VideoClicked(this)" onpause="VideoPaused(this)" class="section-video-main" autobuffer="" controls="" height="100%" width="100%">' +
                        '<source id="dvVideoPlayer" src="' + $rootScope.SpecialContents.FilePath + '" type="video/mp4">' +
                        '</video>');
                    document.getElementById('vdVideoPlayer').addEventListener('ended', VideoFinished, false);
                }
                else if ($rootScope.SpecialContents.DocType == 'PDF') {
                    $("#divPDF").html('<embed id="dvPDFViewer" src="' + $rootScope.SpecialContents.FilePath + '" width="760" height="800"/>');
                }
            }
        });

        // Call this to retrieve locked/unlocked contents.
        //DS_GetContentsByModule();
    }

    ds.DS_UpdateContent = function (topicId, moduleId, contentId) {
        var requestParams = { TopicID: topicId, ModuleID: moduleId, ContentID: contentId };
        $http({
            method: "POST",
            url: "../api/Trainning/UpdateContent",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: requestParams,
        }).then(function success(response) {
            var responseData = response.data;
            ds.DS_GetContentsByModule(topicId, moduleId);

            if (responseData.IsGift == true) {
                alert("Gift Received");
                $rootScope.ActiveContainer = "GiftReceived";
            }
        });
    }

    ds.DS_RateContent = function (topicId, moduleId, contentId, rating) {
        var requestParams = { TopicID: topicId, ModuleID: moduleId, ContentID: contentId, Rating: rating };
        $http({
            method: "POST",
            url: "../api/Trainning/RateContent",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: requestParams,
        }).then(function success(response) {
            var responseData = response.data;
        });
    }

    ds.DS_SubmitAnswers = function (requestParams) {
        $http({
            method: "POST",
            url: "../api/Trainning/SubmitAnswers",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: requestParams,
        }).then(function success(response) {
            var responseData = response.data;
            if (requestParams.ContentType == "SURVEY") {
                // Unlock Flashcard
                ds.DS_UpdateContent(requestParams.TopicID, requestParams.ModuleID, requestParams.ContentID);
            }
            else if (requestParams.ContentType == "FLASHCARD") {
                // Nothing to do
            }
            else if (requestParams.ContentType == "FINALQUIZ") {
                //To see answers
                ds.DS_GetContentDetails(requestParams.TopicID, requestParams.ModuleID, requestParams.ContentID);
            }
        });
    }

    ds.DS_RetakeTest = function (topicId, moduleId, contentId, surveyId) {
        var requestParams = { SurveyID: surveyId };
        $http({
            method: "POST",
            url: "../api/Trainning/RetakeTest",
            headers: {
                'Content-Type': 'application/json; charset=utf-8',
                "Authorization": "Bearer " + accessToken
            },
            data: requestParams,
        }).then(function success(response) {
            var responseData = response.data;
            ds.DS_GetContentDetails(topicId, moduleId, contentId);
        });
    }
});

app.directive('myPostRepeatDirective', function () {
    return function (scope, element, attrs) {
        if (scope.$last) {
            // iteration is complete, do whatever post-processing
            // is necessary
            $('select.select2').select2({
                placeholder: "Select a option",
                allowClear: true
            });

            bsCustomFileInput.init()

            $('.date').datepicker({ uiLibrary: 'bootstrap4', format: 'yyyy-dd-mm' });

            $('.custom-range').on('change', function () {
                $('label[for=' + this.id + ']').text('Value : ' + $(this).val());
            });
        }
    };
});

function LoadData() {
    var formdata = new FormData();
    formdata.append('PageSize', "10000");
    formdata.append('PageNumber', "1");

    $.ajax({
        "dataType": 'json',
        //headers: { "Authorization": "Bearer " + gbl_accessToken },
        "type": "POST",
        "contentType": false,
        "processData": false,
        "url": "../api/Topic/GetUserTopicsasd",
        "data": formdata,
        beforeSend: function () {
            //$(ctrl).parent().append('<div class="loading-small" style="position: fixed;top: 36%;right: 50%;"></div>')
        },
        complete: function () {
            //$('.loading-small').remove();
            // alert("Completed..");
        },
        "success": function (json) {
            try {
                var dataset = JSON.parse(json);
                $("#tblData").append("<tr><th>ID</th><th>Name</th><th>Address</th><th>MobileNo</th></tr>");
                $.each(dataset, function (key, value) {
                    $("#tblData").append("<tr><td>" + value.ID + "</td><td>" + value.Name + "</td><td>" + value.Address + "</td><td>" + value.MobileNo + "</td></tr>");
                });
            }
            catch (err) {
                alert(err);
                $('.loading-small').remove();
            }
        },
        error: function (xhr, textStatus, error) {
            $('.loading-small').remove();
            alert("error thrown..");
            if (typeof console == "object") {
                console.log(xhr.status + "," + xhr.responseText + "," + textStatus + "," + error);
            }
        }
    });
}
