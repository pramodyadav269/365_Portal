<%@ Page Title="" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="Achievement.aspx.cs" Inherits="_365_Portal.Admin.Achievement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Topics</h1>
        </div>

        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <a class="btn bg-yellow float-left" onclick="AddNew();">Add New</a> <a class="btn bg-blue text-white float-right">Save Changes</a>
                    <div class="w-100"></div>
                    <div id="divTable" class="mt-5 table-responsive"></div>
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
                                <input type="text" class="form-control required" id="txtTitle" maxlength="50" placeholder="Title" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtDescription">Description</label>
                                <textarea class="form-control required" placeholder="Description" id="txtDescription"></textarea>
                            </div>
                        </div>

                         <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtlongDescription">Long Description</label>
                                <textarea class="form-control required" placeholder="Description" id="txtlongDescription"></textarea>
                            </div>
                        </div>

                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" id="back" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" id="submit" onclick="Submit();">Submit</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {
            //var s; $('#tblGird').find('tr').each(function i(i, index) { if (this.id != "") { s = s + this.id + ','; } console.log(this.id); }); console.log(s.length);


            View();
        });
        var accessToken = '<%=Session["access_token"]%>';
        var id;
        function AddNew() {

            clearFields('.input-validation')
            toggle('divForm', 'divGird')
            $('#submit').attr('name', INSERT);
            $('#submit').text('SUBMIT');
            $('#back').text('BACK');
            //Submit button name attribute changed to Insert;
        }

        function Submit() {

            var getUrl;
            var requestParams;
            ShowLoader();
            if (inputValidation('.input-validation')) {
                var _Achievement_Id;
                var _SrNo = "1";
                var _Title = $('#txtTitle').val();
                var _Description = $('#txtDescription').val();
                var _LongDescription = $('#txtlongDescription').val();
                if ($('#submit')[0].name == INSERT) {
                    getUrl = "/API/Achievement/CreateAchievement";

                } else {
                    _Achievement_Id = id;
                    getUrl = "/API/Achievement/ModifyAchievement";

                }

                requestParams = { AchievementID: _Achievement_Id, AchievementTitle: _Title, AchivementDescription: _Description, LongDescription: _LongDescription, SrNo: _SrNo,  IsActive: "" };

                try {




                    $.ajax({
                        type: "POST",
                        url: getUrl,
                        headers: { "Authorization": "Bearer " + accessToken },
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                        success: function (response) {
                            try {
                                if (response != null) {
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
                                            icon: "error",
                                            button: "Ok",
                                        });
                                        clearFields('.input-validation');
                                    }
                                }
                                else {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: "Please try Again",
                                        icon: "error",
                                        button: "Ok",
                                    });

                                }
                            }
                            catch (e) {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: "Please try Again",
                                    icon: "error",
                                    button: "Ok",
                                });
                            }
                        },
                        complete: function () {
                            HideLoader();
                        },
                        failure: function (response) {
                            HideLoader();
                            //alert(response.data);
                            Swal.fire({
                                title: "Failure",
                                text: "Please try Again",
                                icon: "error",
                                button: "Ok",
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

        function Edit(AchievementId) {

            id = AchievementId;

            $('#' + id).find("td:not(:last-child)").each(function (i, data) {
                if (this.className == 'title') {
                    $('#txtTitle').val(this.innerText); ///This will find title for Topic 

                }
                if (this.className == 'description') {
                    $('#txtDescription').val(this.innerText);
                }
                if (this.className == 'longdescription') {
                    $('#txtlongDescription').val(this.innerText);
                }
            });
            //content.Title);
            //$('#txtDescription').val(content.Title);
            //$("#chkIsPublished").prop("checked", content.IsPublished);
            inputValidation('.input-validation');
            toggle('divForm', 'divGird');
            $('#submit').attr('name', EDIT);
            $('#submit').text('EDIT');
            $('#back').text('CANCEL');

            //Submit button name attribute changed to EDIT(Modify);
        }

        function Delete(AchievementId) {
            
            id = AchievementId;
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
                        var requestParams = { TopicID: id, IsActive: 0 };
                        var getUrl = "/API/Achievement/DeleteAchievement";

                        $.ajax({
                            type: "POST",
                            url: getUrl,
                            headers: { "Authorization": "Bearer " + accessToken },
                            data: JSON.stringify(requestParams),
                            contentType: "application/json",
                            success: function (response) {
                                try {

                                    var DataSet = $.parseJSON(response);
                                    //console.log(response);
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
                                            icon: "error",
                                            button: "Ok",
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
                                        icon: "error",
                                        button: "Ok",
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
                                    icon: "error",
                                    button: "Ok",
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
        function View() {
            var url = "/API/Achievement/ViewAchievement";

            try {

                var requestParams = { AchievementID: "", AchievementTitle: "", AchivementDescription: "", LongDescription: "", SrNo: "", IsActive: "" };
                ShowLoader();
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
                            //console.log(response);
                            if (DataSet.StatusCode == "1") {
                                var tbl = '<table id="tblGird" class="table table-bordered" style="width: 100%">';
                                tbl += '<thead><tr>';
                                tbl += '<th>Sr.No.';
                                tbl += '<th>Title';
                                tbl += '<th>Description';
                                tbl += '<th>Long Description';    
                                tbl += '<th>ACTION';

                                tbl += '<tbody>';

                                $.each(DataSet.Data, function (i, data) {
                                   

                                    tbl += '<tr id="' + data.AchievementID + '">';
                                    tbl += '<td>' + (i + 1);

                                    tbl += '<td class="title">' + data.Title;
                                    tbl += '<td class="description">' + data.Description;
                                    tbl += '<td class="longdescription">' + data.LongDescription;
                                    
                                    tbl += '<td><i title="Edit" onclick="Edit(' + data.AchievementID + ');" class="fas fa-edit text-warning"></i><i title="Delete" onclick="Delete(' + data.AchievementID + ');" class="fas fa-trash text-danger"></i>';

                                });

                                $('#divTable').empty().append(tbl)

                                $('#tblGird').tableDnD()

                            }
                            else {
                                HideLoader();
                                Swal.fire({
                                    title: "Warning",
                                    text: DataSet.StatusDescription,
                                    icon: "error",
                                    button: "Ok",
                                });
                            }
                        }
                        else {
                            HideLoader();
                            Swal.fire({
                                title: "Warning",
                                text: DataSet.StatusDescription,
                                icon: "error",
                                button: "Ok",
                            });
                        }
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
                    icon: "error",
                    button: "Ok",
                });
            }
        }

        //This funcion is to get and save changes of Serial No
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
