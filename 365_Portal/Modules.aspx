<%@ Page Title="Modules" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Modules.aspx.cs" Inherits="_365_Portal.Modules" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row modules">
        <div class="col-md-12">
            <a class="back" href="Topics.aspx"><i class="fas fa-arrow-left"></i>Back to Topics</a>
            <h1 class="text-center font-weight-bold">Employee Conduct</h1>
            <h6 class="text-center section-title mt-3 color-0-25">Topic</h6>
        </div>
        <div class="col-md-6 mt-4 offset-md-3 completed-progress">
            <div class="row">
                <div class="col-12">
                    <p class="float-left"><span>1 of 6</span> modules completed</p>
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
                <div class="col-md-12 mb-1">
                    <h5 class="section-title">Unlocked Modules</h5>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <a href="#">
                                <div class="card border-0 shadow mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Introduction</h5>
                                        <p class="card-text">Small introduction of the different modules in this topic.</p>
                                        <p class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-4 mb-3">
                            <a href="#">
                                <div class="card border-0 shadow mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Employee Motivation</h5>
                                        <p class="card-text">Different techniques to keep yourself motivated.</p>
                                        <p class="text-right anchor">2/8</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-4 mb-3">
                            <a href="#">
                                <div class="card border-0 shadow mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Ethical Excellence</h5>
                                        <p class="card-text">Become excellent in ethics at the office.</p>
                                        <p class="text-right anchor color-0-25">0/8</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row mt-5 locked">
                <div class="col-md-12 mb-1">
                    <h5 class="section-title">Locked Modules</h5>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <div class="card border-0 mb-3">
                                <div class="card-body">
                                    <h5 class="card-title">Conduct After Hours</h5>
                                    <p class="card-text">When does an employee’s behaviour outside of working hours become the concern of their employee?</p>
                                    <p class="text-right anchor"><i class="fas fa-lock"></i></p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <div class="card border-0 mb-3">
                                <div class="card-body">
                                    <h5 class="card-title">Risks And Boundaries</h5>
                                    <p class="card-text">How to set up boundaries?</p>
                                    <p class="text-right anchor"><i class="fas fa-lock"></i></p>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4 mb-3">
                            <div class="card border-0 mb-3">
                                <div class="card-body">
                                    <h5 class="card-title">Mutual Respect</h5>
                                    <p class="card-text">Methods which helps you to learn respect each other.</p>
                                    <p class="text-right anchor"><i class="fas fa-lock"></i></p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>


