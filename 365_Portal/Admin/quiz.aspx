<%@ Page Title="Quiz" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="quiz.aspx.cs" Inherits="_365_Portal.Admin.Quiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Quiz</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Quiz</h1>
        </div>
        <div>
            <h2>
                <span id="lblTitle"></span>
            </h2>
            <table>
                <tr>
                    <td>Title
                    </td>
                    <td>
                        <input type="text" id="txtSurveyTitle" />
                    </td>
                </tr>
                <tr>
                    <td>Description</td>
                    <td>
                        <textarea rows="4" cols="50" id="txtSurveyDescription"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>Overview</td>
                    <td>
                        <textarea rows="4" cols="50" id="txtSurveyOverview"></textarea>
                    </td>
                </tr>
                <tr>
                    <td>Is Published</td>
                    <td>
                        <input type="checkbox" id="chkIsPublished" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <button id="btnSubmitFlashcard" onclick="SubmitChanges(this);return false;">Save Changes</button>
                    </td>
                </tr>
                <tr id="trScoreSummary">
                    <td>Total Score:
                        <label id="lblTotalScore">100</label>
                    </td>
                    <td>Passing Score: 
                        <input type="range" min="0" max="100" value="75" id="txtPassingScorePercentage" step="5" onchange="ChangePercentage(this.value);" />
                        <label id="lblPercentage">75%</label>
                        <label id="lblPassingScore"></label>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <h2>Questions</h2>
                    </td>
                </tr>
                <tr>
                    <td>Question Title
                    </td>
                    <td>
                        <input type="text" id="txtQuestionTitle" />
                    </td>
                </tr>
                <tr>
                    <td>Question Type
                    </td>
                    <td>
                        <select name="QuestionType" id="ddlQuestionType" onchange="ChangeQuestionType();">
                        </select>
                    </td>
                </tr>
                <tr id="trIsBox" style="display: none;">
                    <td>Display Answer Options as Box</td>
                    <td>
                        <input type="checkbox" id="chkIsBox" />
                    </td>
                </tr>
                <tr id="trAnswerOptions" style="display: none;">
                    <td>
                        <h3>Answer Options</h3>
                        <table>
                            <tr>
                                <td>Answer Option</td>
                                <td>
                                    <input type="text" id="txtTitle" /></td>
                            </tr>
                            <tr id="trCorrectAnsOption" style="display: none;">
                                <td>Is Correct</td>
                                <td>
                                    <input type="checkbox" id="chkIsCorrect" />
                                    <input type="number" id="txtScore" style="display: none;" value="0" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <button id="btnAdd" onclick="AddItem(this);return false;">Add Option</button>
                                    <button id="btnCancelAnsOption" style="margin-left: 25px; display: none;" onclick="CancelAnsOption(this);return false;">Cancel</button>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table id="tblItems" border="1">
                            <thead>
                                <tr>
                                    <th>Title</th>
                                    <th id="thIsCorrect" style="display: none;">Is Correct</th>
                                    <th id="thScore" style="display: none;">Score</th>
                                    <th colspan="2">Action</th>
                                </tr>
                            </thead>
                            <tbody></tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <button id="btnAddQuestion" onclick="AddQuestion(this);return false;">Add Question</button>
                        <button id="btnCancelQuestion" style="margin-left: 25px; display: none;" onclick="CancelQuestion(this);return false;">Cancel</button>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <table id="tblQuestions" border="1">
                <thead>
                    <tr>
                        <th>Question Type</th>
                        <th>Title</th>
                        <th id="thMaxScore">Max Score</th>
                        <th>Is Box</th>
                        <th>Answer Options</th>
                        <th colspan="2">Action</th>
                    </tr>
                </thead>
                <tbody id="tBodyQuestions">
                </tbody>
            </table>
            <br />
            <br />
            <div id="dvQuestionJson"></div>
        </div>
    </div>
    <script>

        var contentType = QueryString()["type"];
        var pasingPercentage = 0;
        var passingScore = 0;
        Questions = [];
        AnswerOptions = [];

        $(document).ready(function () {
            BindQuestionTypes(contentType);

            if (contentType == 1) {
                $("#tblItems #thIsCorrect").hide();
                $("#tblItems #thScore").hide();
                $("#lblTitle").text("Survey");
            }
            else if (contentType == 2) {
                $("#tblItems #thIsCorrect").show();
                $("#tblItems #thScore").hide();
                $("#lblTitle").text("Flashcard Quiz");
            }
            else if (contentType == 3) {
                $("#tblItems #thIsCorrect").show();
                $("#tblItems #thScore").show();
                $("#lblTitle").text("Final Quiz");
            }

            BindQuestions(this);
        });

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
                    alert("At least one option should be selected.");
                    $(cntrl).prop("checked", true);
                }
                else if (totalItems == totalChecked) {
                    alert("All options can not be selected");
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
                        if (n.IsCorrect == false && parseFloat(n.Score) > 0) {
                            n.Score = 0;
                        }
                    });
                    BindAnswerOptions();
                }
            }
        }

        function AddItem(cntrl) {
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
                        alert("Please enter score");
                        return false;
                    }
                    else if ($("#chkIsCorrect").prop("checked") == true && parseInt($("#txtScore").val()) <= 0) {
                        alert("Score should be greater than 0");
                        return false;
                    }
                    else if ($("#chkIsCorrect").prop("checked") == false && parseInt($("#txtScore").val()) > 0) {
                        alert("Score should be greater than 0 for incorrect value");
                        return false;
                    }
                }

                var index = AnswerOptions.length + 1;
                var newAnsOption = { "AnswerID": (index * 10), "SrNo": index, "Title": $("#txtTitle").val(), "IsCorrect": $("#chkIsCorrect").prop("checked"), "Score": $("#txtScore").val() };

                if ($(cntrl).attr("index") == null) {
                    // Add New Record
                    // Ajax Call
                    if ($("#ddlQuestionType").val() == "2" || $("#ddlQuestionType").val() == "3") {
                        if (newAnsOption.IsCorrect == true) {
                            var totalChecked = 0;
                            $.grep(AnswerOptions, function (n, i) {
                                if (n.IsCorrect == true)
                                    totalChecked += 1;
                            });
                            if (totalChecked > 0) {
                                alert("Only one option should be selected");
                                return false;
                            }
                        }
                    }

                    if (IsTitleDuplicate('ANS', AnswerOptions, newAnsOption.Title)) {
                        alert("Answer option cannot be duplicate.");
                        return false;
                    }

                    AnswerOptions.push(newAnsOption);
                }
                else {

                    // Update Existing Record

                    var index = parseInt($(cntrl).attr("index"));
                    if (IsTitleDuplicate('ANS', AnswerOptions, newAnsOption.Title, index)) {
                        alert("Answer option cannot be duplicate.");
                        return false;
                    }

                    var answerOption = $.grep(AnswerOptions, function (n, i) {
                        return n.AnswerID == index;
                    })[0];
                    answerOption.Title = newAnsOption.Title;
                    answerOption.IsCorrect = newAnsOption.IsCorrect;
                    answerOption.Score = newAnsOption.Score;
                }

                CancelAnsOption(cntrl);
                BindAnswerOptions(cntrl);

            }
            else {
                alert("Please enter all required fields.");
            }
        }

        function IsTitleDuplicate(type, ansOptions, title, ID) {
            var duplicateTitle = false;
            if (type = 'ANS') {
                $.grep(ansOptions, function (n, i) {
                    if (n.Title.trim().toUpperCase() == title.trim().toUpperCase() && n.AnswerID != ID) {
                        duplicateTitle = true;
                        return false;
                    }
                });
                return duplicateTitle;
            }
            else if (type = 'QUE') {
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

        function DeleteRow(row) {
            var index = row.attr("index");
            AnswerOptions = $.grep(AnswerOptions, function (n, i) {
                return n.AnswerID != parseInt(index);
            });

            BindAnswerOptions();

            $("#dvQuestionJson").html(JSON.stringify(AnswerOptions));
        }

        function EditRow(row) {
            var index = $(row).attr("index");

            var ansOption = $.grep(AnswerOptions, function (n, i) {
                return n.AnswerID == parseInt(index);
            })[0];

            $("#txtTitle").val(ansOption.Title);
            $("#chkIsCorrect").prop("checked", ansOption.IsCorrect);
            $("#txtScore").val(ansOption.Score);
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
                markup += "<td>" + n.Title + "</td>";
                if (contentType == 2 || contentType == 3)
                    markup += "<td><input index=" + n.AnswerID + " onchange='CheckboxChecked(this);' type='checkbox' " + checkedValue + " /></td>";
                if (contentType == 3)
                    markup += "<td>" + n.Score + "</td>";
                markup += "<td index=" + n.AnswerID + " onclick ='EditRow($(this))'>Edit</td>";
                markup += "<td index=" + n.AnswerID + " onclick ='DeleteRow($(this))'>Delete</td>";
                markup += "</tr>";
                tableBody.append(markup);
            });
        }

        //-------------Questions--------------//

        function DeleteQuestion(row) {
            var index = row.attr("index");
            Questions = $.grep(Questions, function (n, i) {
                return n.QuestionID != parseInt(index);
            });

            BindQuestions();

            $("#dvQuestionJson").html(JSON.stringify(Questions));
        }

        function EditQuestion(row) {
            var index = $(row).attr("index");

            var question = $.grep(Questions, function (n, i) {
                return n.QuestionID == parseInt(index);
            })[0];

            $("#txtQuestionTitle").val(question.Title);
            $("#ddlQuestionType").val(question.QType);
            $("#chkIsBox").prop("checked", question.IsBox);

            AnswerOptions = question.AnswerOptions;

            BindAnswerOptions();

            if (question.QType == 1 || question.QType == 2 || question.QType == 3) {
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
        }

        function BindQuestionTypes() {
            var items = '<option value="0">--Select Question Type--</option>';
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
                $("#txtScore").hide();
                $("#chkIsBox").prop("checked", false);
                $("#chkIsBox").prop("disabled", false);
            }
            else if (contentType == 2) {
                // Flashcard
                items += '<option value="3" selected="true">Radio</option>';
                $("#ddlQuestionType").attr("disabled", "disabled");
                $("#trScoreSummary").hide();
                $("#trCorrectAnsOption").show();
                $("#txtScore").hide();
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
                $("#txtScore").show();
                $("#chkIsBox").prop("checked", false);
                $("#chkIsBox").prop("disabled", false);
            }
            $("#ddlQuestionType").html(items);
        }

        function AddQuestion(cntrl) {
            if ($("#txtQuestionTitle").val().trim() != "" && $("#ddlQuestionType").val().trim() != "0") {
                if ($("#ddlQuestionType").val() == "1" || $("#ddlQuestionType").val() == "2" || $("#ddlQuestionType").val() == "3") {
                    if (AnswerOptions.length == 0) {
                        alert("Please enter all required fields.");
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
                                totalScore += n.Score;
                            }
                            else {
                                unCheckedItemCount += 1;
                            }
                        })[0];

                        if (totalItemCount == checkedItemCount) {
                            alert("All options cannot be selected");
                            return false;
                        }
                        else if (checkedItemCount == 0) {
                            alert("At least one option should be checked");
                            return false;
                        }

                        if (contentType == 3 && totalScore <= 0) {
                            alert("Score should be greater than 0");
                            return false;
                        }

                        if ($("#ddlQuestionType").val() == "2" || $("#ddlQuestionType").val() == "3") {
                            if (checkedItemCount > 1) {
                                alert("Multiple options cannot be selected");
                                return false;
                            }
                        }
                    }
                }

                var index = Questions.length + 1;
                var newQuestion = {
                    "QuestionID": (index * 10)
                    , "SrNo": index
                    , "Title": $("#txtQuestionTitle").val()
                    , "QType": $("#ddlQuestionType").val()
                    , "IsBox": $("#chkIsBox").prop("checked")
                    , "MaxScore": GetMaxScore(AnswerOptions)
                    , "AnswerOptions": AnswerOptions
                };

                if ($(cntrl).attr("index") == null) {
                    // Add Question
                    // Ajax Call

                    if (IsTitleDuplicate('QUE', Questions, newQuestion.Title)) {
                        alert("Question title cannot be duplicate.");
                        return false;
                    }
                    Questions.push(newQuestion);
                }
                else {
                    // Update Question
                    // Ajax Call
                    var index = parseInt($(cntrl).attr("index"));

                    if (IsTitleDuplicate('QUE', Questions, newQuestion.Title, index)) {
                        alert("Question title cannot be duplicate.");
                        return false;
                    }

                    var question = $.grep(Questions, function (n, i) {
                        return n.QuestionID == index;
                    })[0];
                    question.Title = newQuestion.Title;
                    question.QType = newQuestion.QType;
                    question.IsBox = newQuestion.IsBox;
                    question.AnswerOptions = newQuestion.AnswerOptions;
                }
                AnswerOptions = [];
                CancelQuestion(cntrl);
                BindQuestions(cntrl);
            }
            else {
                alert("Please enter all required fields.");
            }
        }

        function UpdateQuizMaxScore() {
            var maxScore = 0.0;
            $.grep(Questions, function (n, i) {
                maxScore += parseFloat(n.MaxScore);
            });
            $("#lblTotalScore").text(maxScore);

            ChangePercentage($("#txtPassingScorePercentage").val());
        }

        function GetMaxScore(ansOptions) {
            var maxScore = 0.0;
            $.grep(ansOptions, function (n, i) {
                if (n.IsCorrect == true) {
                    maxScore += parseFloat(n.Score);
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
                    var checkedValue = "disabled " + (answOption.IsCorrect == true ? "Checked" : "");
                    ansOptions += "<tr><td>" + answOption.SrNo + "</td>";
                    ansOptions += "<td>" + answOption.Title + "</td>";
                    if (contentType == 2 || contentType == 3) {
                        ansOptions += "<td>" + "<input index=" + answOption.AnswerID + " type='checkbox' " + checkedValue + " />" + "</td>";
                    }
                    if (contentType == 3) {
                        ansOptions += "<td>" + answOption.Score + "</td></tr>";
                    }
                });

                var isCheckedBoxValue = "disabled " + (n.IsBox == true ? "Checked" : "");
                var markup = "<tr>";
                markup += "<td>" + n.QType + "</td>";
                markup += "<td>" + n.Title + "</td>"
                if (contentType == 3) {
                    markup += "<td>" + n.MaxScore + "</td>"
                }
                markup += "<td><input index=" + n.QuestionID + " type='checkbox' " + isCheckedBoxValue + " />" + "</td>"
                markup += "<td><table  border='1'> " + ansOptions + "</table></td>";
                markup += "<td index=" + n.QuestionID + " onclick ='EditQuestion($(this))'>Edit</td>";
                markup += "<td index=" + n.QuestionID + " onclick ='DeleteQuestion($(this))'>Delete</td></tr>";
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
            ClearQuestionFields(this);
        }

        function ClearQuestionFields(cntrl) {
            if (contentType != 2) {
                $("#ddlQuestionType").val("0");
                $("#chkIsBox").prop("checked", false);
                $("#trAnswerOptions").hide();
            }
            $("#txtQuestionTitle").val("");
            $("#tblItems tbody").html("");
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

        function SubmitChanges() {
            var question = {
                "Title": $("#txtSurveyTitle").val()
                , "Description": $("#txtSurveyDescription").val()
                , "Overview": $("#txtSurveyOverview").val()
                , "IsPublished": $("#chkIsPublished").val()
                , "TotalScore": $("#lblTotalScore").text()
                , "PassingScore": passingScore
                , "PassingPercentage": pasingPercentage
                , "Questions": Questions
            };

            $("#dvQuestionJson").html(JSON.stringify(question));

            if ($("#txtSurveyTitle").val().trim() == "") {
                alert("Please enter all required fields.");
            }
            else if (contentType == 1) {

            }
            else if (contentType == 2) {

            }
            else if (contentType == 3) {

            }
            // Make Ajax Call
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
