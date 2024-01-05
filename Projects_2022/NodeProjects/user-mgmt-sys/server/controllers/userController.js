const mysql = require('mysql');

//inlude mysql
const pool = mysql.createPool({
    connectionLimit: 100,
    host: process.env.DB_HOST,
    user: process.env.DB_USER,
    password: process.env.DB_PASS,
    database: process.env.DB_NAME
});

//connect to DB

//get views
exports.view = function (req, res) {
    pool.getConnection(function (err, connection) {
        if (err) throw err;
        console.log(`Connected as ID: ${connection.threadId}`);
        connection.query('SELECT * FROM user WHERE status = "active"', function (err, rows) {
            connection.release();
            if (!err) {
                let removedUser = req.query.removed;
                res.render('home', { rows, removedUser });
            } else {
                console.log(err);
            }
            console.log("Data from db\n", rows);
        });
    });
};

//find user by search
exports.find = function (req, res) {
    pool.getConnection(function (err, connection) {
        if (err) throw err;
        console.log(`Connected as ID: ${connection.threadId}`);

        let searchTerm = req.body.search;
        // console.log(searchTerm);

        connection.query('SELECT * FROM user WHERE first_name LIKE ? OR last_name LIKE ?',
            [`%${searchTerm}%`, `%${searchTerm}%`],
            function (err, rows) {
                //This peice of code is not filtering like it is supposed to.
                connection.release();
                if (!err) {
                    res.render('home', { rows });
                } else {
                    console.log(err);
                }
                console.log("Data from db\n", rows);
            });
    });
}

exports.form = function (req, res) {
    res.render("add-user");
}


exports.create = function (req, res) {
    const { first_name, last_name, email, phone, comments } = req.body;
    //res.render('add-user');
    pool.getConnection(function (err, connection) {
        if (err) throw err;
        console.log(`Connected as ID: ${connection.threadId}`);

        connection.query('INSERT INTO user SET first_name = ?, last_name = ?, email = ?, phone = ?, comments = ?',
            [first_name, last_name, email, phone, comments],
            function (err, rows) {
                connection.release();
                if (!err) {
                    // res.render("add-user");
                    res.render('add-user', { rows, alert: `${first_name} has been added successfully` });
                } else {
                    res.render('add-user', { alert: 'User not added.' });
                    console.log(err);
                }
                console.log("The data from user table \n", rows)
            });
    });
}

exports.editUser = function (req, res) {
    pool.getConnection(function (err, connection) {
        if (err) throw err;
        console.log(`Connected as ID: ${connection.threadId}`);
        connection.query('SELECT * FROM user WHERE id = ?', [req.params.id], function (err, rows) {
            connection.release();
            if (!err) {
                res.render('edit-user', { rows });
            } else {
                console.log(err);
            }
            console.log("Data from db\n", rows);
        });
    });
}

exports.update = function (req, res) {
    const { first_name, last_name, email, phone, comments } = req.body;

    pool.getConnection(function (err, connection) {
        if (err) throw err;
        console.log(`Connected as ID: ${connection.threadId}`);

        connection.query('UPDATE user SET first_name = ?, last_name = ?, email = ?, phone = ?, comments = ? WHERE id = ?',
            [first_name, last_name, email, phone, comments, req.params.id], function (err, rows) {
                connection.release();
                if (!err) {
                    pool.getConnection(function (err, connection) {
                        if (err) throw err;
                        console.log(`Connected as ID: ${connection.threadId}`);
                        connection.query('SELECT * FROM user WHERE id = ?',
                            [req.params.id],
                            function (err, rows) {
                                connection.release();
                                if (!err) {
                                    res.render('edit-user', { rows, alert: `${first_name} has been updated.` });
                                } else {
                                    console.log(err);
                                }
                                console.log("Data from db\n", rows);
                            });
                    });

                } else {
                    console.log(err);
                }
                console.log("Data from db\n", rows);
            });
    });
}

// Delete a user
exports.delete = function (req, res) {
    // pool.getConnection(function (err, connection) {
    //     if (err) throw err;
    //     console.log(`Connected as ID: ${connection.threadId}`);
    //     connection.query('DELETE FROM user WHERE id = ?', [req.params.id], function (err, rows) {
    //         connection.release();
    //         if (!err) {
    //             res.redirect('/');
    //         } else {
    //             console.log(err);
    //         }
    //         console.log("Data from db\n", rows);
    //     });
    // });

    pool.getConnection(function (err, connection) {
        if (err) throw err;
        connection.query('UPDATE user SET status = ? WHERE id = ?',
            ['removed', req.params.id],
            function (err, rows) {
                if (!err) {
                    // let removedUser = encodedURIComponent("User successfully removed");
                    // res.redirect('/?removed=' + removedUser);
                   res.redirect('/');
                } else {
                    console.log(err);
                }
                console.log('Data has been deleted: \n', rows);
            });
    });
}

exports.viewUser = function (req, res) {
    pool.getConnection(function (err, connection) {
        if (err) throw err;
        console.log(`Connected as ID: ${connection.threadId}`);
        connection.query('SELECT * FROM user WHERE id = ?', [req.params.id], function (err, rows) {
            connection.release();
            if (!err) {
                res.render('view-user', { rows });
            } else {
                console.log(err);
            }
            console.log("Data from db\n", rows);
        });
    });
};