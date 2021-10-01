"use strict";

$(document).ready(function () {
    $("#sign-out").click((e) => handleSignOut(e));

    $("#sidebarToggle").click(function (e) {
        e.preventDefault();
        $("body").toggleClass('sb-sidenav-toggled');
    })
});

function handleSignOut(e) {

    e.preventDefault();
    $.ajax({
        url: '/Admin/SignOut',
        method: 'get',
        success: function (data) {
            window.location.replace(window.location.origin)
        },
        error: function (req, status, err) {
            alert(req.responseText)
        }
    });
}