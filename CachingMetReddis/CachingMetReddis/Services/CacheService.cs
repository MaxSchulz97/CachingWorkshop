using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CachingMetRedis.Services
{
    public class CacheService
    {
        private static ConnectionMultiplexer connectionMultiplexer = ConnectionMultiplexer.Connect("WappNots.redis.cache.windows.net:6380,password=RhbRWS9ZZrK32EPRL2g8V40kFAL8bHL+2ktSLaIYqqo=,ssl=True,abortConnect=False");
        private static IDatabase database = connectionMultiplexer.GetDatabase();

        public static IDatabase GetInstance()
        {
            return database;
        }
    }
}
