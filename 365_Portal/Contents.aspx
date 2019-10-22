<%@ Page Title="Contents" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Contents.aspx.cs" Inherits="_365_Portal.Contents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row contents">
        <div class="col-md-12 header">
            <a class="back" href="Modules.aspx"><i class="fas fa-arrow-left"></i>Back to Modules</a>
            <a class="btn bg-yellow font-weight-bold" href="#"><i class="fas fa-comments"></i>Discussion</a>
            <h1 class="text-center font-weight-bold">Employee Motivation</h1>
            <h6 class="text-center header-sub-title mt-3">Module</h6>
        </div>
        <div class="col-md-6 mt-4 mb-3 offset-md-3 completed-progress">
            <div class="row">
                <div class="col-12">
                    <p class="float-left"><span>1 of 7</span> content completed</p>
                    <i class="fas fa-trophy fa-lg float-right"></i>
                </div>
                <div class="col-12">
                    <div class="progress border-radius-0">
                        <div class="progress-bar bg-green" role="progressbar" style="width: 25%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                </div>
            </div>
        </div>
        <div class="col-md-10 mt-5 offset-md-1">
            <div class="row">
                <div class="col-md-12 mb-4 overview">
                    <h4 class="font-weight-bold">Overview</h4>
                    <p>
                        In the Employee motivation module, we will guide you through a number of techniques that you can use 
                            to keep yourself motivated. As a result, you'll hopefully stay much more motivated in your office and will have more fun.
                    </p>
                </div>
                <div class="col-md-12 mb-3">
                    <a href="ContentDetails.aspx?key=pdf">
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body">
                                <div class="row align-items-center content-type">
                                    <div class="col-sm-2 col-md-2 col-lg-1">
                                        <img src="Asset/images/pdf-file.png" />
                                    </div>
                                    <div class="col-sm-9 col-md-9 col-lg-10">
                                        <h5 class="card-title">Goal setting</h5>
                                        <p class="card-text">If we speak about motivation, setting goals is crucial.</p>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <p class="anchor text-right"><i class="fas fa-check c-green"></i></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-12 mb-3">
                    <a href="ContentDetails.aspx?key=video">
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body">
                                <div class="row align-items-center content-type">
                                    <div class="col-sm-2 col-md-2 col-lg-1">
                                        <img src="Asset/images/video-file.png" />
                                    </div>
                                    <div class="col-sm-9 col-md-9 col-lg-10">
                                        <h5 class="card-title">Psychology of motivation part 1</h5>
                                        <p class="card-text">Understanding the psychology behind the motivation can be the key.</p>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <p class="anchor text-right"></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-12 mb-3 locked">
                    <a href="#">
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body">
                                <div class="row align-items-center content-type">
                                    <div class="col-sm-2 col-md-2 col-lg-1">
                                        <img src="Asset/images/pdf-file.png" />
                                    </div>
                                    <div class="col-sm-9 col-md-9 col-lg-10">
                                        <h5 class="card-title">Stay positive, stay motivated</h5>
                                        <p class="card-text">How constantly stay motivated.</p>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <p class="anchor text-right"><i class="fas fa-lock"></i></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-12 mb-3 locked">
                    <a href="#">
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body">
                                <div class="row align-items-center content-type">
                                    <div class="col-sm-2 col-md-2 col-lg-1">
                                        <img src="Asset/images/video-file.png" />
                                    </div>
                                    <div class="col-sm-9 col-md-9 col-lg-10">
                                        <h5 class="card-title">Psychology of motivation part 2</h5>
                                        <p class="card-text">Understanding the psychology behind the motivation can be the key.</p>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <p class="anchor text-right"><i class="fas fa-lock"></i></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-12 mb-3">
                    <a href="Survey.aspx">
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body">
                                <div class="row align-items-center content-type">
                                    <div class="col-sm-2 col-md-2 col-lg-1">
                                        <img src="Asset/images/survey.png" />
                                    </div>
                                    <div class="col-sm-9 col-md-9 col-lg-10">
                                        <h5 class="card-title">Daily habits, that can increase mindfulness</h5>
                                        <p class="card-text">How to increase mindfulness with daily habits?</p>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <p class="anchor text-right"></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-12 mb-3">
                    <a href="Flashcards.aspx">
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body">
                                <div class="row align-items-center content-type">
                                    <div class="col-sm-2 col-md-2 col-lg-1">
                                        <img src="Asset/images/flash-card.png" />
                                    </div>
                                    <div class="col-sm-9 col-md-9 col-lg-10">
                                        <h5 class="card-title">Daily habits, that can increase mindfulness</h5>
                                        <p class="card-text">How to increase mindfulness with daily habits?</p>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <p class="anchor text-right"></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-12 mb-3">
                    <a href="#">
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body">
                                <div class="row align-items-center content-type">
                                    <div class="col-sm-2 col-md-2 col-lg-1">
                                        <img src="Asset/images/exam.png" />
                                    </div>
                                    <div class="col-sm-9 col-md-9 col-lg-10">
                                        <h5 class="card-title">Daily habits, that can increase mindfulness</h5>
                                        <p class="card-text">How to increase mindfulness with daily habits?</p>
                                    </div>
                                    <div class="col-sm-1 col-md-1 col-lg-1">
                                        <p class="anchor text-right"></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </a>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


