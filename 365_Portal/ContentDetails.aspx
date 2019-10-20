<%@ Page Title="Content Details" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="ContentDetails.aspx.cs" Inherits="_365_Portal.ContentDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row contents-datials">
        <div class="col-md-12 header">
            <a class="back" href="Contents.aspx"><i class="fas fa-arrow-left"></i>Back to Contents</a>
            <a class="btn bg-yellow font-weight-bold" href="#"><i class="fas fa-comments"></i>Discussion</a>
            <%--<h1 class="text-center font-weight-bold">Employee Motivation</h1>--%>
        </div>
        <div class="col-md-10 mt-5 offset-md-1">
            <div class="row">
                <div class="col-md-12 mb-3 d-none" id="pdfContent">
                    <embed src="Asset/data/test.pdf" />
                    <div class="text-center mt-5">
                        <a class="btn bg-blue font-weight-bold text-white" onclick="toggleSection('video')">Continue</a>
                    </div>
                </div>
                <div class="col-md-10 offset-md-1 mb-3 d-none text-center" id="videoContent">
                    <div class="row">
                        <div class="col-md-12 video-rating text-white d-none" id="videoRating">
                            <h2 class="font-weight-bold">How did you like the video?</h2>
                            <dl class="row text-center">
                                <dt class="col" onclick="nextContent();">
                                    <i class="far fa-grin-hearts fa-5x"></i>
                                    <span>Love it!</span>
                                </dt>
                                <dt class="col" onclick="nextContent();">
                                    <i class="far fa-grin-beam fa-5x"></i>
                                    <span>Like it!</span>
                                </dt>
                                <dt class="col" onclick="nextContent();">
                                    <i class="far fa-meh fa-5x"></i>
                                    <span>Meh</span>
                                </dt>
                                <dt class="col" onclick="nextContent();">
                                    <i class="far fa-frown fa-5x"></i>
                                    <span>Didn't like it!</span>
                                </dt>
                            </dl>
                        </div>
                        <div class="col-md-12 video-control text-white" id="videoControl" onclick="videoPlayPause(1)">
                            <i class="fas fa-play fa-5x"></i>
                        </div>
                        <div class="col-md-12">
                            <video controls id="contentVideo" onended="videoRating()" onpause="videoPlayPause(2)" onseeking="videoPlayPause(1)" onseeked="videoPlayPause(1)">
                                <source src="Asset/data/bunny.mp4" type="video/mp4">
                            </video>
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

    <script>

        var video = document.getElementById("contentVideo");

        $(document).ready(function () {
            toggleSection(QueryStringValue('key'));
        });

        function toggleSection(key) {
            if (key === 'pdf') {
                $('#pdfContent').removeClass('d-none')
                $('#videoContent').addClass('d-none')
            } else if (key === 'video') {
                $('#pdfContent').addClass('d-none')
                $('#videoContent').removeClass('d-none')
            }
        }

        function videoPlayPause(action) {
            if (action == 1) {
                video.play();
                $('#videoControl').addClass('d-none')
            } else if (action == 2) {
                $('#videoControl').removeClass('d-none')
            }
        }

        function videoRating() {
            $('#videoControl').addClass('d-none')
            $('#videoRating').removeClass('d-none')
        }

        function nextContent() {
            alert('thank your for rating!')
        }
    </script>
</asp:Content>
