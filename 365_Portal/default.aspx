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
                                <a href="#" ng-click="GetModulesByTopic()">
                                    <div class="card border-0 shadow mb-3">
                                        <div class="card-body">
                                            <h5 class="card-title">{{topic.Title}}</h5>
                                            <p class="card-text">{{topic.Description}}</p>
                                            <p ng-show="topic.IsCompleted == 1" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-show="topic.IsCompleted != 1" class="text-right anchor">{{topic.Progress}}</p>
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
                <h1 class="text-center font-weight-bold">{{Module.TopicTitle}}</h1>
                <h6 class="text-center section-title mt-3 color-0-25">TOPIC</h6>
            </div>
            <div class="col-md-6 mt-4 offset-md-3 completed-progress">
                <div class="row">
                    <div class="col-12">
                        <p class="float-left"><span>{{Module.CompletedModules}} of {{Module.TotalModules}}</span> modules completed</p>
                        <i class="fas fa-trophy fa-lg float-right"></i>
                    </div>
                    <div class="col-12">
                        <div class="progress border-radius-0">
                            <div class="progress-bar bg-green" role="progressbar" ng-style="{ 'width': Module.CompletedPercentage + '%' }"
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
                                <a href="#" ng-click="GetContentsByModule()">
                                    <div class="card border-0 shadow mb-3">
                                        <div class="card-body">
                                            <h5 class="card-title">{{module.Title}}</h5>
                                            <p class="card-text">{{module.Description}}</p>
                                            <p ng-show="module.IsCompleted == 1" class="text-right anchor"><i class="fas fa-check c-green"></i></p>
                                            <p ng-show="module.IsCompleted != 1" class="text-right anchor">{{module.Progress}}</p>
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
                <h1 class="text-center font-weight-bold">{{Content.ModuleName}}</h1>
                <h6 class="text-center header-sub-title mt-3">Module</h6>
            </div>

            <div class="col-md-6 mt-4 offset-md-3 completed-progress">
                <div class="row">
                    <div class="col-12">
                        <p class="float-left"><span>{{Content.CompletedModules}} of {{Content.TotalModules}}</span> modules completed</p>
                        <i class="fas fa-trophy fa-lg float-right"></i>
                    </div>
                    <div class="col-12">
                        <div class="progress border-radius-0">
                            <div class="progress-bar bg-green" role="progressbar" ng-style="{ 'width': Content.CompletedPercentage + '%' }"
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
                        <a href="#" ng-click="ViewContent(content.Title,content.Type)">
                            <div class="card border-0 shadow mb-3">
                                <div class="card-body">
                                    <div class="row align-items-center content-type">
                                        <div class="col-sm-2 col-md-2 col-lg-1">
                                            <img src="Asset/images/pdf-file.png" />
                                        </div>
                                        <div class="col-sm-9 col-md-9 col-lg-10">
                                            <h5 class="card-title">{{content.Title}}</h5>
                                            <p class="card-text">{{content.Description}}</p>
                                        </div>
                                        <div class="col-sm-1 col-md-1 col-lg-1">
                                            <p class="anchor text-right"><i class="fas fa-check c-green"></i></p>
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
                                            <img src="Asset/images/pdf-file.png" />
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
                    <div class="col-md-12 mb-3" id="pdfContent" ng-show="SpecialContents.FileType == 'PDF'">
                        <%-- <embed src="Asset/data/test.pdf" />--%>
                        <div class="contents-datials embed"  id="divPDF">
                        </div>
                        <div class="text-center mt-5">
                            <a class="btn btn-custom bg-blue font-weight-bold text-white" onclick="toggleSection('video')">Continue</a>
                        </div>
                    </div>

                    <div class="col-md-10 offset-md-1 mb-3 text-center" id="videoContent" ng-show="SpecialContents.FileType == 'VIDEO'" >
                        <div class="row">
                            <div id="dvVideoRating" style="display: none;" class="col-md-12 video-rating text-white d-none">
                                <h2 class="font-weight-bold">How did you like the video?</h2>
                                <dl class="row text-center">
                                    <dt class="col" ng-click="RateVideo(SpecialContents.ContentID,1)">
                                        <i class="far fa-grin-hearts fa-5x"></i>
                                        <span>Love it!</span>
                                    </dt>
                                    <dt class="col" ng-click="RateVideo(SpecialContents.ContentID,1)">
                                        <i class="far fa-grin-beam fa-5x"></i>
                                        <span>Like it!</span>
                                    </dt>
                                    <dt class="col" ng-click="RateVideo(SpecialContents.ContentID,1)">
                                        <i class="far fa-meh fa-5x"></i>
                                        <span>Meh</span>
                                    </dt>
                                    <dt class="col" ng-click="RateVideo(SpecialContents.ContentID,1)">
                                        <i class="far fa-frown fa-5x"></i>
                                        <span>Didn't like it!</span>
                                    </dt>
                                </dl>
                            </div>
                            <div class="col-md-12">
                                <%-- <video controls id="contentVideo" onended="videoRating()">
                                    <source src="Asset/data/bunny.mp4" type="video/mp4">
                                </video>--%>
                                <div id="divVideo">
                                </div>
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

        <div ng-show="ActiveContainer =='ContentSurveyView'">
            <div ng-click="GoBack('Content')">{{ContentGoBackText}}</div>
            <h2>This is survey</h2>
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
                    <input type="radio" name="ansOption.AnswerText" value="1" id="rbSurveyRate1" /><label for="rbSurveyRate1">1</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate2" /><label for="rbSurveyRate2">2</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate3" /><label for="rbSurveyRate3">3</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate4" /><label for="rbSurveyRate4">4</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate5" /><label for="rbSurveyRate5">5</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate6" /><label for="rbSurveyRate6">6</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate7" /><label for="rbSurveyRate7">7</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate8" /><label for="rbSurveyRate8">8</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate9" /><label for="rbSurveyRate9">9</label>
                    <input type="radio" name="ansOption.AnswerText" value="2" id="rbSurveyRate10" /><label for="rbSurveyRate10">10</label>
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
            <button onclick="return false;" ng-click="FlashcardQuestionPrevioustClicked($index,SpecialContents.TotalQuestions)">Previous</button>
            <button onclick="return false;" ng-click="FlashcardQuestionNextClicked($index,SpecialContents.TotalQuestions)">Submit Survey</button>
        </div>

        <div ng-show="ActiveContainer =='ContentFlashcardView'">
            <div ng-click="GoBack('Content')">{{ContentGoBackText}}</div>
            <div>Flashcards</div>
            <div ng-show="ActiveSubContainer =='BeginFlashcard'">
                You have completed all the videos/pdfs in this module.
            <h1>UP NEXT:</h1>
                <div>FLASHCARD ICON</div>
                <div>FLASHCARD TITLE</div>
                <button onclick="return false;">SKIP</button>
                <button onclick="return false;" ng-click="ShowFlashcardIntro()">BEGIN FLASHCARD</button>
            </div>
            <div ng-show="ActiveSubContainer =='FlashcardIntro'">
                <h2>Flashcard intro</h2>
                <div>{{SpecialContents.FlashcardIntro.Title}}</div>
                <div ng-repeat="highlight in SpecialContents.FlashcardIntro.Highlights">
                    <div>
                        <div>{{highlight.Content}}</div>
                    </div>
                </div>
                <a>Skip Flashcards</a>
                <button onclick="return false;" ng-click="ShowFlashcardSlides()">let's go</button>
            </div>
            <div ng-show="ActiveSubContainer =='FlashcardSlides'">
                <div ng-repeat="flashcardSlide in SpecialContents.FlashcardSlides">
                    <div ng-show="$index == CurrIndex">
                        <div>Icon</div>
                        <div>{{flashcardSlide.Content}}</div>
                        <button onclick="return false;" ng-click="FlashcardPreviousClicked($index,SpecialContents.TotalFlashcardSlides)">{{$index ==0 ? 'Previous' :'Previous Card'}}</button>
                        <button onclick="return false;" ng-click="FlashcardNextClicked($index,SpecialContents.TotalFlashcardSlides)">{{($index + 1) == SpecialContents.TotalFlashcardSlides ? 'Begin Flashcard Quiz' :'Next Card'}}</button>
                    </div>
                </div>
            </div>
            <div ng-show="ActiveSubContainer =='FlashcardQuiz'">
                this is flashcard quiz...
                <div ng-repeat="question in SpecialContents.Questions">
                    <div ng-show="$index == CurrIndex">
                        <div>{{question.Title}}</div>
                        <div ng-repeat="ansOption in question.AnswerOptions">
                            <div>{{ansOption.AnswerText}}</div>
                        </div>
                        <button onclick="return false;" ng-click="FlashcardQuestionPrevioustClicked($index,SpecialContents.TotalQuestions)">{{$index ==0 ? 'Previous' :'Previous Question'}}</button>
                        <button onclick="return false;" ng-click="FlashcardQuestionNextClicked($index,SpecialContents.TotalQuestions)">{{($index + 1) == SpecialContents.TotalQuestions ? 'Begin Final Quiz' :'Next Question'}}</button>
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
        function VideoFinished(e) {
            $("#dvVideoRating").show();
            $('#dvVideoRating').removeClass('d-none');
        }

        function VideoPaused(e) {
            //alert("Video Paused");
        }

        function VideoClicked(cntrl) {
            // $("#vdVideoPlayer").trigger( "click" );
            cntrl.paused ? cntrl.play() : cntrl.pause();
        }
    </script>
</asp:Content>

