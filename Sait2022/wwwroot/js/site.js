// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {

    if (performance.navigation.type == 1) {
        console.log("Страница перезагружена");
        $(document).ready(setInterval
            (function () {
                /*$('.onerow').append('<div></div>');*/
                $('.onerow').before('<div></div>');
            }, 6000));
    }
    
});

