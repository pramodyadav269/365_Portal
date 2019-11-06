<%@ Page Title="Profile" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Profile.aspx.cs" Inherits="_365_Portal.Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row user-details">
        <div class="col-md-12 header"> 
            <a class="back" href="Topics.aspx"><i class="fas fa-arrow-left"></i>Back to Dashboard</a>
            <h1 class="text-center font-weight-bold">Your Profile</h1>
        </div>
        <div class="col-md-8 mt-5 offset-md-2">
            <div class="row">
                <div class="col-md-3 text-right">
                    <img class="circle user-photo" src="Asset/images/profile.png" />
                </div>
                <div class="col-md-9">
                    <h2 class="font-weight-bold mt-3">Daniel Curtis</h2>
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
                    <dl class="row text-center">
                        <dt class="col" onclick="openModal();">
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
                        </dt>
                        <%--<div class="w-100"></div>
                        <dt class="col">
                            <img src="Asset/images/diploma.png" />
                            <span>Quiz Master</span>
                        </dt>
                        <dt class="col">
                            <img src="Asset/images/diploma.png" />
                            <span>Perfectionist</span>
                        </dt>
                        <dt class="col">
                            <img src="Asset/images/diploma.png" />
                            <span>Wordsmith</span>
                        </dt>
                        <dt class="col">
                            <img src="Asset/images/diploma.png" class="disabled" />
                            <span>Engager</span>
                        </dt>
                        <dt class="col">
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
                    <dl class="row text-center">
                        <dt class="col-md-3">
                            <img src="Asset/images/next-pdf-icon.svg" />
                            <span>Improve your skills at the company</span>
                        </dt>
                        <dt class="col-md-3">
                            <img src="Asset/images/next-video-icon.svg" />
                            <span>Personal greeting from Jared Green</span>
                        </dt>
                        <dt class="col-md-3">
                            <img src="Asset/images/next-flashcard-icon.svg" />
                            <span>Tips for Effective Goal-Setting</span>
                        </dt>
                    </dl>
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
                                        <img src="Asset/images/engager-icon.svg" class="img-achievements disabled" />
                                    </div>
                                    <div class="col-md-9">
                                        <h3 class="font-weight-bold modal-title">The Engager</h3>
                                        <p class="modal-text">The Engager is dedicated to the platform. She loves interacting with others and sharing her thoughts about the topics.</p>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-10 offset-md-1">
                                <div class="row mt-3 requirements">
                                    <div class="col-md-12">
                                        <h5 class="section-title">Requirements</h5>
                                    </div>
                                    <div class="col-md-12">
                                        <ul class="list-group">
                                            <li class="list-group-item border-0">Be an active part of the community</li>
                                            <li class="list-group-item border-0">Express your opinion</li>
                                            <li class="list-group-item border-0">React to the videos</li>
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
    </div>



    <script>

        function openModal() {
            $('#modalAchievements').modal('show');
        }
    </script>

</asp:Content>
