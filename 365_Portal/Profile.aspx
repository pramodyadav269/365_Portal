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
                        <%--No Gifts Received--%>
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

    <div class="modal fade" id="modalFlashcard" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <a class="close-modal" data-dismiss="modal" aria-label="Close">
                    <img src="Asset/images/close-button.png" class="close" /></a>
                <div class="modal-body contents-datials">
                    <div class="row justify-content-center mt-5 mb-4">
                        <div class="col-12 col-sm-12 col-md-6 mb-3 overview" id="divFlashcard">
                            <div id="dvFlashcard" class="flashcard">
                            </div>
                            <div class="w-100 mt-5 text-center">
                                <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2 d-none" id="btnPrevCard" onclick="previousFlashcard();">Previous card</a>
                                <a href="#" class="btn btn-custom bg-yellow font-weight-bold" id="btnNextCard" onclick="nextFlashcard();">Next Card</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade" id="modalPersonalGiftContent" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="static">
        <div class="modal-dialog modal-lg modal-dialog-centered">
            <div class="modal-content">
                <a class="close-modal" data-dismiss="modal" aria-label="Close">
                    <img src="Asset/images/close-button.png" class="close" /></a>
                <div class="modal-body contents-datials">
                    <div class="row">
                        <div class="col-md-10 offset-md-1 mt-5 mb-4" id="dvPdfViewer" style="display: none;">
                            <div class="row">
                                <div class="col-md-12 mb-3" id="pdfContent">
                                    <div id="divPDF">
                                        <embed src="Asset/data/test.pdf" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-10 offset-md-1 mt-5 mb-4" id="dvVideoViewer" style="display: none;">
                            <div class="row">
                                <div class="video-control text-white" id="videoControl" onclick="VideoPlayPause(1)">
                                    <i class="fas fa-play fa-5x"></i>
                                </div>

                                <div id="dvVideoRating" style="display: none;" class="video-rating text-white">
                                    <div class="video-rating-content">
                                        <h2 class="font-weight-bold">How did you like the video?</h2>
                                        <dl class="row text-center">
                                            <dt class="col" onclick="RateContent(1)">
                                                <i class="far fa-grin-hearts fa-5x"></i>
                                                <span>Love it!</span>
                                            </dt>
                                            <dt class="col" onclick="RateContent(2)">
                                                <i class="far fa-grin-beam fa-5x"></i>
                                                <span>Like it!</span>
                                            </dt>
                                            <dt class="col" onclick="RateContent(3)">
                                                <i class="far fa-meh fa-5x"></i>
                                                <span>Meh</span>
                                            </dt>
                                            <dt class="col" onclick="RateContent(4)">
                                                <i class="far fa-frown fa-5x"></i>
                                                <span>Didn't like it!</span>
                                            </dt>
                                        </dl>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div id="divVideo"></div>
                                    <%--  <video id="vdVideoPlayer" controls id="contentVideo" onended="videoRating()" onpause="videoPlayPause(2)" onseeking="videoPlayPause(1)" onseeked="videoPlayPause(1)">
                                        <source src="Asset/data/bunny.mp4" type="video/mp4">
                                    </video>--%>
                                </div>
                                <div class="col-md-12 mt-4 overview text-left">
                                    <h5 class="font-weight-bold text-uppercase">Goal setting - How to get over obstacles?</h5>
                                    <p>
                                        In this video, we’ll go through the basics of goal setting. Goals are an important aspect 
                            of motivation and can help you a lot in the long-term.
                                    </p>
                                </div>
                            </div>
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

                    // Bind Gifts
                    var giftHtml = "";
                    $.each(gifts, function (i, data) {
                        giftHtml += '<dt onclick="OpenPlayer(this);" class="col-md-3" TopicID=' + data.TopicID + ' ModuleID=' + data.ModuleID + ' ContentID=' + data.ContentID + '>';
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

                    //$('#dvGifts dt').click(function () {
                    //    $('#modalPersonalGiftContent').modal('show');
                    //});

                    HideLoader();
                }
            });
        }

        function OpenPlayer(cntrl) {
            var topicId = $(cntrl).attr("TopicID");
            var moduleId = $(cntrl).attr("ModuleID");
            var contentId = $(cntrl).attr("ContentID");
            GetContentDetails(topicId, moduleId, contentId);
        }

        var selectedGift = {};

        function GetContentDetails(topicId, moduleId, contentId) {
            $('#modalAchievements').modal('hide');
            $('#modalPersonalGiftContent').modal('hide');
            $('#modalFlashcard').modal('hide');
            ShowLoader();
            var requestParams = { TopicID: topicId, ModuleID: moduleId, ContentID: contentId };
            $.ajax({
                method: "POST",
                url: "../api/Trainning/GetContentDetails",
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
            }).then(function success(response) {
                HideLoader();
                var responseData = $.parseJSON(response);
                selectedGift = { TopicID: responseData.TopicID, ModuleID: responseData.ModuleID, ContentID: responseData.ContentID };

                if (responseData.DocType == 'VIDEO') {
                    $("#divVideo").empty().html('<video id="vdVideoPlayer" onclick="VideoClicked(this)" onpause="VideoPaused(this)" class="section-video-main" autobuffer="" controls="" height="100%" width="100%">' +
                        '<source id="dvVideoPlayer" src="' + responseData.FilePath + '" type="video/mp4">' +
                        '</video>');
                    document.getElementById('vdVideoPlayer').addEventListener('ended', VideoFinished, false);
                    $('#videoControl').show();

                    $("#dvVideoViewer").show();
                    $("#dvPdfViewer").hide();
                    $('#modalPersonalGiftContent').modal('show');
                }
                else if (responseData.DocType == 'PDF') {
                    $("#divPDF").empty().html('<embed id="dvPDFViewer" src="' + responseData.FilePath + '" width="760" height="800"/>');
                    $("#dvVideoViewer").hide();
                    $("#dvPdfViewer").show();
                    $('#modalPersonalGiftContent').modal('show');
                }
                else {
                    // Flashcard
                    $("#dvVideoViewer").hide();
                    $("#dvPdfViewer").hide();

                    var totalLength = responseData.Flashcards.length;
                    var flashcard = "";
                    $.each(responseData.Flashcards, function (i, data) {
                        
                        if (i == 0)
                            flashcard += '<div class="card">';
                        else
                            flashcard += '<div class="card d-none">';
                        flashcard += '<img class="card-img-top circle mx-auto" src="Asset/images/employee-illustration.svg" />';
                        flashcard += '<div class="card-body">';
                        flashcard += '<p class="card-text">' + data.Description + '</p>';
                        flashcard += '<p class="text-right anchor">' + (i + 1) + '/' + totalLength + '</p>';
                        flashcard += '</div>';
                        flashcard += '</div>';
                    });
                    $("#dvFlashcard").empty().append(flashcard);
                    $('#modalFlashcard').modal('show');
                }

            });
        }

        function VideoFinished(e) {
            $("#dvVideoRating").show();
            $('#dvVideoRating').removeClass('d-none');
            $('#videoControl').addClass('d-none');
            $('#videoControl').hide();
        }

        function VideoPlayPause(action) {
            if (action == 1) {
                // video.play();
                $('#vdVideoPlayer')[0].play();
                $('#videoControl').addClass('d-none');
                $('#videoControl').hide();
            }
        }

        function VideoPaused(e) {
            //alert("Video Paused");
            $('#videoControl').removeClass('d-none');
            $('#videoControl').hide();
            $('#vdVideoPlayer')[0].pause();
        }

        function nextFlashcard() {

            var currentCard = $('#divFlashcard .card').not('.d-none');

            if ($('#divFlashcard .card').length === (currentCard.index() + 2)) {
                currentCard.next().removeClass('d-none')
                currentCard.addClass('d-none')
                currentCard.addClass('d-none')

                $('#btnNextCard').addClass('d-none')
                $('#btnPrevCard').removeClass('d-none')

            }
            else
                if (currentCard.next().length > 0) {
                    $('#btnPrevCard').removeClass('d-none')
                    currentCard.next().removeClass('d-none')
                    currentCard.addClass('d-none')
                } else {
                    $('#btnNextCard').addClass('d-none')
                }
        }

        function previousFlashcard() {

            var currentCard = $('#divFlashcard .card').not('.d-none');

            if (currentCard.index() === 1) {

                currentCard.prev().removeClass('d-none')
                currentCard.addClass('d-none')

                $('#btnNextCard').removeClass('d-none')
                $('#btnPrevCard').addClass('d-none')


            } else if (currentCard.prev().length > 0) {
                $('#btnPrevCard').removeClass('d-none')
                currentCard.prev().removeClass('d-none')
                currentCard.addClass('d-none')

                $('#btnNextCard').removeClass('d-none')

            }
        }

        function RateContent(rating) {
            var requestParams = { TopicID: selectedGift.TopicID, ModuleID: selectedGift.ModuleID, ContentID: selectedGift.ContentID, Rating: rating };
            $.ajax({
                method: "POST",
                url: "../api/Trainning/RateContent",
                headers: { "Authorization": "Bearer " + accessToken },
                data: JSON.stringify(requestParams),
                contentType: "application/json",
            }).then(function success(response) {
                Swal.fire({
                    title: 'Success',
                    icon: 'success',
                    html: "Success",
                    showConfirmButton: false,
                    showCloseButton: true
                });
            });
        }
    </script>

</asp:Content>
