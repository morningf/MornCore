using System;
using MornCore;
using ProtocolDemo;

namespace MornClientDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            
            while(true)
            {
                var key = Console.ReadLine();
                switch (key)
                {
                    case "exit":
                        return;
                    default:
                        {
                            MornClient client = new MornClient
                            {
                                ServerUrl = "http://localhost:60439/"
                            };

                            AdditionCalculationRequest request = new AdditionCalculationRequest
                            {
                                A = 10,
                                B = 20,
                                L = new System.Collections.Generic.List<int>() { 1, 2, 3, 4 },
                                Remarks = "AdditionCalculationRequest"
                            };

                            Console.WriteLine(request.ToString());
                            Console.WriteLine("request start ...");
                            var response = client.Execute(request);
                            Console.WriteLine(response.ToString());
                        }
                        break;
                }
            }
        }
    }
}
