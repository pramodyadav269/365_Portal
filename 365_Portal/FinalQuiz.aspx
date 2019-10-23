<%@ Page Title="Final Quiz" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="FinalQuiz.aspx.cs" Inherits="_365_Portal.FinalQuiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row quiz">
        <div class="col-md-12 header">
            <a class="back" href="Contents.aspx"><i class="fas fa-arrow-left"></i>Back to Contents</a>
            <h1 class="text-center font-weight-bold">Final Quiz - Employee Motivation</h1>
        </div>
        <div class="col-md-10 mt-5 offset-md-1">
            <div class="row" id="quizQuestion">
            </div>
            <div class="w-100 mt-4 text-center">
                <a href="#" class="btn btn-custom bg-blue font-weight-bold text-white" id="btnCheckAnwsers" onclick="checkAnwsers();">Check Anwsers</a>
                <a href="#" class="btn btn-custom btn-transparent font-weight-bold mr-2 d-none" id="btnResetAnwsers" onclick="setAnwsers();">Take The Test Again</a>
                <a href="Contents.aspx" class="btn btn-custom bg-blue font-weight-bold text-white d-none" id="btnContinue">Continue</a>
            </div>
        </div>
    </div>

    <script>

        $(document).ready(function () {
            setAnwsers();
        });

        function checkAnwsers() {

            $.each($('#quizQuestion .card'), function (index, el) {
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

            $('#btnCheckAnwsers').addClass('d-none')
            $('#btnResetAnwsers').removeClass('d-none')
            $('#btnContinue').removeClass('d-none')
        }

        function setAnwsers() {

            $('#btnCheckAnwsers').removeClass('d-none')
            $('#btnResetAnwsers').addClass('d-none')
            $('#btnContinue').addClass('d-none')

            var array = [1, 2, 3];

            var question = '';
            $.each(array, function (index, value) {
                question += '<div class="col-md-12 mb-3">';
                question += '<div class="card border-0 shadow mb-3">';
                question += '<div class="card-body question">';
                question += '<div class="media mb-4"><h1 class="card-title display-4 font-weight-bold mr-4">' + value + '.</h1>';
                question += '<div class="media-body pr-4">';

                if (value === 1) {

                    question += '<h4 class="mt-0 mb-4">Employee motivation is all about management.</h4>';
                    question += '<div class="box">';
                    question += '<input type="radio" id="rbY" name="rgYN" value="1">';
                    question += '<label for="rbY">True</label>';
                    question += '<input type="radio" id="rbN" name="rgYN" value="0">';
                    question += '<label for="rbN">False</label>';
                    question += '</div>';

                } else if (value === 2) {

                    question += '<h4 class="mt-0 mb-4">Which is the main aspect of motivational psychology?</h5>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQ11" name="rgQuestion1" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQ11">Incentive theory of motivation</label>';
                    question += '</div>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQ12" name="rgQuestion1" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQ12">Drive theory of motivation</label>';
                    question += '</div>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQ13" name="rgQuestion1" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQ13">Arousal theory of motivation</label>';
                    question += '</div>';

                } else if (value === 3) {

                    question += '<h4 class="mt-0 mb-4">Which is the best way to get over obstacle?</h5>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQ21" name="rgQuestion2" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQ21">Embrace self-awareness</label>';
                    question += '</div>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQ22" name="rgQuestion2" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQ22">Use time to your advantage</label>';
                    question += '</div>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQ23" name="rgQuestion2" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQ23">Be patient</label>';
                    question += '</div>';

                }

                question += '</div>';
                question += '<p class="anchor"></p>';
                question += '</div></div></div></div>';
            });

            $('#quizQuestion').empty().append(question);

            $('select.select2').select2({
                placeholder: "Select a option",
                allowClear: true
            });

            bsCustomFileInput.init()

            $('.date').datepicker({ uiLibrary: 'bootstrap4', format: 'yyyy-dd-mm' });

            $('.custom-range').on('change', function () {
                $('label[for=' + this.id + ']').text('Value : ' + $(this).val());
            });
        }
    </script>
</asp:Content>
