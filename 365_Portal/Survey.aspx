<%@ Page Title="Survey" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="Survey.aspx.cs" Inherits="_365_Portal.Survey" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="row survey">
        <div class="col-md-12 header"> 
            <a class="back" href="Contents.aspx"><i class="fas fa-arrow-left"></i>Back to Contents</a>
            <h1 class="text-center font-weight-bold">Survey - Employee Health in General</h1>
        </div>
        <div class="col-md-10 mt-5 offset-md-1">
            <div class="row" id="surveyQuestion">
            </div>
            <div class="text-center mt-4">
                <a href="Flashcards.aspx" class="btn btn-custom bg-blue font-weight-bold text-white">Finsh</a>
            </div>
        </div>
    </div>
    <script>

        $(document).ready(function () {
             
            var array = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15];

            var question = '';
            $.each(array, function (index, value) {
                question += '<div class="col-md-12 mb-3">';
                question += '<div class="card border-0 shadow mb-3">';
                question += '<div class="card-body question">';
                question += '<div class="media mb-4"><h1 class="card-title display-4 font-weight-bold mr-4">' + value + '.</h1>';
                question += '<div class="media-body pr-4">';
                

                if (value === 1) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="form-group">';
                    question += '<input type="text" class="form-control" id="text" placeholder="Type your answer here" />';
                    question += '</div>';

                } else if (value === 2) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="form-group">';
                    question += '<textarea class="form-control" placeholder="Type your answer here" id="textarea"></textarea>';
                    question += '</div>';
                } else if (value === 3) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="form-group">';
                    question += '<select class="form-control select2" id="select">';
                    question += '<option></option>';
                    question += '<option value="1">1</option>';
                    question += '<option value="2">2</option>';
                    question += '<option value="3">3</option>';
                    question += '</select>';
                    question += '</div>';

                } else if (value === 4) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="form-group">';
                    question += '<select class="form-control select2" multiple id="selectMultiple">';
                    question += '<option></option>';
                    question += '<option value="1">1</option>';
                    question += '<option value="2">2</option>';
                    question += '<option value="3">3</option>';
                    question += '</select>';
                    question += '</div>';

                } else if (value === 5) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQQ1" name="rgQuestion" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQQ1">Yes</label>';
                    question += '</div>';
                    question += '<div class="custom-control custom-radio">';
                    question += '<input type="radio" id="rbQQ2" name="rgQuestion" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="rbQQ2">No</label>';
                    question += '</div>';

                } else if (value === 6) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="custom-control custom-checkbox">';
                    question += '<input type="checkbox" id="cbQ1" name="cgQuestion" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="cbQ1">A</label>';
                    question += '</div>';
                    question += '<div class="custom-control custom-checkbox">';
                    question += '<input type="checkbox" id="cbQ2" name="cgQuestion" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="cbQ2">B</label>';
                    question += '</div>';
                    question += '<div class="custom-control custom-checkbox">';
                    question += '<input type="checkbox" id="cbQ3" name="cgQuestion" class="custom-control-input">';
                    question += '<label class="custom-control-label" for="cbQ3">C</label>';
                    question += '</div>';

                } else if (value === 7) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="form-group">';
                    question += '<input type="range" class="custom-range" min="0" max="10" step="1" value="0" id="range">';
                    question += '<label for="range">Value : 0</label>';
                    question += '</div>';

                } else if (value === 8) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="custom-file">';
                    question += '<input type="file" class="custom-file-input" id="file">';
                    question += '<label class="custom-file-label" for="customFile">Choose file</label>';
                    question += '</div>';
                } else if (value === 9) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="form-group">';
                    question += '<input type="text" class="form-control date" id="date" placeholder="Select Date" />';
                    question += '</div>';

                } else if (value === 10) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="rating">';
                    question += '<input type="radio" id="rbQ10" name="rgQuestion" value="10">';
                    question += '<label for="rbQ10">10</label>';
                    question += '<input type="radio" id="rbQ9" name="rgQuestion" value="9">';
                    question += '<label for="rbQ9">9</label>';
                    question += '<input type="radio" id="rbQ8" name="rgQuestion" value="8">';
                    question += '<label for="rbQ8">8</label>';
                    question += '<input type="radio" id="rbQ7" name="rgQuestion" value="7">';
                    question += '<label for="rbQ7">7</label>';
                    question += '<input type="radio" id="rbQ6" name="rgQuestion" value="6">';
                    question += '<label for="rbQ6">6</label>';
                    question += '<input type="radio" id="rbQ5" name="rgQuestion" value="5">';
                    question += '<label for="rbQ5">5</label>';
                    question += '<input type="radio" id="rbQ4" name="rgQuestion" value="4">';
                    question += '<label for="rbQ4">4</label>';
                    question += '<input type="radio" id="rbQ3" name="rgQuestion" value="3">';
                    question += '<label for="rbQ3">3</label>';
                    question += '<input type="radio" id="rbQ2" name="rgQuestion" value="2">';
                    question += '<label for="rbQ2">2</label>';
                    question += '<input type="radio" id="rbQ1" name="rgQuestion" value="1">';
                    question += '<label for="rbQ1">1</label>';
                    question += '</div>';


                } else if (value === 11) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="box">';
                    question += '<input type="radio" id="rbY" name="rgYN" value="1">';
                    question += '<label for="rbY">Yes</label>';
                    question += '<input type="radio" id="rbN" name="rgYN" value="0">';
                    question += '<label for="rbN">No</label>';
                    question += '</div>';

                } else if (value === 12) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="box inline-box">';
                    question += '<input type="radio" id="rbT" name="rgTF" value="1">';
                    question += '<label for="rbT">True</label>';
                    question += '<input type="radio" id="rbF" name="rgTF" value="0">';
                    question += '<label for="rbF">False</label>';
                    question += '</div>';

                } else if (value === 13) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="box">';
                    question += '<input type="checkbox" id="cbY" name="cgYN" value="1">';
                    question += '<label for="cbY">Yes</label>';
                    question += '<input type="checkbox" id="cbN" name="cgYN" value="0">';
                    question += '<label for="cbN">No</label>';
                    question += '</div>';

                } else if (value === 14) {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="box inline-box">';
                    question += '<input type="checkbox" id="cbT" name="cgTF" value="1">';
                    question += '<label for="cbT">True</label>';
                    question += '<input type="checkbox" id="cbF" name="cgTF" value="0">';
                    question += '<label for="cbF">False</label>';
                    question += '</div>';


                } else {
                    question += '<h4 class="mt-0 mb-4">What was your manager like when you argued with him?</h4>';
                    question += '<div class="form-group">';
                    question += '<textarea class="form-control" placeholder="Type your answer here" id="textarea"></textarea>';
                    question += '</div>';
                }

                question += '</div>';
                question += '<p class="anchor"></p>';
                question += '</div></div></div></div>';
            });

            $('#surveyQuestion').empty().append(question);

            $('select.select2').select2({
                placeholder: "Select a option",
                allowClear: true
            });

            bsCustomFileInput.init()

            $('.date').datepicker({ uiLibrary: 'bootstrap4', format: 'yyyy-dd-mm' });

            $('.custom-range').on('change', function () {
                $('label[for=' + this.id + ']').text('Value : ' + $(this).val());
            });



        });
    </script>

</asp:Content>

