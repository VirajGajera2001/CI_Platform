var element=document.getElementsByClassName("allviews");
var img=document.getElementsByClassName("card-image");
var card=document.getElementsByClassName("cardview");
var imgBadge1=document.getElementsByClassName("imgbadge1");
var dataBadge1=document.getElementsByClassName("datebadge1");
var i;
var j;

document.addEventListener("DOMContentLoaded", function() {
  const cbs = document.querySelectorAll("input[type='checkbox']");
  const filtersSection = document.querySelector(".filtersection");
  const clearBtn = document.querySelector("#clearBtn");
  for (let k = 0; k < cbs.length; k++) {
    cbs[k].addEventListener("change", function() {
      if (this.checked) {
        const li = document.createElement("li");
        li.innerHTML=  this.value + `<img src="../CI-Imgs/cancel.png" alt="" class="ms-2" >`;
        li.id = this.value;
        li.addEventListener("click", function() {
          li.remove();
          cbs[k].checked = false;
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
  clearBtn.addEventListener("click", function() {
    const lis = filtersSection.getElementsByTagName('li');
    for (let i = lis.length - 1; i >= 0; i--) {
      lis[i].click();
    }
    // Hide clear button after removing all 'li' elements
    clearBtn.style.display = 'none';
  });
});

  
  
function listView(){
  for(i=0;i<element.length;i++){
    img[i].style.width="30%";
    card[i].style.width="100%";
    element[i].classList.add("col-12");
    element[i].classList.remove("col-lg-4", "col-md-6", "col-sm-12");
    card[i].style.flexDirection="row";
    imgBadge1[i].style.display="none";
    dataBadge1[i].style.display="none";
  }
}
function gridView(){
  for(j=0;j<element.length;j++){
    card[j].style.flexDirection="column";
    element[j].classList.remove("col-12");
    element[j].classList.add("col-lg-4", "col-md-6", "col-sm-12");
    img[j].style.width="100%";
    card[j].style.width="18rem"
    imgBadge1[j].style.display="block";
    dataBadge1[j].style.display="block";
    imgBadge1[j].style.top="160px"
  }
}
function updateOutput() {
  var filter = document.getElementById("filter-input").value;
  var output = document.getElementById("gridview");
  var jsonData =document.getElementsByClassName("bagdata").innerHTML;
  output.innerHTML = "";

  if (filter === "") {
      jsonData.forEach(student => {
          var divs = document.createElement("div");
          divs.classList.add("col-lg-4", "col-md-6", "col-sm-12", "allviews", "p-0");
          divs.innerHTML = `<div class="card cardview mx-auto" style="width: 19rem;">
          <img src="../CI-Imgs/Grow-Trees-On-the-path-to-environment-sustainability-2.png" alt="" class="card-image">
          <div class="imgbadge1 bg-white">
              <p class="ms-3">Environment</p>
          </div>
          <div class="card-body">
                          <h5>`+student.Title+`</h5>
                          <p>`+student.Description+`</p>
              <div class="desc1 d-flex">
                  <div>
                                  <p>`+student.ShortDescription+`</p>
                  </div>
                  <div class="star d-flex ms-auto">
                      <img src="../CI-Imgs/selected-star.png" alt="">
                      <img src="../CI-Imgs/selected-star.png" alt="">
                      <img src="../CI-Imgs/selected-star.png" alt="">
                      <img src="../CI-Imgs/star.png" alt="">
                  </div>
              </div>
              <div class="bg-white datebadge1">
                              <p class="ms-2">From`+student.StartDate+`until`+student.EndDate+`</p>
              </div>
              <hr>
              <div class="desc2 d-flex">
                  <div class="seats d-flex">
                      <img src="../CI-Imgs/Seats-left.png" alt="">
                      <p>10 seats left</p>
                  </div>
                  <div class="hours d-flex ms-auto">
                      <img src="../CI-Imgs/hours.png" alt="">
                      <p>`+student.EndDate+`<br>Deadline</p>
                  </div>
              </div>
              <hr style="margin-top: 30px;">
              <div class="apply d-flex">
                  <a asp-controller="Home" asp-action="Volunteering_Mission" class="btn btn-outline-warning mx-auto">Apply<img src="../CI-Imgs/right-arrow.png" alt=""></a>
              </div>
          </div>
      </div>`;
          output.appendChild(divs);
      });
  } else {
      var filteredData = jsonData.filter(student => {
          return student.Name.toLowerCase().startsWith(filter.toLowerCase());
      });

      filteredData.forEach(student => {
          var divs = document.createElement("div");
          divs.classList.add("col-lg-4", "col-md-6", "col-sm-12", "allviews", "p-0");
          divs.innerHTML = `<div class="card cardview mx-auto" style="width: 19rem;">
          <img src="../CI-Imgs/Grow-Trees-On-the-path-to-environment-sustainability-2.png" alt="" class="card-image">
          <div class="imgbadge1 bg-white">
              <p class="ms-3">Environment</p>
          </div>
          <div class="card-body">
                          <h5>`+student.Title+`</h5>
                          <p>`+student.Description+`</p>
              <div class="desc1 d-flex">
                  <div>
                                  <p>`+student.ShortDescription+`</p>
                  </div>
                  <div class="star d-flex ms-auto">
                      <img src="../CI-Imgs/selected-star.png" alt="">
                      <img src="../CI-Imgs/selected-star.png" alt="">
                      <img src="../CI-Imgs/selected-star.png" alt="">
                      <img src="../CI-Imgs/star.png" alt="">
                  </div>
              </div>
              <div class="bg-white datebadge1">
                              <p class="ms-2">From`+student.StartDate+`until`+student.EndDate+`</p>
              </div>
              <hr>
              <div class="desc2 d-flex">
                  <div class="seats d-flex">
                      <img src="../CI-Imgs/Seats-left.png" alt="">
                      <p>10 seats left</p>
                  </div>
                  <div class="hours d-flex ms-auto">
                      <img src="../CI-Imgs/hours.png" alt="">
                      <p>`+student.EndDate+`<br>Deadline</p>
                  </div>
              </div>
              <hr style="margin-top: 30px;">
              <div class="apply d-flex">
                  <a asp-controller="Home" asp-action="Volunteering_Mission" class="btn btn-outline-warning mx-auto">Apply<img src="../CI-Imgs/right-arrow.png" alt=""></a>
              </div>
          </div>
      </div>`;
          output.appendChild(divs);
      });
  }
}

updateOutput();

// Bind the updateOutput function to the input event of the filter input box
var filterInput = document.getElementById("filter-input");
filterInput.addEventListener("input", updateOutput);