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
        [SubscribeName(Name = "test")]
        public async Task A()
        {
            logger.LogInformation("A");
        }
    }
}
