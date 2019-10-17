
$(document).ready(function () {
    $('select.select2').select2({
        placeholder: "Select a option",
        allowClear: true
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
