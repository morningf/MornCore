using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace MornCore
{
    public static class MornApplicationBuilderExtensions
    {
        public static void RunMornServices(this IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await MornService.ProcessHttpContextAsync(context);
            });
        }
    }
}
