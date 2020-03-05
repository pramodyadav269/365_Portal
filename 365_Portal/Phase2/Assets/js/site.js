


$(document).ready(function () {
    selectInit('select.select2', 'Select a option');
});

function selectInit(el, placeholder) {
    $(el).select2({
        placeholder: placeholder,
        allowClear: true
    });
}