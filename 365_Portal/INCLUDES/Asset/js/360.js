

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
    });

    //$('.sidenav-content-menu')
});