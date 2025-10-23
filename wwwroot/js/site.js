// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

/**
 * CAROUSEL OF IMAGES FUNCTIONALITY:
 */
let currentIndex = 0; // Track the current slide index
const items = document.querySelectorAll('.carousel-items img'); // Select all images in the carousel
const totalItems = items.length; // Get the total number of images

// Function to change the slide
function changeSlide(direction) {
    // Hide the current image
    items[currentIndex].style.display = 'none';

    // Update the current index based on the direction
    currentIndex = (currentIndex + direction + totalItems) % totalItems;

    // Show the new current image
    items[currentIndex].style.display = 'block';
}

// Initialize the carousel by displaying the first image
function initializeCarousel() {
    items.forEach((itm, index) => {
        itm.style.display = index === currentIndex ? 'block' : 'none'; // Show only the current image
    });
}

// Start the carousel
initializeCarousel();

