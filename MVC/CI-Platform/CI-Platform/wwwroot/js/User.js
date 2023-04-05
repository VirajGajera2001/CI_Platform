// Get the addskill, addedskill, and skilldiv elements
const addskill = document.querySelector('.addskill');
const addedskill = document.querySelector('.addedskill');
const skilldiv = document.querySelector('.skilldiv');
const selectedSkills = new Set();

// Add a click event listener to the addskill div
addskill.addEventListener('click', (event) => {
    // Check if the clicked element is a skill name
    if (event.target.tagName === 'P') {
        // Check if the skill name has already been added
        const skillName = event.target.textContent;
        if (selectedSkills.has(skillName)) {
            // Alert if skill is already added
            alert(`${skillName} is already added`);
        } else {
            // Clone the clicked p tag and append it to the addedskill div
            const clone = event.target.cloneNode(true);
            addedskill.appendChild(clone);
            selectedSkills.add(skillName);
        }
    }
});

addedskill.addEventListener('click', (event) => {
    // Check if the clicked element is a skill name
    if (event.target.tagName === 'P') {
        // Remove the clicked p tag from the addedskill div
        event.target.remove();
        // Remove the skill name from the selectedSkills set
        selectedSkills.delete(event.target.textContent);
    }
});



// Add a click event listener to the save button
const saveBtn = document.querySelector('.skillsavebtn');
saveBtn.addEventListener('click', () => {
    // Get all the skill names in the addedskill div
    const addedSkills = addedskill.querySelectorAll('p');
    // Append each skill name to the skilldiv
    addedSkills.forEach((skill) => {
        // Check if the skill already exists in the skilldiv
        if (skilldiv.innerHTML.includes(skill.textContent)) {
            // Alert if skill is already added
            alert(`${skill.textContent} is already added`);
        } else {
            const clone = skill.cloneNode(true);
            clone.setAttribute('id', 'skills');
            skilldiv.appendChild(clone);
        }
    });
});







const profileImg = document.querySelector('.userimg');
const profileImgInput = document.querySelector('#profile-img-input');
const profhidden = document.querySelector(".imghidden");
profileImg.addEventListener('click', () => {
  profileImgInput.click();
});

profileImgInput.addEventListener('change', () => {
    const file = profileImgInput.files[0];
    if (file) {
      const reader = new FileReader();
      reader.addEventListener('load', () => {
          profileImg.src = reader.result;
          console.log(reader.result);
          profhidden.value = reader.result;
      });
      reader.readAsDataURL(file);
    }
  });

function sendskill() {
    const skill = Array.from(document.querySelectorAll('#skills')).map(el => el.getAttribute('value'));
    console.log(skill);
    $.ajax({
        url: '/Home/UserEdit',
        type: 'POST',
        data: { skill: skill },
        success: function (result) {
            document.getElementById("userform").submit();
        }
    });
}

function changePass() {
    const old = document.getElementById("oldpass").value;
    const newp = document.getElementById("newpass").value;
    const confp = document.getElementById("confirmpass").value;
    $.ajax({
        url: '/Home/passEdit',
        type: 'POST',
        data: { old: old, newp: newp, confp: confp },
        success: function (result) {
            if (result.success) {
                Swal.fire(
                    'Your Password is Changed Successfully'
                );
            }
            else {
                Swal.fire(
                    'Your Password is Not Match, Please Enter Correct Old Password'
                );
            }
        }
    });
}
