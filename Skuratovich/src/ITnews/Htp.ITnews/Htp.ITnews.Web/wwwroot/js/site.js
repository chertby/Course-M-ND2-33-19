// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$('.theme').on('click', function () {
    Cookies.set('<ITnews>.Theme', $(this).data('theme'), { expires: 1 });
    location.reload();
});

$('.culture').on('click', function () {
    Cookies.set('<ITnews>.CultureName', $(this).data('culture'), { expires: 1 });
    location.reload();
});