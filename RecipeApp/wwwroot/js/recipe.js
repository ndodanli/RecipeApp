"use strict";

let recipe = {}
$(document).ready(function () {
    $.ajax({
        url: '/Home/GetRecipe/' + window.location.pathname.split('/')[2],
        method: 'get',
        success: function (data) {
            if (data) {
                recipe = data
                createRecipe()
            } else {
                window.location.replace(window.location.origin)
            }
            
        },
        error: function (req, status, err) {
            alert(req.responseText)
        }
    });
});

function createRecipe() {
    const directions = recipe.directions.split(".")
    const ingredients = recipe.ingredients

    $("#recipe-name").text(recipe.name)
    $("#recipe-category").text(recipe.category)
    $("#recipe-servings").text(recipe.servings)
    $("#recipe-image").attr("src", recipe.imagePath)

    for (let i = 0; i < directions.length; i++) {
        if (directions[i].trim() !== "" && !directions[i].includes("\n")) {
            const li = $("<li></li>").text(directions[i].trim() + ".")
            $("#recipe-directions").append(li)
        }
    }
    for (let i = 0; i < ingredients.length; i++) {
        
        if (ingredients[i].trim() !== "" && !ingredients[i].includes("\n")) {
            const li = $("<dd></dd>").text(ingredients[i].trim() + ".")
            $("#recipe-ingredients").append(li)
        }
    }
}