using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using BinanceAPI.Models;
using StackExchange.Redis;
using Binance.Net;
using Binance.Net.Interfaces;

namespace BinanceAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Console.WriteLine("Startup Initialized");
            Redis.redis = ConnectionMultiplexer.Connect("localhost");
            Console.WriteLine("Redis Connected to... " + Redis.redis);
            ISubscriber sub = Redis.redis.GetSubscriber();
            sub.Subscribe("messages", (channel, message) =>
            {
                Console.WriteLine((string)message);
            });
            IBinanceSocketClient socketClient = new BinanceSocketClient();
            IBinanceDataProvider prov = new BinanceDataProvider(socketClient);
            prov.Start();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IBinanceSocketClient, BinanceSocketClient>();
            services.AddSingleton<IBinanceDataProvider, BinanceDataProvider>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
