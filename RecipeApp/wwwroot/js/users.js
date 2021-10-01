"use strict";

let users = []
let roles = []
$(document).ready(function () {
    $.ajax({
        url: '/Admin/GetUsers',
        method: 'get',
        success: function (data) {
            users = data.users
            roles = data.roles
            createTable(users, "users");
            addOptions();
        },
        error: function (req, status, err) {
            alert(req.responseText)
        }
    });

    $("#add-user-button").click(onAddUser)

    $("#update-user-form").submit(handleUpdateSubmit)

    $("#add-user-form").submit(handleAddSubmit)
});

function onDelete(e) {
    $.ajax({
        url: '/Admin/DeleteUser/?id=' + users[e.target.id].id,
        method: 'delete',
        success: function (data) {
            $("#" + e.target.id).parent().remove();
        },
        error: function (req, status, err) {
            if (req.responseText)
                alert(req.responseText)
        }
    });
}

function onUpdate(e) {
    $("#user-permissions").text(users[e.target.id].permissions);
    $("#update-user-modal").modal('show');
    $("#update-save-button").attr("data-index", e.target.id)
}

function handleUpdateSubmit(e) {
    e.preventDefault();

    const formData = $("#update-user-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});
    const index = $("#update-save-button").data("index")

    $.ajax({
        url: '/Admin/UpdateUser',
        method: 'put',
        data:
        {
            id: users[index].id,
            ...formData
        },
        success: function (data) {
            window.location.replace(window.location.origin + "/admin")
        },
        error: function (req, status, err) {
            if (req.responseText)
                alert(req.responseText)
        }
    });
}

function handleAddSubmit(e) {
    e.preventDefault();

    const formData = $("#add-user-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    $.ajax({
        url: '/Admin/AddUser',
        method: 'post',
        data: formData,
        success: function (data) {
            window.location.replace(window.location.origin + "/admin")
        },
        error: function (req, status, err) {
            if (req.responseText)
                alert(req.responseText)
        }
    });
}

function onAddUser() {
    $("#add-user-modal").modal('show');

}

function addOptions() {
    for (let i = 0; i < roles.length; i++) {
        const option = $('<option></option>').attr("value", roles[i]).text(roles[i]);
        $("#roles-add").append(option)
    }

    for (let i = 0; i < roles.length; i++) {
        const option = $('<option></option>').attr("value", roles[i]).text(roles[i]);
        $("#roles-update").append(option)
    }
}