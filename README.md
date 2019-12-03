**HashDex Engineering Challenge**


1. BinanceAPI contains c# .NET server to get information from Binance and publish it on Redis pubsub channel "trades" on default port
2. Middleware contains express server to listen to redis subscribing to channel "trades", and exposing it as an api 
3. Frontend is a react app that calls the express server every second and displays the information on a web page


**usage:**
run an instance of `redis-server` on local terminal

cd into BinanceAPI directory and run the following:
`dotnet build`
`dotnet run`

cd into middleware and run
`npm install`
`node server`

cd into frontend and run
`yarn install`
`yarn start`

**.NET Backend**
The file `BinanceDataProvider.cs` in BinanceAPI/Models does most of the heavy lifting in this project. This is the responsible code for pulling information from the Exchange and publishing it to `trades` channel on Redis. This file includes an Interface that can be used to instantiate a `BinanceDataProvider` Object in the server `startup.cs` file. Additionally, in the Models directory there is a `Trade.cs` model that contains the relevant trade spec as defined in the challenge. The architecture is based off a .NET webapi package, that was modified for use in this project (this part of the repo does not expose an api, but starts the redis server on the default port)

**Middleware**
This is a simple api setup in express.js to listen to subscribe to the redis `trades` channel. This was created after I spent nontrivial time setting up a React.js front end. I found that the browswer is unable to open a TCP connection to listen to redis pubsub in real time, so this must be done in a middleware server. In order for this information to hit the front end, I format and push new trades into an array and expose it as an api call on port 4000.

**React**
I made a react client because it's what I am comfortable with and it looks nice. In order to update trades, I call the express api every second and update the information on screen. Currently, it is not efficient as react must iterate through the returned loop every call to print. This can be solved with some caching. 
