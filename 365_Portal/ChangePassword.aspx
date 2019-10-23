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
            if (SectionValidation('divChangePassword') == true) {

                var Confimed_Pass = $('#txtNewPasswordAgain').val();
                var formdata = new FormData();
                formdata.append('Password', Confimed_Pass);
                formdata.append('DeviceDetails',"");
                formdata.append('DeviceType', "");
                formdata.append('IPAddess', "");
                getController(formdata, "/API/User/ChangePassword", "");
            }

        }

        function getController(formdata, getUrl, flag) {
            //var accessToken = '<%=Session["access_token"]%>';
                $.ajax({
                    type: "POST",
                    url: getUrl,
                    //headers: { "Authorization": "Bearer " + accessToken },
                    data: formdata,
                    contentType: false,
                    processData: false,
                    //async: false,
                    beforeSend: function () {
                    },

                    success: function (response) {
                        var length = 0;

                        var DataSet = $.parseJSON(response);

                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                    /*
                    ,
                    failure: AjaxUDFailure,
                    error: AjaxUDError,
                    complete: function () {
                        isAction = false;
                    }
                    */
                });
            }


            //Validate Section
            function SectionValidation(divId) {
                //if ($(el).val() < 5) {
                //    if (!(regEx.test($(el).val()))) {
                //        $(this).next('span.invalid, span.valid').remove();
                //        var errorspan = $('<span></span>').addClass('valid').text('Password should contain at least 1 Alphabet, 1 Number and 1 Special Character.').css({ position: 'absolute', color: 'green', top: '2.5rem', left: "0.75rem", fontSize: "0.8rem" });
                //        $(this).after(errorspan);
                //    }
                //}
                //var regEx = /^(?=.*[A-Za-z])(?=.*\d)(?=.*[$@$!%*#?&])[A-Za-z\d$@$!%*#?&]{6,}/;
                $('#' + divId).find('.form-group').not(".disabled").each(function (i, el) {
                    //
                    $(el).find('select, .input-field> input[type=password]').each(function (i, el) {
                        //
                        //ALL TEXTBOX REQUIRED
                        if ($(el).attr('type') == "text" && $(el).hasClass('required')) {
                            //
                            if ($(el).val() == "") {
                                $(this).next('span.invalid, span.valid').remove();
                                var errorspan = $('<span></span>').addClass('invalid').text('Required').css({ position: 'absolute', color: 'red', top: '2.5rem', left: "0.75rem", fontSize: "0.8rem" });
                                $(this).after(errorspan);
                            }
                            else {
                                $(this).next('span.invalid, span.valid').remove();
                                var errorspan = $('<span></span>').addClass('valid').text('').css({ position: 'absolute', color: 'green', top: '2.5rem', left: "0.75rem", fontSize: "0.8rem" });
                                $(this).after(errorspan);
                            }
                        }

                    })
                    if ($(this).find('span.invalid').length > 0) {
                        return false;
                    }
                })
                if ($('#' + divId).find('span.invalid').length > 0) {
                    return false;
                }
                else {
                    return true;
                }

            }
    </script>
</asp:Content>

