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

namespace MornServerDemoB
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 本项目地址 http://localhost:60450/

            MornServer.RegisterLocalMethodHandler<AdditionCalculationRequest, AdditionCalculationResponse>(ProcessAdditionCalculation);

            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static AdditionCalculationResponse ProcessAdditionCalculation(AdditionCalculationRequest request)
        {
            var response = new AdditionCalculationResponse
            {
                ABResult = request.A + request.B,
                LResult = request.L.Sum(),
                Remarks = "AdditionCalculationResponse"
            };
            return response;
        }
    }
}
