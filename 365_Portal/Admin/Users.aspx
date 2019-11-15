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
                                <label for="ddlRole">Role</label>
                                <select class="form-control required" id="ddlRole" style="width: 100% !important">
                                    <%--<option></option>
                                    <option value="1">Role 1</option>
                                    <option value="2">Role 2</option>
                                    <option value="3">Role 3</option>--%>
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
                        <div class="col-md-3">
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
                        <div class="col-md-3" id="divGroup" style="display:none;">
                            <div class="form-group">
                                <label for="ddlGroup">Group</label>
                                <select class="form-control  required" id="ddlGroup" style="width: 100% !important">
                                    <%--<option></option>
                                    <option value="1">Group 1</option>
                                    <option value="2">Group 2</option>
                                    <option value="3">Group 3</option>--%>
                                </select>
                            </div>  
                        </div>
                        



                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" id="btnSubmit" onclick="Submit();">Submit</a>
                            <a class="btn bg-yellow float-right" id="btnUpdate" style="display:none;" onclick="Update();">Update</a>
                        </div>

                        <input type="hidden" id="UserID"  value=""/>

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
            GetUsers();            

            /*
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
                    tbl += '<th>First Name';
                    tbl += '<th>Last Name';
                    tbl += '<th>Email ID';
                    tbl += '<th>Position';
                    tbl += '<th>Group';
                    tbl += '<th>Role';
                    tbl += '<th>ACTION';

                    tbl += '<tbody>';

                    $.each(response.data, function (i, data) {
                        tbl += '<tr id="' + data.id + '">';
                        tbl += '<td>' + (i + 1);

                        tbl += '<td>' + data.first_name;
                        tbl += '<td>' + data.last_name;
                        tbl += '<td>' + data.email;
                        tbl += '<td>Position 1'// + data.position;
                        tbl += '<td>Group 1'// + data.group;
                        tbl += '<td>Role 1'// + data.role;
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
            */
        });

        function BindUsers(Table) {
            
            $('#divTable').empty();
            if (Table != undefined && Table.length > 0) {
                $('#divTable').append('<table id="tblGird" class="table table-bordered" style="width: 100%"><thead><tr><th>#</th><th style="display:none;">ID</th><th>First Name</th><th>Last Name</th><th>Email ID</th><th>Position</th><th>Role</th><th>Group</th><th colspan="2">ACTION</th>');
                $('#divTable').append('<tbody>');

                for (var i = 0; i < Table.length ; i++)
                {
                    $('#tblGird').append('<tr>');
                    $('#tblGird').append('<td>' + (i + 1) + '</td>');
                    $('#tblGird').append('<td style="display:none;" id="id">' + Table[i].UserID + '</td>');
                    $('#tblGird').append('<td>' + Table[i].FirstName + '</td>');
                    $('#tblGird').append('<td>' + Table[i].LastName + '</td>');
                    $('#tblGird').append('<td>' + Table[i].EmailID + '</td>');
                    $('#tblGird').append('<td>' + Table[i].Position + '</td>');
                    $('#tblGird').append('<td>' + Table[i].RoleName + '</td>');
                    $('#tblGird').append('<td>' + Table[i].GroupName + '</td>');
                    $('#tblGird').append('<td><i title="Edit" onclick="Edit(this,' + Table[i].UserID + ');" class="fas fa-edit text-warning"></i></td>');
                    $('#tblGird').append('<td><i title="Delete" onclick="Delete(this,' + Table[i].UserID + ');" class="fas fa-trash text-danger"></i></td>');
                    $('#tblGird').append('</tr>');
                }
                //$('#tblGird').append('</tbody>');
                //$('#tblGird').append('</table>');
            }
            else {
                $('#divTable').append('<h2>No users found</h2>');
            }
        }

        function GetUsers()
        {
            var getUrl = "/API/User/GetUsers";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                contentType: "application/json",
                success: function (response) {
                    try {
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        if (DataSet.StatusCode == "1") {
                            //alert(DataSet.StatusDescription);                            
                            BindUsers(DataSet.Data);
                            BindRoleAndGroup('', 'create');
                        }
                        else {
                            //alert(DataSet.StatusDescription);
                            //debugger
                            Swal.fire(DataSet.StatusDescription, {
                                icon: "error",
                            });
                            ClearFields();
                        }
                    }
                    catch (e) {
                        HideLoader();
                        //alert(response);
                        //alert(e.message);
                    }
                },
                failure: function (response) {
                    HideLoader();
                    //alert(response.data);
                }
            });
        }        

        function AddNew() {

            $('#btnSubmit').show();
            $('#btnUpdate').hide();
            clearFields('.input-validation')
            toggle('divForm', 'divGird');
        }

        function Submit()
        {
            var getUrl = "/API/User/CreateUser";
            ProcessCreateUpdate('', getUrl,'create');
            /*
            if (inputValidation('.input-validation')) {
                Swal.fire({
                    title: "Good job!",
                    text: "You clicked the button!",
                    icon: "success",
                    button: "Ok",
                });
            } else {
                Swal.fire({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error",
                    button: "Ok",
                });
            }
            */
        }

        function Update()
        {
            var id = $('#UserID').val();
            var getUrl = "/API/User/UpdateUser";
            ProcessCreateUpdate(id, getUrl, 'update');
        }

        function ProcessCreateUpdate(id, getUrl,flag)
        {
            debugger
            var result = InputValidation();
            if (result.error)
            {
                Swal.fire({
                    title: "Alert",
                    text: result.msg,
                    icon: "error",
                    button: "Ok",
                });
            }
            else {
                ShowLoader();
                var Role = $("#ddlRole option:selected").val();
                var FirstName = $("#txtFname").val();
                var LastName = $("#txtLname").val();
                var EmailID = $("#txtEmailId").val();
                var Password = $("#txtPassword").val();
                var MobileNum = $("#txtMobileNo").val();
                var Position = $("#txtPosition").val();
                var GroupId = $("#ddlGroup option:selected").val();

                if (flag == 'create') {
                    var requestParams = { RoleID: Role, FirstName: FirstName, LastName: LastName, EmailID: EmailID, Password: Password, MobileNum: MobileNum, Position: Position, GroupId: GroupId };
                }
                else {
                    var requestParams = { UserID: id, RoleID: Role, FirstName: FirstName, LastName: LastName, EmailID: EmailID, Password: Password, MobileNum: MobileNum, Position: Position, GroupId: GroupId };
                }
                
                $.ajax({
                    type: "POST",
                    url: getUrl,
                    headers: { "Authorization": "Bearer " + accessToken },
                    data: JSON.stringify(requestParams),
                    contentType: "application/json",
                    success: function (response) {
                        try {
                            debugger
                            var DataSet = $.parseJSON(response);
                            HideLoader();
                            if (DataSet.StatusCode == "1") {
                                Swal.fire(DataSet.Data[0].ReturnMessage, {
                                    icon: "success",
                                }).then((CreateUpdateUser) => {
                                    location.reload();
                                });;
                            }
                            else {
                                if (DataSet.Data != undefined && DataSet.Data.length > 0) {
                                    Swal.fire(DataSet.Data[0].ReturnMessage, {
                                        icon: "error",
                                    });
                                }
                                else {
                                    Swal.fire(DataSet.StatusDescription, {
                                        icon: "error",
                                    });
                                }
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
        }

        function InputValidation()
        {
            
            if ($("#ddlRole option:selected").val() == undefined || $("#ddlRole option:selected").val() == '') {
                return { error: true, msg: "Please select Role" };
            }
            else if ($("#txtFname").val() == undefined || $("#txtFname").val() == '') {
                return { error: true, msg: "Please enter firstname" };
            }
            else if ($("#txtLname").val() == undefined || $("#txtLname").val() == '') {
                return { error: true, msg: "Please enter lastname" };
            }
            else if ($("#txtEmailId").val() == undefined || $("#txtEmailId").val() == '') {
                return { error: true, msg: "Please enter emailid" };
            }
            else if ($("#txtPassword").val() == undefined || $("#txtPassword").val() == '') {
                return { error: true, msg: "Please enter password" };
            }
            return true;
        }

        
        function Edit(ctrl,id) {
            //var id = $(ctrl).closest('tr').attr('id')
            //debugger
            ShowLoader();
            clearFields('.input-validation')
            BindRoleAndGroup(id,'update');
            
        }

        function BindRoleAndGroup(id,flag) {
            var getUrl = "/API/User/BindRoleAndGroup";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                contentType: "application/json",
                success: function (response) {
                    try {
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        if (DataSet.StatusCode == "1") {

                            var Role = DataSet.Data.Data;
                            var Group = DataSet.Data.Data1;

                            //if (Role != undefined && Role.length > 0) 
                            {                                
                                $('#ddlRole').empty().append('<option value="">Select Role</option>');
                                for(var i = 0;i < Role.length ; i++)
                                {
                                    $('#ddlRole').append('<option value="' + Role[i].RoleID + '">' + Role[i].RoleDisplayName + '</option>');
                                }
                            }
                            if (Group != undefined && Group.length > 0) {
                                $('#ddlGroup').empty().append('<option value="">Select Group</option>');
                                for (var i = 0; i < Group.length ; i++) {
                                    $('#ddlGroup').append('<option value="' + Group[i].GroupID + '">' + Group[i].GroupName + '</option>');
                                }
                                $('#divGroup').show();
                            }

                            if (flag == 'update')
                            {
                                BindUserData(id);
                            }
                        }
                        else {
                            if (DataSet.Data != undefined && DataSet.Data.length > 0) {
                                Swal.fire(DataSet.Data[0].ReturnMessage, {
                                    icon: "error",
                                });
                            }
                            else {
                                Swal.fire(DataSet.StatusDescription, {
                                    icon: "error",
                                });
                            }
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

        function BindUserData(id)
        {
            ShowLoader();
            var requestParams = { UserID: id };
            var getUrl = "/API/User/GetUserDetailsForParent";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    try {
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        if (DataSet.StatusCode == "1") {
                            
                            toggle('divForm', 'divGird')

                            $('#ddlRole').val(DataSet.Data[0].RoleID);
                            $('#txtFname').val(DataSet.Data[0].FirstName);
                            $('#txtLname').val(DataSet.Data[0].LastName);
                            $('#txtEmailId').val(DataSet.Data[0].EmailID);
                            $('#txtPassword').val('');
                            $('#txtMobileNo').val(DataSet.Data[0].MobileNum);
                            $('#txtPosition').val(DataSet.Data[0].Position);
                            $('#ddlGroup').val(DataSet.Data[0].GroupID);

                            $('#btnSubmit').hide();
                            $('#btnUpdate').show();
                            $('#UserID').val(id);

                            $('.select2').material_select();
                        }
                        else {
                            if (DataSet.Data != undefined && DataSet.Data.length > 0) {
                                Swal.fire(DataSet.Data[0].ReturnMessage, {
                                    icon: "error",
                                });
                            }
                            else {
                                Swal.fire(DataSet.StatusDescription, {
                                    icon: "error",
                                });
                            }
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

        function Delete(ctrl,id) {
            //var id = $(ctrl).closest('tr').attr('id')
            Swal.fire({
                title: "Are you sure?",
                text: "Do you want to delete user!",
                icon: "warning",
                buttons: true,
                dangerMode: true,
            })
                .then((willDelete) => {
                    if (willDelete) {
                        DeleteUser(id);                        
                    }
                });
        }

        function DeleteUser(id)
        {
            //debugger
            ShowLoader();
            var getUrl = "/API/User/DeleteUser";
            var requestParams = { UserID: id };
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    try {
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        if (DataSet.StatusCode == "1") {
                            Swal.fire(DataSet.Data[0].ReturnMessage, {
                                icon: "success",
                            }).then((deleteuser) => {
                                location.reload();
                            });
                            ;
                        }
                        else {
                            if (DataSet.Data != undefined && DataSet.Data.length > 0) {
                                Swal.fire(DataSet.Data[0].ReturnMessage, {
                                    icon: "error",
                                });
                            }
                            else {
                                Swal.fire(DataSet.StatusDescription, {
                                    icon: "error",
                                });
                            }                            
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
