const express = require('express');
const { engine } = require('express-handlebars');
const bodyParser = require('body-parser');
const mysql = require('mysql');

require('dotenv').config();

const app = express();
const port = process.env.PORT || 2022;

// const port = 5000;

//parsing middleware configuration
//parse application/x-www-form-urlencoded
app.use(bodyParser.urlencoded({ extended: false }));

//parse application/json
app.use(bodyParser.json());

//static Files
app.use(express.static('public'));

//templating engine
app.engine('hbs', engine({extname:'.hbs', defaultLayout: "main"}));
app.set('view engine', 'hbs');

//connection pool
const pool = mysql.createPool({
    connectionLimit     : 100,
    host: process.env.DB_HOST,
    user: process.env.DB_USER,
    password: process.env.DB_PASS,
    database: process.env.DB_NAME
});

//connect to DB
pool.getConnection(function(err, connection){
    if(err) throw err;
    console.log(`Connected as ID: ${ connection.threadId}`);
});

const routes = require("./server/routes/user")
app.use('/', routes);


app.listen(port, function () {
    console.log(`Listening on port ${port}`);
})