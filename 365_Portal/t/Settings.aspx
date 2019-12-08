<%@ Page Title="Settings" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="_365_Portal.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row settings">
        <div class="col-md-12 header mb-4">
            <a class="back" href="Default.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h2 class="text-center font-weight-bold">Your Profile</h2>
        </div>

        <div class="col-md-12 user-details">
            <div class="card shadow border-0 border-radius-0">
                <div class="card-body">

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
                            <div class="row input-validation">
                                <div class="col-md-4">
                                    <img class="circle user-photo" id="imgUserPic" src="../Asset/images/profile.png" />
                                    <div class="custom-file">
                                        <input type="file" class="custom-file-input" id="fileChangePic" onchange="encodeImagetoBase64(this,'companypic')">
                                        <label class="custom-file-label mt-2" for="fileChangePic">Change Organization Logo</label>
                                    </div>
                                </div>

                                <div class="col-md-4" id="divCompanyLogo" style="display: none">
                                    <img class="circle user-photo" id="imgCompLogo" src="../Asset/images/CompanyLogo.png" />
                                    <div class="custom-file">
                                        <input type="file" class="custom-file-input" id="fileChangeCompanyLogo" onchange="encodeImagetoBase64(this,'companypic')">
                                        <label class="custom-file-label mt-2" for="fileChangeCompanyLogo">Change Organization Logo</label>
                                    </div>
                                </div>
                                <div class="w-100"></div>
                                <div class="col-md-12 mt-4 mb-3" id="divCompanyTheme" style="display: none;">
                                </div>
                                <div class="w-100"></div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtFirstName">First Name</label>
                                        <input type="text" class="form-control" id="txtFirstName" disabled />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtLastName">Last Name</label>
                                        <input type="text" class="form-control" id="txtLastName" disabled />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtPosition">Position</label>
                                        <input type="text" class="form-control" id="txtPosition" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtEmail">Email</label>
                                        <input type="email" class="form-control" id="txtEmail" />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="txtGroup">Group</label>
                                        <input type="text" class="form-control" id="txtGroup" disabled />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label for="ddlRole">Role</label>
                                        <input type="text" class="form-control" id="txtRole" disabled />
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div id="divChangePassword" class="mt-4" style="display: none;">
                                        <a class="link font-weight-bold" href="ChangePassword.aspx">Change password</a>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12 mt-4 text-center">
                                    <a class="btn btn-custom bg-blue font-weight-bold text-white" onclick="UpdateUserProfileDetails()">Save</a>
                                </div>
                            </div>
                        </div>

                        <div class="tab-pane fade notification" id="pills-notifications" role="tabpanel" aria-labelledby="pills-notifications-tab">
                            <div class="row">
                                <div class="col-md-10 mt-3 form-page form-control-bg-d">

                                    <div class="custom-control custom-checkbox mb-4">
                                        <input type="checkbox" class="custom-control-input" id="cbEmailNotifications">
                                        <label class="custom-control-label" for="cbEmailNotifications" id="dvMsg1">Notifications</label>
                                        <%--  <span >We’ll send you weekly emails on your current progress.</span>--%>
                                    </div>
                                </div>
                                <div class="col-md-10 mt-3 form-page form-control-bg-d">
                                    <div class="custom-control custom-checkbox">
                                        <input type="checkbox" class="custom-control-input" id="cbPushNotifications">
                                        <label class="custom-control-label" for="cbPushNotifications" id="dvMsg2">Notifications</label>
                                        <%-- <span >We’ll send you notifications about updates and your progress.</span>--%>
                                    </div>

                                    <div class=" col-md-12 text-center mt-5">
                                        <a class="btn btn-custom bg-blue font-weight-bold text-white" onclick="UpdateNotification()">Save</a>
                                    </div>
                                </div>
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
        var customFont = '';
        var buttonFontColor = '';

        var IsFirstLogin = '<%=Session["IsFirstLogin"]%>';
        var IsFirstPasswordNotChanged = '<%=Session["IsFirstPasswordNotChanged"]%>';
        if (IsFirstLogin != undefined && IsFirstLogin.toLowerCase() == 'false') {
            $('#divChangePassword').show();
        }

        $(document).ready(function () {
            var notifcation1Msg = "";
            var notifcation2Msg = "";

            //branding color
            //$('.sidenav').css({ "background-color": "#ED5F5F" })
            //$('.sidenav-content-menu').css({ "background-color": "#FC3158" })
            //$('<style>.sidenav-content .sidenav-nav .sidenav-item.side-menu.active .sidenav-link::after {border-color: transparent #FC3158 transparent transparent;} </style>').appendTo('head');


            // Super Admin
            if (Role == "superadmin") {
                notifcation1Msg = "Notify me when a new organization is modified";
                notifcation2Msg = "Notify me when a new super admin is created";
            }
            else if (Role == "companyadmin") {
                // Admin
                notifcation1Msg = "Notify me when a user is modified";
                notifcation2Msg = "Notify me when a new manager is created";
            }
            else if (Role == "subadmin") {
                // Sub-Admin
                notifcation1Msg = "Notify me when a user is modified";
                notifcation2Msg = "Notify me when coursework is created";
            }
            else if (Role == "enduser") {
                // End User
                notifcation1Msg = "Notify me when new coursework is added";
                notifcation2Msg = "Notify me when coursework is due.";
            }
            $("#dvMsg1").text(notifcation1Msg);
            $("#dvMsg2").text(notifcation2Msg);

            if (Role != undefined && (Role == "superadmin" || Role == "companyadmin")) {
                if (Role == "companyadmin")
                    $('#divCompanyLogo').show();

                $('#divCompanyTheme').empty().append('<div class="col-md-12">Choose your theme colors </div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">Branding Color <input type="color" id="ThemeColor" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">Custom Link Color <input type="color" id="ThemeColor2" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor2"></div>');
                //$('#divCompanyTheme').append('<div class="col-md-12 mt-3">Button Font Color <input type="color" id="ThemeColor3" value="#000000" onchange="assignColor(this)">&nbsp;<input type="text" id="txtThemeColor3"></div>');

                $('#divCompanyTheme').append('<div class="col-sm-12 mt-3">Botton Font Color' +
                    '<div class="row mt-2">' +
                    '<div class="col button-color">' +
                    '<a class="font-weight-bold auto" button-data="auto">Preview</a>' +
                    '<span class="label active">Auto</span>' +
                    '</div>' +
                    '<div class="col button-color">' +
                    '<a class="font-weight-bold dark" button-data="dark">Preview</a>' +
                    '<span class="label">Dark</span>' +
                    '</div>' +
                    '<div class="col button-color">' +
                    '<a class="font-weight-bold light" button-data="light">Preview</a>' +
                    '<span class="label">Light</span>' +
                    '</div>' +
                    '</div></div>');


                $('#divCompanyTheme').append('<div class="col-sm-12 mt-3">Custom Font' +
                    '<div class="row mt-2">' +
                    '<div class="col custom-font">' +
                    '<div class="serif custom-font-style" font-data="serif">' +
                    '<span class="titles">Titles</span>' +
                    '<span class="paragraphs">Paragraphs</span>' +
                    '<span class="captions">Captions</span>' +
                    '</div>' +
                    '<span class="label mt-2">Serif</span>' +
                    '</div>' +
                    '<div class="col custom-font">' +
                    '<div class="sans-serif custom-font-style" font-data="sans-serif">' +
                    '<span class="titles">Titles</span>' +
                    '<span class="paragraphs">Paragraphs</span>' +
                    '<span class="captions">Captions</span>' +
                    '</div>' +
                    '<span class="label mt-2 active">Sans Serif (Default)</span>' +
                    '</div>' +
                    '<div class="col custom-font">' +
                    '<div class="mixed-serif custom-font-style" font-data="mixed-serif">' +
                    '<span class="titles">Titles</span>' +
                    '<span class="paragraphs">Paragraphs</span>' +
                    '<span class="captions">Captions</span>' +
                    '</div>' +
                    '<span class="label mt-2">Mixed Serif</span>' +
                    '</div>' +
                    '</div></div>');

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
            }
            if (Role != undefined && Role == "companyadmin") {
                $('#divCompanyTheme').empty().append('<div class="col-md-12">Choose your theme colors </div>');

                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">Branding Color <input type="color" id="ThemeColor" value="' + Data.ThemeColor + '" onchange="assignColor(this)">&nbsp;<input type="text" value="' + Data.ThemeColor + '" id="txtThemeColor" onkeyup="getCustomColor(this)"></div>');
                $('#divCompanyTheme').append('<div class="col-md-12 mt-3">Custom Link Color <input type="color" id="ThemeColor2" value="' + Data.ThemeColor2 + '" onchange="assignColor(this)">&nbsp;<input type="text" value="' + Data.ThemeColor2 + '" id="txtThemeColor2" onkeyup="getCustomColor(this)"></div>');
                //$('#divCompanyTheme').append('<div class="col-md-12 mt-3">Button Font Color <input type="color" id="ThemeColor3" value="' + Data.ThemeColor3 + '" onchange="assignColor(this)">&nbsp;<input type="text" value="' + Data.ThemeColor3 + '" id="txtThemeColor3" onkeyup="getCustomColor(this)"></div>');



                $('#divCompanyTheme').append('<div class="col-sm-12 mt-3">Botton Font Color' +
                    '<div class="row mt-2">' +
                    '<div class="col button-color">' +
                    '<a class="font-weight-bold auto" button-data="auto">Preview</a>' +
                    '<span class="label" id="dvButtonColor_Auto">Auto</span>' +
                    '</div>' +
                    '<div class="col button-color">' +
                    '<a class="font-weight-bold dark" button-data="dark">Preview</a>' +
                    '<span class="label" id="dvButtonColor_dark">Dark</span>' +
                    '</div>' +
                    '<div class="col button-color">' +
                    '<a class="font-weight-bold light" button-data="light">Preview</a>' +
                    '<span class="label" id="dvButtonColor_light">Light</span>' +
                    '</div>' +
                    '</div></div>');


                $('#divCompanyTheme').append('<div class="col-sm-12 mt-3">Custom Font' +
                    '<div class="row mt-2">' +
                    '<div class="col custom-font">' +
                    '<div class="serif custom-font-style" font-data="serif">' +
                    '<span class="titles">Titles</span>' +
                    '<span class="paragraphs">Paragraphs</span>' +
                    '<span class="captions">Captions</span>' +
                    '</div>' +
                    '<span class="label mt-2"  id="dvCustomFont_serif">Serif</span>' +
                    '</div>' +
                    '<div class="col custom-font">' +
                    '<div class="sans-serif custom-font-style" font-data="sans-serif">' +
                    '<span class="titles">Titles</span>' +
                    '<span class="paragraphs">Paragraphs</span>' +
                    '<span class="captions">Captions</span>' +
                    '</div>' +
                    '<span class="label mt-2" id="dvCustomFont_sans-serif">Sans Serif (Default)</span>' +
                    '</div>' +
                    '<div class="col custom-font">' +
                    '<div class="mixed-serif custom-font-style" font-data="mixed-serif">' +
                    '<span class="titles">Titles</span>' +
                    '<span class="paragraphs">Paragraphs</span>' +
                    '<span class="captions">Captions</span>' +
                    '</div>' +
                    '<span class="label mt-2" id="dvCustomFont_mixed-serif">Mixed Serif</span>' +
                    '</div>' +
                    '</div></div>');

                if (Data.ThemeColor3 == null || Data.ThemeColor3 == "") {
                    Data.ThemeColor3 = "Auto";
                }
                if (Data.ThemeColor4 == null || Data.ThemeColor4 == "") {
                    Data.ThemeColor4 = "sans-serif";
                }

                buttonFontColor = Data.ThemeColor3;
                customFont = Data.ThemeColor4;

                $('.custom-font-style').click(function () {
                    $('.custom-font .label').removeClass('active')
                    $(this).parent().find('.label').addClass('active')
                    //$('body').css({ "font-family": $(this).attr('font-data') });
                    customFont = $(this).attr('font-data');
                });

                $('.button-color a').click(function () {
                    $('.button-color .label').removeClass('active')
                    $(this).parent().find('.label').addClass('active')
                    // $('.btn').removeClass('auto').removeClass('dark').removeClass('light')
                    //$('.btn').addClass($(this).attr('button-data'));
                    buttonFontColor = $(this).attr('button-data');
                });

                $("[id=dvCustomFont_" + Data.ThemeColor4 + "]").addClass('active');
                $("[id=dvButtonColor_" + Data.ThemeColor3 + "]").addClass('active');

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
                ThemeColor3 = buttonFontColor;
                ThemeColor4 = customFont;
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
        }

    </script>
</asp:Content>


