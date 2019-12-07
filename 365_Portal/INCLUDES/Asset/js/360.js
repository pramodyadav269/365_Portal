

$(function () {

    $(".achievements-progress .progress").each(function () {

        var value = $(this).attr('data-value');
        var left = $(this).find('.progress-left .progress-bar');
        var right = $(this).find('.progress-right .progress-bar');

        if (value > 0) {
            if (value <= 50) {
                right.css('transform', 'rotate(' + percentageToDegrees(value) + 'deg)');
            } else {
                right.css('transform', 'rotate(180deg)')
                left.css('transform', 'rotate(' + percentageToDegrees(value - 50) + 'deg)');
            }
        }

    });

    function percentageToDegrees(percentage) {
        return percentage / 100 * 360;
    }

});

//function ToggleNav() {
//    if ($("#sideNav").width() === 250) {
//        closeNav();
//    } else {
//        openNav();
//    }
//}

//function openNav() { 
//    $("#sideNav").css({ "width": "250px", "box-shadow": "0 .5rem 1rem rgba(12, 21, 82,.15)" });
//    $("#sideNav a.side-nav-menu").removeClass('d-none');
//    $(".sidenav-content").addClass('open');
//    $("main").css("margin-left", "250px");
//}

//function closeNav() {
//    $("#sideNav").css({ "width": "80px", "box-shadow": "none" });
//    $("#sideNav a.side-nav-menu").addClass('d-none');
//    $(".sidenav-content").removeClass('open');
//    $("main").css("margin-left", "80px");
//}

$(document).ready(function () {

    $('.sidenav-content .sidenav-item').click(function () {

        $('.sidenav-content .sidenav-item').removeClass('active');
        $('.sidenav-content-menu .sidenav-nav').addClass('d-none');
        $(this).addClass('active');

        if ($(this).hasClass('side-menu')) {
            $('.sidenav-content .sidenav-item').find('.sidenav-link').addClass('cu-tooltiptext');
            $('.sidenav-content .sidenav-item').find('.sidenav-link span:not(.tooltiptext)').css({ "opacity": "0" });
            $('.sidenav-content-menu').css({ "transform": "translate3d(52px, 0, 0)" });
        } else {
            $('.sidenav-content .sidenav-item').find('.sidenav-link').removeClass('cu-tooltiptext');
            $('.sidenav-content .sidenav-item').find('.sidenav-link span:not(.tooltiptext)').css({ "opacity": "100" });
            $('.sidenav-content-menu').css({ "transform": "translate3d(240px, 0, 0)" });
        }
        $('#' + $(this).attr('sidenav-id')).removeClass('d-none');
    });

    $('.task-arrow').click(function () {
        if ($('.admin-task .card-body').hasClass('d-none')) {
            $('.admin-task .card-body').removeClass('d-none');
            $('.task-arrow img').css({ "transform": "rotate(0deg)" });
        } else {
            $('.admin-task .card-body').addClass('d-none');
            $('.task-arrow img').css({ "transform": "rotate(180deg)" });
        }

    });
    //$('.sidenav-content-menu')
});