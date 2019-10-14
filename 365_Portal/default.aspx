<%@ Page Title="365" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Life.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="Asset/customer/default.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server" >
    <div ng-controller="DefaultController">
        <div class="row topics">
            <div class="col-md-12">
                <h1 class="text-center font-weight-bold">{{UserGreeting}}</h1>
            </div>
            <div id="dvTopicContainer" class="col-md-10 mt-5 offset-md-1" ng-show="ActiveContainer =='Topic'">
                <div class="row">
                    <div class="col-md-12 mb-1">
                        <h5 class="section-title">My Topics</h5>
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
            <div id="dvModuleContainer" class="col-md-10 mt-5 offset-md-1" ng-show="ActiveContainer =='Module'">
                <div ng-click="GoBack('Topic')">Back to Topics</div>
                <h2>{{Module.TopicName}}</h2>
                <h2>Modules</h2>
                <div>{{Module.ProgressBarText}}</div>
                <div>Unlocked Modules</div>
                <div ng-repeat="module in Module.UnlockedItems">
                    <div ng-click="GetContentsByModule()">
                        <div>{{module.Title}}</div>
                        <div>{{module.Description}}</div>
                        <div>{{module.Progress}}</div>
                    </div>
                </div>
                <div>Locked Modules</div>
                <div ng-repeat="module in Module.LockedItems">
                    <div>
                        <div>{{module.Title}}</div>
                        <div>{{module.Description}}</div>
                        <div>Locked</div>
                    </div>
                </div>
            </div>
            <div id="dvContentsContainer" ng-show="ActiveContainer =='Content'">
                <div ng-click="GoBack('Module')">Back to Modules</div>
                <h2>{{Content.ModuleName}}</h2>
                <h2>Content</h2>
                <div>{{Content.ProgressBarText}}</div>
                <b>Overview</b>
                In the employee motivation module, will guide you through a number of 
            techniques that you can use to keep yourself motivated. As a result
            you will hopefully stay much more motivated in your office and have more fun.
            <div ng-repeat="content in Content.UnlockedItems">
                <div ng-click="ViewContent(content.Title,content.Type)">
                    <div>{{content.Icon}}</div>
                    <div>{{content.Title}}</div>
                    <div>{{content.Description}}</div>
                </div>
            </div>
                <div ng-repeat="content in Content.LockedItems">
                    <div ng-click="ViewContent(content.Title,content.Type)">
                        <div>{{content.Icon}}</div>
                        <div>{{content.Title}}</div>
                        <div>{{content.Description}}</div>
                        <div>Locked</div>
                    </div>
                </div>
            </div>
            <div ng-show="ActiveContainer =='ContentView'">
                <div ng-click="GoBack('Content')">{{ContentGoBackText}}</div>
                <h2>Display Content View</h2>
                <div ng-show="SpecialContents.FileType == 'VIDEO'" id="divVideo">
                </div>
                <div ng-show="SpecialContents.FileType == 'PDF'" id="divPDF">
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


            <div id="dvVideoRating" style="display: none;">
                <h1>How did u like the video?</h1>
                <div class="section-video-rating-list" ng-click="RateVideo(1)">
                    <div class="section-video-rating-icon">
                        <span class="rating-love-icon"></span>
                    </div>
                    <div>Love it!</div>
                </div>

                <div class="section-video-rating-list" ng-click="RateVideo(2)">
                    <div class="section-video-rating-icon">
                        <span class="rating-like-icon"></span>
                    </div>
                    <div>Like it!</div>
                </div>

                <div class="section-video-rating-list" ng-click="RateVideo(3)">
                    <div class="section-video-rating-icon">
                        <span class="rating-meh-icon"></span>
                    </div>
                    <div>Meh</div>
                </div>

                <div class="section-video-rating-list" ng-click="RateVideo(4)">
                    <div class="section-video-rating-icon">
                        <span class="rating-dislike-icon"></span>
                    </div>
                    <div>Didn't like it!</div>
                </div>
            </div>

            <script>
                function VideoFinished(e) {
                    $("#dvVideoRating").show();
                }
            </script>
</asp:Content>

