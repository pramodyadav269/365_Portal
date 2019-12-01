<%@ Page Title="Quiz" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="quiz.aspx.cs" Inherits="_365_Portal.Admin.Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Quiz</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" id="back"><i class="fas fa-arrow-left"></i>Back to Modules</a>
            <h2 class="text-center font-weight-bold" id="lblTitle"></h2>
        </div>

        <div class="col-md-12" id="dvContentForm" style="display:none;">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row input-validation">
                        <div class="w-100"></div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtSurveyTitle">Title</label>
                                <input type="text" class="form-control required" id="txtSurveyTitle" placeholder="Title" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group checkbox required">
                                <label>Is Published</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="chkIsPublished" name="chkIsPublished" class="custom-control-input">
                                    <label class="custom-control-label" for="chkIsPublished">Yes</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3" id="trScoreSummary">
                            <div class="form-group">
                                <label class="float-left">Total Score: <span id="lblTotalScore">0</span></label>
                                <span class="float-right" id="lblPassingScore"></span>

                                <input type="range" class="custom-range" min="0" max="100" step="5" id="txtPassingScorePercentage" onchange="ChangePercentage(this.value);">

                                <span id="lblPercentage">0%</span>

                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSurveyDescription">Description</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Description" id="txtSurveyDescription"></textarea>
                            </div>
                        </div>

                        <%--  <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtSurveyOverview">Overview</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Overview" id="txtSurveyOverview"></textarea>
                            </div>
                        </div>--%>
                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <div class="float-right">
                                <a class="btn bg-yellow" id="btnSubmitFlashcard" onclick="SubmitChanges(true);return false;">Save Changes</a>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="col-md-12 mt-4" id="dvQuestionMasterForm" style="display: none;">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-12">
                        <a class="btn bg-yellow" id="btnAddQuestionForm" onclick="OpenQuestionForm(this);return false;">Add Question</a>
                            </div>
                        <div class="col-md-6" id="trQuestionForm" style="display: none;">
                            <div class="row input-validation">
                                <div class="form-header col-md-12">
                                    <h3>Questions</h3>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtQuestionTitle">Question Title</label>
                                        <input type="text" class="form-control required" id="txtQuestionTitle" placeholder="Question Title" />
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="ddlQuestionType">Question Type</label>
                                        <select class="form-control select2 required" id="ddlQuestionType" onchange="ChangeQuestionType();" style="width: 100% !important">
                                        </select>
                                    </div>
                                </div>
                                <div class="col-md-12" id="trIsBox" style="display: none;">
                                    <div class="form-group checkbox required">
                                        <label>Display Answer Options as Box</label>
                                        <div class="custom-control custom-checkbox custom-control-inline">
                                            <input type="checkbox" id="chkIsBox" name="chkIsBox" class="custom-control-input">
                                            <label class="custom-control-label" for="chkIsBox">Yes</label>
                                        </div>
                                    </div>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-md-12 mt-4">
                                    <div class="float-right">
                                        <a class="btn bg-yellow" id="btnAddQuestion" onclick="AddQuestion(this);return false;">Add Question</a>
                                        <a class="btn bg-yellow" id="btnCancelQuestion" onclick="CancelQuestion(this);return false;" style="display: none;">Cancel</a>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-6" id="trAnswerOptions" style="display: none;">
                            <div class="row input-validation">
                                <div class="form-header col-md-12">
                                    <h3>Answer Options</h3>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label for="txtTitle">Title</label>
                                        <input type="text" class="form-control required" id="txtTitle" placeholder="Title" />
                                    </div>
                                </div>
                                <div id="trCorrectAnsOption" style="display: none;">
                                    <div class="col-md-12">
                                        <div class="form-group checkbox required">
                                            <label>Is Correct</label>
                                            <div class="custom-control custom-checkbox custom-control-inline">
                                                <input type="checkbox" id="chkIsCorrect" name="chkIsCorrect" class="custom-control-input">
                                                <label class="custom-control-label" for="chkIsCorrect">Yes</label>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-12" id="dvQAnsOptionScore">
                                        <div class="form-group">
                                            <label for="txtScore">Score</label>
                                            <input type="number" class="form-control required" id="txtScore" value="0" />
                                        </div>
                                    </div>
                                </div>
                                <div class="w-100"></div>

                                <div class="col-md-12 mt-4">
                                    <div class="float-right">
                                        <a class="btn bg-yellow" id="btnAdd" onclick="AddAnsOption(this);return false;">Add Option</a>
                                        <a class="btn bg-yellow" id="btnCancelAnsOption" onclick="CancelAnsOption(this);return false;" style="display: none;">Cancel</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col-md-12">
                                    <%--<div id="dvFlashcardSlidesJson"></div>--%>
                                    <div class="mt-3 table-responsive">
                                        <table id="tblItems" class="table table-bordered" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th>Title</th>
                                                    <th id="thIsCorrect" style="display: none;">Is Correct</th>
                                                    <th id="thScore" style="display: none;">Score</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row mt-4">
                        <div class="col-md-12">
                            <div id="dvQuestionJson" style="display: none;"></div>
                            <div class="mt-3 table-responsive">
                                <table id="tblQuestions" class="table table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Sr No</th>
                                            <th>Question Type</th>
                                            <th>Title</th>
                                            <th id="thMaxScore">Max Score</th>
                                            <th>Is Box</th>
                                            <th>Answer Options</th>
                                            <th>Action</th>
                                        </tr>
                                    </thead>
                                    <tbody id="tBodyQuestions"></tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        var accessToken = '<%=Session["access_token"]%>';
        var contentType = QueryString()["type"];
        var pasingPercentage = 0;
        var passingScore = 0;

        var gbl_contentTypeID = 0;
        var gbl_ContentID = 0;
        var gbl_QuestionID = 0;
        var gbl_AnsOptionId = 0;
        var gbl_TopicID = QueryString()["TID"];
        var gbl_ModuleID = QueryString()["MID"];
        var Questions = [];
        var AnswerOptions = [];

        $(document).ready(function () {
            BindQuiz(true);
            BindQuestionTypes(contentType);

            $('#back').attr('href', "Modules.aspx?Id=" + gbl_TopicID);

            if (contentType == 1) {
                $("#tblItems #thIsCorrect").hide();
                $("#tblItems #thScore").hide();
                $("#lblTitle").text("Survey");
                $("#dvContentForm").show();
            }
            else if (contentType == 2) {
                $("#tblItems #thIsCorrect").show();
                $("#tblItems #thScore").hide();
                $("#lblTitle").text("Flashcard Quiz");
                $("#dvContentForm").hide();
            }
            else if (contentType == 3) {
                $("#tblItems #thIsCorrect").show();
                $("#tblItems #thScore").show();
                $("#lblTitle").text("Final Quiz");
                $("#dvContentForm").show();
            }
        });

        function BindQuiz(displayLoader) {
            if (displayLoader)
                ShowLoader();
            if (contentType == 1)
                gbl_contentTypeID = 2;
            else if (contentType == 2)
                gbl_contentTypeID = 3;
            else if (contentType == 3)
                gbl_contentTypeID = 5;
            var requestParams = { TopicID: gbl_TopicID, ModuleID: gbl_ModuleID, ContentID: "0", ContentTypeID: gbl_contentTypeID, IsGift: "0" };
            $.ajax({
                method: "POST",
                url: "../api/Quiz/GetContentList",
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
            }).then(function success(response) {
                HideLoader();
                var responses = JSON.parse(response);
                if (responses.Data.length > 0) {

                    var response = $.grep(responses.Data, function (n, i) {
                        return n.IsGift == false;
                    })[0];
                    gbl_ContentID = response.ContentID;

                    $("#dvQuestionMasterForm").show();
                    $("#txtSurveyTitle").val(response.Title);
                    $("#txtSurveyDescription").val(response.Description);
                    $("#chkIsPublished").prop("checked", response.IsPublished);
                    $("#lblPercentage").text(response.PassingPercent + "%");
                    $("#lblTotalScore").text(response.TotalScore);
                    $("#lblPassingScore").text("Passing Score: " + response.PassingScore);
                    $("#txtPassingScorePercentage").val(response.PassingPercent);

                    Questions = response.Questions == null ? [] : response.Questions;
                    BindQuestions(this);
                }
                else {
                    $("#dvQuestionMasterForm").hide();
                }
            });
        }

        function OpenQuestionForm() {
            $("#trQuestionForm").show();
            $("#trAnswerOptions").show();
            $("#btnCancelQuestion").show();
            gbl_QuestionID = 0;
            gbl_AnsOptionId = 0;
        }

        function CheckboxChecked(cntrl) {
            if (contentType == 2 || contentType == 3) {
                var totalChecked = 0;
                var totalItems = 0;
                $('#tblItems tr:has(td)').find('input[type="checkbox"]').each(function () {
                    if ($(this).prop("checked") == false) {
                    }
                    else {
                        totalChecked += 1;
                    }
                    totalItems += 1;
                });

                // Radio Button: Only 1 radio button should be chedked..
                if (totalChecked == 0) {
                    Swal.fire({
                        title: 'Failure',
                        icon: 'error',
                        html: "At least one option should be selected.",
                        showConfirmButton: true,
                        showCloseButton: true
                    });
                    $(cntrl).prop("checked", true);
                }
                else if (totalItems == totalChecked) {
                    Swal.fire({
                        title: 'Failure',
                        icon: 'error',
                        html: "All options can not be selected",
                        showConfirmButton: true,
                        showCloseButton: true
                    });
                    $(cntrl).prop("checked", false);
                }
                else {
                    if ($("#ddlQuestionType").val() == "2" || $("#ddlQuestionType").val() == "3") {
                        // Radio Button
                        if (totalChecked > 1) {
                            $('#tblItems tr:has(td)').find('input[type="checkbox"]').each(function () {
                                $(this).prop("checked", false);
                            });
                            $(cntrl).prop("checked", true);
                            $.grep(AnswerOptions, function (n, i) {
                                n.IsCorrect = false;
                            });
                        }
                    }
                    else {
                        // Checkbox
                    }
                }

                $.grep(AnswerOptions, function (n, i) {
                    return n.AnswerID == parseInt($(cntrl).attr("index"));
                })[0].IsCorrect = $(cntrl).prop("checked");

                if (contentType == 3) {
                    $.grep(AnswerOptions, function (n, i) {
                        if (n.IsCorrect == false && parseFloat(n.CorrectScore) > 0) {
                            n.CorrectScore = 0;
                        }
                    });
                    BindAnswerOptions();
                }
            }
        }

        function AddAnsOption(cntrl) {
            if (contentType == 1) {
                $("#tblItems #thIsCorrect").hide();
                $("#tblItems #thScore").hide();
            }
            else if (contentType == 2) {
                $("#tblItems #thIsCorrect").show();
                $("#tblItems #thScore").hide();
            }
            else if (contentType == 3) {
                $("#tblItems #thIsCorrect").show();
                $("#tblItems #thScore").show();
            }

            if ($("#txtTitle").val().trim() != "") {
                if (contentType == 3) {
                    if ($("#txtScore").val().trim() == "") {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Please enter score",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }
                    else if ($("#chkIsCorrect").prop("checked") == true && parseInt($("#txtScore").val()) <= 0) {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Score should be greater than 0",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }
                    else if ($("#chkIsCorrect").prop("checked") == false && parseInt($("#txtScore").val()) > 0) {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Score should be greater than 0 for incorrect value",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }
                }

                var index = AnswerOptions.length + 1;
                var newAnsOption = {
                    "ContentTypeID": gbl_contentTypeID
                    , "SrNo": index
                    , "AnswerText": $("#txtTitle").val()
                    , "IsCorrect": $("#chkIsCorrect").prop("checked")
                    , "CorrectScore": $("#txtScore").val()
                };

                if ($(cntrl).attr("index") == null || gbl_AnsOptionId == 0) {

                    if ($("#ddlQuestionType").val() == "2" || $("#ddlQuestionType").val() == "3") {
                        if (newAnsOption.IsCorrect == true) {
                            var totalChecked = 0;
                            $.grep(AnswerOptions, function (n, i) {
                                if (n.IsCorrect == true)
                                    totalChecked += 1;
                            });
                            if (totalChecked > 0) {
                                Swal.fire({
                                    title: 'Failure',
                                    icon: 'error',
                                    html: "Only one option should be selected",
                                    showConfirmButton: true,
                                    showCloseButton: true
                                });
                                return false;
                            }
                        }


                    }

                    if (IsTitleDuplicate('ANS', AnswerOptions, newAnsOption.AnswerText)) {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Answer option cannot be duplicate.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }

                    AnswerOptions.push(newAnsOption);
                    // Ajax Call - Add Answer Option
                    //newAnsOption.Action = 1;
                    //newAnsOption.Type = contentType;
                    //var requestParams = newAnsOption;
                    //$.ajax({
                    //    method: "POST",
                    //    url: "../api/Quiz/ManageAnsOptions",
                    //    headers: { "Authorization": "Bearer " + accessToken },
                    //    data: JSON.stringify(requestParams),
                    //    contentType: "application/json",
                    //}).then(function success(response) {
                    //});
                }
                else {
                    var index = parseInt($(cntrl).attr("index"));
                    if (IsTitleDuplicate('ANS', AnswerOptions, newAnsOption.AnswerText, index)) {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Answer option cannot be duplicate.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }

                    $.grep(AnswerOptions, function (n, i) {
                        if (gbl_QuestionID == 0 ? n.SrNo == index : n.AnswerID == index) {
                            n.AnswerText = newAnsOption.AnswerText;
                            n.IsCorrect = newAnsOption.IsCorrect;
                            n.CorrectScore = newAnsOption.CorrectScore;
                            return false;
                        }
                    })[0];


                    // Ajax Call - Update Answer Option
                    //answerOption.Action = 2;
                    //answerOption.Type = contentType;
                    //var requestParams = answerOption;
                    //$.ajax({
                    //    method: "POST",
                    //    url: "../api/Quiz/ManageAnsOptions",
                    //    headers: { "Authorization": "Bearer " + accessToken },
                    //    data: JSON.stringify(requestParams),
                    //    contentType: "application/json",
                    //}).then(function success(response) {
                    //});
                }

                CancelAnsOption(cntrl);
                BindAnswerOptions(cntrl);

            }
            else {
                Swal.fire({
                    title: 'Failure',
                    icon: 'error',
                    html: "Please enter all required fields.",
                    showConfirmButton: true,
                    showCloseButton: true
                });
            }
        }

        function IsTitleDuplicate(type, ansOptions, title, ID) {
            var duplicateTitle = false;
            if (type == 'ANS') {
                $.grep(ansOptions, function (n, i) {
                    if (n.AnswerText.trim().toUpperCase() == title.trim().toUpperCase() && n.AnswerID != ID) {
                        duplicateTitle = true;
                        return false;
                    }
                });
                return duplicateTitle;
            }
            else if (type == 'QUE') {
                $.grep(ansOptions, function (n, i) {
                    if (n.Title.trim().toUpperCase() == title.trim().toUpperCase() && n.QuestionID != ID) {
                        duplicateTitle = true;
                        return false;
                    }
                });
                return duplicateTitle;
            }
        }

        function ClearAnsOptionFields(cntrl) {
            $("#txtTitle").val("");
            $("#chkIsCorrect").prop("checked", false);
            $("#txtScore").val("0");
        }

        function CancelAnsOption(cntrl) {
            $("#btnCancelAnsOption").hide();
            $("#btnAdd").show();
            $("#btnAdd").text("Add Option");
            $("#btnAdd").removeAttr("index");
            ClearAnsOptionFields(this);
        }

        function DeleteAnsOption(row) {
            var index = row.attr("index");
            if (gbl_QuestionID == 0) {
                AnswerOptions = $.grep(AnswerOptions, function (n, i) {
                    return gbl_QuestionID == 0 ? n.SrNo != parseInt(index) : n.AnswerID != parseInt(index);
                });
                BindAnswerOptions();
                return;
            }
            Swal.fire({
                title: 'Are you sure?',
                text: "Once deleted, you will not be able to revert changes!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.value) {
                    AnswerOptions = $.grep(AnswerOptions, function (n, i) {
                        return n.AnswerID != parseInt(index);
                    });
                    var requestParams = { "Action": 3, "Type": contentType, "AnswerID": index, ContentTypeID: gbl_contentTypeID, ContentID: gbl_ContentID, QuestionID: gbl_QuestionID };
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageAnsOptions",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                    });

                    BindAnswerOptions();
                }
            });
            $("#dvQuestionJson").html(JSON.stringify(AnswerOptions));
        }

        function EditAnsOption(row) {
            var index = $(row).attr("index");
            var ansOption = {};
            gbl_AnsOptionId = index;
            if (gbl_QuestionID == 0) {
                ansOption = $.grep(AnswerOptions, function (n, i) {
                    return n.SrNo == parseInt(index);
                })[0];
            }
            else {
                ansOption = $.grep(AnswerOptions, function (n, i) {
                    return n.AnswerID == parseInt(index);
                })[0];
            }

            $("#txtTitle").val(ansOption.AnswerText);
            $("#chkIsCorrect").prop("checked", ansOption.IsCorrect);
            $("#txtScore").val(ansOption.CorrectScore);
            $("#btnAdd").text("Save Option");
            $("#btnAdd").attr("index", index);
            $("#btnCancelAnsOption").show();
        }

        function BindAnswerOptions() {

            //$("#dvJson").html(JSON.stringify(AnswerOptions));

            var tableBody = $("#tblItems tbody");
            tableBody.html("");

            if (AnswerOptions.length == 0) {
                tableBody.append("<td colspan='10'><center>No Answer Options</center></td>");
            }

            $.grep(AnswerOptions, function (n, i) {
                var checkedValue = n.IsCorrect == true ? "Checked" : "";
                var markup = "<tr>";
                markup += "<td>" + n.AnswerText + "</td>";
                if (contentType == 2 || contentType == 3)
                    markup += "<td><input index=" + (n.AnswerID > 0 ? n.AnswerID : n.SrNo) + " onchange='CheckboxChecked(this);' type='checkbox' " + checkedValue + " /></td>";
                if (contentType == 3)
                    markup += "<td>" + n.CorrectScore + "</td>";
                //markup += "<td index=" + n.AnswerID + " onclick ='EditAnsOption($(this))'>Edit</td>";
                //markup += "<td index=" + n.AnswerID + " onclick ='DeleteAnsOption($(this))'>Delete</td>";
                if (n.AnswerID > 0)
                    markup += '<td><i title="Edit" index=' + n.AnswerID + ' onclick="EditAnsOption($(this));" class="fas fa-edit text-warning"></i><i title="Delete" index=' + n.AnswerID + ' onclick="DeleteAnsOption($(this));" class="fas fa-trash text-danger"></i></td>';
                else
                    markup += '<td><i title="Edit" index=' + n.SrNo + ' onclick="EditAnsOption($(this));" class="fas fa-edit text-warning"></i><i title="Delete" index=' + n.SrNo + ' onclick="DeleteAnsOption($(this));" class="fas fa-trash text-danger"></i></td>';

                markup += "</tr>";
                tableBody.append(markup);
            });
        }

        //-------------Questions--------------//

        function DeleteQuestion(row) {
            var index = row.attr("index");

            Swal.fire({
                title: 'Are you sure?',
                text: "Once deleted, you will not be able to revert changes!",
                icon: 'warning',
                showCancelButton: true,
                confirmButtonColor: '#3085d6',
                cancelButtonColor: '#d33',
                confirmButtonText: 'Yes, delete it!'
            }).then((result) => {
                if (result.value) {
                    Questions = $.grep(Questions, function (n, i) {
                        return n.QuestionID != parseInt(index);
                    });
                    ShowLoader();
                    var requestParams = { "Action": 3, "Type": contentType, "ContentTypeID": gbl_contentTypeID, "QuestionID": index, ContentID: gbl_ContentID };
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageQuestion",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Question deleted successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindQuiz(false);
                    });
                }
            });
            $("#dvQuestionJson").html(JSON.stringify(Questions));
        }

        function EditQuestion(row) {
            var index = $(row).attr("index");

            var question = $.grep(Questions, function (n, i) {
                return n.QuestionID == parseInt(index);
            })[0];

            gbl_QuestionID = index;

            $("#txtQuestionTitle").val(question.Title);
            $("#ddlQuestionType").val(question.QuestionTypeID);
            $("#chkIsBox").prop("checked", question.IsBox);

            AnswerOptions = question.AnswerOptions;

            BindAnswerOptions();

            $("#trQuestionForm").show();
            if (question.QuestionTypeID == 1 || question.QuestionTypeID == 2 || question.QuestionTypeID == 3) {
                $("#trAnswerOptions").show();
                $("#trIsBox").show();
            }
            else {
                $("#trAnswerOptions").hide();
                $("#trIsBox").hide();
            }

            $("#btnAddQuestion").text("Save Question");
            $("#btnAddQuestion").attr("index", index);
            $("#btnCancelQuestion").show();

            $("#ddlQuestionType").trigger('change');
        }

        function BindQuestionTypes() {
            var items = '<option></option>';
            if (contentType == 1) {
                // Survey
                items += '<option value="1">Multiple Choice</option>'
                    + '<option value="2">Dropdown</option>'
                    + '<option value="3">Radio</option>'
                    + '<option value="4">File Upload</option>'
                    + '<option value="5">Scale</option>'
                    + '<option value="6">Textbox</option>'
                    + '<option value="7">Paragraph</option>'
                    + '<option value="8">DateTime</option>';
                $("#trScoreSummary").hide();
                $("#trCorrectAnsOption").hide();
                $("#dvQAnsOptionScore").hide();
                $("#chkIsBox").prop("checked", false);
                $("#chkIsBox").prop("disabled", false);
            }
            else if (contentType == 2) {
                // Flashcard
                items += '<option value="3" selected="true">Radio</option>';
                $("#ddlQuestionType").attr("disabled", "disabled");
                $("#trScoreSummary").hide();
                $("#trCorrectAnsOption").show();
                $("#dvQAnsOptionScore").hide();
                $("#chkIsBox").prop("checked", true);
                $("#chkIsBox").prop("disabled", true);

                $("#trIsBox").show();
                $("#trAnswerOptions").show();
            }
            else if (contentType == 3) {
                // Final Quiz
                items += '<option value="1">Multiple Choice</option>'
                    + '<option value="2">Dropdown</option>'
                    + '<option value="3">Radio</option>';
                $("#trScoreSummary").show();
                $("#trCorrectAnsOption").show();
                $("#dvQAnsOptionScore").show();
                $("#chkIsBox").prop("checked", false);
                $("#chkIsBox").prop("disabled", false);
            }
            $("#ddlQuestionType").html(items);

            $('#ddlQuestionType').select2({
                placeholder: "Select Question Type",
                allowClear: true
            });
        }

        function AddQuestion(cntrl) {
            if ($("#txtQuestionTitle").val().trim() != "" && $("#ddlQuestionType").val().trim() != "0") {
                if ($("#ddlQuestionType").val() == "1" || $("#ddlQuestionType").val() == "2" || $("#ddlQuestionType").val() == "3") {
                    if (AnswerOptions.length == 0) {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Please enter all required fields.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }
                    else if (contentType == 2 || contentType == 3) {
                        // At least one option should be checked
                        // All options cannot be selected
                        var checkedItemCount = 0;
                        var unCheckedItemCount = 0;
                        var totalItemCount = 0;
                        var totalScore = 0;
                        $.grep(AnswerOptions, function (n, i) {
                            totalItemCount += 1;
                            if (n.IsCorrect == true) {
                                checkedItemCount += 1;
                                totalScore += n.CorrectScore;
                            }
                            else {
                                unCheckedItemCount += 1;
                            }
                        })[0];

                        if (totalItemCount == checkedItemCount) {
                            Swal.fire({
                                title: 'Failure',
                                icon: 'error',
                                html: "All options cannot be selected",
                                showConfirmButton: true,
                                showCloseButton: true
                            });
                            return false;
                        }
                        else if (checkedItemCount == 0) {
                            Swal.fire({
                                title: 'Failure',
                                icon: 'error',
                                html: "At least one option should be checked",
                                showConfirmButton: true,
                                showCloseButton: true
                            });
                            return false;
                        }

                        if (contentType == 3 && totalScore <= 0) {
                            Swal.fire({
                                title: 'Failure',
                                icon: 'error',
                                html: "Score should be greater than 0",
                                showConfirmButton: true,
                                showCloseButton: true
                            });
                            return false;
                        }

                        if ($("#ddlQuestionType").val() == "2" || $("#ddlQuestionType").val() == "3") {
                            if (checkedItemCount > 1) {
                                Swal.fire({
                                    title: 'Failure',
                                    icon: 'error',
                                    html: "Multiple options cannot be selected",
                                    showConfirmButton: true,
                                    showCloseButton: true
                                });
                                return false;
                            }
                        }
                    }
                }

                var index = Questions.length + 1;
                var newQuestion = {
                    "ContentTypeID": gbl_contentTypeID
                    , Type: contentType
                    , "QuestionID": 0
                    , "ContentID": gbl_ContentID
                    , "SrNo": index
                    , "Title": $("#txtQuestionTitle").val()
                    , "QType": $("#ddlQuestionType").val()
                    , "IsBox": $("#chkIsBox").prop("checked")
                    , "MaxScore": GetMaxScore(AnswerOptions)
                    , "AnswerOptions": AnswerOptions
                };

                if ($(cntrl).attr("index") == null) {
                    if (IsTitleDuplicate('QUE', Questions, newQuestion.Title)) {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Question title cannot be duplicate.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }
                    //Questions.push(newQuestion);
                    // Save Passing Score in DB
                    if (contentType == 3)
                        SubmitChanges(false);

                    newQuestion.Action = 1;
                    newQuestion.Type = contentType;
                    // Ajax Call - Add Question
                    ShowLoader();
                    var requestParams = newQuestion;
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageQuestion",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Question details updated successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindQuiz(false);
                    });
                }
                else {
                    var index = parseInt($(cntrl).attr("index"));

                    if (IsTitleDuplicate('QUE', Questions, newQuestion.Title, index)) {
                        Swal.fire({
                            title: 'Failure',
                            icon: 'error',
                            html: "Question title cannot be duplicate.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        return false;
                    }

                    // Ajax Call - Update Question
                    // Save Passing Score in DB
                    if (contentType == 3)
                        SubmitChanges(false);
                    ShowLoader();
                    var question = $.grep(Questions, function (n, i) {
                        return n.QuestionID == index;
                    })[0];
                    newQuestion.Action = 2;
                    newQuestion.QuestionID = question.QuestionID;
                    var requestParams = newQuestion;
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageQuestion",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Question details updated successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindQuiz(false);
                    });
                }
                AnswerOptions = [];
                CancelQuestion(cntrl);
            }
            else {
                Swal.fire({
                    title: 'Failure',
                    icon: 'error',
                    html: "Please enter all required fields.",
                    showConfirmButton: true,
                    showCloseButton: true
                });
            }
        }

        function UpdateQuizMaxScore() {
            var maxScore = 0.0;
            $.grep(Questions, function (n, i) {
                maxScore += parseFloat(n.TotalScore);
            });
            $("#lblTotalScore").text(maxScore);

            ChangePercentage($("#txtPassingScorePercentage").val());
        }

        function GetMaxScore(ansOptions) {
            var maxScore = 0.0;
            $.grep(ansOptions, function (n, i) {
                if (n.IsCorrect == true) {
                    maxScore += parseFloat(n.CorrectScore);
                }
            });
            return maxScore;
        }

        function BindQuestions(cntrl) {

            $("#dvQuestionJson").html(JSON.stringify(Questions));

            var tableBody = $("#tblQuestions #tBodyQuestions");
            tableBody.html("");

            if (Questions.length == 0) {
                tableBody.append("<td colspan='10'><center>No Questions</center></td>");
            }

            if (contentType == 3) {
                $("#tblQuestions #thMaxScore").show();
            }
            else {
                $("#tblQuestions #thMaxScore").hide();
            }

            $.grep(Questions, function (n, i) {
                var ansOptions = "";

                $.grep(n.AnswerOptions, function (answOption, indx) {
                    if (indx == 0) {
                        ansOptions += "<tr><th>Sr No</th>";
                        ansOptions += "<th>Answer Text</th>";
                        if (contentType == 2 || contentType == 3) {
                            ansOptions += "<th>Is Correct</th>";
                        }
                        if (contentType == 3) {
                            ansOptions += "<th>Score</th></tr>";
                        }
                    }
                    var checkedValue = "disabled " + (answOption.IsCorrect == true ? "Checked" : "");
                    ansOptions += "<tr><td>" + answOption.SortOrder + "</td>";
                    ansOptions += "<td>" + answOption.AnswerText + "</td>";
                    if (contentType == 2 || contentType == 3) {
                        ansOptions += "<td>" + "<input index=" + answOption.AnswerID + " type='checkbox' " + checkedValue + " />" + "</td>";
                    }
                    if (contentType == 3) {
                        ansOptions += "<td>" + answOption.CorrectScore + "</td></tr>";
                    }
                });

                var isCheckedBoxValue = "disabled " + (n.IsBox == true ? "Checked" : "");
                var markup = "<tr>";
                markup += "<td>" + n.SortOrder + "</td>";
                markup += "<td>" + n.QType + "</td>";
                markup += "<td>" + n.Title + "</td>"
                if (contentType == 3) {
                    markup += "<td>" + n.TotalScore + "</td>"
                }
                markup += "<td><input index=" + n.QuestionID + " type='checkbox' " + isCheckedBoxValue + " />" + "</td>"
                markup += "<td><table  border='1'> " + ansOptions + "</table></td>";
                //markup += "<td index=" + n.QuestionID + " onclick ='EditQuestion($(this))'>Edit</td>";
                //markup += "<td index=" + n.QuestionID + " onclick ='DeleteQuestion($(this))'>Delete</td></tr>";
                markup += '<td><i title="Edit" index=' + n.QuestionID + ' onclick="EditQuestion($(this));" class="fas fa-edit text-warning"></i><i title="Delete" index=' + n.QuestionID + ' onclick="DeleteQuestion($(this));" class="fas fa-trash text-danger"></i></td>';

                tableBody.append(markup);
            });

            BindAnswerOptions(cntrl);

            UpdateQuizMaxScore();
        }

        function CancelQuestion(cntrl) {
            $("#btnCancelQuestion").hide();
            $("#btnAddQuestion").show();
            $("#btnAddQuestion").text("Add Question");
            $("#btnAddQuestion").removeAttr("index");
            $("#trQuestionForm").hide();
            ClearQuestionFields(this);
        }

        function ClearQuestionFields(cntrl) {
            if (contentType != 2) {
                $("#ddlQuestionType").val("");
                $("#chkIsBox").prop("checked", false);
                $("#trAnswerOptions").hide();
            }
            $("#txtQuestionTitle").val("");
            $("#tblItems tbody").html("");

            $("#ddlQuestionType").trigger('change');
        }

        function ChangePercentage(val) {
            pasingPercentage = val;
            $("#lblPercentage").text(val + "%");
            passingScore = Math.round((parseFloat($("#lblTotalScore").text()) * parseInt(val)) / 100);
            $("#lblPassingScore").text("Passing Score: " + passingScore);
        }

        function ChangeQuestionType() {
            var questionType = $("#ddlQuestionType").val();
            if (questionType == "1" || questionType == "2" || questionType == "3") {
                $("#trAnswerOptions").show();
                $("#trIsBox").show();
            }
            else {
                $("#trAnswerOptions").hide();
                $("#trIsBox").hide();
            }
        }

        function SubmitChanges(displayPopup) {
            var question = {
                "TopicID": gbl_TopicID
                , "ModuleID": gbl_ModuleID
                , "ContentID": gbl_ContentID
                , "ContentTypeID": gbl_contentTypeID
                , "Title": $("#txtSurveyTitle").val()
                , "Description": $("#txtSurveyDescription").val()
                , "IsPublished": $("#chkIsPublished").prop("checked")
                , "SkipFlashcard": false
                , "IsGift": false
                , "TotalScore": $("#lblTotalScore").text()
                , "PassingScore": $("#lblPassingScore").text().replace("Passing Score: ", "")
                , "PassingPercentage": $("#lblPercentage").text().replace("%", "")
                , "Questions": Questions
            };

            $("#dvQuestionJson").html(JSON.stringify(question));

            if ($("#txtSurveyTitle").val().trim() == "" || $("#txtSurveyDescription").val().trim() == "") {
                Swal.fire({
                    title: 'Failure',
                    icon: 'error',
                    html: "Please enter all required fields.",
                    showConfirmButton: true,
                    showCloseButton: true
                });
                return false;
            }
            else if (contentType == 1) {

            }
            else if (contentType == 2) {

            }
            else if (contentType == 3) {

            }

            // Make Ajax Call
            if (displayPopup)
                ShowLoader();
            var requestParams = question;
            $.ajax({
                method: "POST",
                url: "../api/Quiz/SaveContent",
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
            }).then(function success(response) {
                if (displayPopup) {
                    HideLoader();
                    Swal.fire({
                        title: 'Success',
                        icon: 'success',
                        html: "Information saved successfully.",
                        showConfirmButton: true,
                        showCloseButton: true
                    });
                    BindQuiz(false);
                }
            });
        }

        // Read a page's GET URL variables and return them as an associative array.
        function QueryString() {
            var vars = [], hash;
            var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < hashes.length; i++) {
                hash = hashes[i].split('=');
                vars.push(hash[0]);
                vars[hash[0]] = hash[1];
            }
            return vars;
        }
    </script>
</asp:Content>
