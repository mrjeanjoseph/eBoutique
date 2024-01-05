
const gallery = document.getElementById("gallery");
const popup = document.getElementById("popup");
const selectedImage = document.getElementById("selectedImage");
const imageIndexes = [1, 2, 3, 4, 5, 6];
indexes = [];
//const selectedIndexes = null;

for(var i = 1; i< 5; i++ ) {	
    console.log(`${i},`);	
    indexes += `${i},`;
}
console.log(indexes.length);

imageIndexes.forEach((i) => {

    let image = document.createElement("img");
    image.src = `./images/img${i}.jpg`;
    image.alt = `Image ${i} is unavailable`;
    image.classList.add("galleryImg");

    image.addEventListener("click", () => {
        //Popup stuff --
        popup.style.transform = `translateY(0)`;
        selectedImage.src = `./images/img${i}.jpg`;
        image.alt = `image ${i} is unavailable`;
    });
    gallery.appendChild(image);
});

//Clicking on the UI gets rid of the image
popup.addEventListener("click", () => {
    popup.style.transform = "translateY(-100%)";
    popup.src = "";
    popup.alt = "";
});