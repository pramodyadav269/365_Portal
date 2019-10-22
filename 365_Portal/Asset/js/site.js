
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
