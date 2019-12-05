
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

    $.extend(true, $.fn.dataTable.defaults, {
        'paging': false,
        'ordering': false,
        'info': false,
        'searching': false,
        dom: '<"tbl-head"Bf><"tbl-body"rt><"tbl-foot"ip>',
        buttons: [
            { extend: 'colvis', columns: ':not(.noVis)' }, {
                extend: 'excel',
                exportOptions: {
                    format: {
                        body: function (data, row, column, node) {
                            if ($(node).hasClass('noVis'))
                                return;
                            data = data.replace(/(&nbsp;|<([^>]+)>)/ig, "");
                            data = data.replace('more_horiz', '');
                            return "\u200C" + data;
                        }
                    }
                }
            }
        ]
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

function ToggleNav() {
    if ($("#sideNav").width() === 250) {
        closeNav();
    } else {
        openNav();
    }
}

function openNav() {
    $("#sideNav").css("width", "250px");
    $("main").css("margin-left", "250px");
}

function closeNav() {
    $("#sideNav").css("width", "0");
    $("main").css("margin-left", "0");
}


function ShowLoader() {
    loader(1);
}

function HideLoader() {
    loader(0);
}

function loader(acion) {
    if (acion === 1) {
        $('.spinner-center').removeClass('d-none');
    } else if (acion === 0) {
        $('.spinner-center').addClass('d-none');
    }
}

function toggle(view, hide) {
    $('#' + view).removeClass('d-none');
    $('#' + hide).addClass('d-none');
}


function clearFields(container) {
    var inputs = $(container);
    inputs.find('[type=text],[type=password],[type=number],[type=email],textarea').val(null);
    inputs.find('select.select2').val(null).trigger('change');
    inputs.find('select').val(null).trigger('change');
    inputs.find('input[type="file"]').val(null);
    inputs.find('.custom-file .custom-file-label').text('Choose file');
    inputs.find('[type=radio], [type=checkbox]').prop('checked', false);


    inputs.find('.is-invalid').removeClass('is-invalid').removeClass('is-valid');
    inputs.find('.is-valid').removeClass('is-invalid').removeClass('is-valid');

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