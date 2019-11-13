<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="_365_Portal.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row user-details">
        <div class="col-md-12 header">
            <a class="back" href="Default.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Your Profile</h1>
        </div>
        <div class="col-md-8 mt-5 offset-md-2">
            <div class="row">
                <div class="col-md-3 text-right">
                    <img id="imgProfilePic" runat="server" class="circle user-photo" src="Asset/images/profile.png" />
                </div>
                <div class="col-md-9">
                    <h2 class="font-weight-bold mt-3" id="lblUserName" runat="server"></h2>
                    <a class="btn btn-outline-dark mt-1" href="Settings.aspx">Edit profile</a>
                </div>
            </div>
        </div>
        <div class="col-md-8 mt-5 offset-md-2">
            <div class="row achievements">
                <div class="col-md-12">
                    <h4 class="section-title">Achievements</h4>
                </div>
                <div class="col-md-12 scroll">
                    <dl class="row text-center" id="dvAchievement">
                        <%-- <dt class="col" onclick="openModal();">
                            <img src="Asset/images/quiz-master-c-icon.svg" />
                            <span>Quiz Master</span>
                        </dt>
                        <dt class="col" onclick="openModal();">
                            <img src="Asset/images/perfectionist-c-icon.svg" />
                            <span>Perfectionist</span>
                        </dt>
                        <dt class="col" onclick="openModal();">
                            <img src="Asset/images/wordsmith-c-icon.svg" />
                            <span>Wordsmith</span>
                        </dt>
                        <dt class="col" onclick="openModal();">
                            <img src="Asset/images/engager-icon.svg" class="disabled" />
                            <span>Engager</span>
                        </dt>
                        <dt class="col" onclick="openModal();">
                            <img src="Asset/images/diploma.png" class="disabled" />
                            <span>Guru</span>
                        </dt>--%>
                    </dl>
                </div>
            </div>
        </div>
        <div class="col-md-8 mt-5 offset-md-2">
            <div class="row gifts">
                <div class="col-md-12">
                    <h4 class="section-title">Your Personal Gifts</h4>
                </div>
                <div class="col-md-12 scroll">
                    <dl id="dvGifts" class="row text-center">
                        No Gifts Received
                    </dl>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalAchievements" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <a class="close-modal" data-dismiss="modal" aria-label="Close">
                    <img src="Asset/images/close-button.png" class="close" /></a>
                <div class="modal-body">
                    <div class="row reward">
                        <div class="col-md-10 offset-md-1">
                            <div class="row mt-3">
                                <div class="col-md-3 mt-3 text-right">
                                    <img id="imgAchievementIcon" src="Asset/images/engager-icon.svg" class="img-achievements disabled" />
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
                                    <img src="Asset/images/reward-icon.svg" class="img-achievements" />
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

    <div class="modal fade" id="modalPersonalGift" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-body">
                    <div class="row">
                        <div class="col-md-10 offset-md-1 text-center mt-4">
                            <img src="Asset/images/suprrise-icon.svg" class="img-achievements" />
                            <p class="modal-text mt-4">Surprise!</p>
                            <h3 class="font-weight-bold modal-title">You just unlocked a personal gift!</h3>
                        </div>
                        <div class="col-md-10 offset-md-1 text-center mt-3">
                            <img src="Asset/images/next-flashcard-icon.svg" class="img-achievements" />
                            <h5 class="modal-title mt-2"><b>Flashcard:</b> How to Motivate Yourself in your Daily Life by Jared Green</h5>
                        </div>
                        <div class="col-md-10 offset-md-1 text-center mt-5 mb-3">
                            <a class="btn btn-custom bg-blue font-weight-bold text-white" data-dismiss="modal" aria-label="Close">Continue</a>
                            <div class="w-100"></div>
                            <span class="note"><b>Note:</b> You can access this gift in your Profile page</span>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <script>

        var achievements = [];
        var gifts = [];
        var accessToken = '<%=Session["access_token"]%>';

        $(document).ready(function () {
            GetAchievementNGifts();
        });

        function openModal(achievementId) {
            $.each(achievements, function (i, data) {
                if (data.AchievementID == achievementId) {
                    $("#dvAchievementTitle").html(data.Title);
                    $("#dvAchievmentDescription").html(data.Description);

                    if (data.Title.includes("quiz master"))
                        $("#imgAchievementIcon").attr("src", 'Asset/images/quiz-master-c-icon.svg');
                    if (data.Title.includes("world"))
                        $("#imgAchievementIcon").attr("src", 'Asset/images/perfectionist-c-icon.svg');
                    if (data.Title.includes("wordsmith"))
                        $("#imgAchievementIcon").attr("src", 'Asset/images/wordsmith-c-icon.svg');
                    if (data.Title.includes("engager"))
                        $("#imgAchievementIcon").attr("src", 'Asset/images/engager-icon.svg');
                    if (data.Title.includes("Guru"))
                        $("#imgAchievementIcon").attr("src", 'Asset/images/diploma.png');

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

        function GetAchievementNGifts() {
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

                    // Bind Gifts
                    var giftHtml = "";
                    $.each(gifts, function (i, data) {
                        giftHtml += '<dt class="col-md-3" ContentID=' + data.ContentID + '>';
                        if (data.ContentType == 'VIDEO')
                            giftHtml += '<img src="Asset/images/next-video-icon.svg" />';
                        else if (data.ContentType == 'FLASHCARD')
                            giftHtml += '<img src="Asset/images/next-flashcard-icon.svg" />';
                        else if (data.ContentType == 'PDF')
                            giftHtml += '<img src="Asset/images/next-pdf-icon.svg" />';
                        giftHtml += '<span>' + data.Title + '</span>'
                        giftHtml += '</dt>';
                    });

                    $("#dvGifts").html(giftHtml);

                    var achievementHtml = "";
                    $.each(achievements, function (i, data) {
                        achievementHtml += '<dt class="col" onclick="openModal(' + data.AchievementID + ');" ContentID=' + data.AchievementID + '>';
                        if (data.Title.includes("quiz master"))
                            achievementHtml += '<img src="Asset/images/quiz-master-c-icon.svg" />';
                        if (data.Title.includes("world"))
                            achievementHtml += '<img src="Asset/images/perfectionist-c-icon.svg" />';
                        if (data.Title.includes("wordsmith"))
                            achievementHtml += '<img src="Asset/images/wordsmith-c-icon.svg" />';
                        if (data.Title.includes("engager"))
                            achievementHtml += '<img src="Asset/images/engager-icon.svg" />';
                        if (data.Title.includes("Guru"))
                            achievementHtml += '<img src="Asset/images/diploma.png" />';
                        achievementHtml += '<span>' + data.Title + '</span>'
                        achievementHtml += '</dt>';
                    });

                    $("#dvAchievement").html(achievementHtml);

                    $('#dvGifts dt').click(function () {
                        $('#modalPersonalGift').modal('show');
                    });
                }
            });
        }
    </script>

</asp:Content>
