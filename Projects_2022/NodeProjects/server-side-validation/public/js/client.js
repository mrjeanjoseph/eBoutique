let form = document.querySelector('form');
form.onsubmit = sendData;

function sendData(e) {
    e.preventDefault();

    let formData = new FormData(form);

    let Params = {
        headers: {'Content-Type': 'application/json'},
        body: JSON.stringify({name: formData.get('name'),}),
        method: "POST"
    }

    fetch('http://localhost:2022/formData', Params)
    .then(response => response.json())
    .then(data => console.log(data))
    .catch(err => console.log(err))
}