using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MornCore;
using ProtocolDemo;

namespace MornServerDemoA
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 本项目地址 http://localhost:60439/

            //以下注册到的URL是MornServerDemoB项目的地址
            MornServer.RegisterRemoteMethodHandler<AdditionCalculationRequest, AdditionCalculationResponse>("http://localhost:60450/");

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
