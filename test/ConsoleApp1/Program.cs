using Microsoft.Extensions.DependencyInjection;
using Naruto.Redis;
using Naruto.Subscribe;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static IServiceCollection serviceDescriptors = new ServiceCollection();
        static async Task Main(string[] args)
        {
            Console.WriteLine("begin");

            serviceDescriptors.AddRedisRepository(a =>
            {
                a.Connection = new string[] { "127.0.0.1" };
            });

            serviceDescriptors.AddScoped<sub1>();

            var serprovider = serviceDescriptors.BuildServiceProvider();
            var redis = serprovider.GetRequiredService<IRedisRepository>();

            for (int i = 0; i < 10; i++)
            {
                var sub = serprovider.CreateScope().ServiceProvider.GetRequiredService(typeof(sub1));

                await NarutoWebSocketServiceExpression.ExecAsync(sub, "Sa", null);

            }

            Console.ReadKey();
        }
    }

    /// <summary>
    /// 张海波
    /// 2020-04-03
    /// 执行对应方法的表达式目录树
    /// </summary>
    public class NarutoWebSocketServiceExpression
    {
        /// <summary>
        /// 存储委托
        /// </summary>
        private static ConcurrentDictionary<string, Delegate> exec;

        static NarutoWebSocketServiceExpression()
        {
            exec = new ConcurrentDictionary<string, Delegate>();
        }

        /// <summary>
        /// 执行方法
        /// </summary>
        /// <param name="service">继承NarutoWebSocketService的服务</param>
        /// <param name="action">执行的方法</param>
        /// <param name="parameterEntity">方法的参数</param>
        /// <returns></returns>
        public static Task ExecAsync(object service, string action, object parameterEntity)
        {
            //从缓存中取
            if (exec.TryGetValue(service.GetType().Name + action, out var res))
            {
                return res.DynamicInvoke(service, parameterEntity) as Task;
            }
            return Create(service, action, parameterEntity);
        }


        /// <summary>
        /// 创建委托
        /// </summary>
        /// <param name="service">继承NarutoWebSocketService的服务</param>
        /// <param name="action">执行的方法</param>
        /// <param name="parameterEntity">方法的参数</param>
        /// <returns></returns>
        private static Task Create(object service, string action, object parameterEntity)
        {
            //定义输入参数
            var p1 = Expression.Parameter(service.GetType(), "service");
            //方法的参数对象
            var methodParameter = Expression.Parameter(parameterEntity == null ? typeof(object) : parameterEntity.GetType(), "methodParameter");

            //动态执行方法
            var method = service.GetType().GetMethod(action, BindingFlags.Public | BindingFlags.Instance);
            //获取参数
            var parameters = method.GetParameters();
            //调用指定的方法
            MethodCallExpression actionCall = null;
            //验证是否方法是否 有参数
            if (parameters.Count() == 0)
            {
                //执行无参方法
                actionCall = Expression.Call(p1, method);
            }
            else
            {
                //执行有参的方法
                actionCall = Expression.Call(p1, method, methodParameter);
            }
            //生成lambda
            var lambda = Expression.Lambda(actionCall, new ParameterExpression[] { p1, methodParameter });
            //获取key
            var key = service.GetType().Name + action;
            //存储
            exec.TryAdd(key, lambda.Compile());

            return exec[key].DynamicInvoke(service, parameterEntity) as Task;
        }
    }

    public class sub1 : ISubscribe
    {
        public sub1(IRedisRepository redisRepository)
        {

        }
        [SubscribeName(Name = "sub1")]
        public async Task Sa()
        {
            Console.WriteLine("sa");
        }
    }


}
