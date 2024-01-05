
async function getRandomUser(){
    const response = await fetch("https://randomuser.me/api/");
    const data = await response.json();
    const user = data.results[0];
    // console.log(user);
    
    displayUser(user);
}

function displayUser(user){
    const name = document.querySelector("#name");
    const gender = document.querySelector("#gender");
    const email = document.querySelector("#email");
    const phone = document.querySelector("#phone");
    const location = document.querySelector("#location");
    const image = document.querySelector("#image");

    name.innerText = `${user.name.title} ${user.name.first} ${user.name.last}`;
    gender.innerText = `${user.gender}`;
    email.innerText = `${user.email}`;
    phone.innerText = `${user.phone}`;
    location.innerText = `${user.location.country}`;
    image.setAttribute('src', `${user.picture.large}`);
}

getRandomUser();