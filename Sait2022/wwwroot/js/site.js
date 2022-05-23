// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

//$('input[type=submit]').on("click", function (e) {
//    e.preventDefault();
//     $.ajax({
//         url: '/Questions',
//         type: 'POST',
//         data: {key:"4"},
//         success: function (responce) { console.log(responce)},
//         error: function (responce) { console.log(responce)}
//    });
//});

if (window.performance) {
    console.log("Perfomance not supported");
    $(document).ready(setInterval(function () {
        $(".container").append("<div></div>");
    }, 1000));
}
if (performance.navigation.type == 1) {
    console.log("Страница перезагружена");
    
} else {
    console.log("Страница не перезагружена");
}
