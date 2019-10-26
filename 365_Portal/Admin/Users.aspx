<%@ Page Title="Users" Language="C#" MasterPageFile="~/Admin/admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="_365_Portal.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Users</h1>
        </div>

        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <a class="btn bg-yellow" onclick="AddNew();">Add New</a>
                    <div class="w-100"></div>
                    <div id="divTable" class="mt-3"></div>
                </div>
            </div>
        </div>

        <div class="col-md-12 d-none" id="divForm">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">

                    <div class="row input-validation">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="text">Text</label>
                                <input type="text" class="form-control required" id="text" placeholder="Text" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="date">Date</label>
                                <input type="text" class="form-control date required" id="date" placeholder="Select Date" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label>File</label>
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input required" id="file">
                                    <label class="custom-file-label" for="customFile">Choose file</label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="textarea">Textarea</label>
                                <textarea class="form-control required" placeholder="Textarea" id="textarea"></textarea>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="select">Select</label>
                                <select class="form-control select2 required" id="select" style="width: 100% !important">
                                    <option></option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                </select>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group radio required">
                                <label>Radio</label>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbQQ1" name="rgQuestion" class="custom-control-input">
                                    <label class="custom-control-label" for="rbQQ1">Yes</label>
                                </div>
                                <div class="custom-control custom-radio custom-control-inline">
                                    <input type="radio" id="rbQQ2" name="rgQuestion" class="custom-control-input">
                                    <label class="custom-control-label" for="rbQQ2">No</label>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-3">
                            <div class="form-group checkbox required">
                                <label>Checkbox</label>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbQ1" name="cgQuestion" class="custom-control-input">
                                    <label class="custom-control-label" for="cbQ1">A</label>
                                </div>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbQ2" name="cgQuestion" class="custom-control-input">
                                    <label class="custom-control-label" for="cbQ2">B</label>
                                </div>
                                <div class="custom-control custom-checkbox custom-control-inline">
                                    <input type="checkbox" id="cbQ3" name="cgQuestion" class="custom-control-input">
                                    <label class="custom-control-label" for="cbQ3">C</label>
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



            loader(1)
            $.ajax({
                type: "GET",
                url: "http://dummy.restapiexample.com/api/v1/employees",
                contentType: false,
                dataType: "json",
                processData: false,
                beforeSend: function () {
                },
                success: function (response) {

                    var tbl = '<table id="tblGird" class="table table-bordered" style="width: 100%">';
                    tbl += '<thead><tr>';
                    tbl += '<th>#';
                    tbl += '<th>ID';
                    tbl += '<th>NAME';
                    tbl += '<th>SALARY';
                    tbl += '<th>AGE';
                    tbl += '<th>ACTION';

                    tbl += '<tbody>';

                    $.each(response, function (i, data) {

                        tbl += '<tr id="' + data.id + '">';
                        tbl += '<td>';

                        tbl += '<td>' + data.id;
                        tbl += '<td>' + data.employee_name;
                        tbl += '<td>' + data.employee_salary;
                        tbl += '<td>' + data.employee_age;
                        tbl += '<td><i title="Edit" onclick="Edit(this);" class="fas fa-edit text-warning"></i><i title="Delete" onclick="Delete(this);" class="fas fa-trash text-danger"></i>';

                    });

                    $('#divTable').empty().append(tbl)

                    var dTable = $('#tblGird').DataTable();

                    dTable.on('order.dt search.dt', function () {
                        dTable.column(0, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
                            cell.innerHTML = i + 1;
                        });
                    }).draw();

                },
                complete: function () {
                    loader(0)
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
