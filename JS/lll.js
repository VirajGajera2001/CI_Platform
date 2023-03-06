// Get references to the checkboxes and card container
const checkboxes = document.querySelectorAll('input[type=checkbox]');
const cardsContainer = document.getElementById('cards');

// Listen for changes to the checkboxes
checkboxes.forEach(checkbox => {
  checkbox.addEventListener('change', filterCards);
});

// Filter the cards based on the selected checkboxes
function filterCards() {
  // Get an array of the selected checkbox values
  const selectedValues = Array.from(checkboxes)
    .filter(checkbox => checkbox.checked)
    .map(checkbox => checkbox.value);
  
  // Loop through the cards and hide/show based on the selected checkboxes
  cardsContainer.querySelectorAll('.card').forEach(card => {
    const country = card.getAttribute('data-country');
    const city = card.getAttribute('data-city');
    const theme = card.getAttribute('data-theme');
    const skill = card.getAttribute('data-skill');
    
    if (
      selectedValues.includes(country) &&
      selectedValues.includes(city) &&
      selectedValues.includes(theme) &&
      selectedValues.includes(skill)
    ) {
      card.style.display = 'block';
    } else {
      card.style.display = 'none';
    }
  });
}
