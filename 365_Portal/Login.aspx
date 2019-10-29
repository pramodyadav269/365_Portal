<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="_365_Portal.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
    <%-- CSS --%>
    <link href="Asset/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Asset/css/all.css" rel="stylesheet" />
    <link href="Asset/css/select2.min.css" rel="stylesheet" />
    <link href="Asset/css/site.css" rel="stylesheet" />

    <%-- JS --%>
    <script src="Asset/js/jquery.min.js"></script>
    <script src="Asset/js/popper.min.js"></script>
    <script src="Asset/js/bootstrap.min.js"></script>
    <script src="Asset/js/bs-custom-file-input.min.js"></script>
    <script src="Asset/js/all.js"></script>
    <script src="Asset/js/select2.min.js"></script>
    <script src="Asset/js/site.js"></script>

</head>
<body>
    <form id="form1" runat="server" class="">
        <div class="container">
            <div class="row">
                <div class="top-logo">
                    <img src="Asset/images/logo.svg" class="logo" />
                </div>

                <div class="col-sm-12 page-card d-none" id="divReg">
                    <div class="card border-0 rounded-0 shadow p-3 mb-5 bg-white">
                        <div class="card-body">
                            <h1 class="card-title mb-3 mt-3 text-center font-weight-bold">Welcome to 360 life!</h1>
                            <h4 class="card-subtitle mb-3 text-center font-weight-bold">Set up your profile</h4>

                            <div class="card-form">
                                <div class="form-group">
                                    <label for="txtEmail">Email</label>
                                    <input type="email" class="form-control" id="txtEmail" placeholder="Your email" />
                                </div>
                                <div class="form-group">
                                    <label for="txtFName">First Name</label>
                                    <input type="text" class="form-control" id="txtFName" placeholder="First name" />
                                </div>
                                <div class="form-group">
                                    <label for="txtLName">Last Name</label>
                                    <input type="text" class="form-control" id="txtLName" placeholder="Last name" />
                                </div>
                                <div class="form-group">
                                    <label for="txtPosition">Position</label>
                                    <input type="text" class="form-control" id="txtPosition" placeholder="Position at the company" />
                                </div>
                                <div class="text-center mt-5">
                                    <a class="btn btn-custom bg-yellow font-weight-bold" onclick="regNext(this)">Next</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 page-card d-none" id="divRegPassword">
                    <span class="back" onclick="toggle('divReg','divRegPassword');"><i class="fas fa-arrow-left"></i>Back</span>
                    <div class="card border-0 rounded-0 shadow p-3 mb-5 bg-white">
                        <div class="card-body">
                            <h1 class="card-title mb-3 mt-3 text-center font-weight-bold">Welcome to 360 life!</h1>
                            <h4 class="card-subtitle mb-3 text-center font-weight-bold">Set up your profile</h4>

                            <div class="card-form">
                                <div class="form-group">
                                    <label for="txtRegPassword">Password</label>
                                    <input type="password" class="form-control" id="txtRegPassword" placeholder="Password" />
                                </div>
                                <div class="form-group">
                                    <label for="txtRegPasswordAgain">Password Again</label>
                                    <input type="password" class="form-control" id="txtRegPasswordAgain" placeholder="Password again" />
                                </div>
                                <div class="text-center mt-5">
                                    <a class="btn btn-custom bg-yellow font-weight-bold">Let’s go!</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 page-card" id="divlogin">
                    <div class="card border-0 rounded-0 shadow p-3 mb-5 bg-white">
                        <div class="card-body">
                            <h1 class="card-title mb-3 mt-3 text-center font-weight-bold">Welcome to 360 life!</h1>
                            <h4 class="card-subtitle mb-3 text-center font-weight-bold">Log in</h4>

                            <div class="card-form">
                                <div class="form-group">
                                    <label for="txtUserEmail">Email</label>
                                    <%--<input type="email" class="form-control" id="txtUserEmail" placeholder="Your email" />--%>
                                    <asp:TextBox ID="txtUserEmail" runat="server" TextMode="Email" class="form-control" placeholder="Your EmailId" />
                                </div>
                                <div class="form-group">
                                    <label for="txtUserPassword">Password</label>
                                    <%--<input type="password" class="form-control" id="txtUserPassword" placeholder="Password"  />--%>
                                    <asp:TextBox ID="txtUserPassword" runat="server" TextMode="Password" class="form-control" placeholder="Password" />
                                </div>
                                <div class="text-center mt-4">
                                    <a class="link font-weight-bold" onclick="toggle('divPasswordRecover','divlogin');">Forgot your password?</a>
                                </div>
                                <div class="text-center mt-4">
                                    <%--<a class="btn btn-custom bg-yellow font-weight-bold" onclick="login(this)">Log In</a>--%>
                                    <asp:Button ID="btnLogin" runat="server" class="btn btn-custom bg-yellow font-weight-bold" Text="Log In" OnClientClick="return login(this);" OnClick="btnLogin_Click" />
                                </div>

                                <asp:Label runat="server" ID="lblError" ForeColor="Red" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-sm-12 page-card d-none" id="divPasswordRecover">
                    <span class="back" onclick="toggle('divlogin','divPasswordRecover');"><i class="fas fa-arrow-left"></i>Back</span>
                    <div class="card border-0 rounded-0 shadow p-3 mb-5 bg-white">
                        <div class="card-body">
                            <h3 class="card-title mb-3 mt-3 text-center font-weight-bold">Recover your password</h3>
                            <p class="card-text mb-3 text-center">Enter your email address and we will send you a link where you can reset your password.</p>
                            <div class="card-form">
                                <div class="form-group">
                                    <label for="txtRecoverEmail">Email</label>
                                    <input type="email" class="form-control" id="txtRecoverEmail" placeholder="Your email" />
                                </div>
                                <div class="text-center mt-5">
                                    <a class="btn btn-custom bg-yellow font-weight-bold" onclick="Recovery()">Recover</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>



            </div>
        </div>
    </form>

    <script>


        function regNext(ctrl) {
            toggle('divRegPassword', 'divReg');
        }


        function login(ctrl) {
            //window.location.href = 'Topics.aspx';
            debugger
            var formdata = new FormData();
            formdata.append('EmailId', $("#txtUserEmail").val());
            formdata.append('UserPwd', $("#txtUserPassword").val());
            getController(formdata, "/API/User/LoginUser", "");
        }

        //var UserRole = '<%=Session["UserRole"]%>';
        //var ajaxRequest = [];
        $(document).ready(function () {
            //getController(formdata, "/API/Payment/GetPaymentRequest", "");
        });

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

        function Recovery() {
            var emailId = $('#txtRecoverEmail').val();
            var requestParams;
            if (emailId != null && emailId != '') {
                requestParams = { EmailId: emailId, MobileNum: '', DeviceDetails: '', DeviceType: '' };
            }
            var getUrl = "/API/User/ForgotPassword";
            $.ajax({
                type: "POST",
                url: getUrl,
                data: JSON.stringify(requestParams),
                contentType: "application/json",
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

    </script>


</body>
</html>
