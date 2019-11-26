using Binance.Net.Interfaces;
using Binance.Net.Objects;
using CryptoExchange.Net.Sockets;
using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using BinanceAPI.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BinanceAPI.Models
{
    public interface IBinanceDataProvider
    {
        BinanceStreamKlineData LastKline { get; }
        Action<BinanceStreamKlineData> OnKlineData { get; set; }

        Task Start();
        Task Stop();
    }

    public class BinanceDataProvider : IBinanceDataProvider
    {
        private IBinanceSocketClient _socketClient;
        private UpdateSubscription _subscription;

        public BinanceStreamKlineData LastKline { get; private set; }
        public Action<BinanceStreamKlineData> OnKlineData { get; set; }

        public BinanceDataProvider(IBinanceSocketClient socketClient)
        {
            _socketClient = socketClient;

            Start().Wait(); // Probably want to do this in some initialization step at application startup
        }

        public async Task Start()
        {
            ISubscriber sub = Redis.redis.GetSubscriber();
            Console.WriteLine("Binance Provider Active");
            var subResult = await _socketClient.SubscribeToTradeUpdatesAsync("ethbtc", data =>
            {
                
                
                Trade tr = new Trade
                {
                    ReceiveTime = data.EventTime, //Time in which the trade was received
                    TradeTime = data.TradeTime,// Time in which the trade was executed
                    TradeId = data.BuyerOrderId.ToString(), // Trade ID as sent by the exchange
                    Symbol = data.Symbol, // Represents the asset being traded
                    Price = data.Price, // Trade price
                    Qty = data.Quantity  // Quantity of assets traded
                };
                Console.WriteLine("RT:{0}\n"+
                "TT{1}\n" +
                "TID:{2}\n"+
                "SYM:{3}\n"+
                "P:{4}\n"+
                "Q:{5}",
                data.EventTime, data.TradeTime, data.TradeId.ToString(), data.Symbol, data.Price, data.Quantity
                );
                var json = JsonSerializer.Serialize(tr);
                sub.Publish("trades", json);
            });
            if (subResult.Success)
            {
                _subscription = subResult.Data;
                Console.WriteLine("Binance Subscribed Successfully");
            }

        }

        public async Task Stop()
        {
            await _socketClient.Unsubscribe(_subscription);
        }
    }
}