﻿using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace HR.DistributedCache
{
    public class RedisService
    {
        private readonly IConfiguration _configuration;

        public RedisService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public RedisKey[] GetAllKeys(string keyPrefix)
        {
            using (var redis = ConnectionMultiplexer.Connect(_configuration.GetConnectionString("Redis")))
            {
                var server = redis.GetServers().Single();
                return server.Keys(pattern: keyPrefix + "*").ToArray();
            }
        }
    }
}
