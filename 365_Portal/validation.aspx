<%@ Page Title="" Language="C#" MasterPageFile="~/Life.Master" AutoEventWireup="true" CodeBehind="validation.aspx.cs" Inherits="_365_Portal.validation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>

        function inputValidation(container) {

            $(container).find('.required').each(function (i, _input) {
                var input = $(_input);

                var val = $(input).val();
                if (input.attr('type') === 'text' || input.attr('type') === 'password' ||
                    input.attr('type') === 'number' || input.attr('type') === 'email' || input.prop("tagName") === "TEXTAREA") {
                    input.removeClass('is-invalid').removeClass('is-valid');
                    if (val === undefined || val === null || val === "") {
                        input.addClass('is-invalid');
                    }
                    else {
                        input.addClass('is-valid');
                    }
                }

                if (input.attr('type') === 'file') {
                    input.removeClass('is-invalid').removeClass('is-valid');
                    if (val === undefined || val === null || val === "") {
                        input.addClass('is-invalid');
                    }
                    else {
                        input.addClass('is-valid');
                    }
                }

                if (input.prop("tagName") === "SELECT") {
                    input.next().removeClass('is-invalid').removeClass('is-valid');
                    if (val !== undefined && val !== null && val !== "" && val.length > 0) {
                        input.next().addClass('is-valid');
                    }
                    else {
                        input.next().addClass('is-invalid');
                    }
                }

                if (input.hasClass('radio') || input.hasClass('checkbox')) {
                    if (input.find('input').is(':checked')) {

                        return true;
                    }
                    else {

                        return false;
                    }
                }
            });

            if ($(container).find('.is-invalid').length < 1) {
                return true;
            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="body" runat="server">
    <div class="input-validation">
        <div class="form-group">
            <label for="txtFullName">First & Last Name</label>
            <input type="text" class="form-control required" id="txtFullName" />
        </div>



        <div class="form-group">
            <label for="txtFullName">First & Last Name</label>
            <input type="text" class="form-control" id="text" placeholder="Type your answer here" />
        </div>


        <div class="form-group">
             <label for="textarea">First & Last Name</label>
            <textarea class="form-control" placeholder="Type your answer here" id="textarea"></textarea>
        </div>

        <div class="form-group">
            <select class="form-control select2" id="select">
                <option></option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
            </select>
        </div>

        <div class="form-group">
            <select class="form-control select2" multiple id="selectMultiple">
                <option></option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
            </select>
        </div>


        <div class="custom-control custom-radio">
            <input type="radio" id="rbQQ1" name="rgQuestion" class="custom-control-input">
            <label class="custom-control-label" for="rbQQ1">Yes</label>
        </div>
        <div class="custom-control custom-radio">
            <input type="radio" id="rbQQ2" name="rgQuestion" class="custom-control-input">
            <label class="custom-control-label" for="rbQQ2">No</label>
        </div>



        <div class="custom-control custom-checkbox">
            <input type="checkbox" id="cbQ1" name="cgQuestion" class="custom-control-input">
            <label class="custom-control-label" for="cbQ1">A</label>
        </div>
        <div class="custom-control custom-checkbox">
            <input type="checkbox" id="cbQ2" name="cgQuestion" class="custom-control-input">
            <label class="custom-control-label" for="cbQ2">B</label>
        </div>
        <div class="custom-control custom-checkbox">
            <input type="checkbox" id="cbQ3" name="cgQuestion" class="custom-control-input">
            <label class="custom-control-label" for="cbQ3">C</label>
        </div>



        <div class="form-group">
            <input type="range" class="custom-range" min="0" max="10" step="1" value="0" id="range">
            <label for="range">Value : 0</label>
        </div>



        <div class="custom-file">
            <input type="file" class="custom-file-input" id="file">
            <label class="custom-file-label" for="customFile">Choose file</label>
        </div>

        <div class="form-group">
            <input type="text" class="form-control date" id="date" placeholder="Select Date" />
        </div>



        <div class="rating">
            <input type="radio" id="rbQ10" name="rgQuestion" value="10">
            <label for="rbQ10">10</label>
            <input type="radio" id="rbQ9" name="rgQuestion" value="9">
            <label for="rbQ9">9</label>
            <input type="radio" id="rbQ8" name="rgQuestion" value="8">
            <label for="rbQ8">8</label>
            <input type="radio" id="rbQ7" name="rgQuestion" value="7">
            <label for="rbQ7">7</label>
            <input type="radio" id="rbQ6" name="rgQuestion" value="6">
            <label for="rbQ6">6</label>
            <input type="radio" id="rbQ5" name="rgQuestion" value="5">
            <label for="rbQ5">5</label>
            <input type="radio" id="rbQ4" name="rgQuestion" value="4">
            <label for="rbQ4">4</label>
            <input type="radio" id="rbQ3" name="rgQuestion" value="3">
            <label for="rbQ3">3</label>
            <input type="radio" id="rbQ2" name="rgQuestion" value="2">
            <label for="rbQ2">2</label>
            <input type="radio" id="rbQ1" name="rgQuestion" value="1">
            <label for="rbQ1">1</label>
        </div>




        <div class="box">
            <input type="radio" id="rbY" name="rgYN" value="1">
            <label for="rbY">Yes</label>
            <input type="radio" id="rbN" name="rgYN" value="0">
            <label for="rbN">No</label>
        </div>



        <div class="box inline-box">
            <input type="radio" id="rbT" name="rgTF" value="1">
            <label for="rbT">True</label>
            <input type="radio" id="rbF" name="rgTF" value="0">
            <label for="rbF">False</label>
        </div>


        <div class="box">
            <input type="checkbox" id="cbY" name="cgYN" value="1">
            <label for="cbY">Yes</label>
            <input type="checkbox" id="cbN" name="cgYN" value="0">
            <label for="cbN">No</label>
        </div>



        <div class="box inline-box">
            <input type="checkbox" id="cbT" name="cgTF" value="1">
            <label for="cbT">True</label>
            <input type="checkbox" id="cbF" name="cgTF" value="0">
            <label for="cbF">False</label>
        </div>




        <div class="mt-4">
            <a class="btn bg-yellow" onclick="inputValidation('.input-validation');">Change password</a>
        </div>
    </div>

    <script>

        $(document).ready(function () {
            $('.datetime').datetimepicker({ uiLibrary: 'bootstrap4', footer: true, format: 'yyyy-dd-mm hh:mm' });
            $('.date').datepicker({ uiLibrary: 'bootstrap4', footer: true, format: 'yyyy-dd-mm' });
            $('.time').timepicker({ uiLibrary: 'bootstrap4', footer: true });

        });
    </script>
</asp:Content>
