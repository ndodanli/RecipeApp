"use strict";

$(document).ready(function () {
    $("#login_btn").click((e) => handleLogin(e));
});

function handleLogin(e) {
    e.preventDefault();
    const username = $('#username').val();
    const password = $('#password').val();

    $.ajax({
        url: '/Admin/Login',
        method: 'post',
        data:
        {
            username: username, password: password
        },
        success: function (data) {
            window.location.replace(window.location.origin + "/admin")
        },
        error: function (req, status, err) {
            $("#login-info").html(req.responseText);
            setTimeout(function () {
                $("#login-info").html("");
            }, 1500)
        }
    });
}