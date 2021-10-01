"use strict";

let recipes = []
let categoryNames = []

$(document).ready(function () {
    $.ajax({
        url: '/Home/GetRecipes',
        method: 'get',
        success: function (data) {
            recipes = data.recipes
            categoryNames = data.categoryNames
            createTable(recipes, "recipes");
            addOptions();
        },
        error: function (req, status, err) {
            console.log("error: ", err.responseText)
        }
    });

    $("#add-recipe-button").click(onAddrecipe)

    $("#update-recipe-form").submit(handleUpdateSubmit)

    $("#add-recipe-form").submit(handleAddSubmit)
});

function onDelete(e) {
    $.ajax({
        url: '/Admin/DeleteRecipe/?id=' + recipes[e.target.id].id,
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
    $("#recipe-permissions").text(recipes[e.target.id].permissions);
    $("#update-recipe-modal").modal('show');
    $("#update-save-button").attr("data-index", e.target.id)
}

function handleUpdateSubmit(e) {
    e.preventDefault();
    const inputData = $("#update-recipe-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    inputData.slug = stringToSlug(inputData.name)

    inputData.ingredients = inputData.ingredients.split(".").filter(ingredient => ingredient !== "")

    const index = $("#update-save-button").data("index")

    inputData.id = recipes[index].id

    const imageBlob = $("#update-image-file")[0].files[0];

    inputData.imageFile = imageBlob;

    const formData = new FormData();

    for (var i = 0; i < inputData.ingredients.length; i++) {
        formData.append('ingredients', inputData.ingredients[i]);
    }

    delete inputData.ingredients

    for (var key in inputData) {
        formData.append(key, inputData[key]);
    }
    
    $.ajax({
        url: '/Admin/UpdateRecipe',
        method: 'put',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            window.location.replace(window.location.origin + "/admin/recipes")
        },
        error: function (req, status, err) {
            alert(err.responseText)
        }
    });
}

function handleAddSubmit(e) {
    e.preventDefault();
    
    const inputData = $("#add-recipe-form").serializeArray().reduce(function (obj, item) {
        obj[item.name] = item.value;
        return obj;
    }, {});

    inputData.slug = stringToSlug(inputData.name)

    inputData.view = 0;

    inputData.ingredients = inputData.ingredients.split(".").filter(ingredient => ingredient !== "")

    const imageBlob = $("#add-image-file")[0].files[0];

    inputData.imageFile = imageBlob;

    const formData = new FormData();

    for (var i = 0; i < inputData.ingredients.length; i++) {
        formData.append('ingredients', inputData.ingredients[i]);
    }

    delete inputData.ingredients

    for (var key in inputData) {
        formData.append(key, inputData[key]);
    }
    
    $.ajax({
        url: '/Admin/AddRecipe',
        method: 'post',
        data: formData,
        contentType: false,
        processData: false,
        success: function (data) {
            window.location.replace(window.location.origin + "/admin/recipes")
        },
        error: function (req, status, err) {
            alert(err.responseText)
        }
    });
}

function onAddrecipe() {
    $("#add-recipe-modal").modal('show');

}

function addOptions() {
    console.log(categoryNames)
    for (let i = 0; i < categoryNames.length; i++) {
        const option = $('<option></option>').attr("value", categoryNames[i]).text(categoryNames[i]);
        $("#categories-add").append(option)
    }

    for (let i = 0; i < categoryNames.length; i++) {
        const option = $('<option></option>').attr("value", categoryNames[i]).text(categoryNames[i]);
        $("#categories-update").append(option)
    }
}