<%@ Page Title="Topics" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="Topics.aspx.cs" Inherits="_365_Portal.Admin.Topics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Topics</h1>
        </div>

        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <a class="btn bg-yellow float-left " onclick="AddNew();">Add New</a> <a class="btn bg-blue text-white float-right" style="display:none;" id="savereorder" onclick="SaveGrid();">Save Reordering</a>
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

                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtDescription">Description</label>
                                <textarea class="form-control required" placeholder="Description" id="txtDescription"></textarea>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group checkbox ">
                                <label>Is Published</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbIsPublished" name="cgIsPublished" class="custom-control-input">
                                    <label class="custom-control-label" for="cbIsPublished">Yes</label>
                                </div>
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
                var _Topic_Id;
                var _SrNo = "1";
                var _Title = $('#txtTitle').val();
                var _Description = $('#txtDescription').val();
                var _IsPublished = $('#cbIsPublished').prop('checked');
                if ($('#submit')[0].name == INSERT) {
                    getUrl = "/API/Content/CreateTopic";

                } else {
                    _Topic_Id = id;
                    getUrl = "/API/Content/ModifyTopic";

                }

                requestParams = { TopicID: _Topic_Id, TopicTitle: _Title, TopicDescription: _Description, IsPublished: _IsPublished, SrNo: _SrNo, MinUnlockedModules: "", UserID: "", IsActive:true };

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
                                    if (DataSet != null && DataSet != "") {
                                        if (DataSet.StatusCode == "1") {
                                            clearFields('.input-validation');
                                            HideLoader();
                                            Swal.fire({
                                                title: "Success",
                                                text: DataSet.StatusDescription,
                                                icon: "success"

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
                                            //clearFields('.input-validation');
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
                        },
                        complete: function () {
                            HideLoader();
                        },
                        failure: function (response) {
                            HideLoader();
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
                        text: "Please try again",
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

        function Edit(Topicid) {

            id = Topicid;

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
            inputValidation('.input-validation');
            toggle('divForm', 'divGird');
            $('#submit').attr('name', EDIT);
            $('#submit').text('UPDATE');
            $('#back').text('CANCEL');

            //Submit button name attribute changed to EDIT(Modify);
        }

        function Delete(Topicid) {
            id = "";
            id = Topicid;

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
                    try {
                        var requestParams = { TopicID: id, IsActive: 0 };
                        var getUrl = "/API/Content/DeleteTopic";

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
                                                icon: "success"

                                            });
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
                            text: "Please try again",
                            icon: "error"

                        });
                    }
                }
            })


            //Swal.fire({
            //    title: "Are you sure?",
            //    text: "Once deleted, you will not be able to revert changes!",
            //    icon: "warning",
            //    buttons: true,
            //    dangerMode: true,
            //})
            //    .then((willDelete) => {
            //        if (willDelete) {
            //            ShowLoader();
            //            try {
            //                var requestParams = { TopicID: id, IsActive: 0 };
            //                var getUrl = "/API/Content/DeleteTopic";

            //                $.ajax({
            //                    type: "POST",
            //                    url: getUrl,
            //                    headers: { "Authorization": "Bearer " + accessToken },
            //                    data: JSON.stringify(requestParams),
            //                    contentType: "application/json",
            //                    success: function (response) {
            //                        try {

            //                            var DataSet = $.parseJSON(response);
            //                            if (DataSet != null && DataSet != "") {
            //                                if (DataSet.StatusCode == "1") {
            //                                    HideLoader();
            //                                    Swal.fire({
            //                                        title: "Success",
            //                                        text: DataSet.StatusDescription,
            //                                        icon: "success"

            //                                    });
            //                                    View();
            //                                }
            //                                else {
            //                                    HideLoader();
            //                                    Swal.fire({
            //                                        title: "Failure",
            //                                        text: DataSet.StatusDescription,
            //                                        icon: "error"

            //                                    });
            //                                }
            //                            }
            //                            else {
            //                                HideLoader();
            //                                Swal.fire({
            //                                    title: "Failure",
            //                                    text: "Please try Again",
            //                                    icon: "error"

            //                                });
            //                            }
            //                        }
            //                        catch (e) {
            //                            HideLoader();
            //                            Swal.fire({
            //                                title: "Failure",
            //                                text: "Please try Again",
            //                                icon: "error"

            //                            });
            //                        }
            //                    },
            //                    complete: function () {
            //                        HideLoader();
            //                    },
            //                    failure: function (response) {
            //                        HideLoader();
            //                        alert(response.data);
            //                        Swal.fire({
            //                            title: "Failure",
            //                            text: "Please try Again",
            //                            icon: "error"

            //                        });
            //                    }
            //                });
            //            }
            //            catch (e) {
            //                HideLoader();
            //                Swal.fire({
            //                    title: "Alert",
            //                    text: "Please try again",
            //                    icon: "error"

            //                });
            //            }

            //        }
            //    });
        }
        function View() {
            var url = "/API/Content/GetTopics";

            try {

                var requestParams = { TopicID: "", TopicTitle: "", TopicDescription: "", IsPublished: "", SrNo: "", MinUnlockedModules: "", UserID: "", IsActive: "" };
                ShowLoader();
                $.ajax({
                    type: "POST",
                    url: url,
                    headers: { "Authorization": "Bearer " + accessToken },
                    data: JSON.stringify(requestParams),
                    contentType: "application/json",
                    processData: false,
                    success: function (response) {
                        var tbl = '<table id="tblGird" class="table table-bordered" style="width: 100%">';
                        tbl += '<thead><tr>';
                        tbl += '<th>Sr.No.';
                        tbl += '<th>Title';
                        tbl += '<th>Description';
                        tbl += '<th>Is Published';
                        tbl += '<th>Total Modules';
                        tbl += '<th>Action';
                        tbl += '<tbody>';
                        if (response != null && response != undefined) {
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

                                            tbl += '<tr id="' + data.TopicID + '">';
                                            tbl += '<td>' + (i + 1);

                                            tbl += '<td title="' + data.Title + '" class="title">' + data.Title;
                                            tbl += '<td title="' + data.Description + '" class="description">' + data.Description;
                                            tbl += '<td title="' + data.IsPublished + '" class="isPublished">' + data.IsPublished;
                                            tbl += '<td title="' + data.ModuleCount + '"><a href=Modules.aspx?Id=' + data.TopicID + '>' + data.ModuleCount + '</a>';
                                            tbl += '<td><i title="Edit" onclick="Edit(' + data.TopicID + ');" class="fas fa-edit text-warning"></i><i title="Delete" onclick="Delete(' + data.TopicID + ');" class="fas fa-trash text-danger"></i>';

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
                                HideLoader();
                                Swal.fire({
                                    title: "Warning",
                                    text: DataSet.StatusDescription,
                                    icon: "error"

                                });
                            }
                        }
                        else {
                            HideLoader();
                            Swal.fire({
                                title: "Warning",
                                text: DataSet.StatusDescription,
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
                    var requestParams = { Type: "1", IDs: sqnData };
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
