var app = angular.module('MasterPage', []);

// CONTROLLER FUNCTIONS
app.controller("DefaultController", function ($scope, DataService) {
    objDs = DataService;
    objDs.DS_GetUserTopics();

    $scope.ActiveContainer = "Topic";
    $scope.NotificationText = "Notifications";

    $scope.GetModulesByTopic = function () {
        $scope.ActiveContainer = "Module";
        objDs.DS_GetModulesByTopic();
    }

    $scope.GetContentsByModule = function () {
        $scope.ActiveContainer = "Content";
        objDs.DS_GetContentsByModule();
    }

    $scope.ViewContent = function (moduleName, type) {
        $scope.ContentGoBackText = moduleName;
        if (type != '') {
            if (type.toLowerCase() == 'survey') {
                $scope.ActiveContainer = "ContentSurveyView";
                objDs.DS_GetSurveyDetails();
            }
            else if (type.toLowerCase() == 'flashcard') {
                $scope.BeginFlashcard();
                objDs.DS_GetFlashcardDetails();
            }
            else if (type.toLowerCase() == 'quiz') {
                $scope.ActiveContainer = "ContentQuizView";
                objDs.DS_GetFinalQuizDetails();
            }
            else {
                $scope.ActiveContainer = "ContentView";
                objDs.DS_GetContentDetails();
            }
        }
        else {
            $scope.ActiveContainer = "ContentView";
            objDs.DS_GetContentDetails();
        }
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

    $scope.FlashcardQuestionNextClicked = function (index, total) {
        if ((index + 1) == total) {
            $scope.ShowFinalQuizIntro();
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
        $scope.ActiveSubContainer = "BeginFlashcard";
    }

    $scope.ShowFlashcardSlides = function () {
        $scope.ActiveContainer = "ContentFlashcardView";
        $scope.ActiveSubContainer = "FlashcardSlides";
        $scope.CurrIndex = 0;
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

    $scope.RateVideo = function (contentId, rating) {
        //Save Rating
        $("#dvVideoRating").hide();
        objDs.DS_RateContent(contentId, rating);
        $scope.GoBack('Content');
    }
});

//COMMON SERVICE OPERATIONS
app.service("DataService", function ($http, $rootScope, $compile) {
    var ds = this;

    ds.DS_GetUserTopics = function (userId) {

        $rootScope.Topics = [
            {
                "TopicID": 1,
                "Title": "Employee Conduct",
                "Description": "Life as an employee can be tough. Let's work together to make it easier.",
                "IsCompleted": true,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "4/10",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 1
            },
            {
                "TopicID": 2,
                "Title": "Workplace Equity",
                "Description": "How to be more accepting and bare with your colleagues.",
                "IsCompleted": false,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "4/10",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 2
            },
            {
                "TopicID": 3,
                "Title": "Staff Efficiency",
                "Description": "Increase your productivity while not losing motivation",
                "IsCompleted": false,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 3
            }
            ,
            {
                "TopicID": 4,
                "Title": "Motivation",
                "Description": "Increase your productivity while not losing motivation",
                "IsCompleted": false,
                "TotalModules": 10,
                "CompletedModules": 4,
                "Progress": "",
                "ProgressBarText": "4 of 10 Completed",
                "SortOrder": 4
            }
        ];

        /*
        $(".mainLoader").show();
        $http({
            method: "POST",
            url: "nlp_report_ajax.aspx?sid=" + QueryString()["sid"],
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            data: $.param({
                reqType: "5",
                ReportType: reportType
            })
        }).then(function success(response) {
            response = response.data;
            var aa = response.substring(response.indexOf("<Response>", 0) + 10, response.indexOf("</Response>", 0));
            aa = JSON.parse(aa);         
        });
        */
    }

    ds.DS_GetModulesByTopic = function (topicId) {
        $rootScope.Module = {
            "TopicId": 1,
            "TopicTitle": "Employee Conduct",
            "TopicDescription": "Employee Conduct",
            "TotalModules": 10, "CompletedModules": 4, "IsCompleted": false,
            "ProgressBarText": "4 of 10 Completed",
            "CompletedPercentage": (4 / 10) * 100,
            "UnlockedItems": [
                {
                    "ModuleId": 1,
                    "Title": "Introduction",
                    "Islocked": false,
                    "Description": "Life as an employee can be tough. Let's work together to make it easier.",
                    "IsCompleted": true,
                    "TotalContents": 10,
                    "CompletedContents": 4,
                    "Progress": "4/10",
                    "ProgressBarText": "4 of 10 Completed",
                    "SortOrder": 1
                },
                {
                    "ModuleId": 1,
                    "Title": "Employee Motivation",
                    "Islocked": false,
                    "Description": "How to be more accepting and bare with your colleagues.",
                    "IsCompleted": false,
                    "TotalContents": 10,
                    "CompletedContents": 4,
                    "Progress": "4/10",
                    "ProgressBarText": "4 of 10 Completed",
                    "SortOrder": 2
                },
                {
                    "ModuleId": 1,
                    "Title": "Ethical Excellence",
                    "Islocked": false,
                    "Description": "Increase your productivity while not losing motivation",
                    "IsCompleted": false,
                    "TotalContents": 10,
                    "CompletedContents": 4,
                    "Progress": "4/10",
                    "ProgressBarText": "4 of 10 Completed",
                    "SortOrder": 3
                }
            ],
            "LockedItems": [
                {
                    "ModuleId": 1,
                    "Title": "Motivation",
                    "Islocked": true,
                    "Description": "Increase your productivity while not losing motivation",
                    "IsCompleted": false,
                    "TotalContents": 10,
                    "CompletedContents": 4,
                    "Progress": "4/10",
                    "ProgressBarText": "4 of 10 Completed",
                    "SortOrder": 4
                }
            ]
        };

    }

    ds.DS_GetContentsByModule = function (moduleId) {
        $rootScope.Content = {
            "ModuleID": 1,
            "ModuleTitle": "Employee Conduct",
            "ModuleDescription": "Employee Conduct",
            "TotalContents": 10,
            "CompletedContents": 4,
            "IsLocked": false,
            "IsCompleted": false,
            "Progress": "4/10",
            "ProgressBarText": "4 of 10 Completed",
            "UnlockedItems": [
                {
                    "ContentId": 1,
                    "Title": "Content 1",
                    "Islocked": false,
                    "IsCompleted": false,
                    "FilePath": "FilePath",
                    "FileType": "Video",
                    "Type": "Content",
                    "Description": "Life as an employee can be tough. Let's work together to make it easier.",
                }],
            "LockedItems": [
                {
                    "Title": "Content 2",
                    "Islocked": false,
                    "IsCompleted": false,
                    "FilePath": "FilePath",
                    "FileType": "Video",
                    "Type": "Content",
                    "Description": "How to be more accepting and bare with your colleagues.",

                },
                {
                    "Title": "Content 3",
                    "Islocked": false,
                    "IsCompleted": false,
                    "FilePath": "FilePath",
                    "FileType": "Video",
                    "Type": "Content",
                    "Description": "Increase your productivity while not losing motivation",
                }
                ,
                {
                    "Title": "Content 4",
                    "Islocked": true,
                    "IsCompleted": false,
                    "FilePath": "FilePath",
                    "FileType": "",
                    "Type": "Flashcard",
                    "Description": "Increase your productivity while not losing motivation",
                }
                ,
                {
                    "Title": "Survey",
                    "Islocked": false,
                    "IsCompleted": false,
                    "FilePath": "FilePath",
                    "FileType": "",
                    "Type": "Survey",
                    "Description": "Increase your productivity while not losing motivation",
                }
                ,
                {
                    "Title": "Flashcard",
                    "Islocked": true,
                    "IsCompleted": false,
                    "FilePath": "FilePath",
                    "FileType": "",
                    "Type": "Flashcard",
                    "Description": "Increase your productivity while not losing motivation",
                }
                ,
                {
                    "Title": "Quiz",
                    "Islocked": true,
                    "IsCompleted": false,
                    "FilePath": "FilePath",
                    "FileType": "",
                    "Type": "Quiz",
                    "Description": "Increase your productivity while not losing motivation",
                }
            ]
        };
    }

    ds.DS_GetContentDetails = function (contentId) {
        // Update Content Details (Mark it as completed, activate next content)
        // Check Whether content is locked or unlocked
        $rootScope.SpecialContents = {
            "ContentID": 1,
            "Type": "CONTENT",
            "ContentTitle": "Employee Conduct",
            "ContentDescription": "Employee Conduct",
            //"FilePath": "http://18.209.29.195/ClientContents/User_1/Topic_1/Module_1/Content_1/PDFs/test.pdf#toolbar=0",
            "FilePath": "http://18.209.29.195/ClientContents/User_1/Topic_1/Module_1/Content_1/Videos/test.mp4",
            "FileType": "VIDEO",
            "IsLocked": false,
            "IsCompleted": false,
            "DisplaySkipFlashcard": true,
            "PassingPercentage": 70,
            "FlashcardIntro": {},
            "TotalFlashcardSlides": 2,
            "FlashcardSlides": [],
            "TotalQuestions": 8,
            "Questions": []
        };

        if ($rootScope.SpecialContents.Type == 'CONTENT') {
            if ($rootScope.SpecialContents.FileType == 'VIDEO') {
                $("#divVideo").html('<video id="vdVideoPlayer" onclick="VideoClicked(this)" onpause="VideoPaused(this)" class="section-video-main" autobuffer="" autoplay="autoplay" controls="" height="100%" width="100%">' +
                    '<source id="dvVideoPlayer" src="' + $rootScope.SpecialContents.FilePath + '" type="video/mp4">' +
                    '</video>');
                document.getElementById('vdVideoPlayer').addEventListener('ended', VideoFinished, false);
            }
            else if ($rootScope.SpecialContents.FileType == 'PDF') {
                $("#divPDF").html('<div style="width: 760px; position: absolute; top: 50%; left: 50%; transform: translate(-50%, -50%); overflow: hidden;">' +
                    '<embed id="dvPDFViewer" src="' + $rootScope.SpecialContents.FilePath + '" width="760" height="800">' +
                    '</div>');
            }
        }

        // Call this to retrieve locked/unlocked contents.
        //DS_GetContentsByModule();
    }

    ds.DS_GetSurveyDetails = function (contentId) {
        $rootScope.SpecialContents = {
            "ContentID": 1,
            "Type": "Survey",
            "ContentTitle": "Employee Conduct",
            "ContentDescription": "Employee Conduct",
            "FilePath": "",
            "FileType": "",
            "IsLocked": false,
            "IsCompleted": false,
            "DisplaySkipFlashcard": true,
            "PassingPercentage": 70,
            "FlashcardIntro": {},
            "TotalFlashcardSlides": 2,
            "FlashcardSlides": [],
            "TotalQuestions": 8,
            "Questions": [
                {
                    "QuestionID": 1,
                    "Title": "Multiple Choice Question",
                    "QuestionType": "1",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Choice 1",
                            "SortOrder": 1,
                            "Score": 1,
                            "IsCorrect": true,
                            "IsSelected": false,
                        },
                        {
                            "AnswerID": 2,
                            "AnswerText": "Choice 2",
                            "SortOrder": 1,
                            "IsCorrect": false,
                            "IsSelected": false,
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                },
                {
                    "QuestionID": 2,
                    "Title": "Dropdown",
                    "QuestionType": "2",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Item 1",
                            "SortOrder": 1,
                            "IsCorrect": false,
                            "IsSelected": false,
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Item 2",
                            "SortOrder": 1,
                            "IsCorrect": false,
                            "IsSelected": false,
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Item 3",
                            "SortOrder": 1,
                            "IsCorrect": false,
                            "IsSelected": false,
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                },
                {
                    "QuestionID": 3,
                    "Title": "Radio Button",
                    "QuestionType": "3",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Option 1",
                            "SortOrder": 1,
                            "IsCorrect": false,
                            "IsSelected": false,
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Option 2",
                            "SortOrder": 1,
                            "IsCorrect": false,
                            "IsSelected": false,
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 4,
                    "Title": "File Upload",
                    "QuestionType": "4",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 4,
                    "Title": "Rating Scale",
                    "QuestionType": "5",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 6,
                    "Title": "TextBox",
                    "QuestionType": "6",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 7,
                    "Title": "Paragraphs",
                    "QuestionType": "7",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": true,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 8,
                    "Title": "Date Time",
                    "QuestionType": "8",
                    "IsAnswered": false,
                    "IsMandatory": false,
                    "IsMultipleLine": true,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
            ]
        };
    }

    ds.DS_GetFlashcardDetails = function (contentId) {
        $rootScope.SpecialContents = {
            "ContentID": 1,
            "Type": "FLASHCARD",
            "ContentTitle": "Employee Conduct",
            "ContentDescription": "Employee Conduct",
            "FilePath": "http://18.209.29.195/ClientContents/User_1/Topic_1/Module_1/Content_1/Videos/test.mp4",
            "FileType": "VIDEO",
            "IsLocked": false,
            "IsCompleted": false,
            "DisplaySkipFlashcard": true,
            "PassingPercentage": 70,
            "FlashcardIntro": {
                "Title": "In this flashcard, we will answer",
                "Highlights": [
                    {
                        "Content": "What makes a good communicator?",
                        "SortOrder": 1
                    },
                    {
                        "Content": "How to motivate yourself being in the office?",
                        "SortOrder": 2
                    },
                    {
                        "Content": "How to be more proactive?",
                        "SortOrder": 3
                    }
                ]
            },
            "TotalFlashcardSlides": 2,
            "FlashcardSlides": [
                {
                    "Content": "This is flashcard content.",
                    "SortOrder": 1
                },
                {
                    "Content": "This is flashcard content.",
                    "SortOrder": 1
                }
            ],
            "TotalQuestions": 8,
            "Questions": [
                {
                    "QuestionID": 1,
                    "Title": "Multiple Choice Question",
                    "QuestionType": "1",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Choice 1",
                            "SortOrder": 1,
                            "Score": 1,
                            "IsCorrect": true
                        },
                        {
                            "AnswerID": 2,
                            "AnswerText": "Choice 2",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                },
                {
                    "QuestionID": 2,
                    "Title": "Dropdown",
                    "QuestionType": "2",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Item 1",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Item 2",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Item 3",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                },
                {
                    "QuestionID": 3,
                    "Title": "Radio Button",
                    "QuestionType": "3",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Option 1",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Option 2",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 4,
                    "Title": "File Upload",
                    "QuestionType": "4",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 4,
                    "Title": "Rating Scale",
                    "QuestionType": "5",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 6,
                    "Title": "TextBox",
                    "QuestionType": "6",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 7,
                    "Title": "Paragraphs",
                    "QuestionType": "7",
                    "IsMandatory": false,
                    "IsMultipleLine": true,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 8,
                    "Title": "Date Time",
                    "QuestionType": "8",
                    "IsMandatory": false,
                    "IsMultipleLine": true,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
            ]
        };
    }

    ds.DS_GetFinalQuizDetails = function (contentId) {
        $rootScope.SpecialContents = {
            "ContentID": 1,
            "Type": "FINALQUIZ",
            "ContentTitle": "Employee Conduct",
            "ContentDescription": "Employee Conduct",
            "FilePath": "http://18.209.29.195/ClientContents/User_1/Topic_1/Module_1/Content_1/PDFs/test.pdf#toolbar=0",
            "FileType": "",
            "IsLocked": false,
            "IsCompleted": false,
            "DisplaySkipFlashcard": true,
            "PassingPercentage": 70,
            "FlashcardIntro": {},
            "TotalFlashcardSlides": 2,
            "FlashcardSlides": [],
            "TotalQuestions": 8,
            "Questions": [
                {
                    "QuestionID": 1,
                    "Title": "Multiple Choice Question",
                    "QuestionType": "1",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Choice 1",
                            "SortOrder": 1,
                            "Score": 1,
                            "IsCorrect": true
                        },
                        {
                            "AnswerID": 2,
                            "AnswerText": "Choice 2",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                },
                {
                    "QuestionID": 2,
                    "Title": "Dropdown",
                    "QuestionType": "2",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Item 1",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Item 2",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Item 3",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                },
                {
                    "QuestionID": 3,
                    "Title": "Radio Button",
                    "QuestionType": "3",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [
                        {
                            "AnswerID": 1,
                            "AnswerText": "Option 1",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                        ,
                        {
                            "AnswerID": 2,
                            "AnswerText": "Option 2",
                            "SortOrder": 1,
                            "IsCorrect": false
                        }
                    ],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 4,
                    "Title": "File Upload",
                    "QuestionType": "4",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 4,
                    "Title": "Rating Scale",
                    "QuestionType": "5",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 6,
                    "Title": "TextBox",
                    "QuestionType": "6",
                    "IsMandatory": false,
                    "IsMultipleLine": false,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 7,
                    "Title": "Paragraphs",
                    "QuestionType": "7",
                    "IsMandatory": false,
                    "IsMultipleLine": true,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
                ,
                {
                    "QuestionID": 8,
                    "Title": "Date Time",
                    "QuestionType": "8",
                    "IsMandatory": false,
                    "IsMultipleLine": true,
                    "MaxLength": "150",
                    "SortOrder": 1,
                    "AnswerOptions": [],
                    "IsCorrect": false,
                    "ScoreEarned": 0,
                    "Value_Text": "",
                    "FileName": "",
                    "FilePath": ""
                }
            ]
        };
    }

    ds.DS_RateContent = function (contentId, rating) {
        // Ajax Call
    }

    ds.DS_SubmitAnswers = function (contentId, rating) {
        // Ajax Call
    }
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
        "url": "../api/Topic/GetUserTopics",
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
