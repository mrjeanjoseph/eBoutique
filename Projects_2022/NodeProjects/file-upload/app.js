const express = require('express');
const fileUpload = require('express-fileupload');
const path = require('path');

const app = express();

app.set('view engine', 'ejs');
app.use(fileUpload({
    useTempFiles: true,
    tempFileDir: path.join(__dirname, 'tmp'),
    createParentPath: true,
    limits: 1024,
}))

app.get('/', async function (request, response, next) {
    response.render('index');
});

app.post('/single', async function (request, response, next) {
    try {
        const file = request.files.mFile;
        console.log(file);

        const customFileName = new Date().getTime().toString() + path.extname(file.name);
        const savePath = path.join(__dirname, 'public', 'uploads', customFileName);

        if (file.truncated) {
            throw new Error("File size is too big...,");
        }
        if (file.mimetype !== 'image/jpeg') {
            throw new Error("Only jpegs are supported")
        }
        await file.mv(savePath);
        response.redirect('/');

    } catch (error) {
        console.log(error);
        response.send("Error uploading file");
    }
})

app.post("/multiple", async function (request, response, next) {
    try {
        const files = request.files.mFiles
        // files.forEach( function(file) {
        //     const savePath = path.join(__dirname, 'public', 'uploads', file.name);
        // })
        // await file.mv(savePath)
        let promises = [];
        files.forEach(function (file) {
            const savePath = path.join(__dirname, 'public', 'uploads', file.name);
            promises.push(file.mv(savePath));
        })
        await Promise.all(promises);

    } catch (error) {
        console.log(error);
        response.send("Error uploading the multiple files...,")
    }
})

const port = 2022;
app.listen(port, function () {
    console.log('server is listening on ' + port);
});