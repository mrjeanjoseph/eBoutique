// user class
class User {
    constructor(id, name, location) {
        this.id = id;
        this.name = name;
        this.location = location;
    }
}

// UI classes
class UI {
    static displayUsers() {
        // const storedUsers = [
        //     {
        //         id: 1,
        //         name: "Veleenah",
        //         location: "Florence, Italy",
        //     },
        //     {
        //         id: 2,
        //         name: "Devereaux",
        //         location: "Paris, France",
        //     },
        // ];        
        // const users = storedUsers;

        const users = BrowserStorage.getUser();
        users.forEach(function (user) {
            UI.addUserToList(user)
        });
    }

    static addUserToList(user) {
        const list = document.querySelector("#user-list");
        const row = document.createElement('tr');

        row.innerHTML = `
        <td>${user.id}</td>
        <td>${user.name}</td>
        <td>${user.location}</td>
        <td><a href="#" class="btn btn-danger btn-sm delete">X</a></td>
        `;

        list.appendChild(row);
    }

    static clearFields() {
        document.querySelector("#id").value = '';
        document.querySelector("#name").value = '';
        document.querySelector("#location").value = '';
    }

    static deleteUser(element) {
        if (element.classList.contains("delete")) {
            element.parentElement.parentElement.remove()
        }
    }

    static showNotification(message, className) {
        const div = document.createElement('div');
        div.className = `alert alert-${className}`;
        div.appendChild(document.createTextNode(message));
        const container = document.querySelector('.container-fluid');
        const form = document.querySelector("#user-form");
        container.insertBefore(div, form);

        //remove timeout after two seconds.
        const alert = document.querySelector(".alert");
        setTimeout(function () {
            alert.remove()
        }, 2000);
    }
}
// Event to display users
document.addEventListener("DOMContentLoaded", UI.displayUsers);

// Event to add a new user
document.querySelector("#user-form").addEventListener("submit", function (e) {
    e.preventDefault();
    const id = document.querySelector("#id").value;
    const name = document.querySelector("#name").value;
    const location = document.querySelector("#location").value;

    if (id === "" || name === "" || location === "") {
        UI.showNotification("Please fill out the form prior to submitting!", "warning");
    } else {
        //instentiate user form
        const user = new User(id, name, location);

        // add user to table
        UI.addUserToList(user);

        BrowserStorage.addUser(user)
        UI.showNotification("User has been added successfully", "success");
        UI.clearFields();
    }

});

// Event to remove user
document.querySelector("#user-list").addEventListener("click", function (e) {
    // console.log(e.target)
    UI.deleteUser(e.target);
    UI.showNotification("User has been deleted successfully", "danger");
    const getUserId = e.target.parentElement.previousElementSibling.previousElementSibling.previousElementSibling.textContent;
    console.log(getUserId);
    BrowserStorage.removeUser(getUserId)
})

//storing data in browser memory while browsing
class BrowserStorage{
    static getUser(){
        let users;
        if(localStorage.getItem('users') === null){
            users = [];
        } else {
            users = JSON.parse(localStorage.getItem('users'));
        }
        return users;
    }
    static addUser(user){
        const users = BrowserStorage.getUser();
        users.push(user);
        localStorage.setItem('users', JSON.stringify(users));
    }
    static removeUser(id){
        const users = BrowserStorage.getUser();
        
        users.forEach(function(user, index) {
            if(user.id === id) {
                users.splice(index, 1);
            }
        });
        localStorage.setItem('users', JSON.stringify(users));
    }
}