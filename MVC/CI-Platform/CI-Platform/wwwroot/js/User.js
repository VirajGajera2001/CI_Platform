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
        event.target.style.backgroundColor = '#F0F0F0';
    }
});

addedskill.addEventListener('click', (event) => {
    // Check if the clicked element is a skill name
    if (event.target.tagName === 'P') {
        // Remove the clicked p tag from the addedskill div
       
        // Remove the skill name from the selectedSkills set
        event.target.style.backgroundColor = '#F0F0F0';
    }
});

const addButton = document.querySelector('#add');
addButton.addEventListener('click', () => {
    // Get all selected p tags in the addedskill div
    const selectedPTags = addskill.querySelectorAll('p.selected');
    // Perform action for each selected p tag
    selectedPTags.forEach((pTag) => {

        // Clone the selected p tag and append it to skilldiv
        const clone = pTag.cloneNode(true);

        let alreadyExists = false;
        addedskill.childNodes.forEach((node) => {
            if (node.nodeType === Node.ELEMENT_NODE && node.textContent === clone.textContent) {
                alreadyExists = true;
                return;
            }
        });
        if (!alreadyExists) {
            // Append the cloned p tag to the addedskill div
            addedskill.appendChild(clone);
            // Remove the 'selected' class from the cloned p tag in the addskill div
            pTag.classList.remove('selected');
            // Remove the 'selected' class from the cloned p tag in the addedskill div
            clone.classList.remove('selected');
            clone.style.backgroundColor = 'white';
            clone.setAttribute("class", "skillsname");
            // Remove the original p tag from the addskill div
            pTag.remove();
        }
    });
});

// Add click event listener to p tags in the addedskill div to toggle selected class
addskill.addEventListener('click', (event) => {
    if (event.target.tagName === 'P') {
        event.target.classList.toggle('selected');
    }
});



// Add click event listener to a button to add selected p tags
const removeButton = document.querySelector('#remove');
removeButton.addEventListener('click', () => {
    // Get all selected p tags in the addedskill div
    const selectedPTags = addedskill.querySelectorAll('p.selected');
    // Perform action for each selected p tag
    selectedPTags.forEach((pTag) => {
        const clone = pTag.cloneNode(true);


        let alreadyExists = false;
        addskill.childNodes.forEach((node) => {
            if (node.nodeType === Node.ELEMENT_NODE && node.textContent === clone.textContent) {
                alreadyExists = true;
                return;
            }
        });
        if (!alreadyExists) {
            addskill.appendChild(clone);
            // Remove the 'selected' class from the cloned p tag in the addskill div
            pTag.classList.remove('selected');

            // Remove the 'selected' class from the cloned p tag in the addedskill div
            clone.classList.remove('selected');
            pTag.style.backgroundColor = 'white';
            // Clone the selected p tag and append it to skilldiv

            // Remove the 'selected' class from the cloned p tag in the addskill div
        }

        pTag.remove();



    });
});

// Add click event listener to p tags in the addedskill div to toggle selected class
addedskill.addEventListener('click', (event) => {
    if (event.target.tagName === 'P') {
        event.target.classList.toggle('selected');
    }
});

function changePass() {
    const old = document.getElementById("oldpass").value;
    const newp = document.getElementById("newpass").value;
    const confp = document.getElementById("confirmpass").value;
    // Define the regular expression pattern
    const regexPattern = /^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$/;
    if (old === null || old === "") {
        document.getElementById("errorContainerOld").textContent = "Old password is required";
        errorContainer.style.color = "red";
    }
    else if (confp === null || confp === "") {
        document.getElementById("errorContainer").textContent = "Confirm password is required";
        errorContainer.style.color = "red";
    }
    else if (newp === null || newp === "") {
        document.getElementById("errorContainerNew").textContent = "New password is required";
        errorContainer.style.color = "red";
    }
    else if (!regexPattern.test(newp)) {
        document.getElementById("errorContainerNew").textContent = "Password must meet the minimum strength requirements.";;
        errorContainer.style.color = "red";
    }
    else {

        // Check if new password and confirm password match
        if (newp !== confp) {
            // Display error message
            document.getElementById("errorContainer").textContent = "New Password and Confirm Password do not match.";
        } else {
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
    }

}





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
            if (file.type == "image/png" || file.type == "image/jpg" || file.type == "image/jpeg") {
                profileImg.src = reader.result;
                profhidden.value = reader.result;
            }
            else {
                Swal.fire(
                    'Please upload only image'
                    )
            }
      });
      reader.readAsDataURL(file);
    }
  });

function sendskill() {
    const skill = Array.from(document.querySelectorAll('.skillsname')).map(el => el.getAttribute('value'));
    console.log(skill);
    $.ajax({
        url: '/Home/saveSkill',
        type: 'POST',
        data: { skill: skill },
        success: function (result) {
            if (result.success) {
                Swal.fire(
                    'Your skill is added'
                ).then((res) => {
                    location.reload();
                });
            }
        }
    });
}

