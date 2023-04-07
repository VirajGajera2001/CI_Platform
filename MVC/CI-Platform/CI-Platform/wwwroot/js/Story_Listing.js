document.addEventListener("DOMContentLoaded", function () {
    const cbs = document.querySelectorAll("input[type='checkbox']");
    const filtersSection = document.querySelector(".filtersection");
    const clearcontainSection = document.querySelector(".clearcontain");
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
                    searchStory();
                    // Hide clear button if no 'li' elements are present
                    if (filtersSection.getElementsByTagName('li').length === 0) {
                        clearBtn.style.display = 'none';
                        clearcontainSection.style.display = "none";
                    }
                });
                filtersSection.appendChild(li);
                // Display clear button if at least one 'li' element is present
                clearBtn.style.display = 'block';
                clearcontainSection.style.display = 'block';
            } else {
                const lis = document.getElementById(this.value);
                lis.remove();
                // Hide clear button if no 'li' elements are present
                if (filtersSection.getElementsByTagName('li').length === 0) {
                    clearBtn.style.display = 'none';
                    clearcontainSection.style.display = 'none';
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
        clearcontainSection.style.display = 'none';
    });
});  

function searchStory(pge) {
    let val = document.getElementById("filter-input").value.toLowerCase();
    var toCoun1 = Array.from(document.querySelectorAll('input[id="Checkme1"]:checked')).map(el => el.value);
    var toCity2 = Array.from(document.querySelectorAll('input[id="Checkme2"]:checked')).map(el => el.value);
    var toTheme2 = Array.from(document.querySelectorAll('input[id="Checkme3"]:checked')).map(el => el.value);
    $.ajax({
        url: '/Home/Story_Listing',
        type: 'POST',
        data: { "StorySearch": val, pg: pge, "ToCountry": toCoun1, "ToCity": toCity2, "ToTheme": toTheme2 },
        success: function (result) {
            $('#gridview').html(result);
        }

        });
}
