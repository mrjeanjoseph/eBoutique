//This is the backend server
const express = require('express');
const bodyParser = require("body-parser");
const path = require("path");
const httpMsgs = require("http-msgs");

const app = express();
var PORT = 2022
app.listen(PORT, function(){
    console.log(`Server is running on http://localhost:${PORT}`)
});
app.use(bodyParser.urlencoded({ extended: false }));

app.use(express.static("assets"));
app.get("/", function (req, res) {
    res.sendFile(path.join(__dirname, "/index.html"));
    // res.sendFile(path.join(__dirname, "C:\\development\\workspace\\metadata.outerpage.html"));
});

app.post("/ajaxdemo", function (req, res) {
    var data = req.body;
    console.log(data);

    // httpMsgs.sendJSON(req, res, {
    //     from: "Server"
    // });
    httpMsgs.send500(req,res, "Invalid from Jean DVC!");
});