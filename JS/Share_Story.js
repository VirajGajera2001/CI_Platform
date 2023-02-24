CKEDITOR.replace( 'editor1' );
const fileInput = document.querySelector('#image-upload');
		const imagePreview = document.querySelector('.image-preview');
		const files = [];
	  
		fileInput.addEventListener('change', function() {
		  const file = this.files[0];
		  const reader = new FileReader();
	  
		  const existingFile = files.find(f => f.name === file.name && f.size === file.size);
		  if (existingFile) {
			addImage(existingFile.dataUrl, existingFile.id);
			return;
		  }
	  
		  reader.addEventListener('load', function() {
			const dataUrl = this.result;
			const id = Date.now();
			files.push({ name: file.name, size: file.size, dataUrl, id });
			addImage(dataUrl, id);
		  });
	  
		  reader.readAsDataURL(file);
		});
	  
		function addImage(dataUrl, id) {
		  const imageContainer = document.createElement('div');
		  const image = new Image();
		  const removeButton = document.createElement('button');
	  
		  image.src = dataUrl;
		  imageContainer.classList.add('image-container');
		  imageContainer.dataset.id = id;
		  removeButton.classList.add('remove-button');
		  removeButton.innerHTML = '<span>X</span>';
		  imageContainer.appendChild(image);
		  imageContainer.appendChild(removeButton);
		  imagePreview.appendChild(imageContainer);
	  
		  removeButton.addEventListener('click', function() {
  		  const index = files.findIndex(f => f.id === id);
          if (index !== -1) files.splice(index, 1);
          imageContainer.remove();
          fileInput.value = ''; // Reset the file input field
          });

		}