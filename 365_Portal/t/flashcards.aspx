<%@ Page Title="Flashcards" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="flashcards.aspx.cs" Inherits="_365_Portal.Admin.Flashcards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Flashcards</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Flashcards</h1>
        </div>


        <div class="col-md-12">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row input-validation">
                        <div class="form-header col-md-12">
                            <h3>Flashcard</h3>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtFlashcardTitle">Flashcard Title</label>
                                <input type="text" class="form-control required" id="txtFlashcardTitle" placeholder="Title" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group checkbox required">
                                <label>Is Gift</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="chkIsGift" name="chkIsGift" class="custom-control-input">
                                    <label class="custom-control-label" for="chkIsGift">Yes</label>
                                </div>
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
                        <div class="col-md-2">
                            <div class="form-group checkbox required">
                                <label>Skip Flashcards</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="chkSkipFlashcards" name="chkSkipFlashcards" class="custom-control-input">
                                    <label class="custom-control-label" for="chkSkipFlashcards">Yes</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtFlashcardDescription">Description</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Description" id="txtFlashcardDescription"></textarea>
                            </div>
                        </div>

                        <%--     <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtFlashcardOverview">Overview</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Overview" id="txtFlashcardOverview"></textarea>
                            </div>
                        </div>--%>


                        <div class="col-md-12">
                            <div class="row input-validation">

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtIntroTitle">Introduction Title</label>
                                        <textarea class="form-control required" placeholder="Introduction Title" id="txtIntroTitle"></textarea>
                                    </div>
                                </div>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtDescription">Bullet Points</label>
                                        <input type="text" class="form-control required" id="txtDescription" placeholder="Bullet Points" />
                                    </div>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-md-12 mt-4">
                                    <div class="float-right">
                                        <a class="btn bg-yellow" id="btnAddIntroItem" onclick="AddIntroItem(this);return false;">Add Bullet</a>
                                        <a class="btn bg-yellow" id="btnCancelIntro" onclick="CancelIntro(this);return false;" style="display: none;">Cancel</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col-md-6">
                                    <div id="dvFlashcardIntroJson" style="display: none;"></div>
                                    <div class="mt-3 table-responsive">
                                        <table id="tblFlashcardIntro" class="table table-bordered" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th>Sr No</th>
                                                    <th>Bullet Point</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-12 mt-3">
                            <div class="row input-validation">
                                <div class="form-header col-md-12">
                                    <h3>Flashcard Slides</h3>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtSlideTitle">Title</label>
                                        <input type="text" class="form-control required" id="txtSlideTitle" placeholder="Title" />
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="txtSlideDescription">Description</label>
                                        <textarea class="form-control required" rows="4" cols="50" placeholder="Description" id="txtSlideDescription"></textarea>
                                    </div>
                                </div>
                                <div class="w-100"></div>

                                <div class="col-md-12 mt-4">
                                    <div class="float-right">
                                        <a class="btn bg-yellow" id="btnAddSlide" onclick="AddSlide(this);return false;">Add Slide</a>
                                        <a class="btn bg-yellow" id="btnCancelSlide" onclick="CancelSlides(this);return false;">Cancel</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-4">
                                <div class="col-md-6">
                                    <div id="dvFlashcardSlidesJson" style="display: none;"></div>
                                    <div class="mt-3 table-responsive">
                                        <table id="tblFlashcardSlides" class="table table-bordered" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th>Sr No</th>
                                                    <th>Title</th>
                                                    <th>Description</th>
                                                    <th>Action</th>
                                                </tr>
                                            </thead>
                                            <tbody></tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <div class="float-right">
                                <a class="btn bg-yellow" id="btnAddFlashcard" onclick="AddFlashcard(this);return false;">Add Flashcard</a>
                                <a class="btn bg-yellow" id="btnCancelFlashcard" onclick="CancelFlashcard(this);return false;">Cancel</a>
                            </div>
                        </div>

                        <div class="col-md-12 mt-3">
                            <div id="dvFlashcardJson"></div>
                            <div class="mt-3 table-responsive">
                                <table id="tblFlashcards" class="table table-bordered" style="width: 100%">
                                    <thead>
                                        <tr>
                                            <th>Sr No</th>
                                            <th>Title</th>
                                            <th>Description</th>
                                            <th>Intro Title</th>
                                            <th>Is Gift</th>
                                            <th>Is Published</th>
                                            <th>Skip Flashcards</th>
                                            <th colspan="2">Action</th>
                                        </tr>
                                    </thead>
                                    <tbody></tbody>
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
        var flashcardIntro = [];
        var flashcardSlides = [];
        var flashcards = [];

        //Flashcard
        var gbl_contentTypeID = 3;
        var gbl_ContentID = 0;
        var gbl_TopicID = QueryString()["TID"];
        var gbl_ModuleID = QueryString()["MID"];

        $(document).ready(function () {
            BindContentList(0);
            //BindFlashcards();
            //BindFlashcardIntro();
            //BindFlashcardSlides();
        });

        function BindContentList(contentId) {
            ShowLoader();
            var requestParams = { TopicID: gbl_TopicID, ModuleID: gbl_ModuleID, ContentID: 0, ContentTypeID: gbl_contentTypeID, IsGift: "0" };
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
                    flashcards = responses.Data == null ? [] : responses.Data;
                    BindFlashcards(this);
                    if (contentId != null && contentId != 0) {
                        var selectedContent = $.grep(responses.Data, function (n, i) {
                            return n.ContentID == contentId;
                        })[0];
                        // Bind Flashcard & Intro 
                        flashcardIntro = selectedContent.FlashcardIntro;
                        flashcardSlides = selectedContent.FlashcardSlides;
                        BindFlashcardIntro();
                        BindFlashcardSlides();
                    }
                }
                else {
                    $("#dvQuestionMasterForm").hide();
                }
            });
        }

        function AddFlashcard(cntrl) {
            if ($("#txtFlashcardTitle").val().trim() != "" && $("#txtFlashcardDescription").val().trim() != "" && $("#txtIntroTitle").val().trim() != "") {
                var index = flashcards.length + 1;
                var newFlashcard = {
                    "ContentID": 0
                    , "TopicID": gbl_TopicID
                    , "ModuleID": gbl_ModuleID
                    , "SrNo": index
                    , "ContentTypeID": gbl_contentTypeID
                    , "Title": $("#txtFlashcardTitle").val()
                    , "Description": $("#txtFlashcardDescription").val()
                    , "IntroTitle": $("#txtIntroTitle").val()
                    , "IsGift": $("#chkIsGift").prop("checked")
                    , "SkipFlashcard": $("#chkSkipFlashcards").prop("checked")
                    , "IsPublished": $("#chkIsPublished").prop("checked")
                    , "FlashcardIntro": flashcardIntro
                    , "FlashcardSlides": flashcardSlides
                    , "TotalScore": 0
                    , "PassingScore": 0
                    , "PassingPercentage": 0
                };

                if ($(cntrl).attr("index") == null) {
                    // Add Flashcard
                    // Ajax Call
                    //flashcards.push(newFlashcard);

                    var requestParams = newFlashcard;
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/SaveContent",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Information saved successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(0);
                    });
                }
                else {
                    // Update Flashcard Intro Item
                    var index = parseInt($(cntrl).attr("index"));
                    //var flashCard = $.grep(flashcards, function (n, i) {
                    //    return n.ContentID == index;
                    //})[0];
                    //flashCard.Title = newFlashcard.Title;
                    //flashCard.Description = newFlashcard.Description;
                    //flashCard.Overview = newFlashcard.Overview;
                    //flashCard.IntroTitle = newFlashcard.IntroTitle;
                    //flashCard.IsGift = newFlashcard.IsGift;
                    //flashCard.IsSkipFlashcards = newFlashcard.IsSkipFlashcards;
                    //flashCard.IsPublished = newFlashcard.IsPublished;
                    //flashCard.FlashcardIntro = newFlashcard.FlashcardIntro;
                    //flashCard.FlashcardSlides = newFlashcard.FlashcardSlides;
                    newFlashcard.ContentID = index;
                    var requestParams = newFlashcard;
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/SaveContent",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Information saved successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
                CancelFlashcard(cntrl);
                BindFlashcards(cntrl);
            }
            else {
                alert("Please enter all required fields.");
            }
        }

        function CancelFlashcard(cntrl) {
            $("#btnCancelFlashcard").hide();
            $("#btnAddFlashcard").show();
            $("#btnAddFlashcard").text("Add Flashcard");
            $("#btnAddFlashcard").removeAttr("index");
            ClearAllFields_Flashcard(this);
        }

        function ClearAllFields_Flashcard(cntrl) {
            $("#txtFlashcardTitle").val("");
            $("#txtFlashcardDescription").val("");
            $("#txtIntroTitle").val("");
            $("#chkIsGift").prop("checked", false);
            $("#chkSkipFlashcards").prop("checked", false);
            $("#chkIsPublished").prop("checked", false);
        }

        function EditFlashcard(row) {
            var index = $(row).attr("index");

            gbl_ContentID = index;
            var flashcard = $.grep(flashcards, function (n, i) {
                return n.ContentID == parseInt(index);
            })[0];

            flashcardIntro = flashcard.FlashcardIntro;
            flashcardSlides = flashcard.FlashcardSlides;
            BindFlashcardIntro();
            BindFlashcardSlides();

            $("#txtFlashcardTitle").val(flashcard.Title);
            $("#txtFlashcardDescription").val(flashcard.Description);
            $("#txtIntroTitle").val(flashcard.IntroTitle);
            $("#chkIsGift").prop("checked", flashcard.IsGift);
            $("#chkSkipFlashcards").prop("checked", flashcard.IsSkipFlashcards);
            $("#chkIsPublished").prop("checked", flashcard.IsPublished);

            $("#btnAddFlashcard").text("Save Flashcard");
            $("#btnAddFlashcard").attr("index", index);
            $("#btnCancelFlashcard").show();
        }

        function DeleteFlashcard(row) {
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
                    ShowLoader();
                    var requestParams = { Action: 3, ContentID: index };
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/DeleteContent",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Flashcard deleted successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
            });
        }

        function BindFlashcards(cntrl) {

            $("#dvFlashcardIntroJson").html(JSON.stringify(flashcards));

            var tableBody = $("#tblFlashcards tbody");
            tableBody.html("");

            if (flashcards.length == 0) {
                tableBody.append("<td colspan='10'><center>No Flashcards</center></td>");
            }

            $.grep(flashcards, function (n, i) {
                var isGiftValue = "disabled " + ((n.IsGift == true) ? "Checked" : "");
                var isPublishedValue = "disabled " + ((n.IsPublished == true) ? "Checked" : "");
                var isSkipFlashcards = "disabled " + ((n.IsSkipFlashcards == true) ? "Checked" : "");
                var markup = "<tr>";
                markup += "<td>" + n.SrNo + "</td>";
                markup += "<td>" + n.Title + "</td>";
                markup += "<td>" + n.Description + "</td>";
                markup += "<td>" + n.IntroTitle + "</td>";
                markup += "<td><input index=" + n.ContentID + " type='checkbox' " + isGiftValue + " /></td>";
                markup += "<td><input index=" + n.ContentID + " type='checkbox' " + isPublishedValue + " /></td>";
                markup += "<td><input index=" + n.ContentID + " type='checkbox' " + isSkipFlashcards + " /></td>";
                markup += '<td><i title="Edit" index=' + n.ContentID + ' onclick="EditFlashcard($(this));" class="fas fa-edit text-warning"></i><i title="Delete" index=' + n.ContentID + ' onclick="DeleteFlashcard($(this));" class="fas fa-trash text-danger"></i></td>';

                markup += "</tr>";
                tableBody.append(markup);
            });
        }

        //--------------Start of Flashcard Intro-------------//

        function AddIntroItem(cntrl) {
            if ($("#txtDescription").val().trim() != "") {
                var index = flashcardIntro.length + 1;
                var newFlashcard = { Action: 1, ContentID: gbl_ContentID, "ID": 0, "SrNo": index, Title: "", "Description": $("#txtDescription").val() };
                if ($(cntrl).attr("index") == null) {
                    // Ajax Call
                    var requestParams = newFlashcard;
                    ShowLoader();
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageFlashcardIntro",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Flashcarad Intro updated successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
                else {
                    // Update Flashcard Intro Item
                    var index = parseInt($(cntrl).attr("index"));
                    newFlashcard.ID = index;
                    newFlashcard.Action = 2;
                    var requestParams = newFlashcard;
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageFlashcardIntro",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Flashcarad Intro updated successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
                CancelIntro(cntrl);
                BindFlashcardIntro(cntrl);
            }
            else {
                alert("Please enter required fields");
            }
        }

        function CancelIntro(cntrl) {
            $("#btnCancelIntro").hide();
            $("#btnAddIntroItem").show();
            $("#btnAddIntroItem").text("Add Bullet");
            $("#btnAddIntroItem").removeAttr("index");
            ClearAllFields_Intro(this);
        }

        function ClearAllFields_Intro(cntrl) {
            $("#txtDescription").val("");
        }

        function EditIntroRow(row) {
            var index = $(row).attr("index");

            var introItem = $.grep(flashcardIntro, function (n, i) {
                return n.ID == parseInt(index);
            })[0];

            $("#txtDescription").val(introItem.Comments);
            $("#btnAddIntroItem").text("Save Item");
            $("#btnAddIntroItem").attr("index", index);
            $("#btnCancelIntro").show();
        }

        function DeleteIntroRow(row) {
            var index = row.attr("index");
            //flashcardIntro = $.grep(flashcardIntro, function (n, i) {
            //    return n.ID != parseInt(index);
            //});

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
                    ShowLoader();
                    var requestParams = { Action: 3, ContentID: gbl_ContentID, "ID": index };
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageFlashcardIntro",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Flashcard intro deleted successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
            });
        }

        function BindFlashcardIntro() {

            $("#dvFlashcardIntroJson").html(JSON.stringify(flashcardIntro));

            var tableBody = $("#tblFlashcardIntro tbody");
            tableBody.html("");

            if (flashcardIntro.length == 0) {
                tableBody.append("<td colspan='10'><center>No Bullet Points</center></td>");
            }

            $.grep(flashcardIntro, function (n, i) {
                var markup = "<tr>";
                markup += "<td>" + n.SrNo + "</td>";
                markup += "<td>" + n.Comments + "</td>";
                //markup += "<td index=" + n.ID + " onclick ='EditIntroRow($(this))'>Edit</td>";
                //markup += "<td index=" + n.ID + " onclick ='DeleteIntroRow($(this))'>Delete</td></tr>";
                markup += '<td><i title="Edit" index=' + n.ID + ' onclick="EditIntroRow($(this));" class="fas fa-edit text-warning"></i><i title="Delete" index=' + n.ID + ' onclick="DeleteIntroRow($(this));" class="fas fa-trash text-danger"></i></td>';


                markup += "</tr>";
                tableBody.append(markup);
            });
        }

        //--------------Start of Flashcard Slides-------------//

        function BindFlashcardSlides() {
            $("#dvFlashcardSlidesJson").html(JSON.stringify(flashcardSlides));

            var tableBody = $("#tblFlashcardSlides tbody");
            tableBody.html("");

            if (flashcardSlides.length == 0) {
                tableBody.append("<td colspan='10'><center>No Flashcards</center></td>");
            }

            $.grep(flashcardSlides, function (n, i) {
                var markup = "<tr>";
                markup += "<td>" + n.SrNo + "</td>";
                markup += "<td>" + n.Title + "</td>";
                markup += "<td>" + n.Description + "</td>";
                //markup += "<td index=" + n.ID + " onclick ='EditSlideRow($(this))'>Edit</td>";
                //markup += "<td index=" + n.ID + " onclick ='DeleteSlideRow($(this))'>Delete</td></tr>";
                markup += '<td><i title="Edit" index=' + n.FlashcardID + ' onclick="EditSlideRow($(this));" class="fas fa-edit text-warning"></i><i title="Delete" index=' + n.FlashcardID + ' onclick="DeleteSlideRow($(this));" class="fas fa-trash text-danger"></i></td>';

                markup += "</tr>";
                tableBody.append(markup);
            });
        }

        function EditSlideRow(row) {
            var index = $(row).attr("index");

            var slideItem = $.grep(flashcardSlides, function (n, i) {
                return n.FlashcardID == parseInt(index);
            })[0];

            $("#txtSlideTitle").val(slideItem.Title);
            $("#txtSlideDescription").val(slideItem.Description);
            $("#btnAddSlide").text("Save Slide");
            $("#btnAddSlide").attr("index", index);
            $("#btnCancelSlide").show();
        }

        function DeleteSlideRow(row) {
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
                    ShowLoader();
                    var requestParams = { Action: 3, ContentID: gbl_ContentID, "FlashcardID": index };
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageFlashcardSlides",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Flashcard deleted successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
            });
        }

        function AddSlide(cntrl) {
            if ($("#txtSlideTitle").val().trim() != "" && $("#txtSlideTitle").val().trim() != "" && $("#txtSlideDescription").val().trim() != "") {

                var index = flashcardSlides.length + 1;
                var newFlashCardSlide = { Action: 1, ContentID: gbl_ContentID, "ID": 0, "SrNo": index, "Title": $("#txtSlideTitle").val(), "Description": $("#txtSlideDescription").val() };

                if ($(cntrl).attr("index") == null) {
                    // Add Slide
                    // Ajax Call

                    if (IsTitleDuplicate(flashcardSlides, newFlashCardSlide.Title)) {
                        alert("Title cannot be duplicate.");
                        return false;
                    }

                    //flashcardSlides.push(newFlashCardSlide);

                    var requestParams = newFlashCardSlide;
                    ShowLoader();
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageFlashcardSlides",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Flashcarad Intro updated successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
                else {
                    // Update Slide
                    // Ajax Call

                    var index = parseInt($(cntrl).attr("index"));
                    if (IsTitleDuplicate(flashcardSlides, newFlashCardSlide.Title, index)) {
                        alert("Title cannot be duplicate.");
                        return false;
                    }

                    //var flashCard = $.grep(flashcardSlides, function (n, i) {
                    //    return n.ID == index;
                    //})[0];
                    //flashCard.Title = newFlashCardSlide.Title;
                    //flashCard.Description = newFlashCardSlide.Description;
                    //newFlashcard.ID = index;
                    //newFlashcard.Action = 2;
                    newFlashCardSlide.Action = 2;
                    newFlashCardSlide.FlashcardID = index;
                    var requestParams = newFlashCardSlide;
                    $.ajax({
                        method: "POST",
                        url: "../api/Quiz/ManageFlashcardSlides",
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                    }).then(function success(response) {
                        HideLoader();
                        Swal.fire({
                            title: 'Success',
                            icon: 'success',
                            html: "Flashcarad Intro updated successfully.",
                            showConfirmButton: true,
                            showCloseButton: true
                        });
                        BindContentList(requestParams.ContentID);
                    });
                }
                CancelSlides(cntrl);
                BindFlashcardSlides(cntrl);
            }
            else {
                alert("Please enter required fields");
            }
        }

        function CancelSlides(cntrl) {
            $("#btnCancelSlide").hide();
            $("#btnAddSlide").show();
            $("#btnAddSlide").text("Add Slide");
            $("#btnAddSlide").removeAttr("index");
            ClearAllFields_Slides(this);
        }

        function ClearAllFields_Slides(cntrl) {
            $("#txtSlideTitle").val("");
            $("#txtSlideDescription").val("");
        }

        function IsTitleDuplicate(jsonArray, title, ID) {
            var duplicateTitle = false;
            $.grep(jsonArray, function (n, i) {
                if (n.Title.trim().toUpperCase() == title.trim().toUpperCase() && n.ID != ID) {
                    duplicateTitle = true;
                    return false;
                }
            });
            return duplicateTitle;
        }

        //--------------End of Flashcard Slides-------------//
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
