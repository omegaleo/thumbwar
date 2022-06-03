// Import necessary packages
const express = require("express");
const bodyParser = require("body-parser");

// create and configure the express app
const PORT = 3000;
const app = express();
app.use(express.json());

// Database Connection Info
const MongoClient = require("mongodb").MongoClient;

// the URL we copied from earlier. Replace username and password with what you created in the initial steps
const url = "mongodb+srv://thumbwar-api:n3lJFdVeasdIurKx@cluster0.e6uob.mongodb.net/Leaderboard?retryWrites=true";
let db;

// The index route
app.get("/", function(req, res) {
   res.send("Thumb War Leaderboard API!");
});

app.post("/leaderboard/add", async function(req, res) {
    // get information of player from POST body data
    let { difficulty, username, time } = req.body;
 
    //post to db
    await db.collection("Leaderboard").insertOne({ difficulty, username, time });
        console.log(`Created leaderboard ${username}`);
        res.send({ status: true, msg: "player created" });
 });

 app.get("/leaderboard/get", async function(req, res) {
    //get top 10 for a difficulty
    let {difficulty} = req.body;
    // retrieve ‘lim’ from the query string info

    db.collection("Leaderboard").find({}, { projection: { difficulty: difficulty } }).limit(10).toArray(function(err, result) {
        if (err)
        {
            res.send({ status: false, msg: "failed to retrieve leaderboard" });
        }
        else
        {
            res.send({ status: true, msg: result });
        }       
    });
 });

// Connect to the database with [url]
(async () => {
   let client = await MongoClient.connect(
       url,
       { useNewUrlParser: true }
   );

   db = client.db("Leaderboard");

   app.listen(PORT, async function() {
       console.log(`Listening on Port ${PORT}`);
       if (db) {
           console.log("Database is Connected!");
       }
   });
})();