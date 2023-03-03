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
        });
        filtersSection.appendChild(li);
      } else {
        const lis = document.getElementById(this.value);
        lis.remove();
      }
    });
  }
  clearBtn.addEventListener("click", function() {
    const lis = filtersSection.getElementsByTagName('li');
    for (let i = lis.length - 1; i >= 0; i--) {
      lis[i].click();
    }
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

