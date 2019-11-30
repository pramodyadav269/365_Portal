<%@ Page Title="365" Language="C#" MasterPageFile="~/t/admin.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Life.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="../includes/Asset/customer/default.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div ng-app="MasterPage" ng-controller="DefaultController">

        <div class="row">
            <div class="col-12 achievements mb-5">
                <div class="card top shadow">
                    <div class="row">
                        <div class="col-sm-12 col-md-6">
                            <div class="card-body">
                                <h5 class="card-title" runat="server" id="dvUserName">Welcome back, John!!</h5>
                                <p class="card-text">Different techniques to keep yourself motivated. Different techniques to keep yourself motivated.</p>
                            </div>
                        </div>
                        <div class="col-sm-12 col-md-6">
                            <ul class="list-group list-group-horizontal" id="dvAchievement">
                                <li class="list-group-item">
                                    <span class="ach-title">Professor</span>
                                    <div class="progress-bar p-circle" data-percent="60" data-duration="1000" data-color="#a7a7a73b,#2DCD7A"></div>
                                    <div class="ach-icon bg-green">
                                        <img src="../includes/Asset/images/college-graduation.png" />
                                    </div>
                                    <span class="ach-percentage">60%</span>
                                </li>
                                <li class="list-group-item">
                                    <span class="ach-title">Influencer</span>
                                    <div class="progress-bar p-circle" data-percent="40" data-duration="1000" data-color="#a7a7a73b,#ED5F5F"></div>
                                    <div class="ach-icon bg-red">
                                        <img src="../includes/Asset/images/user.png" />
                                    </div>
                                    <span class="ach-percentage">40%</span>
                                </li>
                                <li class="list-group-item">
                                    <span class="ach-title">LEGO Leader</span>
                                    <div class="progress-bar p-circle" data-percent="20" data-duration="1000" data-color="#a7a7a73b,#7467F0"></div>
                                    <div class="ach-icon bg-purple">
                                        <img src="../includes/Asset/images/combined-shape.png" />
                                    </div>
                                    <span class="ach-percentage">20%</span>
                                </li>
                                <li class="list-group-item">
                                    <span class="ach-title">G.O.A.T</span>
                                    <div class="progress-bar p-circle" data-percent="1" data-duration="1000" data-color="#a7a7a73b,#FF7F45"></div>
                                    <div class="ach-icon bg-orange">
                                        <img src="../includes/Asset/images/diamond.png" />
                                    </div>
                                    <span class="ach-percentage">0%</span>
                                </li>
                            </ul>
                        </div>
                    </div>
                </div>

                <div class="card bottom admin-task" style="display: none;">
                    <div class="card-body">
                        <div class="row">
                            <div class="col-sm-12 col-md-2">
                                <h6 class="card-title mt-2">Admin Tasks</h6>
                            </div>
                            <div class="col-sm-12 col-md-3 dot-br-2 mr-4">
                                <div class="media">
                                    <img src="../includes/Asset/images/settings.png" class="mr-3">
                                    <div class="media-body">
                                        <h6 class="m-0">Settings</h6>
                                        Configure company settings
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3 dot-br-2 mr-4">
                                <div class="media">
                                    <img src="../includes/Asset/images/learning-library.png" class="mr-3">
                                    <div class="media-body">
                                        <h6 class="m-0">Learning Library</h6>
                                        Browse and manage training
                                    </div>
                                </div>
                            </div>
                            <div class="col-sm-12 col-md-3">
                                <div class="media">
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
        </div>
        <%--Start Topics--%>
        <div class="row" id="dvTopicContainer" ng-if="ActiveContainer =='Topic'">
            <div class="col-12">
                <h6 class="section-title">My Topics</h6>
            </div>
            <div class="col-md-12">
                <div class="row topics">
                    <div class="col-sm-12 col-md-4" ng-repeat="topic in Topics">
                        <div ng-click="GetModulesByTopic(topic.TopicId)" style="cursor: pointer;">
                            <div class="{{topic.Class}}">
                                <img class="card-mask-top" src="{{topic.TopClass}}" />
                                <img class="card-mask-bottom" src="{{topic.BottomClass}}" />
                                <div class="card-body">
                                    <h5 class="card-title">{{topic.Title}}</h5>
                                    <p class="card-text">{{topic.Description}}</p>
                                    <p ng-if="topic.IsCompleted == '1'" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                    <p ng-if="topic.IsCompleted != '1'" class="text-right anchor">{{topic.CompletedModules + '/' + topic.TotalModules}}</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <%--End Topics--%>

        <%--Start Modules--%>
        <div class="row" id="dvModuleContainer" ng-if="ActiveContainer =='Module'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Topic')"><i class="fas fa-arrow-left"></i>BACK TO TOPICS</a>
                <h2 class="text-center font-weight-bold">{{SelectedTopic.Title}}</h2>
                <h6 class="text-center section-title mt-3 color-0-25">TOPIC</h6>
            </div>
            <div class="col-md-6 mt-4 offset-md-3 completed-progress">
                <div class="row">
                    <div class="col-12">
                        <p class="float-left"><span>{{SelectedTopic.CompletedModules}} of {{SelectedTopic.TotalModules}}</span> modules completed</p>
                        <i class="fas fa-trophy fa-lg float-right"></i>
                    </div>
                    <div class="col-12">
                        <div class="progress border-radius-0">
                            <div class="progress-bar bg-green" role="progressbar" ng-style="{ 'width': (SelectedTopic.CompletedModules / SelectedTopic.TotalModules) * 100 + '%' }"
                                aria-valuenow="25" aria-valuemin="0" aria-valuemax="100">
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12 mt-5 modules">
                <div class="row">
                    <div class="col-md-12 mb-1">
                        <h6 class="section-title">Unlocked Modules</h6>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-sm-12 col-md-4" ng-repeat="module in Module.UnlockedItems">
                                <div ng-click="GetContentsByModule(module.TopicID,module.ModuleID)" style="cursor: pointer;">
                                    <div class="{{module.Class}}">
                                        <img class="card-mask-top" src="{{module.TopClass}}" />
                                        <img class="card-mask-bottom" src="{{module.BottomClass}}" />
                                        <div class="card-body">
                                            <h5 class="card-title">{{module.Title}}</h5>
                                            <p class="card-text">{{module.Description}}</p>
                                            <p ng-if="module.IsCompleted == 1" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-if="module.IsCompleted != 1" class="text-right anchor">{{module.CompletedContents + '/' + module.TotalContents}}</p>
                                            <%-- TopicID:{{SelectedTopic.TopicId}},ModuleID:{{module.ModuleID}}--%>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <%--
                             <div class="col-sm-12 col-md-4" ng-repeat="topic in Topics">
                                <div ng-click="GetModulesByTopic(topic.TopicId)" style="cursor: pointer;">
                                    <div class="{{topic.Class}}">
                                        <img class="card-mask-top" src="{{topic.TopClass}}" />
                                        <img class="card-mask-bottom" src="{{topic.BottomClass}}" />
                                        <div class="card-body">
                                            <h5 class="card-title">{{topic.Title}}</h5>
                                            <p class="card-text">{{topic.Description}}</p>
                                            <p ng-if="topic.IsCompleted == '1'" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-if="topic.IsCompleted != '1'" class="text-right anchor">{{topic.CompletedModules + '/' + topic.TotalModules}}</p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            --%>
                        </div>
                    </div>
                </div>
                <div class="row mt-5 locked">
                    <div class="col-md-12 mb-1">
                        <h6 class="section-title">Locked Modules</h6>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-sm-12 col-md-4" ng-repeat="module in Module.LockedItems">
                                <div class="{{module.Class}}">
                                    <img class="card-mask-top" src="{{module.TopClass}}" />
                                    <img class="card-mask-bottom" src="{{module.BottomClass}}" />
                                    <div class="card-body">
                                        <h5 class="card-title">{{module.Title}}</h5>
                                        <p class="card-text">{{module.Description}}</p>
                                        <p class="text-right anchor"><i class="fas fa-lock"></i></p>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--End Modules--%>

        <%--Start Content--%>
        <div class="row contents" id="dvContentsContainer" ng-show="ActiveContainer =='Content'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Module')"><i class="fas fa-arrow-left"></i>Back to Modules</a>
                <a style="display: none;" class="btn bg-yellow font-weight-bold" href="#"><i class="fas fa-comments"></i>Discussion</a>
                <h2 class="text-center font-weight-bold">{{SelectedModule.Title}}</h2>
                <h6 class="text-center header-sub-title mt-3">Module</h6>
            </div>

            <div class="col-md-6 mt-4 offset-md-3 completed-progress">
                <div class="row">
                    <div class="col-12">
                        <p class="float-left"><span>{{SelectedModule.CompletedContents}} of {{SelectedModule.TotalContents}}</span> contents completed</p>
                        <i class="fas fa-trophy fa-lg float-right"></i>
                    </div>
                    <div class="col-12">
                        <div class="progress border-radius-0">
                            <div class="progress-bar bg-green" role="progressbar" ng-style="{ 'width': (SelectedModule.CompletedContents / SelectedModule.TotalContents) * 100  + '%' }"
                                aria-valuenow="25" aria-valuemin="0" aria-valuemax="100">
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="col-md-12 mt-5">
                <div class="row">
                    <div class="col-md-12 mb-4 overview">
                        <h5 class="font-weight-bold">Overview</h5>
                        <p>
                            {{SelectedModule.Overview}}
                        </p>
                    </div>

                    <div class="col-md-12 mb-3" ng-repeat="content in Content.UnlockedItems">
                        <a href="#" ng-click="ViewContent(content.TopicID,content.ModuleID,content.ContentID,content.Title,content.ContentType)">
                            <div class="card border-0 shadow mb-3">
                                <div class="card-body">
                                    <div class="row align-items-center content-type">
                                        <div class="col-sm-2 col-md-2 col-lg-1">
                                            <img ng-if="content.ContentType=='PDF'" src="../Asset/images/pdf-icon.svg" />
                                            <img ng-if="content.ContentType=='VIDEO'" src="../Asset/images/video-icon.svg" />
                                            <img ng-if="content.ContentType=='SURVEY'" src="../Asset/images/survey-icon.svg" />
                                            <img ng-if="content.ContentType=='FLASHCARD'" src="../Asset/images/flashcard-icon.svg" />
                                            <img ng-if="content.ContentType=='FINALQUIZ'" src="../Asset/images/quiz-icon.svg" />
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-10">
                                            <h5 class="card-title">{{content.Title}}</h5>
                                            <p class="card-text">{{content.Description}}</p>
                                        </div>
                                        <div class="col-sm-1 col-md-1 col-lg-1">
                                            <p ng-if="content.IsCompleted =='1'" class="anchor text-right"><i class="fas fa-check c-green"></i></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <div class="col-md-12 mb-3 locked" ng-repeat="content in Content.LockedItems">
                        <a href="#" ng-click="ViewContent(content.Title,content.Type)">
                            <div class="card border-0 shadow mb-3">
                                <div class="card-body">
                                    <div class="row align-items-center content-type">
                                        <div class="col-sm-2 col-md-2 col-lg-1">
                                            <img ng-if="content.ContentType=='PDF'" src="../Asset/images/pdf-icon.svg" />
                                            <img ng-if="content.ContentType=='VIDEO'" src="../Asset/images/video-icon.svg" />
                                            <img ng-if="content.ContentType=='SURVEY'" src="../Asset/images/survey-icon.svg" />
                                            <img ng-if="content.ContentType=='FLASHCARD'" src="../Asset/images/flashcard-icon.svg" />
                                            <img ng-if="content.ContentType=='FINALQUIZ'" src="../Asset/images/quiz-icon.svg" />
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-10">
                                            <h5 class="card-title">{{content.Title}}</h5>
                                            <p class="card-text">{{content.Description}}</p>
                                        </div>
                                        <div class="col-sm-1 col-md-1 col-lg-1">
                                            <p class="anchor text-right"><i class="fas fa-lock"></i></p>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>

                </div>
            </div>
        </div>
        <%--End Content--%>

        <div class="row contents-datials" ng-if="ActiveContainer =='ContentView'">
            <div class="col-md-12 header">
                <a class="back" ng-click="GoBack('Content')"><i class="fas fa-arrow-left"></i>{{ContentGoBackText}}</a>
                <a style="display: none;" class="btn bg-yellow font-weight-bold" href="#"><i class="fas fa-comments"></i>Discussion</a>
            </div>

            <div class="col-md-8 offset-2 mt-5">
                <div class="row">
                    <div class="col-md-12 mb-3" id="pdfContent" ng-show="SpecialContents.DocType == 'PDF'">
                        <div id="divPDF">
                        </div>
                        <div class="text-center mt-5">
                            <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-click="NextContent(SpecialContents.ContentID)">Continue</a>
                        </div>
                    </div>

                    <div class="col-md-12 mb-3 text-center" id="videoContent" ng-show="SpecialContents.DocType == 'VIDEO'">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="video-control text-white" style="display: none;" id="videoControl" onclick="VideoPlayPause(1)">
                                    <i class="fas fa-play fa-5x"></i>
                                </div>
                                <div id="dvVideoRating" style="display: none;" class="video-rating text-white">
                                    <div class="video-rating-content">
                                        <h2 class="font-weight-bold">How did you like the video?</h2>
                                        <dl class="row text-center">
                                            <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,1)">
                                                <i class="far fa-grin-hearts fa-5x"></i>
                                                <%--<img src="../Asset/images/love-icon.svg" />--%>
                                                <span>Love it!</span>
                                            </dt>
                                            <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,2)">
                                                <i class="far fa-grin-beam fa-5x"></i>
                                                <%--<img src="../Asset/images/like-icon.svg" />--%>
                                                <span>Like it!</span>
                                            </dt>
                                            <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,3)">
                                                <i class="far fa-meh fa-5x"></i>
                                                <%--<img src="../Asset/images/meh-icon.svg" />--%>
                                                <span>Meh</span>
                                            </dt>
                                            <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,4)">
                                                <i class="far fa-frown fa-5x"></i>
                                                <%--<img src="../Asset/images/didnt-like-icon.svg" />--%>
                                                <span>Didn't like it!</span>
                                            </dt>
                                        </dl>
                                    </div>
                                </div>
                                <div id="divVideo"></div>
                                <%-- <video controls id="contentVideo" onended="videoRating()">
                                    <source src="../Asset/data/bunny.mp4" type="video/mp4">
                                </video>--%>
                            </div>
                            <div class="col-md-12 mt-4 overview text-left">
                                <h5 class="font-weight-bold text-uppercase">{{SelectedContent.Title}}</h5>
                                <p>
                                    {{SelectedContent.Description}}
                                </p>
                            </div>
                            <div class="col-md-12 text-center mt-5">
                                <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-click="NextContent(SpecialContents.ContentID)">Continue</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row survey" ng-if="ActiveContainer =='ContentSurveyView'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Content')"><i class="fas fa-arrow-left"></i>{{ContentGoBackText}}</a>
                <h2 class="text-center font-weight-bold">{{SelectedContent.Title}}</h2>
            </div>
            <div class="col-md-12 mt-5">
                <div class="row" id="surveyQuestion">
                    <div class="col-md-12 mb-3" ng-repeat="question in SpecialContents.Questions" my-post-repeat-directive>
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body question">
                                <div class="media mb-4">
                                    <h1 class="card-title display-4 font-weight-bold mr-4">{{$index + 1}}.</h1>
                                    <div class="media-body pr-4">
                                        <h5 class="mt-0 mb-4">{{question.Title}}</h5>

                                        <%--Checkbox List--%>
                                        <div ng-if="question.QuestionTypeID == 1 && question.IsBox==false ">
                                            <div class="custom-control custom-checkbox" ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="checkbox" ng-disabled="SpecialContents.IsAnswered ==1" ng-model="ansOption.IsSelected" id="{{'chkAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" name="ansOption.AnswerText_1" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'chkAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <%--Checkbox List with Box--%>
                                        <div ng-if="question.QuestionTypeID == 1 && question.IsBox==true " class="form-group checkbox box">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="checkbox" ng-disabled="SpecialContents.IsAnswered ==1" ng-model="ansOption.IsSelected" id="{{'chkAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" name="ansOption.AnswerText_1" value="{{ansOption.AnswerID}}">
                                                <label for="{{'chkAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <%--Dropdown List--%>
                                        <div class="form-group" ng-if="question.QuestionTypeID == 2 ">
                                            <select ng-disabled="SpecialContents.IsAnswered ==1" class="form-control select2" ng-model="question.Value_Text">
                                                <option value="{{ansOption.AnswerID}}" ng-repeat="ansOption in question.AnswerOptions">{{ansOption.AnswerText}}</option>
                                            </select>
                                        </div>

                                        <%--Radio Button List--%>
                                        <div ng-if="question.QuestionTypeID == 3 && question.IsBox==false  ">
                                            <div class="custom-control custom-radio" ng-repeat="ansOption in question.AnswerOptions">
                                                <input ng-disabled="SpecialContents.IsAnswered ==1" type="radio" id="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}" class="custom-control-input" ng-model="question.Value_Text" name="{{'RadioName_' + question.QuestionID}}" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <%--Radio Button List with box--%>
                                        <div ng-if="question.QuestionTypeID == 3  && question.IsBox==true" class="form-group radio box">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input ng-disabled="SpecialContents.IsAnswered ==1" type="radio" id="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}" class="custom-control-input" ng-model="question.Value_Text" name="{{'RadioName_' + question.QuestionID}}" value="{{ansOption.AnswerID}}">
                                                <label for="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <%--File Upload Control--%>
                                        <div ng-if="question.QuestionTypeID == 4">
                                            <input ng-disabled="SpecialContents.IsAnswered ==1" type="file" questionid="{{question.QuestionID}}" onchange="encodeImagetoBase64(this,angular.element(this).scope())" ng-model="question.Value_Text" class="required" id="file">
                                            <div>{{question.Value_Text}}</div>
                                        </div>

                                        <%--Scale Range Selector--%>
                                        <div ng-if="question.QuestionTypeID == 5" class="rating">
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="10" id="{{'rbSurveyRate_' + $index + '_10'}}" /><label for="{{'rbSurveyRate_' + $index + '_10'}}">10</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="9" id="{{'rbSurveyRate_' + $index + '_9'}}" /><label for="{{'rbSurveyRate_' + $index + '_9'}}">9</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="8" id="{{'rbSurveyRate_' + $index + '_8'}}" /><label for="{{'rbSurveyRate_' + $index + '_8'}}">8</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="7" id="{{'rbSurveyRate_' + $index + '_7'}}" /><label for="{{'rbSurveyRate_' + $index + '_7'}}">7</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="6" id="{{'rbSurveyRate_' + $index + '_6'}}" /><label for="{{'rbSurveyRate_' + $index + '_6'}}">6</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="5" id="{{'rbSurveyRate_' + $index + '_5'}}" /><label for="{{'rbSurveyRate_' + $index + '_5'}}">5</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="4" id="{{'rbSurveyRate_' + $index + '_4'}}" /><label for="{{'rbSurveyRate_' + $index + '_4'}}">4</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="3" id="{{'rbSurveyRate_' + $index + '_3'}}" /><label for="{{'rbSurveyRate_' + $index + '_3'}}">3</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="2" id="{{'rbSurveyRate_' + $index + '_2'}}" /><label for="{{'rbSurveyRate_' + $index + '_2'}}">2</label>
                                            <input type="radio" ng-disabled="SpecialContents.IsAnswered ==1" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="1" id="{{'rbSurveyRate_' + $index + '_1'}}" /><label for="{{'rbSurveyRate_' + $index + '_1'}}">1</label>
                                        </div>

                                        <%--Textbox--%>
                                        <div ng-if="question.QuestionTypeID == 6">
                                            <div class="form-group">
                                                <input ng-disabled="SpecialContents.IsAnswered ==1" type="text" class="form-control" id="{{'txt_' + $index}}" placeholder="Type your answer here" ng-model="question.Value_Text">
                                            </div>
                                        </div>

                                        <%--Text Area--%>
                                        <div ng-if="question.QuestionTypeID == 7">
                                            <div class="form-group">
                                                <textarea ng-disabled="SpecialContents.IsAnswered ==1" class="form-control" placeholder="Type your answer here" id="{{'txtArea_' + $index}}" ng-model="question.Value_Text"></textarea>
                                            </div>
                                        </div>

                                        <%--Date Picker--%>
                                        <div ng-if="question.QuestionTypeID == 8 ">
                                            <div class="form-group">
                                                <input ng-disabled="SpecialContents.IsAnswered ==1" ng-init="question.Value_Text = GetFormattedDate(question.Value_Text)" value="question.Value_Text" type="date" class="form-control" id="{{'date_' + $index}}" placeholder="Select Date" style="width: 25%;" ng-model="question.Value_Text" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center mt-4">
                    <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-if="SpecialContents.IsAnswered==0" ng-click="SubmitAnswers()">Submit Survey</a>
                    <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-if="SpecialContents.IsAnswered==1" ng-click="ShowFlashcardIntro(SpecialContents.ContentID)">Next</a>
                </div>
            </div>
        </div>

        <div class="row flashcards" ng-if="ActiveContainer =='ContentFlashcardView'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Content')"><i class="fas fa-arrow-left"></i>{{ContentGoBackText}}</a>
                <h2 class="text-center font-weight-bold">{{SpecialContents.Title}}</h2>
                <h6 class="text-center header-sub-title mt-3">Flashcards</h6>
            </div>
            <div class="col-md-10 offset-1 mt-4">
                <div class="row justify-content-center">
                    <%--<div class="col-12 col-sm-12 col-md-6 mb-3 overview" ng-if="ActiveSubContainer =='BeginFlashcard'">
                        You have completed all the videos/pdfs in this module.
            <h1>UP NEXT:</h1>
                        <div>FLASHCARD ICON</div>
                        <div>FLASHCARD TITLE</div>
                        <a ng-if="SpecialContents.SkipFlashcards == '1'" href="#" class="link font-weight-bold float-left">Skip Flashcards</a>
                        <a class="btn btn-custom bg-blue font-weight-bold text-white float-right" ng-click="ShowFlashcardIntro()">BEGIN FLASHCARD</a>
                    </div>--%>
                    <div class="col-12 col-sm-12 col-md-6 mb-3 overview" ng-if="ActiveSubContainer =='FlashcardIntro'">
                        <h2>Flashcard intro</h2>
                        <h5 class="font-weight-bold">{{SpecialContents.FlashcardTitle}}</h5>
                        <ul>
                            <li ng-repeat="highlight in SpecialContents.FlashcardsIntro">{{highlight.Comments}}</li>
                        </ul>
                        <div class="w-100 mt-5">
                            <a ng-if="SpecialContents.SkipFlashcards == '0'" href="#" class="link font-weight-bold float-left"
                                ng-click="SkipFlashcard(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID)">Skip Flashcards</a>
                            <a href="#" class="btn btn-custom bg-blue font-weight-bold text-white float-right" ng-click="ShowFlashcardSlides()">Let's Go</a>
                        </div>
                    </div>
                    <div class="col-12 col-sm-12 col-md-6 mb-3 overview" id="divFlashcard" ng-if="ActiveSubContainer =='FlashcardSlides'">
                        <div ng-repeat="flashcardSlide in SpecialContents.Flashcards" ng-if="$index == CurrIndex">
                            <div class="flashcard">
                                <div class="card border-0">
                                    <img class="card-img-top circle mx-auto" src="../Asset/images/profile.png" />
                                    <div class="card-body">
                                        <p class="card-text">
                                            {{flashcardSlide.Description}}
                                        </p>
                                        <p class="text-right anchor">{{($index + 1) +'/'+ (SpecialContents.Flashcards).length}}</p>
                                    </div>
                                </div>
                            </div>
                            <div class="w-100 mt-5 text-center">
                                <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2" id="btnPrevCard" ng-click="FlashcardPreviousClicked(SpecialContents.ContentID,$index,SpecialContents.TotalFlashcardSlides)">{{$index ==0 ? 'Previous' :'Previous Card'}}</a>
                                <a href="#" class="btn btn-custom bg-yellow font-weight-bold" id="btnNextCard" ng-click="FlashcardNextClicked(SpecialContents.ContentID,$index,SpecialContents.TotalFlashcardSlides)">{{($index + 1) == SpecialContents.TotalFlashcardSlides ? 'Begin Flashcard Quiz' :'Next Card'}}</a>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mb-3 overview" id="divFlashcardQuiz" ng-if="ActiveSubContainer =='FlashcardQuiz'">
                        <div ng-repeat="question in SpecialContents.Questions">
                            <div ng-if="$index == CurrIndex">
                                <div class="row justify-content-center flashcard-question">
                                    <div class="col-12 col-sm-12 col-md-6 mb-3 text-center">
                                        <p>{{question.Title}}</p>
                                    </div>
                                    <div class="w-100"></div>
                                    <div class="col-12 col-sm-12 col-md-4 mb-3" ng-click="question.ShowAnwer=true" ng-repeat="ansOption in question.AnswerOptions">
                                        <div class="ng-class: 'card border-0 shadow text-center ' + (question.ShowAnwer ==true? (ansOption.IsCorrect ==true ? 'b-green-2' : 'b-red-2'):'' );">
                                            <div class="card-body">
                                                <h5 class="card-title">{{ansOption.AnswerText}}</h5>
                                                <p class="anchor"></p>
                                                <i ng-if="question.ShowAnwer ==true && ansOption.IsCorrect ==false" class="fas fa-times c-red"></i>
                                                <i ng-if="question.ShowAnwer ==true && ansOption.IsCorrect ==true" class="fas fa-check c-green"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="w-100 mt-5 text-center">
                                    <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2"
                                        id="btnPrevQuestion" ng-click="FlashcardQuestionPrevioustClicked($index,SpecialContents.TotalQuestions)">{{ $index == 0 ? 'Previous' :'Previous Question'}}</a>
                                    <a href="#" class="btn btn-custom bg-yellow font-weight-bold"
                                        id="btnNextQuestion"
                                        ng-click="FlashcardQuestionNextClicked(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,$index,SpecialContents.TotalQuestions)">{{($index + 1) == SpecialContents.TotalQuestions ? 'Begin Final Quiz' :'Next Question'}}</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div class="row" ng-if="ActiveContainer =='ContentQuizView'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Content')"><i class="fas fa-arrow-left"></i>{{ContentGoBackText}}</a>
                <h2 class="text-center font-weight-bold">{{SelectedContent.Title}}</h2>
            </div>

            <div class="col-md-12 mt-5">
                <div class="row" id="finalQuiz">
                    <div class="col-md-12 mb-3" ng-repeat="question in SpecialContents.Questions" my-post-repeat-directive>
                        <div class="ng-class: 'card border-0 shadow mb-3 ' + (question.IsAnswered == true ? (question.IsCorrect ==true ? 'b-green-2' : 'b-red-2'):'' );">
                            <div class="card-body question">
                                <div class="media mb-4">
                                    <h1 class="card-title display-4 font-weight-bold mr-4">{{$index + 1}}.</h1>
                                    <div class="media-body pr-4">
                                        <h5 class="mt-0 mb-4">{{question.Title}}</h5>

                                        <%--Checkbox List--%>
                                        <div ng-if="question.QuestionTypeID == 1 ">
                                            <div class="custom-control custom-checkbox" ng-repeat="ansOption in question.AnswerOptions">
                                                <input ng-disabled="SpecialContents.IsAnswered ==1 && SpecialContents.IsPassed ==1" type="checkbox" ng-model="ansOption.IsSelected" id="{{'chkAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" name="ansOption.AnswerText_1" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'chkAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                                <%-- IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                            </div>
                                        </div>

                                        <%--Dropdown List--%>
                                        <div class="form-group" ng-if="question.QuestionTypeID == 2 ">
                                            <select class="form-control select2" ng-model="question.Value_Text">
                                                <option ng-disabled="SpecialContents.IsAnswered ==1 && SpecialContents.IsPassed ==1" value="{{ansOption.AnswerID}}" ng-repeat="ansOption in question.AnswerOptions">{{ansOption.AnswerText}} 
                                                    <%--IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                                </option>
                                            </select>
                                        </div>

                                        <%--Radio Button List--%>
                                        <div ng-if="question.QuestionTypeID == 3 ">
                                            <div class="custom-control custom-radio" ng-repeat="ansOption in question.AnswerOptions">
                                                <input ng-disabled="SpecialContents.IsAnswered ==1 && SpecialContents.IsPassed ==1" type="radio" id="{{'rbSVAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" ng-model="question.Value_Text" name="ansOption.AnswerText_3" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'rbSVAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                                <%-- IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                            </div>
                                        </div>

                                        <%--Radio Button List with box--%>
                                        <div ng-if="question.QuestionTypeID == 9 " class="box">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input ng-disabled="SpecialContents.IsAnswered ==1 && SpecialContents.IsPassed ==1" type="checkbox" id="{{'rbAnsOption_' + question.QuestionID + $index}}" ng-model="question.Value_Text" name="ansOption.AnswerText_9" value="{{ansOption.AnswerID}}">
                                                <label for="{{'rbAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                                <%--    IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                            </div>
                                        </div>

                                    </div>
                                    <p class="anchor"></p>
                                    <i ng-if="SpecialContents.IsAnswered ==true && question.IsCorrect ==false" class="fas fa-times c-red"></i>
                                    <i ng-if="SpecialContents.IsAnswered ==true && question.IsCorrect ==true" class="fas fa-check c-green"></i>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="w-100 mt-4 text-center">
                        <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-if="SpecialContents.IsAnswered==true && SpecialContents.IsPassed==false"
                            ng-click="RetakeTest(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,SpecialContents.SurveyID)">TAKE THE TEST AGAIN</a>
                        <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-if="SpecialContents.IsAnswered==false && SpecialContents.IsPassed==false"
                            ng-click="SubmitAnswers()">Check Answers</a>
                        <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-if="SpecialContents.IsPassed==true"
                            ng-click="UpdateContent(SpecialContents.Type,SpecialContents.TopicID,SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID)">Continue</a>

                    </div>
                </div>
            </div>
        </div>

        <div id="dvFinalQuiz" ng-if="ActiveContainer =='ContentCompleted'">
            You have completed all the videos/pdfs in this module.
            <h1>UP NEXT:</h1>
            <div>QUIZ ICON</div>
            <div>QUIZ TITLE</div>
            <button onclick="return false;">DO THE QUIZ</button>
        </div>

        <div id="dvModuleCompleted" ng-if="ActiveContainer =='ModuleCompleted'">
            Hurra!, You've just completed module:
            <h1>MODULE NAME</h1>
            <div>You gained the following things</div>
            <div>Highlights 1</div>
            <div>Highlights 2</div>
            <button onclick="return false;">Great</button>
        </div>

        <div class="modal fade" id="modalPersonalGift" tabindex="-1" role="dialog" aria-labelledby="" aria-hidden="true" data-backdrop="static">
            <div class="modal-dialog modal-dialog-centered">
                <div class="modal-content">
                    <div class="modal-body">
                        <div class="row">
                            <div class="col-md-12 text-center mt-4">
                                <img src="../Asset/images/suprrise-icon.svg" class="img-achievements" />
                                <p class="modal-text mt-4">Surprise!</p>
                                <h3 class="font-weight-bold modal-title">You just unlocked a personal gift!</h3>
                            </div>
                            <div class="col-md-12 text-center mt-3">
                                <img ng-if="UnlockGiftData.DocType == 'VIDEO'" src="../Asset/images/next-video-icon.svg" class="img-achievements" />
                                <img ng-if="UnlockGiftData.DocType == 'PDF'" src="../Asset/images/next-pdf-icon.svg" class="img-achievements" />
                                <img ng-if="UnlockGiftData.DocType == 'FLASHCARD'" src="../Asset/images/next-flashcard-icon.svg" class="img-achievements" />
                                <h5 class="modal-title mt-2"><b>{{UnlockGiftData.Title}}:</b> {{UnlockGiftData.Description}}</h5>
                            </div>
                            <div class="col-md-12 text-center mt-5 mb-3">
                                <a class="btn btn-custom bg-blue font-weight-bold text-white" href="Profile.aspx">Continue</a>
                                <div class="w-100"></div>
                                <span class="note"><b>Note:</b> You can access this gift in your Profile page</span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div id="dvAchievementReceived" ng-if="ActiveContainer =='AchievementReceived'">
            <div>Hurray!. You just became guru</div>
            <div>ICON</div>
            <div>Description</div>
            <button onclick="return false;">GREAT</button>
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
        $(document).ready(function () {
            $("#dvMenu_Dashboard").addClass("active");
            $('select.select2').select2({
                placeholder: "Select a option",
                allowClear: true
            });
            //$('.date').datepicker({ uiLibrary: 'bootstrap4', format: 'yyyy-dd-mm' });
            bsCustomFileInput.init();

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

        var accessToken = '<%=Session["access_token"]%>';


        function ChangeFileName(cntrl) {
            var scope = angular.element(cntrl).scope();
            var selectedQuestion = jQuery.grep(scope.SpecialContents.Questions, function (obj) {
                return obj.QuestionID === parseInt($(cntrl).attr("questionid"));
            });

            selectedQuestion[0].Value_Text = cntrl.files[0].name;
            //selectedQuestion.Value_Text = cntrl.files[0].name;
        }

        //function GetFormattedDate(date) {
        //    var dateParts = date.split("-");
        //    date = format(dateParts[1]) + "-" + format(dateParts[0]) + "-" + format(dateParts[2]);
        //    var todayTime = new Date(date);
        //    var month = format(todayTime.getMonth() + 1);
        //    var day = format(todayTime.getDate());
        //    var year = format(todayTime.getFullYear());
        //    return day + "/" + month + "/" + year;
        //}

        function format(str) {
            return str < 10 ? "0" + str : str;
        }

        function encodeImagetoBase64(element, scope) {
            var uploadFile = true;
            if (!(/\.(gif|jpg|jpeg|tiff|png)$/i).test(element.files[0].name)) {
                uploadFile = false;
            }

            if (uploadFile) {
                var sizeInMB = parseInt(element.files[0].size / 1024 / 1024);
                if (sizeInMB <= 2) {
                    var file = element.files[0];
                    var reader = new FileReader();
                    reader.onloadend = function () {
                        scope.question.FilePath = reader.result;
                        scope.question.Value_Text = element.files[0].name;
                    }
                    reader.readAsDataURL(file);
                }
                else {
                    Swal.fire({
                        title: 'Failure',
                        icon: 'error',
                        html: "Maximum file upload size is 2 MB",
                        showConfirmButton: false,
                        showCloseButton: true
                    });
                }
            }
            else {
                Swal.fire({
                    title: 'Failure',
                    icon: 'error',
                    html: "Only images with (gif|jpg|jpeg|tiff|png) extensions can be uploaded.",
                    showConfirmButton: false,
                    showCloseButton: true
                });
            }
        }

    </script>
</asp:Content>

