﻿<%@ Page Title="Users" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="_365_Portal.Admin.Users" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header mb-5">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Users</h2>
        </div>

        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12 col-md-2">
                            <a class="btn bg-yellow" onclick="AddNew();">Add New</a>
                        </div>
                        <div class="col-sm-12 col-md-3" id="dvCompanyFilter" style="display:none;">
                            <div class="form-group">
                                <select id="ddlCompany" class="form-control select2 required" style="width: 100% !important" onchange="GetUsers()">
                                </select>
                            </div>
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
                            <div class="form-group">
                                <label for="ddlRole">Role</label>
                                <select class="form-control required select2" id="ddlRole" style="width: 100% !important">
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
                                <label for="txtPosition">Title</label>
                                <input type="text" class="form-control required" id="txtPosition" placeholder="Title" />
                            </div>
                        </div>
                        <div class="col-md-3" id="divGroup" style="display: none;">
                            <div class="form-group">
                                <label for="ddlGroup">Group</label>
                                <select class="form-control select2 required" id="ddlGroup" style="width: 100% !important">
                                </select>
                            </div>
                        </div>

                        <div class="col-md-3" id="divUpdatePassword">
                            <div class="custom-control custom-checkbox mb-4">
                                <input type="checkbox" onchange="enableUpdatePassword();" class="custom-control-input" id="cbUpdatePassword">
                                <label class="custom-control-label" for="cbUpdatePassword">Change password</label>
                                <input type="password" disabled class="form-control required" id="txtUpdatePassword" placeholder="Password" />
                            </div>
                        </div>

                    <div style="display:none;">

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlGender">Gender</label>
                                <select class="form-control required select2" id="ddlGender" style="width: 100% !important">
                                    <option></option>
                                    <option value="M">Male</option>
                                    <option value="F">Female</option>
                                </select>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlDepartment">Department</label>
                                <select class="form-control required select2" id="ddlDepartment" style="width: 100% !important">
                                </select>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlTeam">Team</label>
                                <select class="form-control required select2" id="ddlTeam" style="width: 100% !important">
                                </select>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <label for="ddlManager">Manager</label>
                                <select class="form-control required select2" id="ddlManager" style="width: 100% !important">
                                </select>
                            </div>
                        </div>

                        <div class="col-md-3" >
                            <div class="form-group">
                                <label for="txtDOJ">Date of Joining</label>
                                <input ng-init="question.Value_Text = GetFormattedDate(question.Value_Text)" value="question.Value_Text" type="date" class="form-control" id="txtDOJ" placeholder="Select Date" style="width: 100%;" ng-model="question.Value_Text" />
                                <%--<input ng-init="question.Value_Text = GetFormattedDate(question.Value_Text)" value="question.Value_Text" type="date" class="form-control" id="{{'date_' + $index}}" placeholder="Select Date" style="width: 100%;" ng-model="question.Value_Text" />--%>
                            </div>
                        </div>

                    </div>

                        <div class="w-100"></div>

                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" id="btnSubmit" onclick="Submit();">Submit</a>
                            <a class="btn bg-yellow float-right" id="btnUpdate" style="display: none;" onclick="Update();">Update</a>
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
            GetUsers();
            if (Role == "superadmin")
                BindCompanies();
        });

        function BindCompanies() {
            $("#dvCompanyFilter").show();
            $.ajax({
                type: "POST",
                url: "../api/Trainning/GetTableData",
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify({ Type: 5 }),
                contentType: "application/json",
                success: function (response) {
                    response = $.parseJSON(response);

                    $('#ddlCompany').empty().append('<option value="0">All Organization</option>');
                    $.each(response.Data, function (index, comp) {
                        $('#ddlCompany').append('<option value="' + comp.CompId + '">' + comp.CompanyName + '</option>');
                    });
                    selectInit('#ddlCompany', 'Select a option');
                }
            });
        }

        function BindUsers(Table) {

            $('#divTable').empty().append();

            var tbl = '<table id="tblGird" class="table table-bordered" style="width:100%">' +
                '<thead><tr><th>#</th><th style="display:none;">ID</th><th>First Name</th><th>Last Name</th><th>Email ID</th><th>Title</th><th>Role</th><th>Groups</th><th style="width:9%">Action</th></thead>'

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
                    tbl += '<td title="' + Table[i].GroupName + '" >' + FormatGroups(Table[i].GroupName) + '</td>';
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

        function FormatGroups(Groups)
        {
            if (Groups != undefined && Groups != '')
            {
                Groups = Groups.replace(/,,/g, ",");
                Groups = Groups.replace(/,\s*$/, "");
            }
            return Groups;
        }

        function GetUsers() {
            ShowLoader();
            var getUrl = "/API/User/GetUsers";
            var requestParams = { Role: "", CompId: $("#ddlCompany").val() == null ? "0" : $("#ddlCompany").val() };
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
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
                $('#divPassword').show();
                $('#btnUpdate').hide();
                $('#divUpdatePassword').hide();
                clearFields('.input-validation')
                toggle('divForm', 'divGird');

                BindDropDown();
            }

            function BindDropDown()
            {
                var getUrl = "/API/User/BindDropDown";
                $.ajax({
                    async: false,
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

                                var Department = DataSet.Data.Data;
                                var Team = DataSet.Data.Data1;
                                var Manager = DataSet.Data.Data2;

                                if (Department != undefined && Department.length > 0) {
                                    $('#ddlDepartment').empty().append('<option></option>');
                                    for (var i = 0; i < Department.length; i++) {
                                        $('#ddlDepartment').append('<option value="' + Department[i].Id + '">' + Department[i].DeptName + '</option>');
                                    }
                                    selectInit('#ddlDepartment', 'Select Department');
                                }
                                if (Team != undefined && Team.length > 0) {
                                    $('#ddlTeam').empty().append('<option></option>');
                                    for (var i = 0; i < Team.length; i++) {
                                        $('#ddlTeam').append('<option value="' + Team[i].Id + '">' + Team[i].TeamName + '</option>');
                                    }
                                    selectInit('#ddlTeam', 'Select Team');
                                }
                                if (Manager != undefined && Manager.length > 0) {
                                    $('#ddlManager').empty().append('<option></option>');
                                    for (var i = 0; i < Manager.length; i++) {
                                        $('#ddlManager').append('<option value="' + Manager[i].UserID + '">' + Manager[i].ManagerID + '</option>');
                                    }
                                    selectInit('#ddlManager', 'Select Manager');
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

            function Submit() {
                var getUrl = "/API/User/CreateUser";
                ProcessCreateUpdate('', getUrl, 'create');
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

            function Update() {
                var id = $('#UserID').val();
                var getUrl = "/API/User/UpdateUser";
                ProcessCreateUpdate(id, getUrl, 'update');
            }

            function ProcessCreateUpdate(id, getUrl, flag) {
                var result = InputValidation(flag);
                if (result.error) {
                    Swal.fire({
                        title: "Alert",
                        text: result.msg,
                        icon: "error",
                        button: "Ok",
                    });
                }
                else {
                    debugger
                    ShowLoader();
                    var Role = $("#ddlRole option:selected").val();
                    var FirstName = $("#txtFname").val();
                    var LastName = $("#txtLname").val();
                    var EmailID = $("#txtEmailId").val();

                    var Password = '';
                    var UpdateFlag = '0';
                    if (flag == 'create') {
                        Password = $("#txtPassword").val();
                    }
                    else {
                        if ($('#cbUpdatePassword').prop('checked') == true) {
                            Password = $("#txtUpdatePassword").val();
                            UpdateFlag = '1';
                        }
                    }
                    debugger
                    var MobileNum = $("#txtMobileNo").val();
                    var Position = $("#txtPosition").val();
                    var GroupId = $("#ddlGroup option:selected").val();

                    var Gender = $("#ddlGender option:selected").val();
                    var DepartmentID = $("#ddlDepartment option:selected").val();
                    var TeamID = $("#ddlTeam option:selected").val();
                    var ManagerID = $("#ddlManager option:selected").val();
                    var DOJ = $("#txtDOJ").val();

                    if (flag == 'create') {
                        var requestParams = { RoleID: Role, FirstName: FirstName, LastName: LastName, EmailID: EmailID, Password: Password, MobileNum: MobileNum, Position: Position, GroupId: GroupId, UpdateFlag: UpdateFlag, Gender: Gender, DepartmentID: DepartmentID, TeamID: TeamID, ManagerID: ManagerID, DOJ: DOJ };
                    }
                    else {
                        var requestParams = { UserID: id, RoleID: Role, FirstName: FirstName, LastName: LastName, EmailID: EmailID, Password: Password, MobileNum: MobileNum, Position: Position, GroupId: GroupId, UpdateFlag: UpdateFlag, Gender: Gender, DepartmentID: DepartmentID, TeamID: TeamID, ManagerID: ManagerID, DOJ: DOJ };
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

            function InputValidation(flag) {
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
                else if (!IsValidEmail($("#txtEmailId").val())) {
                    return { error: true, msg: "Please provide valid EmailID of user" };
                }
                //else if ($("#ddlGender option:selected").val() == undefined || $("#ddlGender option:selected").val() == '') {
                //    return { error: true, msg: "Please select Gender" };
                //}
                //else if ($("#ddlDepartment option:selected").val() == undefined || $("#ddlDepartment option:selected").val() == '') {
                //    return { error: true, msg: "Please select Department" };
                //}
                //else if ($("#ddlTeam option:selected").val() == undefined || $("#ddlTeam option:selected").val() == '') {
                //    return { error: true, msg: "Please select Team" };
                //}
                //else if ($("#ddlManager option:selected").val() == undefined || $("#ddlManager option:selected").val() == '') {
                //    return { error: true, msg: "Please select Manager" };
                //}
                //else if ($("#txtDOJ").val() == undefined || $("#txtDOJ").val() == '') {
                //    return { error: true, msg: "Please enter Date of Joining" };
                //}

                if (flag == 'create') {
                    if ($("#txtPassword").val() == undefined || $("#txtPassword").val() == '') {
                        return { error: true, msg: "Please enter password" };
                    }
                }
                else {
                    if ($('#cbUpdatePassword').prop('checked') == true && ($("#txtUpdatePassword").val() == undefined || $("#txtUpdatePassword").val() == '')) {
                        return { error: true, msg: "Please enter password" };
                    }
                }
                return true;
            }
        
            function IsValidEmail(EmailId){
                debugger
                //var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                var reg = /^([0-9a-zA-Z]([\+\-_\.]+)*)+\@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$/;
                if (reg.test(EmailId) == false) {
                    return false;
                }
                return true;
            }

            function Edit(ctrl, id) {
                //var id = $(ctrl).closest('tr').attr('id')
                //debugger
                ShowLoader();
                clearFields('.input-validation');
                BindDropDown();
                BindRoleAndGroup(id, 'update');                
            }

            function BindRoleAndGroup(id, flag) {
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

                                if (Role != undefined && Role.length > 0) {
                                    $('#ddlRole').empty().append('<option></option>');
                                    for (var i = 0; i < Role.length; i++) {
                                        $('#ddlRole').append('<option value="' + Role[i].RoleID + '">' + Role[i].RoleDisplayName + '</option>');
                                    }
                                    selectInit('#ddlRole', 'Select Role');
                                }
                                if (Group != undefined && Group.length > 0) {
                                    $('#ddlGroup').empty().append('<option></option>');
                                    for (var i = 0; i < Group.length; i++) {
                                        $('#ddlGroup').append('<option value="' + Group[i].GroupID + '">' + Group[i].GroupName + '</option>');
                                    }
                                    //$('#divGroup').show();
                                    selectInit('#ddlGroup', 'Select Group');
                                }

                                if (flag == 'update') {
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

            function BindUserData(id) {
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
                            debugger
                            var DataSet = $.parseJSON(response);
                            HideLoader();
                            if (DataSet.StatusCode == "1") {

                                toggle('divForm', 'divGird')

                                $('#ddlRole').val(DataSet.Data[0].RoleID).trigger('change');
                                $('#txtFname').val(DataSet.Data[0].FirstName);
                                $('#txtLname').val(DataSet.Data[0].LastName);
                                $('#txtEmailId').val(DataSet.Data[0].EmailID);
                                $('#txtPassword').val('');
                                $('#txtMobileNo').val(DataSet.Data[0].MobileNum);
                                $('#txtPosition').val(DataSet.Data[0].Position);
                                $('#ddlGroup').val(DataSet.Data[0].GroupID).trigger('change');
                                
                                $('#ddlGender').val(DataSet.Data[0].Gender).trigger('change');
                                $('#ddlDepartment').val(DataSet.Data[0].DeptID).trigger('change');
                                $('#ddlTeam').val(DataSet.Data[0].TeamID).trigger('change');
                                $('#ddlManager').val(DataSet.Data[0].ManagerID).trigger('change');

                                if (DataSet.Data[0].StartDate != undefined && DataSet.Data[0].StartDate != '')
                                {
                                    var fromDate = new Date(DataSet.Data[0].StartDate);
                                    var Year = fromDate.getFullYear();
                                    var Month = fromDate.getMonth() + 1;
                                    if (Month < 10)
                                    {
                                        Month = '0' + Month;
                                    }
                                    var Day = fromDate.getDay();
                                    if (Day < 10) {
                                        Day = '0' + Day;
                                    }
                                    var date = Year + '-' + Month + '-' + Day;

                                    $('#txtDOJ').val(date);
                                }                                

                                $('#btnSubmit').hide();
                                $('#divPassword').hide();
                                $('#btnUpdate').show();
                                $('#divUpdatePassword').show();
                                $('#UserID').val(id);
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

            function Delete(ctrl, id) {
                //var id = $(ctrl).closest('tr').attr('id')


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
                        DeleteUser(id);
                    }
                })
            }

            function DeleteUser(id) {
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

            function enableUpdatePassword() {
                //debugger
                if ($('#cbUpdatePassword').prop('checked')) {
                    $("#txtUpdatePassword").removeAttr("disabled");
                }
                else {
                    $("#txtUpdatePassword").attr("disabled", "disabled");
                }
            }

    </script>
</asp:Content>
