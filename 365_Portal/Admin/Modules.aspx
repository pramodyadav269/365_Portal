<%@ Page Title="Modules" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="Modules.aspx.cs" Inherits="_365_Portal.Admin.Modules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="Topics.aspx"><i class="fas fa-arrow-left"></i>Back to Topics</a>
            <h1 class="text-center font-weight-bold">Modules</h1>
        </div>

        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <a class="btn bg-yellow" onclick="AddNew();">Add New</a> <a class="btn bg-blue text-white float-right" onclick="SaveGrdid();">Save Changes</a>
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
                                <label for="ddlTopic">Topic</label>
                                <select class="form-control select2 required" id="ddlTopic" style="width: 100% !important">
                               <%--     <option></option>
                                    <option value="1">Topic 1</option>
                                    <option value="2">Topic 2</option>
                                    <option value="3">Topic 3</option>--%>
                                </select>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtTitle">Title</label>
                                <input type="text" class="form-control required" id="txtTitle" placeholder="Title" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtOverview">Overview</label>
                                <textarea class="form-control required" placeholder="Overview" id="txtOverview"></textarea>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtDescription">Description</label>
                                <textarea class="form-control required" placeholder="Description" id="txtDescription"></textarea>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group checkbox required">
                                <label>Is Published</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbIsPublished" name="cgIsPublished" class="custom-control-input" value="1">
                                    <label class="custom-control-label" for="cbIsPublished">Yes</label>
                                </div>
                            </div>
                        </div>
                        <%--   <div class="col-md-3">
                            <div class="form-group checkbox required">
                                <label>Skip Flashcard</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbSkipFlashcard" name="cgcbSkipFlashcard" class="custom-control-input" value="1">
                                    <label class="custom-control-label" for="cbSkipFlashcard">Yes</label>
                                </div>
                            </div>
                        </div>--%>



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
            View();
            bindTopics();
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
                                for (var i = 0; i < Topic.length ; i++) {
                                    $('#ddlTopic').append('<option value="' + Topic[i].TopicID + '">' + Topic[i].Title + '</option>');
                                }
                            }
                            else {
                                swal({
                                    title: "Failure",
                                    text: DataSet.StatusDescription,
                                    type: "error"
                                });
                            }
                    },
                    complete: function () {
                    },
                    failure: function (response) {        
                        swal({
                            title: "Failure",
                            text: "Please try Again",
                            type: "error"
                        });
                    }
                });

            } catch (e)
            {

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
                $('#submit').text('EDIT');
                $('#back').text('CANCEL');
                inputValidation('.input-validation');
                //Submit button name attribute changed to EDIT(Modify);
            }
            else {
                swal({
                    title: "Failure",
                    text: "Please try Again",
                    type: "error"
                });
            }
        }
        function Submit() {
            var getUrl;
            ShowLoader();
            if (inputValidation('.input-validation')) {
                var _Topic_Id = $('#ddlTopic option:selected').val();
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
                    var requestParams = { TopicID: _Topic_Id, ModuleTitle: _Title, ModuleOverview: _Overview, ModuleDescription: _Description, IsPublished: _IsPublished, SrNo: _SrNo, UserID: "", IsActive: "", ModuleID: ID };


                    $.ajax({
                        type: "POST",
                        url: getUrl,
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                        success: function (response) {
                            try {

                                var DataSet = $.parseJSON(response);
                                console.log(response);
                                if (DataSet.StatusCode == "1") {
                                    clearFields('.input-validation');
                                    HideLoader();
                                    Swal.fire({
                                        title: "Success",
                                        text: DataSet.StatusDescription,
                                        icon: "success",
                                        button: "Ok",
                                    }).then((value) => {
                                        if (value) {
                                            toggle('divGird', 'divForm');
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
                                    clearFields('.input-validation');

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
                        icon: "error",
                        button: "Ok",
                    });
                }
            }
            else {
                HideLoader();
                Swal.fire({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error",
                    button: "Ok",
                });
            }
        }

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
                $('#submit').text('EDIT');
                $('#back').text('CANCEL');
                inputValidation('.input-validation');
                //Submit button name attribute changed to EDIT(Modify);
            }
            else {
                swal({
                    title: "Failure",
                    text: "Please try Again",
                    type: "error"
                });
            }
        }

        function Delete(ModuleId) {

            if ((TopicID != null && TopicID != undefined) && (ModuleId != null && ModuleId != undefined)) {

                Swal.fire({
                    title: "Are you sure?",
                    text: "Once deleted, you will not be able to revert changes!",
                    icon: "warning",
                    buttons: true,
                    dangerMode: true,
                })
                    .then((willDelete) => {
                        if (willDelete) {
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
                                            console.log(response);
                                            if (DataSet.StatusCode == "1") {
                                                HideLoader();
                                                Swal.fire({
                                                    title: "Success",
                                                    text: DataSet.StatusDescription,
                                                    icon: "success",
                                                    button: "Ok",
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
                                                    type: "error"
                                                });
                                            }
                                        }
                                        catch (e) {
                                            HideLoader();
                                            //alert(response);
                                            //alert(e.message);
                                            Swal.fire({
                                                title: "Failure",
                                                text: "Please try Again",
                                                type: "error"
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
                                            type: "error"
                                        });
                                    }
                                });
                            }
                            catch (e) {
                                HideLoader();
                                Swal.fire({
                                    title: "Alert",
                                    text: "Oops! An Occured. Please try again",
                                    icon: "error",
                                    button: "Ok",
                                });
                            }

                        }
                    });
            }
            else {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    type: "error"
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
                    //type: "GET",
                    //url: "https://reqres.in/api/users?page=1",
                    type: "POST",
                    url: url,
                    headers: { "Authorization": "Bearer " + accessToken },
                    data: JSON.stringify(requestParams),
                    contentType: "application/json",
                    processData: false,
                    success: function (response) {
                        try {
                            if (response) {
                                var DataSet = $.parseJSON(response);

                                if (DataSet.StatusCode == "1") {
                                    var tbl = '<table id="tblGird" class="table table-bordered" style="width: 100%">';
                                    tbl += '<thead><tr>';
                                    tbl += '<th>Sr.No.';
                                    tbl += '<th>Topic';
                                    tbl += '<th>Title';
                                    tbl += '<th>Overview';
                                    tbl += '<th>Description';
                                    tbl += '<th>Is Published';
                                    //tbl += '<th>Skip Flashcard';
                                    tbl += '<th>Contents';
                                    tbl += '<th>Survey';
                                    tbl += '<th>Flashcards';
                                    tbl += '<th>Flashcard Quiz';
                                    tbl += '<th>Final Quiz';
                                    //tbl += '<th>Personal Gifts';
                                    tbl += '<th>ACTION';
                                    tbl += '<tbody>';


                                    $.each(DataSet.Data, function (i, data) {
                                        if (data.IsPublished == "1") {
                                            data.IsPublished = "Yes";
                                        }
                                        else {
                                            data.IsPublished = "No";
                                        }
                                        tbl += '<tr id="' + data.ModuleID + '">';
                                        tbl += '<td>' + (i + 1);
                                        tbl += '<td class="topicname">' + data.TopicTitle;
                                        tbl += '<td class="title">' + data.Title;
                                        tbl += '<td class="overview">' + data.Overview;
                                        tbl += '<td class="description">' + data.Description;
                                        tbl += '<td class="isPublished">' + data.IsPublished;
                                        tbl += '<td><a href="Contents.aspx?ModuleID=' + data.ModuleID + '">'+data.ModuleID+'</a>';
                                        tbl += '<td><a href="Quiz.aspx?type=1&ModuleID=' + data.ModuleID + '">' + data.ModuleID + '</a>';
                                        tbl += '<td><a href="Flashcards.aspx?ModuleID=' + data.ModuleID + '">' + data.ModuleID + '</a>';
                                        tbl += '<td><a href="Quiz.aspx?type=2&ModuleID=' + data.ModuleID + '">' + data.ModuleID + '</a>';
                                        tbl += '<td><a href="Quiz.aspx?type=3&ModuleID=' + data.ModuleID + '">' + data.ModuleID + '</a>';
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        tbl += '<td><i title="Edit" onclick="Edit(' + data.ModuleID + ');" class="fas fa-edit text-warning"></i><i title="Delete" onclick="Delete(' + data.ModuleID + ');" class="fas fa-trash text-danger"></i>';


                                    });

                                    $('#divTable').empty().append(tbl)

                                    $('#tblGird').tableDnD()
                                }
                                else {
                                    swal({
                                        title: "Failure",
                                        text: DataSet.StatusDescription,
                                        type: "error"
                                    });
                                }

                            }
                            else {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    type: "error"
                                });
                            }
                        }
                        catch (e) {
                            HideLoader();
                            Swal.fire({
                                title: "Failure",
                                text: "Please try Again",
                                type: "error"
                            });
                        }
                    },
                    //var dTable = $('#tblGird').DataTable();

                    //dTable.on('order.dt search.dt', function () {
                    //    dTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    //        cell.innerHTML = i + 1;
                    //    });
                    //}).draw();


                    complete: function () {
                        HideLoader();
                    }
                });
            }
            catch (e) {
                Swal.fire({
                    title: "Failure",
                    text: "Please try Again",
                    type: "error"
                });
            }
        }

        function SaveGrdid() {

            var s;
            $('#tblGird').find('tr').each(function i(i, index) {
                if (this.id != "") {
                    s = s + this.id + ',';
                }
                console.log(this.id);
            });
            console.log(s.length);
            var _SrNo = s;

        }
        function back() {
            toggle('divGird', 'divForm');
            View();
        }

    </script>
</asp:Content>
