"use strict";
let roles = []
let currentId = -1;

$(document).ready(function () {
    $.ajax({
        url: '/Admin/GetRoles',
        method: 'get',
        success: function (data) {
            roles = data;
            createTable(roles, "roles");
        },
        error: function (req, status, err) {
            console.log("error: ", err.responseText)
        }
    });

    $("#add-role-button").click(onAddRole)

    $("#update-role-form").submit(handleUpdateSubmit)

    $("#add-role-form").submit(handleAddSubmit)
});

function onDelete(e) {
    e.preventDefault()
    $.ajax({
        url: '/Admin/DeleteRole/?id=' + roles[e.target.id].id,
        method: 'delete',
        success: function (data) {
            $("#" + e.target.id).parent().remove();
        },
        error: function (req, status, err) {
            alert(req.responseText)
        }
    });
}

function onUpdate(e) {
    $("#role-permissions").text(roles[e.target.id].permissions);
    $("#update-role-modal").modal('show');
    $("#update-save-button").attr("data-index", e.target.id)
}

function handleUpdateSubmit(e) {
    e.preventDefault()
    const formData = $("#update-role-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});
    formData.permissions = formData.permissions.split(",");

    const index = $("#update-save-button").data("index")

    $.ajax({
        url: '/Admin/UpdateRole',
        method: 'put',
        data:
        {
            roleId: roles[index].id, permissions: formData.permissions
        },
        success: function (data) {
            window.location.replace(window.location.origin + "/admin/roles")
        },
        error: function (req, status, err) {
            alert(err.responseText)
        }
    });
}

function handleAddSubmit(e) {
    e.preventDefault()
    const formData = $("#add-role-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    formData.permissions = formData.permissions.split(",");

    $.ajax({
        url: '/Admin/AddRole',
        method: 'post',
        data: formData,
        success: function (data) {
            window.location.replace(window.location.origin + "/admin/roles")
        },
        error: function (req, status, err) {
            alert(err.responseText)
        }
    });
}

function onAddRole() {
    $("#add-role-modal").modal('show');
}

