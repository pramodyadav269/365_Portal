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
        <div class="row topics" id="dvTopicContainer" ng-show="ActiveContainer =='Topic'">
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
                                            <p ng-show="topic.IsCompleted == '1'" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-show="topic.IsCompleted != '1'" class="text-right anchor">{{topic.CompletedModules + '/' + topic.TotalModules}}</p>
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
        <div id="dvModuleContainer" class="row modules" ng-show="ActiveContainer =='Module'">
            <div class="col-md-12">
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
                                            <p ng-show="module.IsCompleted == 1" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-show="module.IsCompleted != 1" class="text-right anchor">{{module.CompletedContents + '/' + module.TotalContents}}</p>
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
                            In the employee motivation module, will guide you through a number of 
            techniques that you can use to keep yourself motivated. As a result
            you will hopefully stay much more motivated in your office and have more fun.
                        </p>
                    </div>

                    <div class="col-md-12 mb-3" ng-repeat="content in Content.UnlockedItems">
                        <a href="#" ng-click="ViewContent(content.TopicID,content.ModuleID,content.ContentID,content.Title,content.ContentType)">
                            <div class="card border-0 shadow mb-3">
                                <div class="card-body">
                                    <div class="row align-items-center content-type">
                                        <div class="col-sm-2 col-md-2 col-lg-1">
                                            <img ng-show="content.ContentType=='PDF'" src="Asset/images/pdf-file.png" />
                                            <img ng-show="content.ContentType=='VIDEO'" src="Asset/images/video-file.png" />
                                            <img ng-show="content.ContentType=='SURVEY'" src="Asset/images/survey.png" />
                                            <img ng-show="content.ContentType=='FLASHCARD'" src="Asset/images/flash-card.png" />
                                            <img ng-show="content.ContentType=='FINALQUIZ'" src="Asset/images/exam.png" />
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-10">
                                            <h5 class="card-title">{{content.Title}}</h5>
                                            <p class="card-text">{{content.Description}}</p>
                                        </div>
                                        <div class="col-sm-1 col-md-1 col-lg-1">
                                            <p ng-show="content.IsCompleted =='1'" class="anchor text-right"><i class="fas fa-check c-green"></i></p>
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
                                            <img ng-show="content.ContentType == 'PDF'" src="Asset/images/pdf-file.png" />
                                            <img ng-show="content.ContentType == 'VIDEO'" src="Asset/images/video-file.png" />
                                            <img ng-show="content.ContentType == 'SURVEY'" src="Asset/images/survey.png" />
                                            <img ng-show="content.ContentType == 'FLASHCARD'" src="Asset/images/flash-card.png" />
                                            <img ng-show="content.ContentType == 'FINALQUIZ'" src="Asset/images/exam.png" />
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

        <div class="row contents-datials" ng-show="ActiveContainer =='ContentView'">
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
                            <a class="btn btn-custom bg-blue font-weight-bold text-white" onclick="toggleSection('video')">Continue</a>
                        </div>
                    </div>

                    <div class="col-md-10 offset-md-1 mb-3 text-center" id="videoContent" ng-show="SpecialContents.DocType == 'VIDEO'">
                        <div class="row">
                            <div id="dvVideoRating" style="display: none;" class="col-md-12 video-rating text-white d-none">
                                <h2 class="font-weight-bold">How did you like the video?</h2>
                                <dl class="row text-center">
                                    <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,1)">
                                        <i class="far fa-grin-hearts fa-5x"></i>
                                        <span>Love it!</span>
                                    </dt>
                                    <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,2)">
                                        <i class="far fa-grin-beam fa-5x"></i>
                                        <span>Like it!</span>
                                    </dt>
                                    <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,3)">
                                        <i class="far fa-meh fa-5x"></i>
                                        <span>Meh</span>
                                    </dt>
                                    <dt class="col" ng-click="RateVideo(SpecialContents.TopicID,SpecialContents.ModuleID,SpecialContents.ContentID,4)">
                                        <i class="far fa-frown fa-5x"></i>
                                        <span>Didn't like it!</span>
                                    </dt>
                                </dl>
                            </div>
                            <div class="col-md-12 video-control text-white" id="videoControl" onclick="VideoPlayPause(1)">
                                <i class="fas fa-play fa-5x"></i>
                            </div>
                            <div class="col-md-12">
                                <%-- <video controls id="contentVideo" onended="videoRating()">
                                    <source src="Asset/data/bunny.mp4" type="video/mp4">
                                </video>--%>
                                <div id="divVideo">
                                </div>
                            </div>
                            <div class="col-md-12 mt-4 overview text-left">
                                <h5 class="font-weight-bold text-uppercase">{{SelectedContent.Title}}</h5>
                                <p>
                                    {{SelectedContent.Description}}
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="row survey" ng-show="ActiveContainer =='ContentSurveyView'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Content')"><i class="fas fa-arrow-left"></i>{{ContentGoBackText}}</a>
                <h1 class="text-center font-weight-bold">Survey - Employee Health in General</h1>
            </div>
            <div class="col-md-10 mt-5 offset-md-1">
                <div class="row" id="surveyQuestion">
                    <div class="col-md-12 mb-3" ng-repeat="question in SpecialContents.Questions" my-post-repeat-directive>
                        <div class="card border-0 shadow mb-3">
                            <div class="card-body question">
                                <div class="row align-items-center content-type">
                                    <div class="col-md-2">
                                        <h1 class="card-title display-4 font-weight-bold">{{$index + 1}}.</h1>
                                    </div>
                                    <div class="col-md-10 question-content">
                                        <h5 class="card-title">{{question.Title}}</h5>

                                        <div ng-show="question.QuestionTypeID == 1 ">
                                            <div class="custom-control custom-checkbox" ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="checkbox" id="{{'chkAnsOption_' + $index}}" class="custom-control-input" name="ansOption.AnswerText" value="ansOption.AnswerID">
                                                <label class="custom-control-label" for="{{'chkAnsOption_' + $index}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <%-- <div class="form-group" data-select2-id="5">
                                            <select class="form-control select2 select2-hidden-accessible" multiple="" id="selectMultiple" data-select2-id="selectMultiple" tabindex="-1" aria-hidden="true">
                                                <option data-select2-id="13"></option>
                                                <option value="1" data-select2-id="14">1</option>
                                                <option value="2" data-select2-id="15">2</option>
                                                <option value="3" data-select2-id="16">3</option>
                                            </select>
                                            <span class="select2 select2-container select2-container--default select2-container--below" dir="ltr" data-select2-id="3" style="width: 728.325px;"><span class="selection"><span class="select2-selection select2-selection--multiple" role="combobox" aria-haspopup="true" aria-expanded="false" tabindex="-1" aria-disabled="false">
                                                <ul class="select2-selection__rendered">
                                                    <span class="select2-selection__clear" title="Remove all items" data-select2-id="20">×</span><li class="select2-search select2-search--inline">
                                                        <input class="select2-search__field" type="search" tabindex="0" autocomplete="off" autocorrect="off" autocapitalize="none" spellcheck="false" role="searchbox" aria-autocomplete="list" placeholder="Select a option" style="width: 718.325px;"></li>
                                                </ul>
                                            </span></span><span class="dropdown-wrapper" aria-hidden="true"></span></span>
                                        </div>--%>

                                        <div class="form-group" data-select2-id="5" ng-show="question.QuestionTypeID == 2 " data-select2-id="selectMultiple" tabindex="-1" aria-hidden="true">
                                            <select class="form-control select2 select2-hidden-accessible" multiple="">
                                                <option data-select2-id="14" ng-repeat="ansOption in question.AnswerOptions" value="ansOption.AnswerID">{{ansOption.AnswerText}}</option>
                                            </select>
                                        </div>

                                        <div class="custom-control custom-radio" ng-show="question.QuestionTypeID == 3 ">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="radio" id="{{'rbAnsOption_' + $index}}" name="ansOption.AnswerText" class="custom-control-input" value="ansOption.AnswerID">
                                                <label class="custom-control-label" for="{{'rbAnsOption_' + $index}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <div ng-show="question.QuestionTypeID == 9 " class="box">
                                            <div ng-repeat="ansOption in question.AnswerOptions">
                                                <input type="checkbox" id="{{'rbAnsOption_' + $index}}" name="ansOption.AnswerText" value="ansOption.AnswerID">
                                                <label for="{{'rbAnsOption_' + $index}}">{{ansOption.AnswerText}}</label>
                                            </div>
                                        </div>

                                        <div ng-show="question.QuestionTypeID == 4 " class="custom-file">
                                            <input type="file" class="custom-file-input" id="file"><label class="custom-file-label" for="customFile">Choose file</label>
                                        </div>

                                        <div ng-show="question.QuestionTypeID == 5" class="rating">
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate10" /><label for="rbSurveyRate10">10</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate9" /><label for="rbSurveyRate9">9</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate8" /><label for="rbSurveyRate8">8</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate7" /><label for="rbSurveyRate7">7</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate6" /><label for="rbSurveyRate6">6</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate5" /><label for="rbSurveyRate5">5</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate4" /><label for="rbSurveyRate4">4</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate3" /><label for="rbSurveyRate3">3</label>
                                            <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate2" /><label for="rbSurveyRate2">2</label>
                                            <input type="radio" name="ansOption.AnswerText" value="1" id="rbSurveyRate1" /><label for="rbSurveyRate1">1</label>
                                        </div>

                                        <div ng-show="question.QuestionTypeID == 6 ">
                                            <div class="form-group">
                                                <input type="text" class="form-control" id="text" placeholder="Type your answer here">
                                            </div>
                                        </div>

                                        <div ng-show="question.QuestionTypeID == 7">
                                            <div class="form-group">
                                                <textarea class="form-control" placeholder="Type your answer here" id="textarea"></textarea>
                                            </div>
                                        </div>

                                        <div ng-show="question.QuestionTypeID == 8 ">
                                            <div class="form-group">
                                                <div role="wrapper" class="gj-datepicker gj-datepicker-bootstrap gj-unselectable input-group">
                                                    <input type="text" class="form-control date" id="{{'date_' + $index}}" placeholder="Select Date" data-type="datepicker" data-guid="a55dfd8e-b9b5-0a16-2e0b-1268af1fae1d" data-datepicker="true" role="input"><span class="input-group-append" role="right-icon">
                                                        <button class="btn btn-custom btn-outline-secondary border-left-0" type="button"><i class="gj-icon">event</i></button></span>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="text-center mt-4">
                    <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-click="FlashcardQuestionPrevioustClicked($index,SpecialContents.TotalQuestions)">Previous</a>
                    <a class="btn btn-custom bg-blue font-weight-bold text-white" ng-click="FlashcardQuestionNextClicked($index,SpecialContents.TotalQuestions)">Finish</a>
                </div>
            </div>
        </div>

        <div class="row flashcards" ng-show="ActiveContainer =='ContentFlashcardView'">
            <div class="col-md-12 header">
                <a class="back" href="#" ng-click="GoBack('Content')"><i class="fas fa-arrow-left"></i>{{ContentGoBackText}}</a>
                <h1 class="text-center font-weight-bold">Employee Motivation</h1>
                <h6 class="text-center header-sub-title mt-3">Flashcards</h6>
            </div>
            <div class="col-md-10 mt-4 offset-md-1">
                <div class="row justify-content-center">
                    <div class="col-12 col-sm-12 col-md-6 mb-3 overview" ng-show="ActiveSubContainer =='BeginFlashcard'">
                        You have completed all the videos/pdfs in this module.
            <h1>UP NEXT:</h1>
                        <div>FLASHCARD ICON</div>
                        <div>FLASHCARD TITLE</div>
                        <button onclick="return false;">SKIP</button>
                        <button onclick="return false;" ng-click="ShowFlashcardIntro()">BEGIN FLASHCARD</button>
                    </div>
                    <div class="col-12 col-sm-12 col-md-6 mb-3 overview" ng-show="ActiveSubContainer =='FlashcardIntro'">
                        <h2>Flashcard intro</h2>
                        <h5 class="font-weight-bold">{{SpecialContents.FlashcardTitle}}</h5>
                        <ul>
                             <li ng-repeat="highlight in SpecialContents.FlachardsIntro">{{highlight.Comments}}</li>
                        </ul>
                        <div class="w-100 mt-5">
                            <a ng-show="SpecialContents.SkipFlashcards == '1'" href="#" class="link font-weight-bold float-left">Skip Flashcards</a>
                            <a href="#" class="btn btn-custom bg-blue font-weight-bold text-white float-right" ng-click="ShowFlashcardSlides()">Let's Go</a>
                        </div>
                    </div>
                    <div class="col-12 col-sm-12 col-md-6 mb-3 overview" id="divFlashcard" ng-show="ActiveSubContainer =='FlashcardSlides'">
                        <div ng-repeat="flashcardSlide in SpecialContents.Flachards" ng-show="$index == CurrIndex">
                            <div class="flashcard">
                                <div class="card border-0">
                                    <img class="card-img-top circle mx-auto" src="Asset/images/profile.png" />
                                    <div class="card-body">
                                        <p class="card-text">
                                            {{flashcardSlide.Description}}
                                        </p>
                                        <p class="text-right anchor">{{($index + 1) +'/'+ (SpecialContents.Flachards).length}}</p>
                                    </div>
                                </div>
                            </div>
                            <div class="w-100 mt-5 text-center">
                                <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2" id="btnPrevCard" ng-click="FlashcardPreviousClicked($index,SpecialContents.TotalFlashcardSlides)">{{$index ==0 ? 'Previous' :'Previous Card'}}</a>
                                <a href="#" class="btn btn-custom bg-yellow font-weight-bold" id="btnNextCard" ng-click="FlashcardNextClicked($index,SpecialContents.TotalFlashcardSlides)">{{($index + 1) == SpecialContents.TotalFlashcardSlides ? 'Begin Flashcard Quiz' :'Next Card'}}</a>
                                <%--                                <a href="#" class="btn btn-custom bg-blue font-weight-bold text-white" id="btnBeginQuiz" ng-click="FlashcardNextClicked($index,SpecialContents.TotalFlashcardSlides)">{{($index + 1) == SpecialContents.TotalFlashcardSlides ? 'Begin Flashcard Quiz' :'Next Card'}}</a>--%>
                            </div>
                        </div>
                    </div>
                    <div class="col-12 mb-3 overview" id="divFlashcardQuiz" ng-show="ActiveSubContainer =='FlashcardQuiz'">
                        <div ng-repeat="question in SpecialContents.Questions">
                            <div ng-show="$index == CurrIndex">
                                <div class="row justify-content-center flashcard-question">
                                    <div class="col-12 col-sm-12 col-md-6 mb-3 text-center">
                                        <p>{{question.Title}}</p>
                                    </div>
                                    <div class="w-100"></div>
                                    <div class="col-12 col-sm-12 col-md-4 mb-3" ng-repeat="ansOption in question.AnswerOptions">
                                        <div class="ng-class: 'card border-0 shadow text-center ' + (ansOption.IsCorrect ==true ? 'b-green-2' : 'b-red-2' );">
                                            <div class="card-body">
                                                <h5 class="card-title">{{ansOption.AnswerText}}</h5>
                                                <p class="anchor"></p>
                                                <i ng-show="ansOption.IsCorrect ==false" class="fas fa-times c-red"></i>
                                                <i ng-show="ansOption.IsCorrect ==true" class="fas fa-check c-green"></i>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="w-100 mt-5 text-center">
                                    <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2 d-none" id="btnPrevQuestion" ng-click="FlashcardQuestionPrevioustClicked($index,SpecialContents.TotalQuestions)">{{$index ==0 ? 'Previous' :'Previous Question'}}</a>
                                    <a href="#" class="btn btn-custom bg-yellow font-weight-bold" id="btnNextQuestion" ng-click="FlashcardQuestionNextClicked($index,SpecialContents.TotalQuestions)">{{($index + 1) == SpecialContents.TotalQuestions ? 'Begin Final Quiz' :'Next Question'}}</a>
                                    <a href="Contents.aspx" class="btn btn-custom bg-blue font-weight-bold text-white d-none" ng-click="FlashcardQuestionNextClicked($index,SpecialContents.TotalQuestions)" id="btnContinue">{{($index + 1) == SpecialContents.TotalQuestions ? 'Begin Final Quiz' :'Next Question'}}</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>


        <div ng-show="ActiveContainer =='ContentQuizView'">
            <div ng-click="GoBack('Content')">{{ContentGoBackText}}</div>
            <h2>This is Final Quiz</h2>
            <div ng-repeat="question in SpecialContents.Questions">
                <h1>{{$index}}</h1>
                <div>{{question.Title}}</div>

                <div ng-show="question.QuestionType == 1 ">
                    <div ng-repeat="ansOption in question.AnswerOptions">
                        <input type="checkbox" name="ansOption.AnswerText" value="ansOption.AnswerID">
                        {{ansOption.AnswerText}}<br>
                    </div>
                </div>

                <div ng-show="question.QuestionType == 2 ">
                    <select>
                        <option ng-repeat="ansOption in question.AnswerOptions" value="ansOption.AnswerID">{{ansOption.AnswerText}}</option>
                    </select>
                </div>

                <div ng-show="question.QuestionType == 3 " ng-repeat="ansOption in question.AnswerOptions">
                    <input type="radio" name="ansOption.AnswerText" value="ansOption.AnswerID">
                    {{ansOption.AnswerText}}<br>
                </div>

                <div ng-show="question.QuestionType == 4 ">
                    Select a file:
                    <input type="file" name="myFile"><br>
                </div>

                <div ng-show="question.QuestionType == 5">
                    <input type="radio" name="ansOption.AnswerText" value="1" id="rbQuizRate1" /><label for="rbQuizRate1">1</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate2" /><label for="rbQuizRate2">2</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate3" /><label for="rbQuizRate3">3</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate4" /><label for="rbQuizRate4">4</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate5" /><label for="rbQuizRate5">5</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate6" /><label for="rbQuizRate6">6</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate7" /><label for="rbQuizRate7">7</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate8" /><label for="rbQuizRate8">8</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate9" /><label for="rbQuizRate9">9</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbQuizRate10" /><label for="rbQuizRate10">10</label>
                </div>

                <div ng-show="question.QuestionType == 6 ">
                    <input type="text" name="txta">
                </div>
                <div ng-show="question.QuestionType == 7">
                    <textarea rows="4" cols="50"></textarea>
                </div>

                <div ng-show="question.QuestionType == 8 ">
                    <input type="text" name="Date Time">
                </div>
            </div>
            <button onclick="return false;" ng-click="FlashcardQuestionPrevioustClicked($index,SpecialContents.TotalQuestions)">TAKE THE TEST AGAIN</button>
            <button onclick="return false;" ng-click="FlashcardQuestionNextClicked($index,SpecialContents.TotalQuestions)">CHECK ANSWERS | CONTINUE</button>
        </div>

        <div id="dvFinalQuiz" ng-show="ActiveContainer =='ContentCompleted'">
            You have completed all the videos/pdfs in this module.
            <h1>UP NEXT:</h1>
            <div>QUIZ ICON</div>
            <div>QUIZ TITLE</div>
            <button onclick="return false;">DO THE QUIZ</button>
        </div>

        <div id="dvModuleCompleted" ng-show="ActiveContainer =='ModuleCompleted'">
            Hurra!, You've just completed module:
            <h1>MODULE NAME</h1>
            <div>You gained the following things</div>
            <div>Highlights 1</div>
            <div>Highlights 2</div>
            <button onclick="return false;">Great</button>
        </div>

        <div id="dvGiftReceived" ng-show="ActiveContainer =='GiftReceived'">
            <div>Image</div>
            <div>Surprise</div>
            <div>You just unlocked personal Video or PDF</div>
            <div>Video Player or PDF</div>
            <div>Video/PDF Title</div>
            <button onclick="return false;">CONTINUE</button>
            <div>Note: You can access this gift in your profile page.</div>
        </div>

        <div id="dvAchievementReceived" ng-show="ActiveContainer =='AchievementReceived'">
            <div>Hurray!. You just became guru</div>
            <div>ICON</div>
            <div>Description</div>
            <button onclick="return false;">GREAT</button>
        </div>
    </div>

    <script>
        $(document).ready(function () {

        });

        function VideoFinished(e) {
            $("#dvVideoRating").show();
            $('#dvVideoRating').removeClass('d-none');
            $('#videoControl').addClass('d-none');
        }

        function VideoPlayPause(action) {
            if (action == 1) {
                // video.play();
                $('#vdVideoPlayer')[0].play();
                $('#videoControl').addClass('d-none')
            }
        }

        function VideoPaused(e) {
            //alert("Video Paused");
            $('#videoControl').removeClass('d-none');
            $('#vdVideoPlayer')[0].pause();
        }

        var accessToken = '<%=Session["access_token"]%>';

        //function VideoClicked(cntrl) {
        //    // $("#vdVideoPlayer").trigger( "click" );
        //    cntrl.paused ? cntrl.play() : cntrl.pause();
        //}
    </script>
</asp:Content>

