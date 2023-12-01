// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function desencolar() {
    setInterval(desencola1, 2 * 60 * 1000);
    setTimeout(desencola2, 3 * 60 * 1000);
}

function desencola1() {
    $.post('/Home/Index', { queue: 1 }, function() { });
    location.reload(true);
}

function desencola2() {
    $.post('/Home/Index', { queue: 2 }, function() { });
    location.reload(true);
}
