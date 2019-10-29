<%@ Page Title="Settings" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="_365_Portal.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row settings">
        <div class="col-md-12 header"> 
            <a class="back" href="Topics.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Set up your Profile</h1>
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
                            <img class="circle user-photo" id="imgUserPic" src="Asset/images/profile.png" />
                        </div> 
                        <div class="col-md-12 mt-3">
                            <div class="custom-file">
                                <%--<input type="file" class="custom-file-input" id="fileChangePic" onchange="setImgSrc(this, 'imgUserPic')">--%>
                                <input type="file" class="custom-file-input" id="fileChangePic" onchange="encodeImagetoBase64(this,'userpic')">
                                <label class="custom-file-label" for="customFile">Change Profile Pic</label>
                            </div>
                        </div>

                        <div id="divCompanyLogo" style="display:none">
                            <div class="col-md-12">
                                <img class="circle user-photo" id="imgCompanyLogo" src="Asset/images/CompanyLogo.png" />
                            </div>
                            <div class="col-md-12 mt-3">
                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="fileChangeCompanyLogo" onchange="encodeImagetoBase64(this,'companypic')">
                                    <label class="custom-file-label" for="customFile">Change Company Logo</label>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-10 mt-5 form-page form-control-bg-d">
                            <div class="form-group">
                                <label for="txtFirstName">First Name</label>
                                <input type="text" class="form-control" id="txtFirstName" disabled />
                            </div>
                            <div class="form-group">
                                <label for="txtLastName">Last Name</label>
                                <input type="text" class="form-control" id="txtLastName" disabled/>
                            </div>
                            <div class="form-group">
                                <label for="txtPosition">Position</label>
                                <input type="text" class="form-control" id="txtPosition"  />
                            </div>
                            <div class="form-group">
                                <label for="txtEmail">Email</label>
                                <input type="email" class="form-control" id="txtEmail"  />
                            </div>                            
                            <div class="form-group">
                                <label for="ddlGroup">Group</label>
                                <select class="form-control select2" id="ddlGroup"disabled></select>
                            </div>
                            <div class="form-group" >
                                <label for="ddlRole">Role</label>
                                <select class="form-control select2" id="ddlRole" disabled></select>
                            </div>
                            <div id="divChangePassword" class="mt-4" style="display:none;">
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
                                <a class="btn btn-custom bg-blue font-weight-bold text-white">Save</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script>

        debugger
        var accessToken = '<%=Session["access_token"]%>';
        var Role = '<%=Session["RoleName"]%>';

        var IsFirstLogin = '<%=Session["IsFirstLogin"]%>';
        var IsFirstPasswordChanged = '<%=Session["IsFirstPasswordChanged"]%>';
        if (IsFirstLogin != undefined && IsFirstLogin.toLowerCase() == 'false')
        {
            $('#divChangePassword').show();
        }


        $(document).ready(function () {                        
            if (Role != undefined && (Role == "superadmin" || Role == "companyadmin"))
            {
                $('#divCompanyLogo').show();
            }
            GetUserProfileDetails();
        });


        function GetUserProfileDetails()
        {
            //var requestParams = { OldPassword: '', NewPassword: '', DeviceDetails: "", DeviceType: "", IPAddess: "" };
            var getUrl = "/API/User/GetMyProfile";
            $.ajax({
                type: "POST",
                url: getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                //data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    try {
                        debugger
                        var DataSet = $.parseJSON(response);
                        console.log(response);
                        if (DataSet.StatusCode == "1") {
                            //alert(DataSet.StatusDescription);                            
                            BindFields(DataSet.Data);
                        }
                        else {
                            alert(DataSet.StatusDescription);
                            ClearFields();
                        }
                    }
                    catch (e) {
                        alert(response);
                        alert(e.message);
                    }
                },
                failure: function (response) {
                    alert(response.data);
                }
            });
        }
        function ClearFields()
        {
            $('#txtFirstName').val('');
            $('#txtLastName').val('');
            $('#txtPosition').val('');
            $('#txtEmail').val('');
            $("#cbEmailNotifications").prop('checked', false);
            $("#cbPushNotifications").prop('checked', false);
        }
        function BindFields(Data)
        {
            $('#txtFirstName').val(Data.FirstName);
            $('#txtLastName').val(Data.LastName);
            $('#txtPosition').val(Data.Position);
            $('#txtEmail').val(Data.EmailID);
            $("#cbEmailNotifications").prop('checked', Data.EmailNotification);
            $("#cbPushNotifications").prop('checked', Data.PushNotification);

            $("#ddlGroup").append("<option value='" + Data.Role + "'selected >" + Data.Role + "</option>");
            $("#ddlRole").append("<option value='" + Data.Role + "'selected >" + Data.Role + "</option>");

            if (Data.ProfilePicFile != undefined && Data.ProfilePicFile != '')
            {
                document.getElementById('imgUserPic')
                .setAttribute(
                    'src', 'data:image/png;base64,' + Data.ProfilePicFile
                );
            }

            if (Role != undefined && (Role == "superadmin" || Role == "companyadmin") && Data.CompanyProfilePicFile != undefined && Data.CompanyProfilePicFile != '')
            {
                document.getElementById('imgCompanyLogo')
                .setAttribute(
                    'src', 'data:image/png;base64,' + Data.CompanyProfilePicFile
                );
            }
        }

        var base64UserProfileString = '';
        var base64CompanyProfileString = '';
        function encodeImagetoBase64(element,flag) {
            debugger
            var file = element.files[0];
            var reader = new FileReader();
            reader.onloadend = function () {

                if (flag == 'userpic') {
                    base64UserProfileString = reader.result;
                }
                else if (flag == 'companypic') {                
                    base64CompanyProfileString = reader.result;
                }
                //alert(base64UserProfileString);
                //$("#base64").attr("href", reader.result);
                //rawString = $("#base64").text(reader.result);
                //base64UserProfileString = rawString[0].textContent.split(",").pop();
            }
            reader.readAsDataURL(file);
        }

        function UpdateUserProfileDetails() {
            debugger
            var EmailID = $('#txtEmail').val();
            var Position = $('#txtPosition').val();
            var EmailNotification = $('#cbEmailNotifications').prop('checked');
            var PushNotification = $('#cbPushNotifications').prop('checked');
            var ThemeColor = '#ffffff';

            var requestParams = { EmailID: EmailID, Position: Position, EmailNotification: EmailNotification, PushNotification: PushNotification, UserProfileImageBase64: base64UserProfileString, CompanyProfileImageBase64: base64CompanyProfileString,CompanyThemeColor:ThemeColor };
            var getUrl = "/API/User/UpdateMyProfile";
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
                        //console.log(response);
                        if (DataSet.StatusCode == "1") {
                            //BindFields(DataSet.Data);
                            alert(DataSet.Data.ReturnMessage);
                            if (IsFirstPasswordChanged != undefined && IsFirstPasswordChanged.toLowerCase() == 'true') {
                                window.location.href = "ChangePassword.aspx";
                            }
                            else {
                                location.reload();
                            }
                        }
                        else {
                            //alert(DataSet.StatusDescription);
                            ClearFields();
                        }
                    }
                    catch (e) {
                        alert(response);
                        alert(e.message);
                    }
                },
                failure: function (response) {
                    alert(response.data);
                }
            });
        }


        function setImgSrc(ctrl, img) {
            debugger
            $('#' + img).attr('src', URL.createObjectURL(ctrl.files[0]));
        }

        

    </script>
</asp:Content>


