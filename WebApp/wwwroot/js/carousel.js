function showImage(carouselId, currentImageIndex) {
    var carousel = document.getElementById(carouselId);
    if (carousel) {
        var carouselItems = carousel.querySelectorAll('.carousel-item');

        carouselItems.forEach(function (item, index) {
            item.style.display = index === currentImageIndex ? 'block' : 'none';
        });
    } else {
        console.error('Carousel element not found: ' + carouselId);
    }
}

function showPreviousImage(carouselId) {
    var totalImages = getTotalImages(carouselId);
    var currentImageIndex = getCurrentImageIndex(carouselId);
    currentImageIndex = (currentImageIndex - 1 + totalImages) % totalImages;
    showImage(carouselId, currentImageIndex);
}

function showNextImage(carouselId) {
    var totalImages = getTotalImages(carouselId);
    var currentImageIndex = getCurrentImageIndex(carouselId);
    currentImageIndex = (currentImageIndex + 1) % totalImages;
    showImage(carouselId, currentImageIndex);
}

function getCurrentImageIndex(carouselId) {
    var carousel = document.getElementById(carouselId);
    var carouselItems = carousel ? carousel.querySelectorAll('.carousel-item') : [];
    for (var i = 0; i < carouselItems.length; i++) {
        if (carouselItems[i].style.display === 'block') {
            return i;
        }
    }
    return 0;
}

function getTotalImages(carouselId) {
    var carousel = document.getElementById(carouselId);
    var carouselItems = carousel ? carousel.querySelectorAll('.carousel-item') : [];
    return carouselItems.length;
}

document.addEventListener("DOMContentLoaded", function () {
    showImage('carousel_@Model.Cars.First().CarId', 0);
});
