var element = document.getElementsByClassName("allviews");
var img = document.getElementsByClassName("card-image");
var card = document.getElementsByClassName("cardview");
var imgBadge1 = document.getElementsByClassName("imgbadge1");
var dataBadge1 = document.getElementsByClassName("datebadge1");
var i;
var j;

//var tosort = document.querySelectorAll('#sortBy > li');
//tosort.forEach((ele) => {
//    ele.addEventListener("click", )
//})

document.addEventListener("DOMContentLoaded", function () {
    const cbs = document.querySelectorAll("input[type='checkbox']");
    const filtersSection = document.querySelector(".filtersection");
    const clearBtn = document.querySelector("#clearBtn");
    for (let k = 0; k < cbs.length; k++) {
        cbs[k].addEventListener("change", function () {
            if (this.checked) {
                const li = document.createElement("li");
                li.innerHTML = this.name + `<img src="../CI-Imgs/cancel.png" alt="" class="ms-2" >`;
                li.id = this.value;
                li.addEventListener("click", function () {
                    li.remove();
                    cbs[k].checked = false;
                    searchMission();
                    // Hide clear button if no 'li' elements are present
                    if (filtersSection.getElementsByTagName('li').length === 0) {
                        clearBtn.style.display = 'none';
                    }
                });
                filtersSection.appendChild(li);
                // Display clear button if at least one 'li' element is present
                clearBtn.style.display = 'block';
            } else {
                const lis = document.getElementById(this.value);
                lis.remove();
                // Hide clear button if no 'li' elements are present
                if (filtersSection.getElementsByTagName('li').length === 0) {
                    clearBtn.style.display = 'none';
                }
            }
        });
    }
    clearBtn.addEventListener("click", function () {
        const lis = filtersSection.getElementsByTagName('li');
        for (let i = lis.length - 1; i >= 0; i--) {
            lis[i].click();
        }
        // Hide clear button after removing all 'li' elements
        clearBtn.style.display = 'none';
    });
});

function listView() {
    for (i = 0; i < element.length; i++) {
        img[i].style.width = "30%";
        card[i].style.width = "100%";
        element[i].classList.add("col-12");
        element[i].classList.remove("col-lg-4", "col-md-6", "col-sm-12");
        card[i].style.flexDirection = "row";
        imgBadge1[i].style.display = "none";
        dataBadge1[i].style.display = "none";
    }
}
function gridView() {
    for (j = 0; j < element.length; j++) {
        card[j].style.flexDirection = "column";
        element[j].classList.remove("col-12");
        element[j].classList.add("col-lg-4", "col-md-6", "col-sm-12");
        img[j].style.width = "100%";
        card[j].style.width = "90%";
        card[j].style.height = "95%"
        imgBadge1[j].style.display = "block";
        dataBadge1[j].style.display = "block";
    }
}
var sortBy = "";
document.querySelectorAll("#sortBy > li").forEach((ele) => {
    ele.addEventListener("click", (evt) => {
        sortBy = evt.target.getAttribute("value");
        searchMission();
    });
});

function searchMission(pge) {
    let val = document.getElementById("filter-input").value.toLowerCase();
    var toCoun1 = Array.from(document.querySelectorAll('input[id="Checkme1"]:checked')).map(el => el.value);
    var toCity2 = Array.from(document.querySelectorAll('input[id="Checkme2"]:checked')).map(el => el.value);
    var toTheme2 = Array.from(document.querySelectorAll('input[id="Checkme3"]:checked')).map(el => el.value);
    console.log(sortBy);
    $.ajax({
        url: '/Home/Landing',
        type: 'POST',
        data: { Title: val, pg: pge, "ToCountry": toCoun1, "ToCity": toCity2, "ToTheme": toTheme2, "sortValue": sortBy },
        success: function (result) {
            $('#gridview').html(result);
        },
        error: function () {
            alert("errors");
        }
    });
}


    
