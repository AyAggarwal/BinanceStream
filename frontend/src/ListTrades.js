import React, { Component } from "react";
const Redis = require('ioredis');

const channel = 'trades';



class ListTrades extends Component {
  constructor(props) {
    super(props);
    this.state = {
      trades: ""
    };

    this.redis = new Redis();

    this.updateList = this.updateList.bind(this);
    this.PubSub = require("pubsub-js");
  }

  //dropdown menu functions
  updateList = function(msg, data) {
    this.redis.on('message', (channel, message) => {
        console.log(`Received the following message from ${channel}: ${message}`);
    });
  };

    componentDidMount() {
        this.redis.subscribe(channel, (error, count) => {
            if (error) {
                throw new Error(error);
            }
            console.log(`Subscribed to ${count} channel. Listening for updates on the ${channel} channel.`);
        });
  }

  render() {
    return (
      <div>
        <h1>ETH-BTC Trades on Binance</h1>
        <div>
          {this.state.trades}
        </div>
      </div>
    );
  }
}
export default ListTrades;
