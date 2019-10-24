
$(document).ready(function () {
    $('select.select2').select2({
        placeholder: "Select a option",
        allowClear: true
    }); 

    bsCustomFileInput.init();

    $('.date').datepicker({ uiLibrary: 'bootstrap4', format: 'yyyy-dd-mm' });

    $('.custom-range').on('change', function () {
        $('label[for=' + this.id + ']').text('Value : ' + $(this).val());
    });

});

function QueryStringValue(param) {
    var url = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < url.length; i++) {
        var urlparam = url[i].split('=');
        if (urlparam[0] === param) {
            return urlparam[1];
        }
    }
}

function toggle(view, hide) {
    $('#' + view).removeClass('d-none');
    $('#' + hide).addClass('d-none');
}

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
            input.find('input').removeClass('is-invalid').removeClass('is-valid');
            if (input.find('input').is(':checked')) {

                input.find('input').addClass('is-valid');
            }
            else {
                input.find('input').addClass('is-invalid');
            }
        }
    });

    if ($(container).find('.is-invalid').length < 1) {
        return true;
    } else {
        return false;
    }
}