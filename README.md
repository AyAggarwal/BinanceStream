HashDex Engineering Challenge


1. BinanceAPI contains c# .NET server to get information from Binance and publish it on Redis pubsub channel "trades" on default port
2. Middleware contains express server to listen to redis subscribing to channel "trades", and exposing it as an api 
3. Frontend is a react app that calls the express server every second and displays the information on a web page


usage:

cd into BinanceAPI directory and run the following:
`dotnet build`
`dotnet run`

cd into middleware and run
`npm install`
`node server`

cd into frontend and run
`yarn install`
`yarn start`