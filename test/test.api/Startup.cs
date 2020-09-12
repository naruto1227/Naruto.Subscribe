using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Naruto.Subscribe;
using Naruto.Subscribe.Extension;
using Naruto.Subscribe.Interface;
using Naruto.Subscribe.Provider.RabbitMQ;
using Naruto.Subscribe.Provider.Redis;

namespace test.api
{
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration _configuration)
        {
            configuration = _configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //注入订阅服务 如果只需要 发布服务，则可以不用注入此方法，注入对应的发布服务即可
            services.AddSubscribeServices(typeof(Startup).Assembly);
            //services.AddRedisRepository(a =>
            //{
            //    a.Connection = new string[] { "127.0.0.1" };
            //});
            //注入redis订阅服务
            //services.AddRedisSubscribe();
            //注入redis发布服务
            //services.AddRedisPublishServices();
            //注入mq订阅服务
            services.AddRabbitMQSubscribe(configuration.GetSection("mq"));
            //注入mq发布服务
            services.AddRabbitMQPublishServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            //await app.EnableSubscribe();

            NarutoMessageAopEvent.RegisterPreHandlerEvent((subscribeName, msg) =>
            {
                msg = "{'id':'jjj'}";
                Console.WriteLine(subscribeName + msg);
            });

            NarutoMessageAopEvent.RegisterAfterHandlerEvent((subscribeName, msg) =>
            {
                Console.WriteLine(subscribeName + msg);
            });
        }
    }
}
