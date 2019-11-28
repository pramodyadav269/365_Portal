(function ($) {
    $.fn.loading = function () {
        var DEFAULTS = {
            backgroundColor: '#b3cef6',
            progressColor: '#4b86db',
            percent: 0,
            duration: 2000
        };

        $(this).each(function () {
            var $target = $(this);

            var opts = {
                backgroundColor: $target.data('color') ? $target.data('color').split(',')[0] : DEFAULTS.backgroundColor,
                progressColor: $target.data('color') ? $target.data('color').split(',')[1] : DEFAULTS.progressColor,
                percent: $target.data('percent') ? $target.data('percent') : DEFAULTS.percent,
                duration: $target.data('duration') ? $target.data('duration') : DEFAULTS.duration
            };
            // console.log(opts);

            $target.append('<div class="background"></div><div class="rotate"></div><div class="left"></div><div class="right"></div><div class=""><span></span></div>');

            $target.find('.background').css('background-color', opts.backgroundColor);
            $target.find('.left').css('background-color', opts.backgroundColor);
            $target.find('.rotate').css('background-color', opts.progressColor);
            $target.find('.right').css('background-color', opts.progressColor);

            var $rotate = $target.find('.rotate');
            setTimeout(function () {
                $rotate.css({
                    'transition': 'transform ' + opts.duration + 'ms linear',
                    'transform': 'rotate(' + opts.percent * 3.6 + 'deg)'
                });
            }, 1);

            if (opts.percent > 50) {
                var animationRight = 'toggle ' + (opts.duration / opts.percent * 50) + 'ms step-end';
                var animationLeft = 'toggle ' + (opts.duration / opts.percent * 50) + 'ms step-start';
                $target.find('.right').css({
                    animation: animationRight,
                    opacity: 1
                });
                $target.find('.left').css({
                    animation: animationLeft,
                    opacity: 0
                });
            }
        });
    }
})(jQuery);

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
    $(".progress-bar").loading();
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
        $('#'+$(this).attr('sidenav-id')).removeClass('d-none');
    });

    //$('.sidenav-content-menu')
});