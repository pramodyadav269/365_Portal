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

                        <div class="col-md-12">
                            <img class="circle user-photo" id="imgUserPic" src="../Asset/images/profile.png" />
                        </div>
                        <div class="col-md-12 mt-3">
                            <div class="custom-file">
                                <input type="file" class="custom-file-input" id="fileChangePic" onchange="encodeImagetoBase64(this,'userpic')">
                                <label class="custom-file-label" for="customFile">Change Profile Pic</label><br /><br />
                            </div>
                        </div>

                        <div id="divCompanyLogo" style="display: none">
                            <div class="col-md-12">
                                <img class="circle user-photo" id="imgCompLogo" src="../Asset/images/CompanyLogo.png" />
                            </div>
                            <div class="col-md-12 mt-3">
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="fileChangeCompanyLogo" onchange="encodeImagetoBase64(this,'companypic')">
                                    <label class="custom-file-label" for="customFile">Change Organization Logo</label>
                                </div>
                            </div>
                        </div>

                        <div id="divCompanyTheme" style="display: none; padding-top: 20px;">
                            <div class="col-md-12">Choose your theme colors </div>
                            <div class="col-md-12 mt-3">Branding Color <input type="color" id="ThemeColor" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor"></div>
                            <div class="col-md-12 mt-3">Custom Link Color <input type="color" id="ThemeColor2" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor2"></div>
                            <div class="col-md-12 mt-3">Button Font Color <input type="color" id="ThemeColor3" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor3"></div>
                            <div class="col-md-12 mt-3">Custom Font<input type="text" id="txtThemeColor4"></div>
                        </div>

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

                        <div class="col-md-3" id="divUpdatePassword" style="display:none;">
                            <div class="custom-control custom-checkbox mb-4">
                                <input type="checkbox" onchange="enableUpdatePassword();" class="custom-control-input" id="cbUpdatePassword">
                                <label class="custom-control-label" for="cbUpdatePassword">Want to change password!</label>
                                <input type="password" disabled class="form-control required" id="txtUpdatePassword" placeholder="Password" />
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
            GetAdminUsers();
            //if (Role == "superadmin")
                //BindCompanies();
        });

        function GetAdminUsers() {
            ShowLoader();
            var getUrl = "/API/Organization/GetAdminUsers";
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

            BindCountry('create');
            BindRole('create');
        }

        function BindCountry(flag)
        {
            if ($('#ddlCountry > option') != undefined && $('#ddlCountry > option').length == 0)
            {
                ShowLoader();
                var getUrl = "/API/Organization/GetCountry";
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
                            if (DataSet.StatusCode == "1") {

                                var Country = DataSet.Data;
                                if (Country != undefined && Country.length > 0) {
                                    $('#ddlCountry').empty().append('<option></option>');
                                    for (var i = 0; i < Country.length; i++) {
                                        $('#ddlCountry').append('<option value="' + Country[i].Id + '">' + Country[i].CountryName + '</option>');
                                    }
                                    selectInit('#Country', 'Select Country');
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
        }

        function BindRole(flag)
        {
            if ($('#ddlRole > option') != undefined && $('#ddlRole > option').length == 0)
            {
                ShowLoader();
                var getUrl = "/API/Organization/BindRole";
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

                                var Role = DataSet.Data;
                                if (Role != undefined && Role.length > 0) {
                                    $('#ddlRole').empty().append('<option></option>');
                                    for (var i = 0; i < Role.length; i++) {
                                        $('#ddlRole').append('<option value="' + Role[i].RoleID + '">' + Role[i].RoleDisplayName + '</option>');
                                    }
                                    selectInit('#ddlRole', 'Select Role');
                                }

                                if (flag == 'update') {
                                    BindUserData(id);
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
                        //debugger
                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        if (DataSet.StatusCode == "1") {

                            toggle('divForm', 'divGird')

                            $('#ThemeColor').val(Data.ThemeColor);
                            $('#ThemeColor2').val(Data.ThemeColor2);
                            $('#ThemeColor3').val(Data.ThemeColor3);

                            $('#txtThemeColor').val(Data.ThemeColor);
                            $('#txtThemeColor2').val(Data.ThemeColor2);
                            $('#txtThemeColor3').val(Data.ThemeColor3);

                            $('#txtThemeColor4').val(Data.ThemeColor4);


                            $('#txtBusinessName').val(DataSet.Data[0].CompName);
                            $('#ddlEmployeeCount').val(DataSet.Data[0].EmployeeCount);
                            $('#ddlCountry').val(DataSet.Data[0].Country);

                            $('#ddlRole').val(DataSet.Data[0].RoleID);
                            $('#txtFname').val(DataSet.Data[0].FirstName);
                            $('#txtLname').val(DataSet.Data[0].LastName);
                            $('#txtEmailId').val(DataSet.Data[0].EmailID);
                            $('#txtPassword').val('');
                            $('#txtMobileNo').val(DataSet.Data[0].MobileNum);
                            $('#txtPosition').val(DataSet.Data[0].Position);
                            //$('#ddlGroup').val(DataSet.Data[0].GroupID);

                            $('#btnSubmit').hide();
                            $('#divPassword').hide();
                            $('#btnUpdate').show();
                            $('#divUpdatePassword').show();
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

        function Submit() {
            var getUrl = "/API/Organization/CreateAdminUser";
            ProcessCreateUpdate('', getUrl, 'create');
        }

        function Update() {
            var id = $('#UserID').val();
            var getUrl = "/API/User/UpdateAdminUser";
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
                ShowLoader();

                var theInput = document.getElementById("ThemeColor");
                var ThemeColor = theInput.value;
                theInput = document.getElementById("ThemeColor2");
                var ThemeColor2 = theInput.value;
                theInput = document.getElementById("ThemeColor3");
                var ThemeColor3 = theInput.value;
                theInput = document.getElementById("txtThemeColor4");
                var ThemeColor4 = theInput.value;

                var BusinessName = $("#txtBusinessName").val();
                var EmployeeCount = $("#ddlEmployeeCount option:selected").val();
                var Country = $("#ddlCountry option:selected").val();
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

                var MobileNum = $("#txtMobileNo").val();
                var Position = $("#txtPosition").val();
                //var GroupId = $("#ddlGroup option:selected").val();

                if (flag == 'create') {
                    var requestParams = {
                        UserProfileImageBase64: base64UserProfileString, CompanyProfileImageBase64: base64CompanyProfileString, CompanyThemeColor: ThemeColor, CompanyThemeColor2: ThemeColor2
                        , CompanyThemeColor3: ThemeColor3, CompanyThemeColor4: ThemeColor4, BusinessName: BusinessName, EmployeeCount: EmployeeCount, Country: Country, RoleID: Role, FirstName: FirstName
                        , LastName: LastName, EmailID: EmailID, Password: Password, MobileNum: MobileNum, Position: Position, UpdateFlag: UpdateFlag
                    };
                }
                else {
                    var requestParams = {
                        UserID: id, UserProfileImageBase64: base64UserProfileString, CompanyProfileImageBase64: base64CompanyProfileString, CompanyThemeColor: ThemeColor
                        , CompanyThemeColor2: ThemeColor2, CompanyThemeColor3: ThemeColor3, CompanyThemeColor4: ThemeColor4, BusinessName: BusinessName, EmployeeCount: EmployeeCount, Country: Country
                        , RoleID: Role, FirstName: FirstName, LastName: LastName, EmailID: EmailID, Password: Password, MobileNum: MobileNum, Position: Position, UpdateFlag: UpdateFlag
                    };
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
                                }).then((CreateUpdateAdminUser) => {
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
            
            if ($("#txtBusinessName").val() == undefined || $("#txtBusinessName").val() == '') {
                return { error: true, msg: "Please enter Business Name" };
            }
            else if ($("#ddlEmployeeCount option:selected").val() == undefined || $("#ddlEmployeeCount option:selected").val() == '') {
                return { error: true, msg: "Please select Employee count" };
            }
            else if ($("#ddlCountry option:selected").val() == undefined || $("#ddlCountry option:selected").val() == '') {
                return { error: true, msg: "Please select Country" };
            }
            else if ($("#ddlRole option:selected").val() == undefined || $("#ddlRole option:selected").val() == '') {
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

        function enableUpdatePassword() {
            //debugger
            if ($('#cbUpdatePassword').prop('checked')) {
                $("#txtUpdatePassword").removeAttr("disabled");
            }
            else {
                $("#txtUpdatePassword").attr("disabled", "disabled");
            }
        }

        var base64UserProfileString = '';
        var base64CompanyProfileString = '';
        function encodeImagetoBase64(element, flag) {
            //debugger
            var file = element.files[0];
            var size = file.size;
            if (file.size != undefined) {
                if (file.size < 5000000) {
                    var reader = new FileReader();
                    reader.onloadend = function () {

                        if (flag == 'userpic') {
                            base64UserProfileString = reader.result;
                            $("#imgUserPic").attr("src", base64UserProfileString);
                        }
                        else if (flag == 'companypic') {
                            base64CompanyProfileString = reader.result;
                            $("#imgCompLogo").attr("src", base64CompanyProfileString);
                        }
                    }
                    reader.readAsDataURL(file);
                }
                else {
                    Swal.fire("File size should not be greater than 5MB", {
                        icon: "error",
                    });
                }
            }
            else {
                Swal.fire("Invalid File", {
                    icon: "error",
                });
            }

        }

        function Edit(ctrl, id) {
            ShowLoader();
            clearFields('.input-validation')
            //BindRoleAndGroup(id, 'update');
            BindCountry('update');
            BindRole('update');
        }

    </script>

</asp:Content>
