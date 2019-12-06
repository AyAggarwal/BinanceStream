const express = require("express");
const app = express();
const port = 4000;
var redis = require("redis");
client = redis.createClient();

var cors = require('cors');
var recenttrades = [];
test = "";

client.on("message", function(channel, message) {
    var object = JSON.parse(message);
    var string = "\n{ReceiveTime: " + object.ReceiveTime + ", TradeTime: " + object.TradeTime + ", TradeID: " + object.TradeId + ", Symbol: " + object.Symbol + ", Price: " + object.Price + ", Qty: " + object.Qty + "}\n\n";
    recenttrades.push(string);
});

client.subscribe("trades");

app.use(cors())
app.get("/", (req, res) => res.send(recenttrades));

app.listen(port, () => console.log(`Example app listening on port ${port}!`));
