<%@ Page Title="Modules" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="Modules.aspx.cs" Inherits="_365_Portal.Admin.Modules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="Topics.aspx"><i class="fas fa-arrow-left"></i>Back to Topics</a>
            <h1 class="text-center font-weight-bold" id="module"></h1>
        </div>

        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body hide">
                    <a class="btn bg-yellow" onclick="AddNew();">Add New</a> <a class="btn bg-blue text-white float-right" style="display: none;" id="savereorder" onclick="SaveGrid();">Save Reordering</a>
                    <div class="w-100"></div>
                    <div id="divTable" class="mt-3 table-responsive"></div>
                </div>
            </div>
        </div>

        <div class="col-md-12 d-none" id="divForm">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">

                    <div class="row input-validation">


                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtTitle">Title</label>
                                <input type="text" class="form-control required" id="txtTitle" placeholder="Title" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group checkbox">
                                <label>Is Published</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbIsPublished" name="cgIsPublished" class="custom-control-input">
                                    <label class="custom-control-label" for="cbIsPublished">Yes</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtOverview">Overview</label>
                                <textarea class="form-control required" placeholder="Overview" id="txtOverview"></textarea>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtDescription">Description</label>
                                <textarea class="form-control required" placeholder="Description" id="txtDescription"></textarea>
                            </div>
                        </div>

                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" onclick="back()">Back</a>
                            <a class="btn bg-yellow float-right" id="submit" onclick="Submit();">Submit</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>
        var TopicID;
        var _ModuleID;
        $(document).ready(function () {
            TopicID = GetParameterValues('Id');
            bindTopics();
            View();

        });
        var accessToken = '<%=Session["access_token"]%>';
        var id = "";
        function bindTopics() {
            //Temporary Binding topics
            var getUrl = "/API/Content/GetTopics";
            var requestParams = { TopicID: "", TopicTitle: "", TopicDescription: "", IsPublished: "", SrNo: "", MinUnlockedModules: "", UserID: "", IsActive: "" };
            try {
                $.ajax({
                    type: "POST",
                    url: getUrl,
                    headers: { "Authorization": "Bearer " + accessToken },
                    data: JSON.stringify(requestParams),
                    contentType: "application/json",
                    processData: false,
                    success: function (response) {
                        var DataSet = $.parseJSON(response);
                        var Topic = DataSet.Data;
                        if (DataSet.StatusCode == "1") {
                            $('#ddlTopic').empty().append('<option></option>');
                            for (var i = 0; i < Topic.length; i++) {
                                if (TopicID == Topic[i].TopicID) {
                                    $('#module').html(Topic[i].Title);
                                }
                                //$('#ddlTopic').append('<option value="' + Topic[i].TopicID + '">' + Topic[i].Title + '</option>');
                            }
                        }
                        else {
                            Swal.fire({
                                title: "Failure",
                                text: DataSet.StatusDescription,
                                icon: "error"

                            });
                        }
                    },
                    complete: function () {
                    },
                    failure: function (response) {
                        Swal.fire({
                            title: "Failure",
                            text: "Please try Again",
                            icon: "error"
                        });
                    }
                });

            } catch (e) {

            }

        }

        function AddNew() {
            clearFields('.input-validation');
            $('#ddlTopic').val(TopicID).trigger("change");
            $('#ddlTopic').attr("disabled", true);
            toggle('divForm', 'divGird');
            $('#submit').attr('name', INSERT);
            $('#submit').text('SUBMIT');
            $('#back').text('BACK');
            //Submit button name attribute changed to Insert;
        }

        function Edit(ModuleId) {
            if (ModuleId != null && ModuleId != '') {
                _ModuleID = ModuleId; //Initalizing Global varaiable of Module ID;
                $('#ddlTopic').val(TopicID).trigger("change");
                $('#' + _ModuleID).find("td:not(:last-child)").each(function (i, data) {
                    if (TopicID != null || TopicID != undefined) {
                        $('#ddlTopic option:selected').val(TopicID); ///This will find title for Topic

                    }
                    if (this.className == 'title') {
                        $('#txtTitle').val(this.innerText); ///This will find title for Topic 

                    }
                    if (this.className == 'description') {
                        $('#txtDescription').val(this.innerText);
                    }
                    if (this.className == 'overview') {
                        $('#txtOverview').val(this.innerText);
                    }
                    if (this.className == 'isPublished') {
                        if (this.innerText == "Yes") {
                            $('#cbIsPublished').prop('checked', true);
                        }
                        else {
                            $('#cbIsPublished').prop('checked', false);
                        }

                    }
                });
                $('#ddlTopic').attr("disabled", true);
                toggle('divForm', 'divGird');
                $('#submit').attr('name', EDIT);
                $('#submit').text('UPDATE');
                $('#back').text('CANCEL');
                inputValidation('.input-validation');
                //Submit button name attribute changed to EDIT(Modify);
            }
            else {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    icon: "error"

                });
            }
        }
        function Submit() {
            var getUrl;
            ShowLoader();
            if (inputValidation('.input-validation')) {
                var _Topic_Id = TopicID;
                var _Title = $('#txtTitle').val();
                var _Overview = $('#txtOverview').val();
                var _Description = $('#txtDescription').val();
                var _IsPublished = $('#cbIsPublished').prop('checked');

                var ID;
                if ($('#submit')[0].name == INSERT) {
                    getUrl = "/API/Content/CreateModule";
                } else {
                    ID = _ModuleID;
                    getUrl = "/API/Content/ModifyModule";
                }


                var _SrNo = "";
                try {
                    var requestParams = { TopicID: _Topic_Id, ModuleTitle: _Title, ModuleOverview: _Overview, ModuleDescription: _Description, IsPublished: _IsPublished, SrNo: _SrNo, UserID: "", IsActive: true, ModuleID: ID };


                    $.ajax({
                        type: "POST",
                        url: getUrl,
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                        success: function (response) {
                            try {

                                var DataSet = $.parseJSON(response);
                                if (DataSet != null && DataSet != "") {
                                    if (DataSet.StatusCode == "1") {
                                        clearFields('.input-validation');
                                        HideLoader();
                                        Swal.fire({
                                            title: "Success",
                                            text: DataSet.StatusDescription,
                                            icon: "success",

                                        });
                                        toggle('divGird', 'divForm');
                                        View();
                                    }
                                    else {
                                        HideLoader();
                                        Swal.fire({
                                            title: "Failure",
                                            text: DataSet.StatusDescription,
                                            icon: "error"
                                        });
                                    }
                                    //clearFields('.input-validation');

                                }
                                else {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"
                                    });
                                }
                            }
                            catch (e) {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"
                                });
                            }
                        },
                        complete: function () {
                            HideLoader();
                        },
                        failure: function (response) {
                            HideLoader();
                            alert(response.data);
                            Swal.fire({
                                title: "Failure",
                                text: "Please try Again",
                                icon: "error"
                            });
                        }
                    });
                }
                catch (e) {
                    HideLoader();
                    Swal.fire({
                        title: "Alert",
                        text: "Oops! An Occured. Please try again",
                        icon: "error"

                    });
                }
            }
            else {
                HideLoader();
                Swal.fire({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error"

                });
            }
        }



        function Delete(ModuleId) {

            if ((TopicID != null && TopicID != undefined) && (ModuleId != null && ModuleId != undefined)) {

                Swal.fire({
                    title: 'Are you sure?',
                    text: "Do you want to delete user!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, delete it!'
                }).then((result) => {
                    if (result.value) {
                        ShowLoader();
                        try {
                            var requestParams = { TopicID: TopicID, ModuleID: ModuleId, IsActive: 0 };
                            var getUrl = "/API/Content/DeleteModule";

                            $.ajax({
                                type: "POST",
                                url: getUrl,
                                headers: { "Authorization": "Bearer " + accessToken },
                                data: JSON.stringify(requestParams),
                                contentType: "application/json",
                                success: function (response) {
                                    try {

                                        var DataSet = $.parseJSON(response);
                                        if (DataSet != null && DataSet != "") {
                                            if (DataSet.StatusCode == "1") {
                                                HideLoader();
                                                Swal.fire({
                                                    title: "Success",
                                                    text: DataSet.StatusDescription,
                                                    icon: "success",

                                                }).then((value) => {
                                                    if (value) {

                                                        View();
                                                    }
                                                });


                                            }
                                            else {
                                                HideLoader();
                                                Swal.fire({
                                                    title: "Failure",
                                                    text: DataSet.StatusDescription,
                                                    icon: "error"

                                                });
                                            }
                                        }
                                        else {
                                            HideLoader();
                                            Swal.fire({
                                                title: "Failure",
                                                text: "Please try Again",
                                                icon: "error"
                                            });
                                        }
                                    }
                                    catch (e) {
                                        HideLoader();
                                        Swal.fire({
                                            title: "Failure",
                                            text: "Please try Again",
                                            icon: "error"
                                        });
                                    }
                                },
                                complete: function () {
                                    HideLoader();
                                },
                                failure: function (response) {
                                    HideLoader();
                                    alert(response.data);
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"

                                    });
                                }
                            });
                        }
                        catch (e) {
                            HideLoader();
                            Swal.fire({
                                title: "Failure",
                                text: "Please try again",
                                icon: "error"

                            });
                        }

                    }
                });
            }
            else {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    icon: "error"

                });
            }
        }

        function View() {
            var requestParams;
            var url = "/API/Content/GetModules";
            if (TopicID != null || TopicID != undefined) {
                requestParams = { TopicID: TopicID, IsActive: "" };
            }

            ShowLoader();
            try {
                $.ajax({
                    type: "POST",
                    url: url,
                    headers: { "Authorization": "Bearer " + accessToken },
                    data: JSON.stringify(requestParams),
                    contentType: "application/json",
                    processData: false,
                    success: function (response) {
                        try {
                            var tbl = '<table id="tblGird" class="table table-bordered" style="width: 100%">';
                            tbl += '<thead><tr>';
                            tbl += '<th>Sr.No.';
                            tbl += '<th>Topic';
                            tbl += '<th>Title';
                            tbl += '<th>Overview';
                            tbl += '<th>Description';
                            tbl += '<th>Is Published';
                            //tbl += '<th>Skip Flashcard';
                            tbl += '<th>All Contents';
                            tbl += '<th>Survey';
                            tbl += '<th>Flashcards';
                            tbl += '<th>Final Quiz';
                            //tbl += '<th>Personal Gifts';
                            tbl += '<th>Action';
                            tbl += '<tbody>';
                            if (response) {
                                var DataSet = $.parseJSON(response);

                                if (DataSet != null && DataSet != "") {
                                    if (DataSet.StatusCode == "1") {


                                        if (DataSet.Data.length > 0) {
                                            $.each(DataSet.Data, function (i, data) {
                                                if (data.IsPublished == "1") {
                                                    data.IsPublished = "Yes";
                                                }
                                                else {
                                                    data.IsPublished = "No";
                                                }
                                                tbl += '<tr id="' + data.ModuleID + '">';
                                                tbl += '<td>' + (i + 1);
                                                tbl += '<td title="' + data.TopicTitle + '"  class="topicname">' + data.TopicTitle;
                                                tbl += '<td title="' + data.Title + '" class="title">' + data.Title;
                                                tbl += '<td title="' + data.Overview + '" class="overview">' + data.Overview;
                                                tbl += '<td title="' + data.Description + '" class="description">' + data.Description;
                                                tbl += '<td title="' + data.IsPublished + '" class="isPublished">' + data.IsPublished;
                                                tbl += '<td title="' + data.ModuleID + '"><a href="ContentList.aspx?TopicID=' + TopicID + '&ModuleID=' + data.ModuleID + '">' + data.ContentCount + '</a>';
                                                tbl += '<td title="' + data.ModuleID + '"><a href="Quiz.aspx?Type=1&TID=' + TopicID + '&MID=' + data.ModuleID + '">' + data.SurveyCount + '</a>';
                                                tbl += '<td title="' + data.ModuleID + '"><a href="Flashcards.aspx?Type=2&TID=' + TopicID + '&MID=' + data.ModuleID + '">' + data.FlashCardCount + '</a>';
                                                tbl += '<td title="' + data.ModuleID + '"><a href="Quiz.aspx?Type=3&TID=' + TopicID + '&MID=' + data.ModuleID + '">' + data.FinalQuizCount + '</a>';
                                                tbl += '<td><i title="Edit" onclick="Edit(' + data.ModuleID + ');" class="fas fa-edit text-warning"></i><i title="Delete" onclick="Delete(' + data.ModuleID + ');" class="fas fa-trash text-danger"></i>';


                                            });
                                        }
                                    }
                                    else {
                                        swal({
                                            title: "Failure",
                                            text: DataSet.StatusDescription,
                                            icon: "error"

                                        });
                                    }
                                }
                                else {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"

                                    });
                                }
                            }
                            else {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"

                                });
                            }
                        }
                        catch (e) {
                            HideLoader();
                            Swal.fire({
                                title: "Failure",
                                text: "Please try Again",
                                icon: "error"

                            });
                        }
                        $('#divTable').empty().append(tbl);
                        $('#tblGird').DataTable();
                        $('#tblGird').tableDnD({
                            onDragStart: function (table, row) {
                                $('#savereorder').show();

                            }
                        });
                    },
                    complete: function () {
                        HideLoader();
                    }
                });
            }
            catch (e) {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    icon: "error"

                });
            }
        }

        //This funcion is to get and save changes of Serial No
        function SaveGrid() {
            try {
                ShowLoader();
                var sqnData = "";
                var array = [];
                var url = "/API/Content/ReOrderContent";
                $.each($('#tblGird tbody tr'), function (i, data) {
                    //var obj = {};
                    //obj['id'] = $(data).attr('id');
                    //obj['title'] = $(data).find('.title').text();
                    //obj['sqn'] = i + 1;

                    //array.push(obj);
                    sqnData += $(data).attr('id') + ",";
                });
                sqnData = sqnData.replace(/,(?=\s*$)/, '');
                //sqnData = JSON.stringify(array);
                if (sqnData != "") {
                    var requestParams = { Type: "2", IDs: sqnData };
                    $.ajax({
                        type: "POST",
                        url: url,
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                        processData: false,
                        success: function (response) {
                            if (response != null && response != undefined) {
                                var DataSet = $.parseJSON(response);
                                if (DataSet != null && DataSet != "") {
                                    if (DataSet.StatusCode == "1") {
                                        if (DataSet.Data.length > 0) {
                                            $('#savereorder').hide();
                                            View();
                                        }
                                        else {
                                            Swal.fire({
                                                title: "Failure",
                                                text: "Please try Again",
                                                icon: "error"
                                            });
                                        }
                                    }
                                    else {
                                        Swal.fire({
                                            title: "Failure",
                                            text: DataSet.Data.ReturnMessage,
                                            icon: "error"
                                        });
                                    }
                                }
                                else {
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error"
                                    });
                                }
                            }
                            else {
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error"
                                });
                            }
                        },
                        complete: function () {
                            HideLoader();


                        }
                    });
                }
                else {
                    Swal.fire({
                        title: "Failure",
                        text: "Please try Again",
                        icon: "error"
                    });

                }

            }
            catch (e) {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    icon: "error"
                });
            }

        }
        function back() {
            toggle('divGird', 'divForm');
            View();
        }

    </script>
</asp:Content>
