<%@ Page Title="Settings" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="_365_Portal.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row settings">
        <div class="col-md-12 header">
            <a class="back" href="Default.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Your Profile</h1>
        </div>
        <div class="col-md-6 offset-md-3 mt-5">
            <ul class="nav nav-pills mb-3" id="pills-tab" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" id="pills-profile-tab" data-toggle="pill" href="#pills-profile" role="tab" aria-controls="pills-profile" aria-selected="true">Profile</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="pills-notifications-tab" data-toggle="pill" href="#pills-notifications" role="tab" aria-controls="pills-notifications" aria-selected="false">Notifications</a>
                </li>
            </ul>
            <div class="tab-content mt-5" id="pills-tabContent">
                <div class="tab-pane fade show active" id="pills-profile" role="tabpanel" aria-labelledby="pills-profile-tab">
                    <div class="row">
                        <div class="col-md-12">
                            <img class="circle user-photo" id="imgUserPic" src="../Asset/images/profile.png" />
                        </div>
                        <div class="col-md-12 mt-3">
                            <div class="custom-file">
                                <%--<input type="file" class="custom-file-input" id="fileChangePic" onchange="setImgSrc(this, 'imgUserPic')">--%>
                                <input type="file" class="custom-file-input" id="fileChangePic" onchange="encodeImagetoBase64(this,'userpic')">
                                <label class="custom-file-label" for="customFile">Change Profile Pic</label>
                            </div>
                        </div>

                        <div id="divCompanyLogo" style="display: none">
                            <div class="col-md-12">
                                <img class="circle user-photo" id="imgCompLogo" src="../Asset/images/CompanyLogo.png" />
                            </div>
                            <div class="col-md-12 mt-3">
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="fileChangeCompanyLogo" onchange="encodeImagetoBase64(this,'companypic')">
                                    <label class="custom-file-label" for="customFile">Change Company Logo</label>
                                </div>
                            </div>
                        </div>


                        <div id="divCompanyTheme" style="display: none; padding-top: 20px;">
                        </div>


                        <div class="col-md-10 mt-5 form-page form-control-bg-d">
                            <div class="form-group">
                                <label for="txtFirstName">First Name</label>
                                <input type="text" class="form-control" id="txtFirstName" disabled />
                            </div>
                            <div class="form-group">
                                <label for="txtLastName">Last Name</label>
                                <input type="text" class="form-control" id="txtLastName" disabled />
                            </div>
                            <div class="form-group">
                                <label for="txtPosition">Position</label>
                                <input type="text" class="form-control" id="txtPosition" />
                            </div>
                            <div class="form-group">
                                <label for="txtEmail">Email</label>
                                <input type="email" class="form-control" id="txtEmail" />
                            </div>
                            <div class="form-group">
                                <label for="txtGroup">Group</label>
                                <input type="text" class="form-control" id="txtGroup" disabled />
                            </div>
                            <div class="form-group">
                                <label for="ddlRole">Role</label>
                                <input type="text" class="form-control" id="txtRole" disabled />
                            </div>
                            <div id="divChangePassword" class="mt-4" style="display: none;">
                                <a class="link font-weight-bold" href="ChangePassword.aspx">Change password</a>
                            </div>
                            <div class="text-center mt-5">
                                <a class="btn btn-custom bg-blue font-weight-bold text-white" onclick="UpdateUserProfileDetails()">Save</a>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade notification" id="pills-notifications" role="tabpanel" aria-labelledby="pills-notifications-tab">
                    <div class="row">
                        <div class="col-md-10 mt-3 form-page form-control-bg-d">

                            <div class="custom-control custom-checkbox mb-4">
                                <input type="checkbox" class="custom-control-input" id="cbEmailNotifications">
                                <label class="custom-control-label" for="cbEmailNotifications">Email Notifications</label>
                                <span>We’ll send you weekly emails on your current progress.</span>
                            </div>

                            <div class="custom-control custom-checkbox">
                                <input type="checkbox" class="custom-control-input" id="cbPushNotifications">
                                <label class="custom-control-label" for="cbPushNotifications">Push Notifications</label>
                                <span>We’ll send you notifications about updates and your progress.</span>
                            </div>

                            <div class="text-center mt-5">
                                <a class="btn btn-custom bg-blue font-weight-bold text-white" onclick="UpdateNotification()">Save</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>

        var accessToken = '<%=Session["access_token"]%>';
        var Role = '<%=Session["RoleName"]%>';

        var IsFirstLogin = '<%=Session["IsFirstLogin"]%>';
        var IsFirstPasswordNotChanged = '<%=Session["IsFirstPasswordNotChanged"]%>';
        if (IsFirstLogin != undefined && IsFirstLogin.toLowerCase() == 'false') {
            $('#divChangePassword').show();
        }

        $(document).ready(function () {
            if (Role != undefined && (Role == "superadmin" || Role == "companyadmin")) {
                $('#divCompanyLogo').show();
                //$('#divCompanyTheme').append('Choose your theme colors ');
                //$('#divCompanyTheme').append('<input type="color" value="#000000" id="ThemeColor1">');
                //$('#divCompanyTheme').append('<input type="color" value="#000000" id="ThemeColor2">');
                //$('#divCompanyTheme').append('<input type="color" value="#000000" id="ThemeColor3">');
                //$('#divCompanyTheme').append('<input type="color" value="#000000" id="ThemeColor4">');
                //$('#divCompanyTheme').show();

                $('#divCompanyTheme').empty().append('<div class="col-md-12">Choose your theme colors </div>');

                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 1 <input type="color" id="ThemeColor" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 2 <input type="color" id="ThemeColor2" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor2"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 3 <input type="color" id="ThemeColor3" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor3"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 4 <input type="color" id="ThemeColor4" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor4"></div>');
                $('#divCompanyTheme').show();
            }
            GetUserProfileDetails();
        });

        function GetUserProfileDetails() {
            ShowLoader();

            var getUrl = "/API/User/GetMyProfile";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                //data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    try {

                        var DataSet = $.parseJSON(response);
                        HideLoader();
                        if (DataSet.StatusCode == "1") {
                            //alert(DataSet.StatusDescription);                            
                            BindFields(DataSet.Data);
                        }
                        else {
                            //alert(DataSet.StatusDescription);
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

        function ClearFields() {
            $('#txtFirstName').val('');
            $('#txtLastName').val('');
            $('#txtPosition').val('');
            $('#txtEmail').val('');
            $("#cbEmailNotifications").prop('checked', false);
            $("#cbPushNotifications").prop('checked', false);
        }

        function BindFields(Data) {
            $('#txtFirstName').val(Data.FirstName);
            $('#txtLastName').val(Data.LastName);
            $('#txtPosition').val(Data.Position);
            $('#txtEmail').val(Data.EmailID);
            $("#cbEmailNotifications").prop('checked', Data.EmailNotification);
            $("#cbPushNotifications").prop('checked', Data.PushNotification);

            //$("#ddlGroup").append("<option value='" + Data.GroupName + "'selected >" + Data.GroupName + "</option>");
            //$("#ddlRole").append("<option value='" + Data.Role + "'selected >" + Data.Role + "</option>");

            $("#txtGroup").val(Data.GroupName);
            $("#txtRole").val(Data.Role);

            if (Data.ProfilePicFile != undefined && Data.ProfilePicFile != '') {
                //document.getElementById('imgUserPic')
                //.setAttribute(                    
                //    'src', 'data:image/png;base64,' + Data.ProfilePicFile
                //);

                $("#imgUserPic").attr("src", "../Files/ProfilePic/" + Data.ProfilePicFile);
            }

            if (Role != undefined && (Role == "superadmin" || Role == "companyadmin") && Data.CompanyProfilePicFile != undefined && Data.CompanyProfilePicFile != '') {
                //document.getElementById('imgCompLogo')
                //.setAttribute(
                //    'src', 'data:image/png;base64,' + Data.CompanyProfilePicFile
                //);

                $("#imgCompLogo").attr("src", "../Files/CompLogo/" + Data.CompanyProfilePicFile);
                $('#divCompanyTheme').empty().append('<div class="col-md-12">Choose your theme colors </div>');

                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 1 <input type="color" id="ThemeColor" value="' + Data.ThemeColor + '" onchange="assignColor(this)">&nbsp;<input type="text" value="' + Data.ThemeColor + '" id="txtThemeColor" onkeyup="getCustomColor(this)"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 2 <input type="color" id="ThemeColor2" value="' + Data.ThemeColor2 + '" onchange="assignColor(this)">&nbsp;<input type="text" value="' + Data.ThemeColor2 + '" id="txtThemeColor2" onkeyup="getCustomColor(this)"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 3 <input type="color" id="ThemeColor3" value="' + Data.ThemeColor3 + '" onchange="assignColor(this)">&nbsp;<input type="text" value="' + Data.ThemeColor3 + '" id="txtThemeColor3" onkeyup="getCustomColor(this)"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">color 4 <input type="color" id="ThemeColor4" value="' + Data.ThemeColor4 + '" onchange="assignColor(this)">&nbsp;<input type="text" value="' + Data.ThemeColor4 + '" id="txtThemeColor4" onkeyup="getCustomColor(this)"></div>');
                $('#divCompanyTheme').show();
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
                        //alert(base64UserProfileString);
                        //$("#base64").attr("href", reader.result);
                        //rawString = $("#base64").text(reader.result);
                        //base64UserProfileString = rawString[0].textContent.split(",").pop();
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

        function UpdateUserProfileDetails() {
            ShowLoader();
            var EmailID = $('#txtEmail').val();
            var Position = $('#txtPosition').val();
            //var EmailNotification = $('#cbEmailNotifications').prop('checked');
            //var PushNotification = $('#cbPushNotifications').prop('checked');

            var ThemeColor = '';
            var ThemeColor2 = '';
            var ThemeColor3 = '';
            var ThemeColor4 = '';
            if (Role != undefined && (Role == "superadmin" || Role == "companyadmin")) {
                var theInput = document.getElementById("ThemeColor");
                ThemeColor = theInput.value;
                theInput = document.getElementById("ThemeColor2");
                ThemeColor2 = theInput.value;
                theInput = document.getElementById("ThemeColor3");
                ThemeColor3 = theInput.value;
                theInput = document.getElementById("ThemeColor4");
                ThemeColor4 = theInput.value;
            }

            var requestParams = { EmailID: EmailID, Position: Position, UserProfileImageBase64: base64UserProfileString, CompanyProfileImageBase64: base64CompanyProfileString, CompanyThemeColor: ThemeColor, CompanyThemeColor2: ThemeColor2, CompanyThemeColor3: ThemeColor3, CompanyThemeColor4: ThemeColor4 };
            var getUrl = "/API/User/UpdateMyProfile";
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

                            //Swal.fire(DataSet.Data.ReturnMessage, {
                            //    icon: "success",
                            //}).then((UpdateUserProfile) => {
                            //    if (IsFirstPasswordNotChanged != undefined && IsFirstPasswordNotChanged.toLowerCase() == 'true') {
                            //        window.location.href = "ChangePassword.aspx";
                            //    }
                            //    else {
                            //        location.reload();
                            //    }
                            //    });;

                            Swal.fire({
                                title: 'Success',
                                icon: 'success',
                                html: DataSet.Data.ReturnMessage,
                                showConfirmButton: true,
                                showCloseButton: true
                            }).then((UpdateUserProfile) => {
                                if (IsFirstPasswordNotChanged != undefined && IsFirstPasswordNotChanged.toLowerCase() == 'true') {
                                    window.location.href = "ChangePassword.aspx";
                                }
                                else {
                                    location.reload();
                                }
                            });;

                            //alert(DataSet.Data.ReturnMessage);
                            //if (IsFirstPasswordNotChanged != undefined && IsFirstPasswordNotChanged.toLowerCase() == 'true') {
                            //    window.location.href = "ChangePassword.aspx";
                            //}
                            //else {
                            //    location.reload();
                            //}
                        }
                        else {
                            HideLoader();
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

        function UpdateNotification() {
            ShowLoader();
            var EmailNotification = $('#cbEmailNotifications').prop('checked');
            var PushNotification = $('#cbPushNotifications').prop('checked');
            var requestParams = { EmailNotification: EmailNotification, PushNotification: PushNotification };
            var getUrl = "/API/User/UpdateNotification";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    try {

                        HideLoader();
                        var DataSet = $.parseJSON(response);
                        if (DataSet.StatusCode == "1") {
                            //Swal.fire(DataSet.Data.ReturnMessage, {
                            //    icon: "success",
                            //}).then((CreateUpdateUser) => {
                            //    location.reload();
                            //});;

                            Swal.fire({
                                title: 'Success',
                                icon: 'success',
                                html: DataSet.Data.ReturnMessage,
                                showConfirmButton: true,
                                showCloseButton: true
                            }).then((CreateUpdateUser) => {
                                location.reload();
                            });;
                        }
                        else {
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
                    alert(response.data);
                }
            });
        }

        function setImgSrc(ctrl, img) {
            $('#' + img).attr('src', URL.createObjectURL(ctrl.files[0]));
        }

        function assignColor(obj) {
            if (obj.id == 'ThemeColor') {
                $('#txtThemeColor').val(obj.value);
            }
            else if (obj.id == 'ThemeColor2') {
                $('#txtThemeColor2').val(obj.value);
            }
            else if (obj.id == 'ThemeColor3') {
                $('#txtThemeColor3').val(obj.value);
            }
            else if (obj.id == 'ThemeColor4') {
                $('#txtThemeColor4').val(obj.value);
            }
        }

        function getCustomColor(obj) {
            if (obj.id == 'txtThemeColor') {
                document.getElementById("ThemeColor").value = obj.value;
            }
            else if (obj.id == 'txtThemeColor2') {
                document.getElementById("ThemeColor2").value = obj.value;
            }
            else if (obj.id == 'txtThemeColor3') {
                document.getElementById("ThemeColor3").value = obj.value;
            }
            else if (obj.id == 'txtThemeColor4') {
                document.getElementById("ThemeColor4").value = obj.value;
            }
        }

    </script>
</asp:Content>


