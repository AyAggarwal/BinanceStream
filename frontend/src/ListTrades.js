import React, { Component } from "react";
import { S_IFDIR } from "constants";
const channel = "trades";
const axios = require("axios");


class ListTrades extends Component {
  constructor(props) {
    super(props);
    this.state = {
      trades: [],
      test: ["1","2","3"]
    };

    this.updateList = this.updateList.bind(this);
    this.timer = setInterval(this.updateList, 1000)
  }

  //dropdown menu functions
  updateList() {
    var config = {
      headers: {'Access-Control-Allow-Origin': '*'}
  };
  let frame = this;
    axios.get("http://localhost:4000/", config)
    .then(function (response) {
      frame.setState({
        trades: response.data
      })
      console.log(frame.state.trades)
    })
    .catch(function (error) {
      console.log(error)
    })
    }


  render() {
    return (
      <div>
        <h1>ETH-BTC Trades on Binance</h1>
        <div>{this.state.trades.map(txt => <p>{txt}</p>)}</div>
      </div>
    );
  }
}
export default ListTrades;
