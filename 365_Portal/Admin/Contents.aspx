<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="Contents.aspx.cs" Inherits="_365_Portal.Admin.Contents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Contents</h1>
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
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="ddlDocType">Doc Type</label>
                                <select class="form-control select2 required" id="ddlDocType" style="width: 100% !important">
                                    <option></option>
                                    <option value="PDF">PDF</option>
                                    <option value="Video">Video</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="form-group">
                                <label for="txtTitle">Title</label>
                                <input type="text" class="form-control required" id="txtTitle" placeholder="Title" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group checkbox required">
                                <label>Is Gift</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="chkIsGift" name="chkIsGift" class="custom-control-input" value="1">
                                    <label class="custom-control-label" for="chkIsGift">Yes</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group checkbox required">
                                <label>Is Published</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="chkIsPublished" name="chkIsPublished" class="custom-control-input" value="1">
                                    <label class="custom-control-label" for="chkIsPublished">Yes</label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtDescription">Description</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Description" id="txtDescription"></textarea>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtOverview">Overview</label>
                                <textarea class="form-control required" rows="4" cols="50" placeholder="Overview" id="txtOverview"></textarea>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="txtFilePath">File Path/URL</label>
                                <input type="text" class="form-control required" id="txtFilePath" placeholder="https://example.com" />
                            </div>
                        </div>
                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" id="btnCancel" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" id="btnSaveChanges" onclick="SaveChanges(this);return false;">Add Content</a>
                        </div>

                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {



            ShowLoader();
            $.ajax({
                type: "GET",
                url: "https://reqres.in/api/users?page=1",
                contentType: false,
                dataType: "json",
                processData: false,
                beforeSend: function () {
                },
                success: function (response) {

                    var tbl = '<table id="tblGird" class="table table-bordered" style="width: 100%">';
                    tbl += '<thead><tr>';
                    tbl += '<th>#';
                    tbl += '<th>Topic';
                    tbl += '<th>Module';
                    tbl += '<th>Doc Type';
                    tbl += '<th>Is Gift';
                    tbl += '<th>File Path';
                    tbl += '<th>Title';
                    tbl += '<th>Overview';
                    tbl += '<th>Description';
                    tbl += '<th>Is Published';
                    tbl += '<th>ACTION';

                    tbl += '<tbody>';

                    $.each(response.data, function (i, data) {

                        tbl += '<tr id="' + data.id + '">';
                        tbl += '<td>' + (i + 1);

                        tbl += '<td>' + data.first_name;
                        tbl += '<td>' + data.first_name;
                        tbl += '<td>PDF' //+ data.last_name;
                        tbl += '<td>Yes' // + data.IsPublished;
                        tbl += '<td>' + data.last_name;
                        tbl += '<td>' + data.last_name;
                        tbl += '<td>' + data.last_name;
                        tbl += '<td>' + data.last_name;
                        tbl += '<td>Yes' // + data.IsPublished;
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
                    HideLoader();
                }
            });
        });


        function AddNew() {
            clearFields('.input-validation')
            toggle('divForm', 'divGird')
        }

        function Submit() {

            if (inputValidation('.input-validation')) {
                swal({
                    title: "Good job!",
                    text: "You clicked the button!",
                    icon: "success",
                    button: "Ok",
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
