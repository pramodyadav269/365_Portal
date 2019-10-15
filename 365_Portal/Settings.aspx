<%@ Page Title="Settings" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="_365_Portal.Settings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
        <div class="row settings">
            <div class="col-md-12 header">
                <a class="back" href="Topics.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
                <h1 class="text-center font-weight-bold">Settings</h1>
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
                                <%--<a class="btn btn-outline-dark mt-1">Change Pic</a>--%>

                                <div class="custom-file">
                                    <input type="file" class="custom-file-input" id="fileChangePic" onchange="setImgSrc(this, 'imgUserPic')">
                                    <label class="custom-file-label" for="customFile">Change Pic</label>
                                </div>
                            </div>
                            <div class="col-md-10 mt-5 form-page form-control-bg-d">
                                <div class="form-group">
                                    <label for="txtFullName">First & Last Name</label>
                                    <input type="text" class="form-control" id="txtFullName" value="Daniel Curtis" />
                                </div>
                                <div class="form-group">
                                    <label for="txtEmail">Email</label>
                                    <input type="email" class="form-control" id="txtEmail" value="daniel.curtis@gmail.com" />
                                </div>
                                <div class="form-group">
                                    <label for="txtPosition">Position</label>
                                    <input type="text" class="form-control" id="txtPosition" value="Sr. Forntend Developer" />
                                </div>
                                <div class="form-group">
                                    <label for="ddlSelect">Example select</label>
                                    <select class="form-control select2" id="ddlSelect">
                                        <option>1</option>
                                        <option>2</option>
                                        <option>3</option>
                                        <option>4</option>
                                        <option>5</option>
                                    </select>
                                </div>
                                <div class="mt-4">
                                    <a class="link font-weight-bold" href="ChangePassword.aspx">Change password</a>
                                </div>
                                <div class="text-center mt-5">
                                    <a class="btn bg-blue font-weight-bold text-white">Save</a>
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
                                    <a class="btn bg-blue font-weight-bold text-white">Save</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    <script>

        function setImgSrc(ctrl, img) {
            $('#' + img).attr('src', URL.createObjectURL(ctrl.files[0]));


        }


    </script>
</asp:Content>


