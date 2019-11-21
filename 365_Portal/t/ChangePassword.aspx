<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="_365_Portal.ChangePassword" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-md-12 header">
            <a class="back" href="Settings.aspx"><i class="fas fa-arrow-left"></i>Back to Profile</a>
            <h1 class="text-center font-weight-bold">Change Password</h1>
        </div>
        <div class="col-md-5 offset-md-4 mt-5" id="divChangePassword">
            <div class="row">
                <div class="col-md-10 form-page form-control-bg-d">
                    <div class="form-group">
                        <label for="txtCurrentPassword">Current Password</label>
                        <input type="password" class="form-control" id="txtCurrentPassword" required="required" placeholder="Current password here" />
                    </div>
                    <div class="form-group">
                        <label for="txtNewPassword">New Password</label>
                        <input type="password" class="form-control" id="txtNewPassword" required="required" placeholder="New password here" />
                    </div>
                    <div class="form-group">
                        <label for="txtNewPasswordAgain">New Password Again</label>
                        <input type="password" class="form-control" id="txtNewPasswordAgain" required="required" placeholder="New password again here" />
                    </div>
                    <div class="text-center mt-5">
                        <a class="btn bg-blue font-weight-bold text-white" onclick="ChangePassword()">Change Password</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {


        });
        var accessToken = '<%=Session["access_token"]%>';
        var UserRole = '<%=Session["RoleName"]%>';
        function ChangePassword() {
            ShowLoader();
            if (inputValidation('.input-validation')) {
                var Old_Password = $('#txtCurrentPassword').val();
                var Confirmed_Password = $('#txtNewPasswordAgain').val();
                var requestParams = { OldPassword: Old_Password, NewPassword: Confirmed_Password, DeviceDetails: "", DeviceType: "", IPAddess: "" };
                var getUrl = "/API/User/ChangePassword";

                $.ajax({
                    type: "POST",
                    url: getUrl,
                    headers: { "Authorization": "Bearer " + accessToken },
                    data: JSON.stringify(requestParams),
                    contentType: "application/json",
                    success: function (response) {
                        try {

                            var DataSet = $.parseJSON(response);
                            if (DataSet != null && DataSet != "") {
                                if (DataSet.StatusCode == "1") {
                                    Swal.fire({
                                        title: "Success",
                                        text: "Password has been Changed Successfully",
                                        icon: "success"
                                    }).then((result) => {
                                        if (result.value) {
                                            var uri;
                                            Swal.fire({
                                                text: "Please choose following option.",
                                                icon: "info",
                                                showCancelButton: true,
                                                confirmButtonColor: '#30D644',
                                                cancelButtonColor: '#DD3333',
                                                confirmButtonText: 'Keep me logged in',
                                                cancelButtonText: 'Log Out',
                                            }).then((result) => {
                                                console.log(result.value);
                                                if (!result.value) {

                                                    Logout();
                                                }
                                                else {
                                                    if (UserRole.toUpperCase() == 'ENDUSER') {
                                                        uri = "default.aspx";
                                                    }
                                                    else {
                                                        uri = "dashboard.aspx";
                                                    }

                                                    window.location.href = uri;
                                                }
                                            });
                                        }
                                    });
                                    ClearFields();
                                }
                                else {
                                    Swal.fire({
                                        title: "Failure",
                                        text: DataSet.StatusDescription,
                                        type: "error"
                                    });
                                    // ClearFields();
                                }
                            }
                            else {
                                HideLoader();
                                Swal.fire({
                                    title: "Alert",
                                    text: "Please Try Again",
                                    icon: "error"
                                });
                            }
                        }
                        catch (e) {
                            HideLoader();
                            Swal.fire({
                                title: "Alert",
                                text: "Please Try Again",
                                icon: "error"
                            });
                        }
                    },
                    complete: function () {
                        HideLoader();
                        
                    },
                    failure: function (response) {
                        HideLoader();
                        Swal.fire({
                            title: "Alert",
                            text: "Please Try Again",
                            icon: "error"
                        });

                    }
                });
            }
            else {
                Swal.fire({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error",
                });
            }
        }

        function ClearFields() {
            $('#txtCurrentPassword').val('');
            $('#txtNewPassword').val('');
            $('#txtNewPasswordAgain').val('');
        }
        function Logout() {
            //var formdata = new FormData();
            var _getUrl = "/api/User/UserLogout";
            $.ajax({
                type: "POST",
                url: _getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                data: "",
                contentType: "application/json",
                success: function (response) {
                    try {
                        var DataSet = $.parseJSON(response);
                        if (DataSet != '' && DataSet != null) {
                            if (DataSet.Data.ReturnCode == "1") {
                                var uri = "Login.aspx";
                                window.location.replace(uri);
                            }
                            else {
                                Swal.fire({
                                    title: "Alert",
                                    text: "Unable to Logout! Please Try Again",
                                    icon: "error"
                                });
                            }
                        }
                        else {
                            Swal.fire({
                                title: "Alert",
                                text: "Unable to Logout! Please Try Again",
                                icon: "error"
                            });
                        }
                    }
                    catch (ex) {
                        Swal.fire({
                            title: "Alert",
                            text: "Unable to Logout",
                            icon: "error"
                        });
                    }
                },
                failure: function (response) {
                    Swal.fire({
                        title: "Alert",
                        text: "Unable to Logout! Please Try Again",
                        icon: "error"
                    });
                }
            });
        }

    </script>
</asp:Content>

