<%@ Page Title="365" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Life.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Asset/customer/default.js"></script>
    <style>
        .contents-datials .embed {
            width: 100%;
            height: 35rem;
        }
    </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div ng-controller="DefaultController">
        <%--Start Topics--%>
        <div class="row topics" id="dvTopicContainer" ng-if="ActiveContainer =='Topic'">
            <div class="col-md-12">
                <h1 class="text-center font-weight-bold" id="dvUserName" runat="server"></h1>
            </div>
            <div class="col-md-10 mt-5 offset-md-1">
                <div class="row">
                    <div class="col-md-12 mb-1">
                        <h5 class="section-title">MY TOPICS</h5>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4 mb-3" ng-repeat="topic in Topics">
                                <a href="#" ng-click="GetModulesByTopic(topic.TopicId)">
                                    <div class="card border-0 shadow mb-3">
                                        <div class="card-body">
                                            <h5 class="card-title">{{topic.Title}}</h5>
                                            <p class="card-text">{{topic.Description}}</p>
                                            <p ng-if="topic.IsCompleted == '1'" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-if="topic.IsCompleted != '1'" class="text-right anchor">{{topic.CompletedModules + '/' + topic.TotalModules}}</p>
                                        </div>
                                    </div>
                                </a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--End Topics--%>

        <%--Start Modules--%>
        <div id="dvModuleContainer" class="row modules" ng-if="ActiveContainer =='Module'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Topic')"><i class="fas fa-arrow-left"></i>BACK TO TOPICS</a>
                <h1 class="text-center font-weight-bold">{{SelectedTopic.Title}}</h1>
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
            <div class="col-md-10 mt-5 offset-md-1">
                <div class="row">
                    <div class="col-md-12 mb-1">
                        <h5 class="section-title">Unlocked Modules</h5>
                    </div>
                    <div class="col-md-12">
                        <div class="row">
                            <div class="col-md-4 mb-3" ng-repeat="module in Module.UnlockedItems">
                                <a href="#" ng-click="GetContentsByModule(module.TopicID,module.ModuleID)">
                                    <div class="card border-0 shadow mb-3">
                                        <div class="card-body">
                                            <h5 class="card-title">{{module.Title}}</h5>
                                            <p class="card-text">{{module.Description}}</p>
                                            <p ng-if="module.IsCompleted == 1" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-if="module.IsCompleted != 1" class="text-right anchor">{{module.CompletedContents + '/' + module.TotalContents}}</p>
                                            <%-- TopicID:{{SelectedTopic.TopicId}},ModuleID:{{module.ModuleID}}--%>
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
                            <div class="col-md-4 mb-3" ng-repeat="module in Module.LockedItems">
                                <div class="card border-0 mb-3">
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
                <a class="btn bg-yellow font-weight-bold" href="#"><i class="fas fa-comments"></i>Discussion</a>
                <h1 class="text-center font-weight-bold">{{SelectedModule.Title}}</h1>
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
            <div class="col-md-10 mt-5 offset-md-1">
                <div class="row">
                    <div class="col-md-12 mb-4 overview">
                        <h4 class="font-weight-bold">Overview</h4>
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
                                            <img ng-if="content.ContentType=='PDF'" src="Asset/images/pdf-icon.svg" />
                                            <img ng-if="content.ContentType=='VIDEO'" src="Asset/images/video-icon.svg" />
                                            <img ng-if="content.ContentType=='SURVEY'" src="Asset/images/survey-icon.svg" />
                                            <img ng-if="content.ContentType=='FLASHCARD'" src="Asset/images/flashcard-icon.svg" />
                                            <img ng-if="content.ContentType=='FINALQUIZ'" src="Asset/images/quiz-icon.svg" />
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
                                            <img ng-if="content.ContentType=='PDF'" src="Asset/images/pdf-icon.svg" />
                                            <img ng-if="content.ContentType=='VIDEO'" src="Asset/images/video-icon.svg" />
                                            <img ng-if="content.ContentType=='SURVEY'" src="Asset/images/survey-icon.svg" />
                                            <img ng-if="content.ContentType=='FLASHCARD'" src="Asset/images/flashcard-icon.svg" />
                                            <img ng-if="content.ContentType=='FINALQUIZ'" src="Asset/images/quiz-icon.svg" />
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
                <a class="btn bg-yellow font-weight-bold" href="#"><i class="fas fa-comments"></i>Discussion</a>
            </div>

            <div class="col-md-10 mt-5 offset-md-1">
                <div class="row">
                    <div class="col-md-12 mb-3" id="pdfContent" ng-show="SpecialContents.DocType == 'PDF'">
                        <div id="divPDF">
                        </div>
                        <div class="text-center mt-5">
                            <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-click="NextContent(SpecialContents.ContentID)">Continue</a>
                        </div>
                    </div>

                    <div class="col-md-10 offset-md-1 mb-3 text-center" id="videoContent" ng-show="SpecialContents.DocType == 'VIDEO'">
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
                                                <%--<img src="Asset/images/love-icon.svg" />--%>
                                                <span>Love it!</span>
                                            </dt>
                                            <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,2)">
                                                <i class="far fa-grin-beam fa-5x"></i>
                                                <%--<img src="Asset/images/like-icon.svg" />--%>
                                                <span>Like it!</span>
                                            </dt>
                                            <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,3)">
                                                <i class="far fa-meh fa-5x"></i>
                                                <%--<img src="Asset/images/meh-icon.svg" />--%>
                                                <span>Meh</span>
                                            </dt>
                                            <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,4)">
                                                <i class="far fa-frown fa-5x"></i>
                                                <%--<img src="Asset/images/didnt-like-icon.svg" />--%>
                                                <span>Didn't like it!</span>
                                            </dt>
                                        </dl>
                                    </div>
                                </div>
                                <div id="divVideo"></div>
                                <%-- <video controls id="contentVideo" onended="videoRating()">
                                    <source src="Asset/data/bunny.mp4" type="video/mp4">
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
                <h1 class="text-center font-weight-bold">{{SelectedContent.Title}}</h1>
            </div>
            <div class="col-md-10 mt-5 offset-md-1">
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
                                                <input type="checkbox" ng-model="ansOption.IsSelected" id="{{'chkAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" name="ansOption.AnswerText_1" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'chkAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                            <%--       <a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>
                                            Selected:{{Message}}--%>
                                        </div>

                                        <%--Checkbox List with Box--%>
                                        <div ng-if="question.QuestionTypeID == 1 && question.IsBox==true " class="form-group checkbox box">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="checkbox" ng-model="ansOption.IsSelected" id="{{'chkAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" name="ansOption.AnswerText_1" value="{{ansOption.AnswerID}}">
                                                <label for="{{'chkAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                            <%--       <a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>
                                            Selected:{{Message}}--%>
                                        </div>

                                        <%--Dropdown List--%>
                                        <div class="form-group" ng-if="question.QuestionTypeID == 2 ">
                                            <select class="form-control select2" ng-model="question.Value_Text">
                                                <option value="{{ansOption.AnswerID}}" ng-repeat="ansOption in question.AnswerOptions">{{ansOption.AnswerText}}</option>
                                            </select>
                                            <%--  <a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>
                                            Selected:{{question.Value_Text}}--%>
                                        </div>

                                        <%--Radio Button List--%>
                                        <div ng-if="question.QuestionTypeID == 3 && question.IsBox==false  ">
                                            <div class="custom-control custom-radio" ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="radio" id="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}" class="custom-control-input" ng-model="question.Value_Text" name="{{'RadioName_' + question.QuestionID}}" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                            <%-- <a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>--%>
                                            <%-- Selected:{{question.Value_Text}}--%>
                                        </div>

                                        <%--Radio Button List with box--%>
                                        <div ng-if="question.QuestionTypeID == 3  && question.IsBox==true" class="form-group radio box">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="radio" id="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}" class="custom-control-input" ng-model="question.Value_Text" name="{{'RadioName_' + question.QuestionID}}" value="{{ansOption.AnswerID}}">
                                                <label for="{{'rbSVAnsOption_' + question.QuestionID + '_' + ansOption.AnswerID}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <%--File Upload Control--%>
                                        <div ng-if="question.QuestionTypeID == 4">
                                            <input type="file" questionid="{{question.QuestionID}}" onchange="encodeImagetoBase64(this,angular.element(this).scope())" ng-model="question.Value_Text" class="required" id="file">
                                            <div>{{question.Value_Text}}</div>
                                        </div>

                                        <%--Scale Range Selector--%>
                                        <div ng-if="question.QuestionTypeID == 5" class="rating">
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="10" id="{{'rbSurveyRate_' + $index + '_10'}}" /><label for="{{'rbSurveyRate_' + $index + '_10'}}">10</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="9" id="{{'rbSurveyRate_' + $index + '_9'}}" /><label for="{{'rbSurveyRate_' + $index + '_9'}}">9</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="8" id="{{'rbSurveyRate_' + $index + '_8'}}" /><label for="{{'rbSurveyRate_' + $index + '_8'}}">8</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="7" id="{{'rbSurveyRate_' + $index + '_7'}}" /><label for="{{'rbSurveyRate_' + $index + '_7'}}">7</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="6" id="{{'rbSurveyRate_' + $index + '_6'}}" /><label for="{{'rbSurveyRate_' + $index + '_6'}}">6</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="5" id="{{'rbSurveyRate_' + $index + '_5'}}" /><label for="{{'rbSurveyRate_' + $index + '_5'}}">5</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="4" id="{{'rbSurveyRate_' + $index + '_4'}}" /><label for="{{'rbSurveyRate_' + $index + '_4'}}">4</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="3" id="{{'rbSurveyRate_' + $index + '_3'}}" /><label for="{{'rbSurveyRate_' + $index + '_3'}}">3</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="2" id="{{'rbSurveyRate_' + $index + '_2'}}" /><label for="{{'rbSurveyRate_' + $index + '_2'}}">2</label>
                                            <input type="radio" name="{{'CustomRating_' + $index}}" ng-model="question.Value_Text" value="1" id="{{'rbSurveyRate_' + $index + '_1'}}" /><label for="{{'rbSurveyRate_' + $index + '_1'}}">1</label>
                                            <%-- Selected:{{question.Value_Text}}--%>
                                        </div>

                                        <%--Textbox--%>
                                        <div ng-if="question.QuestionTypeID == 6">
                                            <div class="form-group">
                                                <input type="text" class="form-control" id="{{'txt_' + $index}}" placeholder="Type your answer here" ng-model="question.Value_Text">
                                                <%-- Selected: {{question.Value_Text}}--%>
                                            </div>
                                        </div>

                                        <%--Text Area--%>
                                        <div ng-if="question.QuestionTypeID == 7">
                                            <div class="form-group">
                                                <textarea class="form-control" placeholder="Type your answer here" id="{{'txtArea_' + $index}}" ng-model="question.Value_Text"></textarea>
                                                <%--   Selected: {{question.Value_Text}}--%>
                                            </div>
                                        </div>

                                        <%--Date Picker--%>
                                        <div ng-if="question.QuestionTypeID == 8 ">
                                            <div class="form-group">
                                                <%--<input type="text" class="form-control date" id="{{'date_' + $index}}" placeholder="Select Date" />--%>
                                                <input ng-init="question.Value_Text = GetFormattedDate(question.Value_Text)" value="{{question.Value_Text}}" type="date" class="form-control" id="{{'date_' + $index}}" placeholder="Select Date" style="width: 25%;" ng-model="question.Value_Text" />
                                                <%-- Selected: {{question.Value_Text}}--%>
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
                <h1 class="text-center font-weight-bold">{{SpecialContents.Title}}</h1>
                <h6 class="text-center header-sub-title mt-3">Flashcards</h6>
            </div>
            <div class="col-md-10 mt-4 offset-md-1">
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
                                    <img class="card-img-top circle mx-auto" src="Asset/images/profile.png" />
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
                                <%--                                <a href="#" class="btn btn-custom bg-blue font-weight-bold text-white" id="btnBeginQuiz" ng-click="FlashcardNextClicked($index,SpecialContents.TotalFlashcardSlides)">{{($index + 1) == SpecialContents.TotalFlashcardSlides ? 'Begin Flashcard Quiz' :'Next Card'}}</a>--%>
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


        <div ng-if="ActiveContainer =='ContentQuizView'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Content')"><i class="fas fa-arrow-left"></i>{{ContentGoBackText}}</a>
                <h1 class="text-center font-weight-bold">{{SelectedContent.Title}}</h1>
            </div>

            <div class="col-md-10 mt-5 offset-md-1">
                <div class="row" id="finalQuiz">
                    <%--  IsAnswered: {{SpecialContents.IsAnswered}}
