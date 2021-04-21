const express = require("express"); // Mounting Server
const mysql = require("mysql"); // Database handling
const { reset } = require("nodemon"); // Helps avoiding server restart on development
const path = require('path'); // For using different local files
const app = express();

app.use(express.json()); // Body Parser for handling raw json
app.use(express.urlencoded({ extended: false })); // Handle form submissions
app.use(express.static(path.join(__dirname, "/../Frontend"))); // Use a static folder (Frontend) for html and css

// Create connection with local database
const db = mysql.createConnection({
    host: "localhost",
    user: "root",
    password: "STEAMadmin",
    database: "steam_users",
    port: "3306",
});

db.connect((err) => {
    if (err) {
        throw err;
    } else {
        console.log("connected");
    }
});

app.post('/access', (req, res) => {
    console.log('llegue');
    const newMember = req.body;
    db.query(`INSERT INTO users(email, pswd, nombre, edad, sexo, curlvl) values('${newMember.email}','${newMember.pswd}','${newMember.nombre}','${newMember.edad}','${newMember.sexo}', 1 )`);
    res.redirect('/access.html');
});

app.get('/access', (req, res) => { db.query(`SELECT `) })


const PORT = process.env.PORT || 5000; // Port for deployment or local (for development)
app.listen(PORT, () => console.log(`Server started on ${PORT}`)); // Listening onthe decided port