const express = require("express");
const path = require("path");
const port = 2023;
let serverMsg = `Server running on port: ${port}.`

const app = express();
app.use("/static", express.static(path.resolve(__dirname, "frontend", "static")));

app.get("/*", (req, res) => {
    res.sendFile(path.resolve(__dirname, "frontend", "index.html"));
});

app.listen(process.env.PORT || port, () => console.log(serverMsg))