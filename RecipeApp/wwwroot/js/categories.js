"use strict";

let categories = []
let currentId = -1;

$(document).ready(function () {
    $.ajax({
        url: '/Admin/GetCategories',
        method: 'get',
        success: function (data) {
            categories = data;
            createTable(categories, "categories");
        },
        error: function (req, status, err) {
            console.log("error: ", err.responseText)
        }
    });

    $("#add-category-button").click(onAddRole)

    $("#update-category-form").submit(handleUpdateSubmit)

    $("#add-category-form").submit(handleAddSubmit)
});

function onDelete(e) {
    e.preventDefault()
    $.ajax({
        url: '/Admin/DeleteCategory/?id=' + categories[e.target.id].id,
        method: 'delete',
        success: function (data) {
            $("#" + e.target.id).parent().remove();
        },
        error: function (req, status, err) {
            console.log("error: ", err.responseText)
        }
    });
}

function onUpdate(e) {
    $("#category-permissions").text(categories[e.target.id].permissions);
    $("#update-category-modal").modal('show');
    $("#update-save-button").attr("data-index", e.target.id)
}

function handleUpdateSubmit(e) {
    e.preventDefault()
    const formData = $("#update-category-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    const index = $("#update-save-button").data("index")

    $.ajax({
        url: '/Admin/UpdateCategory',
        method: 'put',
        data:
        {
            id: categories[index].id,
            ...formData
        },
        success: function (data) {
            window.location.replace(window.location.origin + "/admin/categories")
        },
        error: function (req, status, err) {
            alert(err.responseText)
        }
    });
}

function handleAddSubmit(e) {
    e.preventDefault()
    const formData = $("#add-category-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    $.ajax({
        url: '/Admin/AddCategory',
        method: 'post',
        data: formData,
        success: function (data) {
            window.location.replace(window.location.origin + "/admin/categories")
        },
        error: function (req, status, err) {
            alert(err.responseText)
        }
    });
}

function onAddRole() {
    $("#add-category-modal").modal('show');
}

