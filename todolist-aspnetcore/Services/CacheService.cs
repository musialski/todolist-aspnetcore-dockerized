using StackExchange.Redis;

namespace todolistaspnetcore.Services
{
    public class CacheService : ICacheService
    {
        public async Task<string> GetFromListAsync(string hostName, int port,
            string listKey, string elementKey)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{hostName}:{port}");
            IDatabase redisdb = redis.GetDatabase();

            long elementIndex = await redisdb.ListPositionAsync(listKey, elementKey);
            if (elementIndex == -1)
            {
                return null;
            }

            return await redisdb.ListGetByIndexAsync(listKey, elementIndex);
        }
        public async Task<RedisValue[]> GetAllFromListAsync(string hostName, int port,
            string listKey)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{hostName}:{port}");
            IDatabase redisdb = redis.GetDatabase();

            long listLen = await redisdb.ListLengthAsync(listKey);
            if (listLen == -1)
            {
                return null;
            }

            return await redisdb.ListRangeAsync(listKey, stop: listLen);
        }
        public async Task<long> AddToListAsync(string hostName, int port,
            string listKey, string value)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{hostName}:{port}");
            IDatabase redisdb = redis.GetDatabase();

            return await redisdb.ListRightPushAsync(listKey, value);
        }
        public async Task<long> RemoveFromListAsync(string hostName, int port,
            string listKey, string value)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{hostName}:{port}");
            IDatabase redisdb = redis.GetDatabase();

            return await redisdb.ListRemoveAsync(listKey, value);
        }
        public async Task<bool> RemoveList(string hostName, int port, string listKey)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{hostName}:{port}");
            IDatabase redisdb = redis.GetDatabase();

            return await redisdb.KeyDeleteAsync(listKey);
        }
        public async Task<bool> KeyExpireAsync(string hostName, int port, string key,
            int expirySeconds)
        {
            IConnectionMultiplexer redis = ConnectionMultiplexer.Connect($"{hostName}:{port}");
            IDatabase redisdb = redis.GetDatabase();

            return await redisdb.KeyExpireAsync(key, TimeSpan.FromSeconds(expirySeconds));
        }
    }
}