let slideIndex = 1;
showSlides(slideIndex);

// Next/previous controls
function plusSlides(n) {
  showSlides(slideIndex += n);
}

// Thumbnail image controls
function currentSlide(n) {
  showSlides(slideIndex = n);
}

function showSlides(n) {
  let i;
  let slides = document.getElementsByClassName("mySlides");
  let dots = document.getElementsByClassName("demo");
  let captionText = document.getElementById("caption");
  if (n > slides.length) {slideIndex = 1}
  if (n < 1) {slideIndex = slides.length}
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";
  }
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex-1].style.display = "block";
  dots[slideIndex-1].className += " active";
  captionText.innerHTML = dots[slideIndex-1].alt;
}

function addRating(starId,missionId, Id) {
    $.ajax({
        url: '/Home/Addrating',
        type: 'POST',
        data: { missionId: missionId, Id: Id, rating: starId },
        success: function (result) {
            if (parseInt(result.ratingExists.rating, 10)) {
                for (i = 1; i <= parseInt(result.ratingExists.rating, 10); i++) {
                    var starbtn = document.getElementById(String(i));
                    starbtn.style.color = "#F88634";
                }
                for (i = parseInt(result.ratingExists.rating, 10) + 1; i <= 5; i++) {
                    var starbtn = document.getElementById(String(i));
                    starbtn.style.color = "black";
                }
            }
            else {
                for (i = 1; i <= parseInt(result.newRating.rating, 10); i++) {
                    var starbtn = document.getElementById(String(i));
                    starbtn.style.color = "#F88634";
                }
                for (i = parseInt(result.ratingExists.rating, 10) + 1; i <= 5; i++) {
                    var starbtn = document.getElementById(String(i));
                    starbtn.style.color = "black";
                }
            }
    },
        error: function () {
            alert("could not like mission");
        }
    });
}
