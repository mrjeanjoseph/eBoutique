const ul = document.querySelector("ul");
const input = ul.querySelector("input");
const countTags = document.querySelector(".inputTag-details span");

let maxNumTags = 10;
let tags = [];
createTag();

function numTagTracker() {
    input.focus();
    countTags.innerText = maxNumTags - tags.length; // substracting max value from tags length
}

function createTag() {
    //remove all li tags before adding so there will be no duplicate tags
    ul.querySelectorAll("li").forEach(li => li.remove());
    tags.slice().reverse().forEach(tag => {
        let liTag = `<li>${tag} <i class="uil uil-multiply" onclick="removeTag(this, '${tag}')"></i></li>`;
        ul.insertAdjacentHTML("afterbegin", liTag); //inserting or adding li inside ul tag
    });
    numTagTracker();
}

function removeTag(element, tag) {
    let index = tags.indexOf(tag); //getting removing tag index
    tags = [...tags.slice(0, index), ...tags.slice(index + 1)]; //remove or exclude tag from an array
    element.parentElement.remove();//removing li of removed tags
    numTagTracker();
}

function addTag(e) {
    if (e.key == "Enter") {
        let tag = e.target.value.replace(/\s+/g, ' '); //removing unwanted spaces
        if (tag.length > 1 && !tags.includes(tag)) {// if tag length is greater than 1 and the tag does exists already
            if(tags.length < 10) {
                tag.split(',').forEach(tag => {//splitting each tag from comma (,)
                    tags.push(tag); //adding each tag inside array
                    createTag();
                })
            }
        }
        e.target.value = "";
    }
}

input.addEventListener("keyup", addTag);

const removeAllTags = document.querySelector("button");
removeAllTags.addEventListener("click", () => {
    tags.length = 0; // making tags array empty
    ul.querySelectorAll("li").forEach(li => li.remove()); //removing all li tags
    numTagTracker();
})