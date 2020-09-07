using Microsoft.Extensions.Logging;
using Naruto.Subscribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace test.api
{
    public class SubscribeTest : ISubscribe
    {
        private readonly ILogger logger;

        public SubscribeTest(ILogger<SubscribeTest> _logger)
        {
            logger = _logger;
        }
        [Subscribe("test")]
        public async Task A()
        {
            logger.LogInformation("A");
        }
        [Subscribe("test2")]
        public async Task B(testDTO testDTO)
        {

        }
    }
    public class testDTO
    {
        public string id { get; set; }
    }
}
