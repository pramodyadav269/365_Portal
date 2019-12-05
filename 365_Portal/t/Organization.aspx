<%@ Page Title="" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="Organization.aspx.cs" Inherits="_365_Portal.t.Organization" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Manage Organization</h2>
        </div>


        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12 col-md-2">
                            <a class="btn bg-yellow" onclick="AddNew();">Add New</a>
                        </div>
                    </div>
                    <div id="divTable" class="mt-3 table-responsive"></div>
                </div>
            </div>
        </div>


        <div class="col-md-12 d-none" id="divForm">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">


                    <div class="row input-validation">
                        <div class="col-md-3">
                            <h3 class="text-left font-weight-bold">Organization Details</h3>
                        </div>
                    </div>

                    <div class="row input-validation">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtBusinessName">Business Name</label>
                                <input type="text" class="form-control required" id="txtBusinessName" placeholder="Business Name" />
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlEmployeeCount">No Of Employee</label>
                                <select class="form-control required select2" id="ddlEmployeeCount" style="width: 100% !important">
                                    <option></option>
                                    <option value="1">Just You</option>
                                    <option value="2-9">2-9</option>
                                    <option value="10-99">10-99</option>
                                    <option value="300+">300+</option>
                                </select>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlCountry">Country</label>
                                <select class="form-control required select2" id="ddlCountry" style="width: 100% !important">
                                </select>
                            </div>
                        </div>
                    </div>




                    <div class="row input-validation">
                        <div class="col-md-3">
                            <h3 class="text-left font-weight-bold">Admin Details</h3>
                        </div>
                    </div>

                    <div class="row input-validation">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlRole">Role</label>
                                <select class="form-control required select2" id="ddlRole" style="width: 100% !important">
                                </select>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtFname">First Name</label>
                                <input type="text" class="form-control required" id="txtFname" placeholder="First Name" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtLname">Last Name</label>
                                <input type="text" class="form-control required" id="txtLname" placeholder="Last Name" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtEmailId">Email ID</label>
                                <input type="text" class="form-control required" id="txtEmailId" placeholder="Email ID" />
                            </div>
                        </div>

                        <div class="col-md-3" id="divPassword">
                            <div class="form-group">
                                <label for="txtPassword">Password</label>
                                <input type="password" class="form-control required" id="txtPassword" placeholder="Password" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtMobileNo">Mobile No</label>
                                <input type="text" class="form-control required" id="txtMobileNo" placeholder="Mobile No" />
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="txtPosition">Position</label>
                                <input type="text" class="form-control required" id="txtPosition" placeholder="Position" />
                            </div>
                        </div>

                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" id="btnSubmit" onclick="Submit();">Submit</a>
                        </div>

                        <input type="hidden" id="UserID" value="" />
                            
                    </div>
                </div>
            </div>
        </div>


    </div>


    <script>

        var accessToken = '<%=Session["access_token"]%>';
        var Role = '<%=Session["RoleName"]%>';
        var id = '';

        $(document).ready(function () {
            //debugger
            ShowLoader();
            GetAdminUsers();
            //if (Role == "superadmin")
                //BindCompanies();
        });

        function GetAdminUsers() {
            ShowLoader();
            var getUrl = "/API/User/GetAdminUsers";
            //var requestParams = { Role: "", CompId: $("#ddlCompany").val() == null ? "0" : $("#ddlCompany").val() };
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                //data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    HideLoader();
                    try {
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        debugger
                        if (DataSet.StatusCode == "1") {
                            BindUsers(DataSet.Data);
                        }
                        else {
                            Swal.fire(DataSet.StatusDescription, {
                                icon: "error",
                            });
                            ClearFields();
                        }

                        BindRoleAndGroup('', 'create');
                    }
                    catch (e) {
                        HideLoader();
                    }
                },
                failure: function (response) {
                    HideLoader();
                }
            });
        }

        function BindUsers(Table) {

            $('#divTable').empty().append();

            var tbl = '<table id="tblGird" class="table table-bordered" style="width:100%">' +
                '<thead><tr><th>#</th><th style="display:none;">ID</th><th>First Name</th><th>Last Name</th><th>Email ID</th><th>Position</th><th>Role</th><th>Group</th><th>Action</th></thead>'

            tbl += '<tbody>';
            if (Table != undefined && Table.length > 0) {
                for (var i = 0; i < Table.length; i++) {
                    tbl += '<tr>';
                    tbl += '<td>' + (i + 1) + '</td>';
                    tbl += '<td style="display:none;" id="id">' + Table[i].UserID + '</td>';
                    tbl += '<td title="' + Table[i].FirstName + '" >' + Table[i].FirstName + '</td>';
                    tbl += '<td title="' + Table[i].LastName + '" >' + Table[i].LastName + '</td>';
                    tbl += '<td title="' + Table[i].EmailID + '" >' + Table[i].EmailID + '</td>';
                    tbl += '<td title="' + Table[i].Position + '" >' + Table[i].Position + '</td>';
                    tbl += '<td title="' + Table[i].RoleName + '" >' + Table[i].RoleName + '</td>';
                    tbl += '<td title="' + Table[i].GroupName + '" >' + Table[i].GroupName + '</td>';
                    tbl += '<td><i title="Edit" onclick="Edit(this,' + Table[i].UserID + ');" class="fas fa-edit text-warning"></i>' +
                        '<i title="Delete" onclick="Delete(this,' + Table[i].UserID + ');" class="fas fa-trash text-danger"></i></td>';
                    tbl += '</tr>';
                }
            }
            tbl += '</tbody>';
            tbl += '</table>';
            $('#divTable').empty().append(tbl);
            $('#tblGird').DataTable();
        }

        function AddNew() {

            $('#btnSubmit').show();
            $('#divPassword').show();
            $('#btnUpdate').hide();
            $('#divUpdatePassword').hide();
            clearFields('.input-validation')
            toggle('divForm', 'divGird');

            BindCountry();
        }

        function BindCountry()
        {
            ShowLoader();
            var getUrl = "/API/User/GetCountry";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                contentType: "application/json",
                success: function (response) {
                    HideLoader();
                    try {
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        debugger
                        if (DataSet.StatusCode == "1") {

                            var Country = DataSet.Data.Data;
                            if (Country != undefined && Country.length > 0)
                            {
                                $('#ddlRole').empty().append('<option></option>');
                                for (var i = 0; i < Role.length; i++)
                                {
                                    $('#ddlCountry').append('<option value="' + Role[i].Id + '">' + Role[i].CountryName + '</option>');
                                }
                                selectInit('#ddlRole', 'Select Country');
                            }
                        }
                        else {
                            Swal.fire(DataSet.StatusDescription, {
                                icon: "error",
                            });
                            ClearFields();
                        }
                    }
                    catch (e) {
                        HideLoader();
                    }
                },
                failure: function (response) {
                    HideLoader();
                }
            });
        }
    </script>

</asp:Content>
