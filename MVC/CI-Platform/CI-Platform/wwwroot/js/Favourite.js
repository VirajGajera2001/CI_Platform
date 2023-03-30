var slideIndex = 1;
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
    if (n > slides.length) { slideIndex = 1 }
    if (n < 1) { slideIndex = slides.length }
    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }
    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }
    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
    captionText.innerHTML = dots[slideIndex - 1].alt;
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
                location.reload();
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
                location.reload();
            }
            
    },
        error: function () {
            alert("could not like mission");
        }
    });
}

function addFav(missionId, Id) {
    $.ajax({
        url: '/Home/AddFav',
        type: 'POST',
        data: { missionId: missionId, Id: Id},
        success: function (result) {
            if (result.isLiked) {
                var favbtn = document.getElementById("favoicon");
                favbtn.style.color = "black";
            }
            else {
                var favbtn = document.getElementById("favoicon");
                favbtn.style.color ="#F88634"
            }
            location.reload();
        },
        error: function () {
            alert("could not like mission");
        }
        })
}

function sendRec(missionId) {
    const toMail = Array.from(document.querySelectorAll('input[name="Checkme"]:checked')).map(el => el.value);
    console.log(toMail);
    $.ajax({
        url: '/Home/SendRec',
        type: 'POST',
        data: { missionId: missionId, ToMail: toMail },
        success: function (result) {
            alert("send");
        }
    });
}

function recentVol(pge,MissionId) {
    $.ajax({
        url: '/Home/RecentVol',
        type: 'POST',
        data: { pg: pge, MissionId: MissionId },
        success: function (result) {
            $('#recentvol').html(result);
        },
        error: function (result) {
            debugger
            alert("could not like recentvol");
        }
    });   
}
