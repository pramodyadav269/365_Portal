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
                    <a class="btn bg-yellow" onclick="AddNew();">Add New</a>
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
                            <a class="btn bg-yellow float-left" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" onclick="Submit();">Submit</a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {


            var url = "API/Module/ModuleAllAction";
            showLoader();
            $.ajax({
                type: "GET",
                url: "https://reqres.in/api/users?page=1",
               // type: "POST",
                //url: url,
                contentType: "",
                dataType: "json",
                processData: false,
                beforeSend: function () {
                },
                success: function (response) {

                    var tbl = '<table id="tblGird" class="table table-bordered" style="width: 100%">';
                    tbl += '<thead><tr>';
                    tbl += '<th>#';
                    tbl += '<th>Topic';
                    tbl += '<th>Title';
                    tbl += '<th>Overview';
                    tbl += '<th>Description';
                    tbl += '<th>Is Published';
                    tbl += '<th>Skip Flashcard';
                    tbl += '<th>Total Contents';
                    tbl += '<th>Survey';
                    tbl += '<th>Flashcards';
                    tbl += '<th>Flashcard Quiz';
                    tbl += '<th>Final Quiz';
                    tbl += '<th>Personal Gifts';
                    tbl += '<th>ACTION';

                    tbl += '<tbody>';


                    $.each(response.data, function (i, data) {

                        tbl += '<tr id="' + data.id + '">';
                        tbl += '<td>' + (i + 1);

                        tbl += '<td>' + data.first_name;
                        tbl += '<td>' + data.first_name;
                        tbl += '<td>' + data.last_name;
                        tbl += '<td>' + data.last_name;
                        tbl += '<td>Yes' // + data.IsPublished;
                        tbl += '<td>No' // + data.IsPublished;
                        tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                        tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                        tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                        tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                        tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                        tbl += '<td><a href="Modules.aspx?Id=1">' + (i) + '</a>'
                        tbl += '<td><i title="Edit" onclick="Edit(this);" class="fas fa-edit text-warning"></i><i title="Delete" onclick="Delete(this);" class="fas fa-trash text-danger"></i>';

                    });

                    $('#divTable').empty().append(tbl)

                    //var dTable = $('#tblGird').DataTable();

                    //dTable.on('order.dt search.dt', function () {
                    //    dTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                    //        cell.innerHTML = i + 1;
                    //    });
                    //}).draw();

                },
                complete: function () {
                    hideLoader();
                }
            });
        });


        function AddNew() {
            clearFields('.input-validation')
            toggle('divForm', 'divGird')
        }

        function Submit() {
            showLoader();
            if (inputValidation('.input-validation')) {
                var _Action = INSERT;
                var _Topic_Id = $('#ddlTopic option:selected').val();
                var _Title = $('#txtTitle').val();
                var _Overview = $('#txtOverview').val();
                var _Description = $('#txtDescription').val();
                var _IsPublished = $('#cbIsPublished').val();
                var _SkipFlashcard = $('#cbSkipFlashcard').val();

                var requestParams = { Action: _Action, TopicID: _Topic_Id, Title: _Title, Overview: _Overview, Description: _Description, IsPublished: _IsPublished, _SkipFlashcard: "" };
                var getUrl = "/API/Module/ModuleAllAction";

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
                                swal({
                                    title: "Good job!",
                                    text: "You clicked the button!",
                                    icon: "success",
                                    button: "Ok",
                                });

                                clearFields('.input-validation');
                            }
                            else {
                                swal({
                                    title: "Failure",
                                    text: DataSet.StatusDescription,
                                    type: "error"
                                });
                                clearFields('.input-validation');
                            }
                        }
                        catch (e) {
                            alert(response);
                            alert(e.message);
                        }
                    },
                    complete: function () {
                        hideLoader();
                    },
                    failure: function (response) {
                        hideLoader();
                        alert(response.data);
                    }
                });

            } else {
                swal({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error",
                    button: "Ok",
                });
            }
        }

        function Edit(ctrl) {
            var id = $(ctrl).closest('tr').attr('id')
            toggle('divForm', 'divGird');
        }

        function Delete(ctrl) {
            var id = $(ctrl).closest('tr').attr('id')
            swal({
                title: "Are you sure?",
                text: "Once deleted, you will not be able to recover this imaginary file!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        swal("Poof! Your imaginary file has been deleted!", {
                            icon: "success",
                        });
                    }
                });
        }

    </script>
</asp:Content>
