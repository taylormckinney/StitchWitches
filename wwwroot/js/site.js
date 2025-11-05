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
    /*
    //menu button styling on click: 
    var btns = document.getElementsByClassName("filterButton");
    Array.from(btns).foreach((btn) => {
        if (btn.id == category) {
            btn.classList.add('selected');
        }
        else {
            btn.classList.remove('selected');
        }
    }); */

} //ends func


