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
			image.dataset.id = id;
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

			  removeButton.addEventListener('click', function () {
				  // Find the index of the file object in the array
				  const index = files.findIndex(f => f.id === id);

				  // If the file object exists in the array, remove it
				  if (index !== -1) {
					  files.splice(index, 1);
				  }

				  // Remove the image container from the preview
				  imageContainer.remove();
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
function shareStory(userId,value) {
	var dataUrls = files.map(file => file.dataUrl);
	console.log(value);
	var missionId = document.getElementById("Missionname").value;
	var title = document.getElementById("StoryTitle").value;
	var date = document.getElementById("releasedate").value;
	var uid = userId;
	var previewbtn = document.querySelector('.previewbtn');
	var editor = CKEDITOR.instances.editor1;
	var editordata = editor.getData();
	$.ajax({
		url: '/Home/Share_Storys',
		type: 'POST',
		data: { "Image": dataUrls, "MissionId": missionId, "Title": title, "Date": date, "Description": editordata, "UserId": userId, "Value": value },
		success: function (result) {
			if (result.success) {
				Swal.fire(
					'Your Story Is Added Successfully'
				);
				
				previewbtn.classList.remove("d-none");
				previewbtn.id = result.storyid;
				const url = `/Home/Story_Detail?StoryId=${result.storyid}&UserId=${userId}`;

				// Set the "href" attribute of the "prw" element to the constructed URL
				previewbtn.href = url;
			}
			else {
				Swal.fire(
					'Your story is added!'
				).then((res) => {
					location.reload();
				});
			}	
		}
		
	});
}
function searchStoryByMission(UserId) {
	while (files.length > 0) {
		files.pop();
	}
	var missionId = document.getElementById("Missionname").value;
	$.ajax({
		url: '/Home/StoryEdit',
		type: 'POST',
		data: { MissionId: missionId, UserId: UserId },
		success: function (result) {
			if (result.success) {
				var story = result.story;
				$('#StoryTitle').val(story.title);
				var date = new Date(story.publishedAt);
				var formattedDate = date.getFullYear() + "-" + (date.getMonth() + 1).toString().padStart(2, '0') + "-" + date.getDate().toString().padStart(2, '0');
				$('#releasedate').val(formattedDate);
				CKEDITOR.instances['editor1'].setData(story.storyDescription);
				var media = result.storyimage;
				$('.image-preview').empty();
				media.forEach(function (medium) {
					var mediumPath = medium.path;
					const id = Date.now();
					files.push({ dataUrl: mediumPath, id });
					addImage(mediumPath, id);
				});
			}
			else if (result.success == "notadded") {

			}
			else {
				Swal.fire(
					'Your story is already shared!',
					'Please Select Another Mission'
				).then((res) => {
					location.reload();
				});
			}
		},
		error: function () {
			alert("could not like mission");
		}
	});
}

	