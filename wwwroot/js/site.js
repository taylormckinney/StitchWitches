// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/**
 * Filtering on InventoryItem/Index page
 */

function filterItems(category) {

    //filtering items based on category:
    var items = document.getElementsByClassName("indexItem");
    for (var item of items){
        if (item.classList.contains(category) || category == "All") {
            item.style.display = "block";
        }
        else {
            item.style.display = "none";
        }
    }

    activeButtonStyling(category);

} //ends filterItems function


function checkStock() {
    var items = document.getElementsByClassName("indexItem");
    for (var item of items) {
        if (item.classList.contains("InStock")) {
            item.style.display = "block";
        }
        else {
            item.style.display = "none";
        }

        activeButtonStyling("InStock");
    }
}

function activeButtonStyling(selected) {
    var btns = document.getElementsByClassName("filterButton");
    for (var btn of btns) {

        if (btn.id == selected) {
            btn.classList.add('active');
        }
        else {
            btn.classList.remove('active');
        }

    }
}