<%@ Page Title="" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Topics.aspx.cs" Inherits="_365_Portal.Topics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row topics">
        <div class="col-md-12">
            <h1 class="text-center font-weight-bold">Hello, Daniel!</h1>
        </div>
        <div class="col-md-10 mt-5 offset-md-1">
            <div class="row">
                <div class="col-md-12 mb-1">
                    <h5 class="section-title">My Topics</h5>
                </div>
                <div class="col-md-12">
                    <div class="row">
                        <div class="col-md-4 mb-3">
                            <a href="#">
                                <div class="card border-0 shadow mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Employee Conduct</h5>
                                        <p class="card-text">Life as an employee can be tough. Let’s work together to make it easier.</p>
                                        <p class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-4 mb-3">
                            <a href="#">
                                <div class="card border-0 shadow mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Workplace Equity</h5>
                                        <p class="card-text">How to be more accepting and bare with your collaegues.</p>
                                        <p class="text-right anchor">2/8</p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-4 mb-3">
                            <a href="#">
                                <div class="card border-0 shadow mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Staff Efficiency</h5>
                                        <p class="card-text">Increase your productivity while not losing motivation.</p>
                                        <p class="text-right anchor"></p>
                                    </div>
                                </div>
                            </a>
                        </div>
                        <div class="col-md-4 mb-3">
                            <a href="#">
                                <div class="card border-0 shadow mb-3">
                                    <div class="card-body">
                                        <h5 class="card-title">Motivation</h5>
                                        <p class="card-text">Increase your productivity while not losing motivation.</p>
                                        <p class="text-right anchor"></p>
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

