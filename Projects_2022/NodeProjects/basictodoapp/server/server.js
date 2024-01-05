const express = require('express');
const morgan = require('morgan');
const apiRouter = require('./routes');

const app = express();

app.use(morgan("dev"));
app.use(express.urlencoded({ extended: false }));
app.use(express.json());
app.use(express.static('public'));
app.use('/api', apiRouter);

const port = process.env.PORT || 2022;
app.listen(port, function() {
    console.log(`Server is all ear on port: ${port}`);
})