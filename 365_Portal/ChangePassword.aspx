<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="_365_Portal.ChangePassword" %>

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
                            console.log(response);
                            if (DataSet.StatusCode == "1") {
                                //alert(DataSet.Data[0].ReturnMessage);
                                swal({
                                    title: "Success",
                                    text: "Password has been Changed Successfully",
                                    type: "success",
                                    icon: "success"
                                }).then((value) => {
                                    if (value) {
                                        var uri;
                                        swal({
                                            text: "Please Select any Option for move forward",
                                            icon: "warning",
                                            buttons: ["Keep me logged in", "Log Out"],
                                            dangerMode: true,
                                        }).then((login) => {
                                            if (login) {
                                                Logout();
                                            }
                                            else {
                                                if (UserRole.toUpperCase() == 'ENDUSER')
                                                {
                                                    uri = "default.aspx";
                                                }
                                                else
                                                {
                                                    uri = "./admin/dashboard.aspx";
                                                }
                                               
                                                window.location.href=uri    ;
                                            }
                                        });
                                    }
                                });
                                ClearFields();
                            }
                            else {
                                swal({
                                    title: "Failure",
                                    text: DataSet.StatusDescription,
                                    type: "error"
                                });
                                ClearFields();
                            }
                        }
                        catch (e) {
                            alert(response);
                            alert(e.message);
                        }
                    },
                    complete: function () {
                        HideLoader();
                    },
                    failure: function (response) {
                        HideLoader();
                        alert(response.data);
                    }
                });
            }
            else {
                swal({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error",
                    button: "Ok",
                });
            }
        }

        function ClearFields() {
            $('#txtCurrentPassword').val('');
            $('#txtNewPassword').val('');
            $('#txtNewPasswordAgain').val('');
        }
        function Logout() {
            //var uri = "Login.aspx";
            //window.location.replace(uri);
            var _getUrl = "api/User/UserLogout";
            $.ajax({
                type: "POST",
                url: _getUrl,
                headers: { "Authorization": "Bearer " + accessToken },
                data: formdata,
                contentType: "application/json",
                success: function (response) {
                    try {
                        var DataSet = $.parseJSON(response);
                        console.log(response);
                        if (DataSet.ReturnCode == "0") {
                            var uri = "Login.aspx";
                            window.location.replace(uri);
                        }
                        else {
                            swal({
                                title: "Alert",
                                text: "Unable to Logout",
                                icon: "error",
                                button: "Ok",
                            });
                        }
                    }
                    catch (ex)
                    {
                        swal({
                            title: "Alert",
                            text: "Unable to Logout",
                            icon: "error",
                            button: "Ok",
                        });
                    }
                },
                failure: function (response) {
                    alert(response.data);
                }
            });
        }

    </script>
</asp:Content>

