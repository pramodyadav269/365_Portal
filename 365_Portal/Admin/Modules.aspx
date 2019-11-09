<%@ Page Title="Modules" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="Modules.aspx.cs" Inherits="_365_Portal.Admin.Modules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
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
                                    <option></option>
                                    <option value="1">Topic 1</option>
                                    <option value="2">Topic 2</option>
                                    <option value="3">Topic 3</option>
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
                        <div class="col-md-3">
                            <div class="form-group checkbox required">
                                <label>Skip Flashcard</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbSkipFlashcard" name="cgcbSkipFlashcard" class="custom-control-input" value="1">
                                    <label class="custom-control-label" for="cbSkipFlashcard">Yes</label>
                                </div>
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

        $(document).ready(function () {

            View();
            bindTopics();
        });
        var accessToken = '<%=Session["access_token"]%>';
        var id = "";
        function bindTopics() {

        }

        function AddNew() {
            clearFields('.input-validation')
            toggle('divForm', 'divGird');
            $('#submit').attr('name', INSERT);
            //Submit button name attribute changed to Insert;
        }

        function Submit() {
            var getUrl;
            ShowLoader();
            if (inputValidation('.input-validation')) {
                if ($('#submit')[0].name == INSERT) {
                    getUrl = "/API/Content/CreateModule";
                } else {
                    getUrl = "API/Content/ModifyModule";
                }
                var _Topic_Id = $('#ddlTopic option:selected').val();
                var _Title = $('#txtTitle').val();
                var _Overview = $('#txtOverview').val();
                var _Description = $('#txtDescription').val();
                var _IsPublished = $('#cbIsPublished').val();
                var _SkipFlashcard = $('#cbSkipFlashcard').val();
                var _SrNo = "";
                try {
                    var requestParams = { TopicID: _Topic_Id, ModuleTitle: _Title, ModuleOverview: _Overview, ModuleDescription: _Description, IsPublished: _IsPublished, SrNo: _SrNo, UserID: "", IsActive: "", ModuleID: "1" };


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
                                    swal({
                                        title: "Success",
                                        text: DataSet.StatusDescription,
                                        icon: "success",
                                        button: "Ok",
                                    });


                                }
                                else {
                                    HideLoader();
                                    swal({
                                        title: "Failure",
                                        text: DataSet.StatusDescription,
                                        type: "error"
                                    });
                                    clearFields('.input-validation');
                                }
                            }
                            catch (e) {
                                HideLoader();
                                swal({
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
                            swal({
                                title: "Failure",
                                text: "Please try Again",
                                type: "error"
                            });
                        }
                    });
                }
                catch (e) {
                    HideLoader();
                    swal({
                        title: "Alert",
                        text: "Oops! An Occured. Please try again",
                        icon: "error",
                        button: "Ok",
                    });
                }
            }
            else {
                HideLoader();
                swal({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error",
                    button: "Ok",
                });
            }
        }

        function Edit(ModuleId) {
            //var id = $(ctrl).closest('tr').attr('id');
            toggle('divForm', 'divGird');
            $('#submit').attr('name', EDIT);

            $('#' + id).find("td:not(:last-child)").each(function (i, data) {
                if (this.className == 'title') {
                    $('#txtTitle').val(this.innerText); ///This will find title for Topic 

                }
                if (this.className == 'description') {
                    $('#txtDescription').val(this.innerText);
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
            //Submit button name attribute changed to EDIT(Modify);
        }

        function Delete(ModuleId) {
            
            id = ModuleId;
            swal({
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
                            var requestParams = { TopicID: id, ModuleID: ModuleId };
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
                                            swal({
                                                title: "Success",
                                                text: DataSet.StatusDescription,
                                                icon: "success",
                                                button: "Ok",
                                            });


                                        }
                                        else {
                                            HideLoader();
                                            swal({
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
                                        swal({
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
                                    swal({
                                        title: "Failure",
                                        text: "Please try Again",
                                        type: "error"
                                    });
                                }
                            });
                        }
                        catch (e) {
                            HideLoader();
                            swal({
                                title: "Alert",
                                text: "Oops! An Occured. Please try again",
                                icon: "error",
                                button: "Ok",
                            });
                        }

                    }
                });
        }

        function View() {
            var url = "/API/Content/GetModules";

            var requestParams = { TopicID: "", ModuleID: "1", ModuleTitle: "", ModuleOverview: "", ModuleDescription: "", IsPublished: "", SrNo: "", UserID: "", IsActive: "" };
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
                                    tbl += '<th>#';
                                    tbl += '<th>Topic';
                                    tbl += '<th>Title';
                                    tbl += '<th>Overview';
                                    tbl += '<th>Description';
                                    tbl += '<th>Is Published';
                                    //tbl += '<th>Skip Flashcard';
                                    //tbl += '<th>Total Contents';
                                    //tbl += '<th>Survey';
                                    //tbl += '<th>Flashcards';
                                    //tbl += '<th>Flashcard Quiz';
                                    //tbl += '<th>Final Quiz';
                                    //tbl += '<th>Personal Gifts';
                                    tbl += '<th>ACTION';
                                    tbl += '<tbody>';


                                    $.each(DataSet.data, function (i, data) {
                                        if (data.IsPublished == "1") {
                                            data.IsPublished = "Yes";
                                        }
                                        else {
                                            data.IsPublished = "No";
                                        }
                                        tbl += '<tr id="' + data.ModuleID + '">';
                                        tbl += '<td>' + (i + 1);
                                        tbl += '<td class="topicname">' + data.TopicName;
                                        tbl += '<td class="title">' + data.Title;
                                        tbl += '<td class="description">' + data.Description;
                                        tbl += '<td class="isPublished">' + data.IsPublished;
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        //tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                                        tbl += '<td><i title="Edit" onclick="Edit(' + data.ModuleID + ');" class="fas fa-edit text-warning"></i><i title="Delete" onclick="Delete(' + ata.ModuleID + ');" class="fas fa-trash text-danger"></i>';


                                    });

                                    $('#divTable').empty().append(tbl)

                                    $('#tblGird').tableDnD()
                                }
                                else {
                                    swal({
                                        title: "Warning",
                                        text: DataSet.StatusDescription,
                                        icon: "error",
                                        button: "Ok",
                                    });
                                }
                            }
                            else {
                                HideLoader();
                                swal({
                                    title: "Failure",
                                    text: "Please try Again",
                                    type: "error"
                                });
                            }
                        }
                        catch (e) {
                            HideLoader();
                            swal({
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
                swal({
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
