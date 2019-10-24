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
        function ChangePassword() {
            var Confimed_Pass = $('#txtNewPasswordAgain').val();
            var formdata = {};

            //formdata.append('Password', Confimed_Pass);
            //formdata.append('DeviceDetails', "");
            //formdata.append('DeviceType', "");
            //formdata.append('IPAddess', "");
            
            var getUrl = "/API/User/ChangePassword";
            $.ajax({
                type: "POST",
                url: getUrl,
                //headers: { "Authorization": "Bearer " + accessToken },
                data: formdata,
                contentType: false,
                processData: false,
                beforeSend: function () {
                },
                success: function (response) {

                    try {
                        var DataSet = $.parseJSON($.parseJSON(response));
                        if (DataSet[0].ReturnCode == "1") {
                            alert(DataSet[0].ReturnMessage);
                        }
                        else {
                            alert(DataSet[0].ReturnMessage);
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
            $('#txtCurrentPassword').val('');
            $('#txtNewPassword').val('');
            $('#txtNewPasswordAgain').val('');
        }

    </script>
</asp:Content>

