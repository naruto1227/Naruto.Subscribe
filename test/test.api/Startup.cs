using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Provider.Redis;

namespace test.api
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRedisRepository(a =>
            {
                a.Connection = new string[] { "127.0.0.1" };
            });
            services.AddSubscribeServices(typeof(Startup).Assembly);
            services.AddRedisSubscribe();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public  async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          
            app.UseRouting();

            app.Map(new PathString("/test"), app2 =>
            {
                app2.Run(async content =>
                {
                    var sr = app.ApplicationServices.GetRequiredService<INarutoPublish>();
                    await sr.PublishAsync("test");
                });
            });

            app.Map(new PathString("/test2"), app2 =>
            {
                app2.Run(async content =>
                {
                    var sr = app.ApplicationServices.GetRequiredService<INarutoPublish>();
                    await sr.PublishAsync("test2", new testDTO
                    {
                        id = "asdad"
                    });
                });
            });
            app.UseEndpoints(endpoints =>
            {
            });

            await app.EnableSubscribe();
        }
    }
}
