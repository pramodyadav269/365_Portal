<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="dashboard.aspx.cs" Inherits="_365_Portal.Admin.dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server"> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row">
        <div class="col-12 achievements">
            <div class="card top shadow">
                <div class="row">
                    <div class="col-sm-12 col-md-6">
                        <div class="card-body">
                            <h5 class="card-title font-weight-bold" style="font-weight:bold" runat="server" id="dvUserName">Welcome back, John!!</h5>
                            <p class="card-text">Welcome to 365!</p>
                        </div>
                    </div>
                    <div class="col-sm-12 col-md-6">
                            <ul class="list-group list-group-horizontal" id="dvAchievement">
                                <li class="list-group-item" onclick="openModal(1)">
                                    <span class="ach-title">Professor</span>
                                    <div class="progress" data-value='80'>
                                        <span class="progress-left">
                                            <span class="progress-bar bc-green"></span>
                                        </span>
                                        <span class="progress-right">
                                            <span class="progress-bar bc-green"></span>
                                        </span>
                                        <div class="progress-value w-100 h-100 rounded-circle d-flex align-items-center justify-content-center">
                                            <div class="ach-icon bg-green">
                                                <img src="../includes/Asset/images/college-graduation.png" />
                                            </div>
                                        </div>
                                    </div>
                                    <span class="ach-percentage">80%</span>
                                </li>

                                <li class="list-group-item" onclick="openModal(2)">
                                    <span class="ach-title">Influencer</span>
                                    <div class="progress" data-value='60'>
                                        <span class="progress-left">
                                            <span class="progress-bar bc-red"></span>
                                        </span>
                                        <span class="progress-right">
                                            <span class="progress-bar bc-red"></span>
                                        </span>
                                        <div class="progress-value w-100 h-100 rounded-circle d-flex align-items-center justify-content-center">
                                            <div class="ach-icon bg-red">
                                                <img src="../includes/Asset/images/user.png" />
                                            </div>
                                        </div>
                                    </div>
                                    <span class="ach-percentage">60%</span>
                                </li>
                                <li class="list-group-item" onclick="openModal(3)">
                                    <span class="ach-title">LEGO Leader</span>
                                    <div class="progress" data-value='30'>
                                        <span class="progress-left">
                                            <span class="progress-bar bc-purple"></span>
                                        </span>
                                        <span class="progress-right">
                                            <span class="progress-bar bc-purple"></span>
                                        </span>
                                        <div class="progress-value w-100 h-100 rounded-circle d-flex align-items-center justify-content-center">
                                            <div class="ach-icon bg-purple">
                                                <img src="../includes/Asset/images/combined-shape.png" />
                                            </div>
                                        </div>
                                    </div>
                                    <span class="ach-percentage">30%</span>
                                </li>
                                <li class="list-group-item" onclick="openModal(4)">
                                    <span class="ach-title">G.O.A.T</span>
                                    <div class="progress" data-value='0'>
                                        <span class="progress-left">
                                            <span class="progress-bar bc-orange"></span>
                                        </span>
                                        <span class="progress-right">
                                            <span class="progress-bar bc-orange"></span>
                                        </span>
                                        <div class="progress-value w-100 h-100 rounded-circle d-flex align-items-center justify-content-center">
                                            <div class="ach-icon bg-orange">
                                                <img src="../includes/Asset/images/diamond.png" />
                                            </div>
                                        </div>
                                    </div>
                                    <span class="ach-percentage">0%</span>
                                </li>
                            </ul>
                        </div>
                </div>
            </div>

            <div class="card bottom admin-task" id="dvAdminTasks" runat="server" visible="false">
                <div class="card-body">
                    <div class="row">
                        <div class="col-sm-12 col-md-2">
                            <h6 class="card-title mt-2">Admin Tasks</h6>
                        </div>
                        <div class="col-sm-12 col-md-3 dot-br-2 mr-4">
                            <div class="media" onclick="location.href='settings.aspx';" style="cursor: pointer;">
                                <img src="../includes/Asset/images/settings.png" class="mr-3">
                                <div class="media-body">
                                    <h6 class="m-0">Settings</h6>
                                    Configure company settings
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-3 dot-br-2 mr-4">
                            <div class="media" onclick="location.href='topics.aspx';" style="cursor: pointer;">
                                <img src="../includes/Asset/images/learning-library.png" class="mr-3">
                                <div class="media-body">
                                    <h6 class="m-0">Learning Library</h6>
                                    Browse and manage training
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-3">
                            <div class="media" onclick="location.href='users.aspx';" style="cursor: pointer;">
                                <img src="../includes/Asset/images/add-people.png" class="mr-3">
                                <div class="media-body">
                                    <h6 class="m-0">Manage People</h6>
                                    Add and Remove People
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <a class="task-arrow">
                        <img src="../INCLUDES/Asset/images/up-arrow.png" /></a>
            </div>
        </div>

        <div class="modal fade" id="modalAchievements" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <a class="close-modal" data-dismiss="modal" aria-label="Close">
                        <img src="../Asset/images/close-button.png" class="close" /></a>
                    <div class="modal-body">
                        <div class="row reward">
                            <div class="col-md-10 offset-md-1">
                                <div class="row mt-3">
                                    <div class="col-md-3 mt-3 text-right">
                                        <img id="imgAchievementIcon" src="../Asset/images/engager-icon.svg" class="img-achievements disabled" />
                                    </div>
                                    <div class="col-md-9">
                                        <h3 class="font-weight-bold modal-title" id="dvAchievementTitle">The Engager</h3>
                                        <p class="modal-text" id="dvAchievmentDescription">The Engager is dedicated to the platform. She loves interacting with others and sharing her thoughts about the topics.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-10 offset-md-1">
                                <div class="row mt-3 requirements">
                                    <div class="col-md-12">
                                        <h5 class="section-title">Requirements</h5>
                                    </div>
                                    <div class="col-md-12">
                                        <ul class="list-group" id="dvRequirements">
                                            <%--<li class="list-group-item border-0">Be an active part of the community</li>
                                            <li class="list-group-item border-0">Express your opinion</li>
                                            <li class="list-group-item border-0">React to the videos</li>--%>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-10 offset-md-1">
                                <div class="row mt-4">
                                    <div class="col-md-12">
                                        <h5 class="section-title">Your Reward on completion</h5>
                                    </div>
                                    <div class="col-md-12 text-center mt-3">
                                        <img src="../Asset/images/reward-icon.svg" class="img-achievements" />
                                    </div>
                                    <div class="col-md-12 text-center mt-5 mb-4">
                                        <a class="btn btn-custom bg-blue font-weight-bold text-white" data-dismiss="modal" aria-label="Close">Got It!</a>
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
        $(document).ready(function () {
            $("#dvMenu_Dashboard").addClass("active");
            GetAchievements();
        });

        var achievements = [];

        function GetAchievements() {
            ShowLoader();
            var requestParams = { contact_name: "Scott", company_name: "HP" };
            $.ajax({
                type: "POST",
                url: "../api/Trainning/GetAchievementNGifts",
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
                success: function (response) {
                    achievements = $.parseJSON(response).Achievements;
                    gifts = $.parseJSON(response).Gifts;
                    HideLoader();
                }
            });
        }

        function openModal(achievementId) {
            $.each(achievements, function (i, data) {
                if (data.AchievementID == achievementId) {
                    $("#dvAchievementTitle").html(data.Title);
                    $("#dvAchievmentDescription").html(data.Description);

                    if (data.Title.includes("quiz master"))
                        $("#imgAchievementIcon").attr("src", '../Asset/images/quiz-master-c-icon.svg');
                    if (data.Title.includes("world"))
                        $("#imgAchievementIcon").attr("src", '../Asset/images/perfectionist-c-icon.svg');
                    if (data.Title.includes("wordsmith"))
                        $("#imgAchievementIcon").attr("src", '../Asset/images/wordsmith-c-icon.svg');
                    if (data.Title.includes("engager"))
                        $("#imgAchievementIcon").attr("src", '../Asset/images/engager-icon.svg');
                    if (data.Title.includes("Guru"))
                        $("#imgAchievementIcon").attr("src", '../Asset/images/diploma.png');

                    var reqHtml = "";
                    $.each(data.Requirements, function (indx, req) {
                        reqHtml += '<li class="list-group-item border-0">' + req.Description + '</li>';
                    });
                    $("#dvRequirements").html(reqHtml);

                    return false;
                }
            });

            $('#modalAchievements').modal('show');
        }
    </script>
</asp:Content>
