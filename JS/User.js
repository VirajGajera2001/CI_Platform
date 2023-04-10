// Get references to the necessary DOM elements
const addSkillDiv = document.querySelector('.addskill');
const addedSkillDiv = document.querySelector('.addedskill');
const skillDiv = document.querySelector('.skilldiv');
const saveChangesButton = document.querySelector('.skillsavebtn');

// Store the selected skills in a Set object
const selectedSkills = new Set();

// Add click event listeners to each skill in the "addskill" div
addSkillDiv.addEventListener('click', (event) => {
  if (event.target.tagName === 'P') {
    // Get the skill name
    const skillName = event.target.textContent;
    if (selectedSkills.has(skillName)) {
      // Alert if skill is already added
      alert(`${skillName} is already added`);
    } else {
      // Add the skill to the "addedskill" div
      addedSkillDiv.innerHTML += `<p>${skillName}</p>`;
      // Add the skill to the selected skills set
      selectedSkills.add(skillName);
    }
  }
});

// Add click event listener to the "Save changes" button
saveChangesButton.addEventListener('click', () => {
  // Add all the skills from the "addedskill" div to the "skilldiv"
  addedSkillDiv.querySelectorAll('p').forEach((skill) => {
    skillDiv.innerHTML += `<p>${skill.textContent}</p>`;
  });
  // Clear the "addedskill" div
  addedSkillDiv.innerHTML = '';
  // Clear the selected skills set
  selectedSkills.clear();
});


const profileImg = document.querySelector('#profileImg');
const profileImgInput = document.querySelector('#profile-img-input');

profileImgInput.addEventListener('change', () => {
    const file = profileImgInput.files[0];
    if (file) {
      const reader = new FileReader();
      reader.addEventListener('load', () => {
        profileImg.value = reader.result;
      });
      reader.readAsDataURL(file);
    }
  });
  