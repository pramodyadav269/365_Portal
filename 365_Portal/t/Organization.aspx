<%@ Page Title="" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="Organization.aspx.cs" Inherits="_365_Portal.t.Organization" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">

    <div class="row settings">
        <div class="col-md-12 header mb-4">
            <a class="back" href="dashboard.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Manage Organization</h2>
        </div>
        <div class="col-md-12" id="divGird">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12 col-md-2">
                            <a class="btn bg-yellow" id="btnAddNew" style="display: none" onclick="AddNew();">Add New</a>
                        </div>
                    </div>
                    <div id="divTable" class="mt-3 table-responsive"></div>
                </div>
            </div>
        </div>

        <div class="col-md-12 d-none user-details" id="divForm">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">


                    <div class="row input-validation">
                        <div class="col-md-3">
                            <h3 class="text-left font-weight-bold">Organization Details</h3>
                        </div>
                    </div>

                    <div class="row input-validation">

                        <div class="col-md-4">
                            <img class="circle user-photo" id="imgUserPic" src="../Asset/images/profile.png" />
                            <div class="custom-file">
                                <input type="file" class="custom-file-input" id="fileChangePic" onchange="encodeImagetoBase64(this,'userpic')">
                                <label class="custom-file-label" for="customFile">Change Profile Pic</label><br />
                                <br />
                            </div>
                        </div>

                            <div class="col-md-4" id="divFavicon">
                                <img class="circle user-photo" id="imgFavicon" src="../INCLUDES/Asset/images/menu.png" />
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="fileChangeFavicon" onchange="encodeImagetoBase64(this,'favicon')">
                                    <label class="custom-file-label mt-2" for="fileChangeFavicon">Change Favicon</label>
                                </div>
                            </div>
                            <div class="w-100"></div>
                            <div class="col-md-12 mt-4 mb-3" id="divCompanyTheme">
                                <div class="row ">
                                    <div class="col-md-12">Choose your theme colors </div>
                                    <div class="col-md-12 mt-3">
                                        Branding Color
                                    <input type="color" id="ThemeColor" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor">
                                    </div>
                                    <div class="col-md-12 mt-3">
                                        Custom Link Color
                                    <input type="color" id="ThemeColor2" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor2">
                                    </div>
                                    <div class="col-md-12 mt-3 d-none">
                                        Button Font Color
                                    <input type="color" id="ThemeColor3" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor3">
                                    </div>
                                    <div class="col-md-12 mt-3 d-none">Custom Font<input type="text" id="txtCustomFont"></div>
                                    <div class="w-100 mt-3"></div>
                                    <div class="col-sm-6">
                                        Botton Font Color
                                        <div class="row mt-2">
                                            <div class="col button-color">
                                                <a class="font-weight-bold auto" button-data="auto">Preview</a>
                                                <span class="label active">Auto</span>
                                            </div>
                                            <div class="col button-color">
                                                <a class="font-weight-bold dark" button-data="dark">Preview</a>
                                                <span class="label">Dark</span>
                                            </div>
                                            <div class="col button-color">
                                                <a class="font-weight-bold light" button-data="light">Preview</a>
                                                <span class="label">Light</span>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        Custom Font
                                        <div class="row mt-2">
                                            <div class="col custom-font">
                                                <div class="serif custom-font-style" font-data="serif">
                                                    <span class="titles">Titles</span>
                                                    <span class="paragraphs">Paragraphs</span>
                                                    <span class="captions">Captions</span>
                                                </div>
                                                <span class="label mt-2">Serif</span>
                                            </div>
                                            <div class="col custom-font">
                                                <div class="sans-serif custom-font-style" font-data="sans-serif">
                                                    <span class="titles">Titles</span>
                                                    <span class="paragraphs">Paragraphs</span>
                                                    <span class="captions">Captions</span>
                                                </div>
                                                <span class="label mt-2 active">Sans Serif (Default)</span>
                                            </div>
                                            <div class="col custom-font">
                                                <div class="mixed-serif custom-font-style" font-data="mixed-serif">
                                                    <span class="titles">Titles</span>
                                                    <span class="paragraphs">Paragraphs</span>
                                                    <span class="captions">Captions</span>
                                                </div>
                                                <span class="label mt-2">Mixed Serif</span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="w-100"></div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="txtBusinessName">Business Name</label>
                                    <input type="text" class="form-control required" id="txtBusinessName" placeholder="Business Name" />
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="ddlEmployeeCount">No Of Employee</label>
                                    <select class="form-control select2 required" id="ddlEmployeeCount" style="width: 100% !important">
                                        <option></option>
                                        <option value="1">Just You</option>
                                        <option value="2">2-9</option>
                                        <option value="3">10-99</option>
                                        <option value="4">300+</option>
                                    </select>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="ddlCountry">Country</label>
                                    <select class="form-control select2 required" id="ddlCountry" style="width: 100% !important">
                                    </select>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div id="divAdminDetails" style="display: none;">
                        <div class="row input-validation mt-3">

                            <div class="form-header col-md-12">
                                <h3>Admin Details</h3>
                            </div>
                            <div class="w-100"></div>

                            <div class="col-md-4">
                                <img class="circle user-photo" id="imgUserPic" src="../Asset/images/profile.png" />
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="fileChangePic" onchange="encodeImagetoBase64(this,'userpic')">
                                    <label class="custom-file-label mt-2" for="fileChangePic">Change Profile Pic</label><br />
                                    <br />
                                </div>
                            </div>
                        </div>
                        <div class="row input-validation">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label for="ddlRole">Role</label>
                                    <select class="form-control select2 required" id="ddlRole" style="width: 100% !important">
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

                            <div class="col-md-3" id="divUpdatePassword" style="display: none;">
                                <div class="custom-control custom-checkbox mb-4">
                                    <input type="checkbox" onchange="enableUpdatePassword();" class="custom-control-input" id="cbUpdatePassword">
                                    <label class="custom-control-label" for="cbUpdatePassword">Want to change password!</label>
                                    <input type="password" disabled class="form-control required" id="txtUpdatePassword" placeholder="Password" />
                                </div>
                            </div>

                            <input type="hidden" id="UserID" value="" />
                        </div>
                    </div>

                    <div class="row input-validation">
                        <div class="col-md-12 mt-4">
                            <a class="btn bg-yellow float-left" id="btnBack" onclick="toggle('divGird', 'divForm')">Back</a>
                            <a class="btn bg-yellow float-right" id="btnSubmit" onclick="Submit();">Submit</a>
                            <a class="btn bg-yellow float-right" id="btnUpdate" style="display: none;" onclick="Update();">Update</a>
                        </div>
                    </div>

                </div>
            </div>
        </div>
    </div>


    <script>

        var accessToken = '<%=Session["access_token"]%>';
        var Role = '<%=Session["RoleName"]%>';
        var id = '';
        var customFont = '';
        var buttonFontColor = '';

        $(document).ready(function () {
            //debugger
            ShowLoader();
            if (Role == "superadmin") {
                $('#btnAddNew').show();
                GetAdminUsers();
            }
            else if (Role == "companyadmin") {
                clearFields('.input-validation');
                BindCountry('update');
                BindUserData(0);

                $('#ddlRole').empty().append('<option value="">Select Option</option>');
                $('#ddlRole').append('<option value="10">Company Admin</option>');
                $("#ddlRole").prop("disabled", true);

                $('#btnAddNew').hide();
                $('#btnBack').hide();
                $('#btnSubmit').hide();
                $('#divPassword').hide();
                $('#btnUpdate').show();
                $('#divUpdatePassword').show();
            }
            else {
                Swal.fire("You do not have access for this functionality", {
                    icon: "error",
                });
            }

            $('.custom-font-style').click(function () {
                $('.custom-font .label').removeClass('active')
                $(this).parent().find('.label').addClass('active')
                $('body').css({ "font-family": $(this).attr('font-data') })
            })


            $('.button-color a').click(function () {
                $('.button-color .label').removeClass('active')
                $(this).parent().find('.label').addClass('active')
                $('.btn').removeClass('auto').removeClass('dark').removeClass('light')
                $('.btn').addClass($(this).attr('button-data'))
            })
        });

            $('.button-color a').click(function () {
                $('.button-color .label').removeClass('active')
                $(this).parent().find('.label').addClass('active')
                $('.btn').removeClass('auto').removeClass('dark').removeClass('light')
                $('.btn').addClass($(this).attr('button-data'));
                buttonFontColor = $(this).attr('button-data');
            });


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
                        //debugger
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
                '<thead><tr><th>#</th><th style="display:none;">ID</th><th>First Name</th><th>Last Name</th><th>Email ID</th><th>Position</th><th>Role</th><th>Action</th></thead>'

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
                    tbl += '<td><i  title="Edit" onclick="Edit(this,' + Table[i].UserID + ');" class="fas fa-edit text-warning"></i>' +
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
            clearFields('.input-validation');

            //$("#imgUserPic").attr("src", "../Files/ProfilePic/" + DataSet.Data[0].ProfilePicFile);
            //$("#imgCompLogo").attr("src", "../Files/CompLogo/" + DataSet.Data[0].CompanyProfilePicFile);
            $("#imgUserPic").attr("src", "../Asset/images/profile.png");
            $("#imgCompLogo").attr("src", "../Asset/images/CompanyLogo.png");
            $("#imgFavicon").attr("src", "../INCLUDES/Asset/images/menu.png");

            toggle('divForm', 'divGird');

            BindCountry('create');
            BindRole('create');
        }

        function BindCountry(flag) {
            ShowLoader();
            if ($('#ddlCountry > option') != undefined && $('#ddlCountry > option').length == 0) {
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
                                    $('#ddlCountry').empty().append('<option value="">Select Option</option>');
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
            HideLoader();
        }

        function BindRole(id, flag) {
            ShowLoader();
            if ($('#ddlRole > option') != undefined && $('#ddlRole > option').length == 0) {
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
                            //debugger
                            if (DataSet.StatusCode == "1") {

                                var Role = DataSet.Data;
                                if (Role != undefined && Role.length > 0) {
                                    $('#ddlRole').empty().append('<option value="">Select Option</option>');
                                    $('#ddlRole').empty();
                                    for (var i = 0; i < Role.length; i++) {
                                        $('#ddlRole').append('<option value="' + Role[i].RoleID + '">' + Role[i].RoleDisplayName + '</option>');
                                    }
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
            if (flag == 'update') {
                BindUserData(id);
            }
            HideLoader();
        }

        function BindUserData(id) {
            //debugger
            ShowLoader();
            var requestParams = { UserID: id };

            var getUrl = "";
            if (Role == "superadmin") {
                getUrl = "/API/Organization/GetAdminUserDetailsForParent";
            }
            else {
                getUrl = "/API/Organization/GetAdminUserDetails";
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

                            toggle('divForm', 'divGird')

                            $('#ThemeColor').val(DataSet.Data[0].CompanyThemeColor);
                            $('#ThemeColor2').val(DataSet.Data[0].CompanyThemeColor2);
                            //$('#ThemeColor3').val(DataSet.Data[0].CompanyThemeColor3);

                            $('#txtThemeColor').val(DataSet.Data[0].CompanyThemeColor);
                            $('#txtThemeColor2').val(DataSet.Data[0].CompanyThemeColor2);
                            //$('#txtThemeColor3').val(DataSet.Data[0].CompanyThemeColor3);
                            //$('#txtCustomFont').val(DataSet.Data[0].CompanyThemeColor4);

                            $('#txtBusinessName').val(DataSet.Data[0].BusinessName);
                            $('#ddlEmployeeCount').val(DataSet.Data[0].NoOfEmployees);
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

                            if (DataSet.Data[0].ProfilePicFile != undefined && DataSet.Data[0].ProfilePicFile != '') {
                                $("#imgUserPic").attr("src", "../Files/ProfilePic/" + DataSet.Data[0].ProfilePicFile);
                            }

                            if (DataSet.Data[0].CompanyProfilePicFile != undefined && DataSet.Data[0].CompanyProfilePicFile != '') {
                                $("#imgCompLogo").attr("src", "../Files/CompLogo/" + DataSet.Data[0].CompanyProfilePicFile);
                            }
                            if (DataSet.Data[0].FaviconPicFile != undefined && DataSet.Data[0].FaviconPicFile != '') {
                                $("#imgFavicon").attr("src", "../Files/Favicon/" + DataSet.Data[0].FaviconPicFile);
                            }

                            if (Role == "superadmin") {
                                $("#divAdminDetails").show();
                            }
                            else if (Role == "companyadmin") {
                                $("#divAdminDetails").hide();
                            }
                            if (DataSet.Data[0].CompanyThemeColor3 == null || DataSet.Data[0].CompanyThemeColor3 == "") {
                                DataSet.Data[0].CompanyThemeColor3 = "auto";
                            }
                            if (DataSet.Data[0].CompanyThemeColor4 == null || DataSet.Data[0].CompanyThemeColor4 == "") {
                                DataSet.Data[0].CompanyThemeColor4 = "sans-serif";
                            }

                            $("[id=dvCustomFont_" + DataSet.Data[0].CompanyThemeColor4 + "]").addClass('active');
                            $("[id=dvButtonColor_" + DataSet.Data[0].CompanyThemeColor3 + "]").addClass('active');

                            //$('.select2').material_select();
                            //selectInit('#ddlCountry', 'Select Country');
                            //selectInit('#ddlRole', 'Select Role');
                            //selectInit('#ddlEmployeeCount', 'Select No Of Employee');
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
            //debugger
            var id = $('#UserID').val();
            var getUrl = "";

            if (Role == "superadmin") {
                getUrl = "/API/Organization/UpdateAdminUserForParent";
            }
            else {
                getUrl = "/API/Organization/UpdateAdminUser";
            }

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
                //debugger
                var theInput = document.getElementById("ThemeColor");
                var ThemeColor = theInput.value;
                theInput = document.getElementById("ThemeColor2");
                var ThemeColor2 = theInput.value;
                //theInput = document.getElementById("ThemeColor3");
                //var ThemeColor3 = theInput.value;
                //theInput = document.getElementById("txtCustomFont");
                //var CustomFont = theInput.value;

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
                        UserProfileImageBase64: base64UserProfileString, CompanyProfileImageBase64: base64CompanyProfileString, FaviconImageBase64: base64FaviconString
                        , CompanyThemeColor: ThemeColor, CompanyThemeColor2: ThemeColor2, CompanyThemeColor3: ThemeColor3, CompanyCustomFont: CustomFont
                        , BusinessName: BusinessName, EmployeeCount: EmployeeCount, Country: Country, RoleID: Role, FirstName: FirstName
                        , LastName: LastName, EmailID: EmailID, Password: Password, MobileNum: MobileNum, Position: Position, UpdateFlag: UpdateFlag
                    };
                }
                else {
                    var requestParams = {
                        UserID: id, UserProfileImageBase64: base64UserProfileString, CompanyProfileImageBase64: base64CompanyProfileString, FaviconImageBase64: base64FaviconString
                        , CompanyThemeColor: ThemeColor, CompanyThemeColor2: ThemeColor2, CompanyThemeColor3: ThemeColor3, CompanyCustomFont: CustomFont
                        , BusinessName: BusinessName, EmployeeCount: EmployeeCount, Country: Country
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
                            //debugger
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


            if (Role == "superadmin") {
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
        var base64FaviconString = '';
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
                        else if (flag == 'favicon') {
                            base64FaviconString = reader.result;
                            $("#imgFavicon").attr("src", base64FaviconString);
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
            debugger
            ShowLoader();
            clearFields('.input-validation')
            $("#imgUserPic").attr("src", "../Asset/images/profile.png");
            $("#imgCompLogo").attr("src", "../Asset/images/CompanyLogo.png");
            $("#imgFavicon").attr("src", "../INCLUDES/Asset/images/menu.png");

            BindCountry('update');
            BindRole(id, 'update');
        }

        function assignColor(obj) {
            if (obj.id == 'ThemeColor') {
                $('#txtThemeColor').val(obj.value);
            }
            else if (obj.id == 'ThemeColor2') {
                $('#txtThemeColor2').val(obj.value);
            }
            //else if (obj.id == 'ThemeColor3') {
            //    $('#txtThemeColor3').val(obj.value);
            //}
            //else if (obj.id == 'CustomFont') {
            //    $('#txtCustomFont').val(obj.value);
            //}
        }

        function Delete(ctrl, id) {
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

    </script>

</asp:Content>
