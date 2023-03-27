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
			console.log(files);
		  removeButton.addEventListener('click', function() {
  		  const index = files.findIndex(f => f.id === id);
          if (index !== -1) files.splice(index, 1);
          imageContainer.remove();
			  fileInput.value = ''; // Reset the file input field

			  removeButton.addEventListener('click', function () {
				  // Find the index of the file object in the array
				  const index = files.findIndex(f => f.id === id);

				  // If the file object exists in the array, remove it
				  if (index !== -1) {
					  files.splice(index, 1);
				  }

				  // Remove the image container from the preview
				  imageContainer.remove();
				  console.log(files);
				  // Reset the file input field
				  fileInput.value = '';
				  
			  });

          });

}

function dropHandler(ev) {

	// Prevent default behavior (Prevent file from being opened)
	ev.preventDefault();

	if (ev.dataTransfer.items) {
		// Use DataTransferItemList interface to access the file(s)
		[...ev.dataTransfer.items].forEach((item, i) => {
			// If dropped items aren't files, reject them
			if (item.kind === "file") {
				const file = item.getAsFile();
				
				const id = Date.now();

				// Create a FileReader object to read the contents of the dropped file
				const reader = new FileReader();
				reader.onload = function () {
					// Print the URL of the dropped file

					files.push({ name: file.name, size: file.size, dataUrl:reader.result, id });
					addImage(reader.result, id);
				};
				reader.readAsDataURL(file);
			}
		});
	} else {
		// Use DataTransfer interface to access the file(s)
		[...ev.dataTransfer.files].forEach((file, i) => {
			
		});
	}
}

function dragOverHandler(ev) {

	// Prevent default behavior (Prevent file from being opened)
	ev.preventDefault();
}
function shareStory(userId) {
	var dataUrls = files.map(file => file.dataUrl);
	var missionId = document.getElementById("Missionname").value;
	var title = document.getElementById("storytitle").value;
	var date = document.getElementById("releasedate").value;
	var uid = userId;
	console.log(uid);
	var editor = CKEDITOR.instances.editor1;
	var editordata = editor.getData();
	console.log(editordata);
	console.log(missionId);
	console.log(title);
	console.log(date);
	$.ajax({
		url: '/Home/Share_Story',
		type: 'POST',
		data: { "Image": dataUrls, "MissionId": missionId, "Title": title, "Date": date, "Description": editordata, "UserId": userId },
		success: function (result) {
			if (result.success) {
				alert("Your Story Is Added Successfully");
				
			}
			else {
				alert("You alreay share the story");
				
			}
			location.reload();
		}
		
	});
}
