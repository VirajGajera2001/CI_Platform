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