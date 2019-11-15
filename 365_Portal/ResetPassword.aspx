<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="_365_Portal.ResetPassword" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Reset Password</title>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no" />
   <%-- CSS --%>
    <link href="Asset/css/bootstrap.min.css" rel="stylesheet" />
    <link href="Asset/css/all.css" rel="stylesheet" />
    <link href="Asset/css/gijgo.min.css" rel="stylesheet" />
    <link href="Asset/css/select2.min.css" rel="stylesheet" />
    <link href="Asset/css/dataTables.bootstrap4.min.css" rel="stylesheet" />
    <link href="Asset/css/site.css" rel="stylesheet" />

    <%-- JS --%>
    <script src="Asset/js/jquery.min.js"></script>
    <script src="Asset/js/popper.min.js"></script>
    <script src="Asset/js/angular.min.js"></script>
    <script src="Asset/js/bootstrap.min.js"></script>
    <script src="Asset/js/all.js"></script>
    <script src="Asset/js/gijgo.min.js"></script>
    <script src="Asset/js/sweetalert2.js"></script>
    <script src="Asset/js/bs-custom-file-input.min.js"></script>
    <script src="Asset/js/select2.min.js"></script>
    <script src="Asset/js/jquery.dataTables.min.js"></script>
    <script src="Asset/js/dataTables.bootstrap4.min.js"></script>
    <script src="Asset/js/jquery.tablednd.js"></script>
    <script src="Asset/js/site.js"></script>

</head>
<body>
    <form id="form1" runat="server" class="">
        <div class="container">
            <div class="row">
                <div class="col-sm-12 page-card d-none" id="divRegPassword">
                    <%-- <span class="back" onclick="toggle('divReg','divRegPassword');"><i class="fas fa-arrow-left"></i>Back</span>--%>
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
                                    <a class="btn btn-custom bg-yellow font-weight-bold" onclick="ChangePassword()">Change Password</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
    <script>
        var t;
        $(document).ready(function () {
            try {
                t = GetParameterValues('Token');
                verify(t);
            }
            catch (e) {
                Swal.fire({
                    title: "Failure",
                    text: "Please try again",
                    icon: "error"
                });
            }
        });

        function GetParameterValues(param) {
            var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
            for (var i = 0; i < url.length; i++) {
                var urlparam = url[i].split('=');
                if (urlparam[0] == param) {
                    return urlparam[1];
                }
            }
        }

        function verify(token) {
            ShowLoader();
            var requestParams;
            if (token != null && token != '') {
                requestParams = { Token: token };
                var getUrl = "/API/User/TokenVerification";
                $.ajax({
                    type: "POST",
                    url: getUrl,
                    data: JSON.stringify(requestParams),
                    contentType: "application/json",
                    success: function (response) {
                        HideLoader();
                        var length = 0;

                        var DataSet = $.parseJSON(response);
                        if (DataSet != null && DataSet != "") {
                            if (DataSet.StatusCode == "1") {
                                $('#divRegPassword').removeClass('d-none');
                            }
                            else {
                                HideLoader();
                                Swal.fire({
                                    title: "Failure",
                                    text: DataSet.StatusDescription,
                                    icon: "error"
                                });

                            }
                        }
                        else {
                            Swal.fire({
                                title: "Failure",
                                text: "Please try again",
                                icon: "error"
                            });
                        }
                    },
                    failure: function (response) {
                        HideLoader();
                        var DataSet = $.parseJSON(response);
                        Swal.fire({
                            title: "Failure",
                            text: DataSet.StatusDescription,
                            icon: "error"
                        });
                    }
                });
            }
            else {
                HideLoader();
                Swal.fire({
                    title: "Failure",
                    text: "Please try again",
                    icon: "error"
                });
            }

        }

        function ChangePassword() {
            ShowLoader();
            if (inputValidation('.input-validation')) {
                if ((($('#txtRegPassword').val() != '' && $('#txtRegPassword').val() != undefined) && ($('#txtRegPasswordAgain').val() != '' && $('#txtRegPasswordAgain').val() != undefined))
                    && ($('#txtRegPassword').val().toUpperCase().toString() == $('#txtRegPasswordAgain').val().toUpperCase().toString())) {
                    var requestParams = { Password: $('#txtRegPasswordAgain').val(), Token: t, DeviceDetails: "", DeviceType: "" };
                    var getUrl = "/API/User/ResetPassword";
                    $.ajax({
                        type: "POST",
                        url: getUrl,
                        data: JSON.stringify(requestParams),
                        contentType: "application/json",
                        success: function (response) {
                            var length = 0;
                            HideLoader();
                            var DataSet = $.parseJSON(response);
                            if (DataSet != "" && DataSet != null) {
                                if (DataSet.StatusCode == "1") {
                                    Swal.fire({
                                        title: "Success",
                                        text: "Password has been Changed Successfully",
                                        icon: "success"
                                    }).then((value) => {
                                        if (value) {
                                            clearFields('.input-validation');
                                            window.location = 'Login.aspx';
                                        }
                                    });
                                }
                                else {
                                    HideLoader();
                                    Swal.fire({
                                        title: "Failure",
                                        text: DataSet.StatusDescription,
                                        icon: "error"
                                    });

                                }
                            }
                            else {
                                Swal.fire({
                                    title: "Alert",
                                    text: "Fill all fields",
                                    icon: "error",
                                    button: "Ok",
                                });
                            }

                        },
                        failure: function (response) {
                            HideLoader();
                            var DataSet = $.parseJSON(response);
                            if (DataSet != null && DataSet != '') {
                                Swal.fire({
                                    title: "Failure",
                                    text: DataSet.StatusDescription,
                                    icon: "error"
                                });
                            }
                            else {
                                Swal.fire({
                                    title: "Alert",
                                    text: "Fill all fields",
                                    icon: "error",
                                    button: "Ok",
                                });
                            }
                        }
                    });
                }
                else {
                    HideLoader();
                    Swal.fire({
                        title: "Alert",
                        text: "Fill all fields",
                        icon: "error",
                        button: "Ok",
                    });
                }
            }
            else {
                HideLoader();
                Swal.fire({
                    title: "Alert",
                    text: "Fill all fields",
                    icon: "error",
                    button: "Ok",
                });
            }
        }
    </script>
</body>
</html>
