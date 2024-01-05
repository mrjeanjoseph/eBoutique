const express = require('express');
const app = express();
const port = 2022;
const path = require('path');
const bodyParser = require('body-parser');
const { check, validationResult } = require('express-validator');

let urlencoded = bodyParser.urlencoded({ extended: false });

app.use(bodyParser.json());
app.use(urlencoded);
app.use(express.static(__dirname + '/public'));

app.get('/', (request, response) => {
    response.sendFile(path.join(__dirname + '/dashboard.html'));
});

app.post("/formData", [

], (request, response) => {
    console.log(response.body);
})

app.listen(port, () => console.log('Server running on port ' + port));