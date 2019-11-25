using System;

namespace BinanceAPI.Models
{
    public class Trade
    {
        public DateTime ReceiveTime { get; set; } //Time in which the trade was received
        public DateTime TradeTime { get; set; } // Time in which the trade was executed
        public string TradeId { get; set; } // Trade ID as sent by the exchange
        public string Symbol { get; set; } // Represents the asset being traded
        public decimal Price { get; set; } // Trade price
        public decimal Qty { get; set; } // Quantity of assets traded
    }
}