PassingScore:{{SpecialContents.PassingScore}}
TotalScore:{{SpecialContents.TotalScore}}
ScoreEarned:{{SpecialContents.ScoreEarned}}
IsPassed:{{SpecialContents.IsPassed}}
                    PercentageEarned:{{SpecialContents.PercentageEarned}}
                    ContentID: {{SpecialContents.ContentID}}--%>
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
                                                <input type="checkbox" ng-model="ansOption.IsSelected" id="{{'chkAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" name="ansOption.AnswerText_1" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'chkAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                                <%-- IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                            </div>
                                            <%-- <a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>
                                          Selected:{{Message}}--%>
                                        </div>

                                        <%--Dropdown List--%>
                                        <div class="form-group" ng-if="question.QuestionTypeID == 2 ">
                                            <select class="form-control select2" ng-model="question.Value_Text">
                                                <option value="{{ansOption.AnswerID}}" ng-repeat="ansOption in question.AnswerOptions">{{ansOption.AnswerText}} 
                                                    <%--IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                                </option>
                                            </select>
                                            <%--  <a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>
                                            Selected:{{question.Value_Text}}--%>
                                        </div>

                                        <%--Radio Button List--%>
                                        <div ng-if="question.QuestionTypeID == 3 ">
                                            <div class="custom-control custom-radio" ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="radio" id="{{'rbSVAnsOption_' + question.QuestionID + $index}}" class="custom-control-input" ng-model="question.Value_Text" name="ansOption.AnswerText_3" value="{{ansOption.AnswerID}}">
                                                <label class="custom-control-label" for="{{'rbSVAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                                <%-- IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                            </div>
                                            <%-- <a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>--%>
                                            <%-- Selected:{{question.Value_Text}}--%>
                                        </div>

                                        <%--Radio Button List with box--%>
                                        <div ng-if="question.QuestionTypeID == 9 " class="box">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="checkbox" id="{{'rbAnsOption_' + question.QuestionID + $index}}" ng-model="question.Value_Text" name="ansOption.AnswerText_9" value="{{ansOption.AnswerID}}">
                                                <label for="{{'rbAnsOption_' + question.QuestionID + $index}}">{{ansOption.AnswerText}}</label>
                                                <%--    IsCorrect {{ansOption.IsCorrect}} , CorrectScore{{ansOption.CorrectScore}} ,InCorrectScore{{ansOption.InCorrectScore}}--%>
                                            </div>
                                            <%--<a href="#" ng-click="GetSelectedValues(question.AnswerOptions)">Check</a>
                                            Selected:{{question.Value_Text}}--%>
                                        </div>

                                    </div>
                                    <%--      IsAnswered:  {{question.IsAnswered}}
                                   TotalScore: {{question.TotalScore}}
                                    ScoreEarned: {{question.ScoreEarned}}
                                  IsCorrect:  {{question.IsCorrect}}--%>
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
                            ng-click="UpdateContent(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID)">Continue</a>

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
                            <div class="col-md-10 offset-md-1 text-center mt-4">
                                <img src="Asset/images/suprrise-icon.svg" class="img-achievements" />
                                <p class="modal-text mt-4">Surprise!</p>
                                <h3 class="font-weight-bold modal-title">You just unlocked a personal gift!</h3>
                            </div>
                            <div class="col-md-10 offset-md-1 text-center mt-3">
                                <img ng-if="UnlockGiftData.DocType == 'VIDEO'" src="Asset/images/next-video-icon.svg" class="img-achievements" />
                                <img ng-if="UnlockGiftData.DocType == 'PDF'" src="Asset/images/next-pdf-icon.svg" class="img-achievements" />
                                <img ng-if="UnlockGiftData.DocType == 'FLASHCARD'" src="Asset/images/next-flashcard-icon.svg" class="img-achievements" />
                                <h5 class="modal-title mt-2"><b>{{UnlockGiftData.Title}}:</b> {{UnlockGiftData.Description}}</h5>
                            </div>
                            <div class="col-md-10 offset-md-1 text-center mt-5 mb-3">
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
    </div>

    <script>
        $(document).ready(function () {
            $('select.select2').select2({
                placeholder: "Select a option",
                allowClear: true
            });
            //$('.date').datepicker({ uiLibrary: 'bootstrap4', format: 'yyyy-dd-mm' });
            bsCustomFileInput.init();
        });

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

        function GetFormattedDate(date) {
            var todayTime = date;
            var month = format(todayTime.getMonth() + 1);
            var day = format(todayTime.getDate());
            var year = format(todayTime.getFullYear());
            return day + "/" + month + "/" + year;
        }

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

