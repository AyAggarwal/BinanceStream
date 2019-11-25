using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using BinanceAPI.Models;

namespace BinanceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TradeController : ControllerBase
    {
        ISubscriber sub = Redis.redis.GetSubscriber();

        private readonly ILogger<TradeController> _logger;

        public TradeController(ILogger<TradeController> logger)
        {
            _logger = logger;
            
        }

        [HttpGet]
        public String Get()
        {
            sub.Publish("messages", "hello");
            return "t";
        }
    }
}
