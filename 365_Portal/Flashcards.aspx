<%@ Page Title="Flashcards" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Flashcards.aspx.cs" Inherits="_365_Portal.Flashcards" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row flashcards">
        <div class="col-md-12 header">
            <a class="back" href="Contents.aspx"><i class="fas fa-arrow-left"></i>Back to Contents</a>
            <h1 class="text-center font-weight-bold">Employee Motivation</h1>
            <h6 class="text-center header-sub-title mt-3">Flashcards</h6>
        </div>
        <div class="col-md-10 mt-4 offset-md-1">
            <div class="row justify-content-center">
                <div class="col-12 col-sm-12 col-md-6 mb-3 overview d-none" id="divFlashcardOverview">
                    <h5 class="font-weight-bold">In this flashcard series we’ll answer:</h5>
                    <ul>
                        <li>What makes a good communicator?</li>
                        <li>How to motivate yourself when being in the office?</li>
                        <li>How to be more productive?</li>
                    </ul>
                    <div class="w-100 mt-5">
                        <a href="Contents.aspx" class="link font-weight-bold float-left">Skip Flashcards</a>
                        <a href="#" class="btn btn-custom bg-blue font-weight-bold text-white float-right" onclick="toggle('divFlashcard', 'divFlashcardOverview');">Let's Go</a>
                    </div>
                </div>

                <div class="col-12 col-sm-12 col-md-6 mb-3 overview d-none" id="divFlashcard">
                    <div class="flashcard">
                        <div class="card border-0">
                            <img class="card-img-top circle mx-auto" src="Asset/images/profile.png" />
                            <div class="card-body">
                                <p class="card-text">
                                    There’s a big difference between imposing goals on 
                                    employees and encouraging them to suggest goals of their own.
                                </p>
                                <p class="text-right anchor">1/4</p>
                            </div>
                        </div>
                        <div class="card border-0 d-none">
                            <img class="card-img-top circle mx-auto" src="Asset/images/profile.png" />
                            <div class="card-body">
                                <p class="card-text">
                                    When their suggested goals align with company objectives, 
                                    a manager can help develop action plans to attain those goals.
                                </p>
                                <p class="text-right anchor">2/4</p>
                            </div>
                        </div>
                        <div class="card border-0 d-none">
                            <img class="card-img-top circle mx-auto" src="Asset/images/profile.png" />
                            <div class="card-body">
                                <p class="card-text">
                                    Goal-setting sometimes fails when the objective is too ambitious 
                                    or simply unattainable, given the employee’s skill set and available resources.
                                </p>
                                <p class="text-right anchor">3/4</p>
                            </div>
                        </div>
                        <div class="card border-0 d-none">
                            <img class="card-img-top circle mx-auto" src="Asset/images/profile.png" />
                            <div class="card-body">
                                <p class="card-text">
                                    Burdening an employee with an unattainable goal can lead to frustration 
                                    with the process and a resulting lack of motivation for further improvement.
                                </p>
                                <p class="text-right anchor">4/4</p>
                            </div>
                        </div>
                    </div>
                    <div class="w-100 mt-5 text-center"> 
                        <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2 d-none" id="btnPrevCard" onclick="previousFlashcard();">Previous card</a>
                        <a href="#" class="btn btn-custom bg-yellow font-weight-bold" id="btnNextCard" onclick="nextFlashcard();">Next Card</a>
                        <a href="#" class="btn btn-custom bg-blue font-weight-bold text-white d-none" id="btnBeginQuiz" onclick="toggle('divFlashcardQuiz', 'divFlashcard');">Begin Flashcard Quiz</a>
                    </div>
                </div>

                <div class="col-12 mb-3 overview" id="divFlashcardQuiz">
                    <div class="row justify-content-center flashcard-question">
                        <div class="col-12 col-sm-12 col-md-6 mb-3 text-center">
                            <p>What is the main difference between the goals of management and the employees?</p>
                        </div>
                        <div class="w-100"></div>
                        <div class="col-12 col-sm-12 col-md-4 mb-3">
                            <div class="card border-0 shadow text-center">
                                <div class="card-body">
                                    <h5 class="card-title">Employees do everything for themself, management don’t</h5>
                                    <p class="anchor"></p>
                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-4 mb-3">
                            <div class="card border-0 shadow text-center">
                                <div class="card-body">
                                    <h5 class="card-title">The management helps to the employees</h5>
                                    <p class="anchor"></p>

                                </div>
                            </div>
                        </div>
                        <div class="col-12 col-sm-12 col-md-4 mb-3">
                            <div class="card border-0 shadow text-center">
                                <div class="card-body">
                                    <h5 class="card-title">Employees do everything for themself, management don’t</h5>
                                    <p class="anchor"></p>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="w-100 mt-5 text-center">
                        <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2 d-none" id="btnPrevQuestion" onclick="previousFlashcardQuestion();">Previous Question</a>
                        <a href="#" class="btn btn-custom bg-yellow font-weight-bold" id="btnNextQuestion" onclick="nextFlashcardQuestion();">Next Question</a>
                        <a href="Contents.aspx" class="btn btn-custom bg-blue font-weight-bold text-white d-none" id="btnContinue">Continue</a>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {

            $('.flashcard-question .card').on('click', function () {

                $.each($('.flashcard-question .card'), function (index, el) {
                    if (index == 0) {
                        $(el).addClass('b-red-2')
                        $(el).find('.card-body p.anchor').empty().append('<i class="fas fa-times c-red"></i>')
                    } else if (index == 1) {
                        $(el).addClass('b-green-2')
                        $(el).find('.card-body p.anchor').empty().append('<i class="fas fa-check c-green"></i>')
                    } else {
                        $(el).addClass('b-red-2')
                        $(el).find('.card-body p.anchor').empty().append('<i class="fas fa-times c-red"></i>')
                    }

                });


            });

        });


        function nextFlashcard() {

            var currentCard = $('#divFlashcard .card').not('.d-none');

            if ($('#divFlashcard .card').length === (currentCard.index() + 2)) {
                currentCard.next().removeClass('d-none')
                currentCard.addClass('d-none')

                $('#btnNextCard').addClass('d-none')
                $('#btnBeginQuiz').removeClass('d-none') 
            }
            else
                if (currentCard.next().length > 0) {
                    $('#btnPrevCard').removeClass('d-none')
                    currentCard.next().removeClass('d-none')
                    currentCard.addClass('d-none')
                } else {
                    $('#btnNextCard').addClass('d-none')
                    $('#btnBeginQuiz').removeClass('d-none')
                }

        }

        function previousFlashcard() {

            var currentCard = $('#divFlashcard .card').not('.d-none');

            if (currentCard.index() === 1) {
                $('#btnPrevCard').addClass('d-none')

                currentCard.prev().removeClass('d-none')
                currentCard.addClass('d-none')

            } else if (currentCard.prev().length > 0) {
                $('#btnPrevCard').removeClass('d-none')
                currentCard.prev().removeClass('d-none')
                currentCard.addClass('d-none')

                $('#btnNextCard').removeClass('d-none')
                $('#btnBeginQuiz').addClass('d-none')
            }

        }

    </script>
</asp:Content>
