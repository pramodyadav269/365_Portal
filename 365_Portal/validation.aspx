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
            <label for="exampleFormControlTextarea1">Example textarea</label>
            <textarea class="form-control required" id="exampleFormControlTextarea1" rows="3"></textarea>
        </div>

        <div class="form-group">
            <label for="ddlSelectw">Example select</label>
            <select class="form-control select2 required" id="ddlSelectw">
                <option></option>
                <option value="1">1</option>
                <option value="2">2</option>
                <option value="3">3</option>
                <option value="4">4</option>
                <option value="5">5</option>
            </select>
        </div>
        <div class="form-group">
            <label for="exampleFormControlFile1">Example file input</label>
            <input type="file" class="form-control-file required" id="exampleFormControlFile1">
        </div>

        <div class="custom-file">
            <input type="file" class="custom-file-input required" id="validatedCustomFile">
            <label class="custom-file-label" for="validatedCustomFile">Choose file...</label>
        </div>


        <div class="custom-file">
    <input type="file" class="custom-file-input required" id="validatedCustomFile1">
    <label class="custom-file-label" for="validatedCustomFile1">Choose file...</label>
    <div class="invalid-feedback">Example invalid custom file feedback</div>
  </div>


        <div class="form-check">
            <input class="form-check-input" type="checkbox" value="" id="defaultCheck1">
            <label class="form-check-label" for="defaultCheck1">
                Default checkbox
            </label>
        </div>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio1" value="option1">
            <label class="form-check-label" for="inlineRadio1">1</label>
        </div>
        <div class="form-check form-check-inline">
            <input class="form-check-input" type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
            <label class="form-check-label" for="inlineRadio2">2</label>
        </div>

        <div class="mt-4">
            <a class="link font-weight-bold" onclick="inputValidation('.input-validation');">Change password</a>
        </div>
    </div>
</asp:Content>
