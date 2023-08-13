const images = document.querySelectorAll('.slider img');
const prevButton = document.querySelector('.slider-nav.prev');
const nextButton = document.querySelector('.slider-nav.next');
let currentIndex = 0;

function showImage(index) {
    images.forEach(image => {
        image.style.display = 'none';
    });
    images[index].style.display = 'block';
}

function navigate(direction) {
    if (direction === 'prev') {
        currentIndex = (currentIndex - 1 + images.length) % images.length;
    }

    else if (direction === 'next') {
        currentIndex = (currentIndex + 1) % images.length;
    }

    showImage(currentIndex);
}

prevButton.addEventListener('click', () => navigate('prev'));
nextButton.addEventListener('click', () => navigate('next'));

// Automatic image switching every 3 seconds
function changeImage() {
    currentIndex = (currentIndex + 1) % images.length;
    showImage(currentIndex);
}

setInterval(changeImage, 3000);

showImage(currentIndex); // Show the initial image
