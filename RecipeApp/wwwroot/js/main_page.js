"use strict";

let recipes = []
let categories = []
$(document).ready(function () {
    $.ajax({
        url: '/Home/GetRecipes',
        method: 'get',
        success: function (data) {
            recipes = data.recipes
            categories = data.categoryNames
            createCategories()
            createRecipes()
        },
        error: function (req, status, err) {
            alert(req.responseText)
        }
    });

    $("#show-all").click(showAllCategories)
    $("#most-viewed").click(filterByMostView)
    $("#last-viewed").click(filterByLastView)
    
});

function filterByMostView() {
    $(".ct-container").remove();
    $.ajax({
        url: '/Home/GetRecipes/?filter=most',
        method: 'get',
        success: function (data) {
            recipes = data
            createRecipes()
        },
        error: function (req, status, err) {
            alert(req.responseText)
        }
    });
}

function filterByLastView() {
    $(".ct-container").remove();
    $.ajax({
        url: '/Home/GetRecipes/?filter=last',
        method: 'get',
        success: function (data) {
            recipes = data
            createRecipes()
        },
        error: function (req, status, err) {
            alert(req.responseText)
        }
    });
}

function showAllCategories() {
    categories.forEach(category => $(`.${category}`).show())
}

function handleFilterCategories(e) {
    $(`.${e.target.innerText}`).show();
    const filteredCategories = categories.filter(category => category !== e.target.innerText)
    filteredCategories.forEach(category => $(`.${category}`).hide())
}

function createCategories() {
    for (let i = 0; i < categories.length; i++) {
        const div = $("<div></div>");
        const subDiv = $("<div></div>").addClass("category-item text-center p-3 m-1").text(categories[i]);
        subDiv.click(handleFilterCategories)
        div.append(subDiv);
        $("#category-items").append(div);
    }
}

function createRecipes() {
    for (let i = 0; i < recipes.length; i++) {
        const div = $("<div></div>").addClass("col-lg-4 col-md-6 col-sm-12 wow fadeIn ct-container").addClass(recipes[i].category);
        const subDiv = $("<div></div>").addClass("recipe-item text-center");
        const anchor = $("<a></a>").attr("href", "recipe/" + recipes[i].slug);
        const image = $("<img/>").attr("src", recipes[i].imagePath);
        const h2 = $("<h2></h2>").text(recipes[i].name)
        const h5 = $("<h5></h5>").text(recipes[i].category)

        anchor.append(image);
        anchor.append(h2);
        anchor.append(h5);

        subDiv.append(anchor)

        div.append(subDiv);

        $("#recipe-items").append(div);
    }
}
