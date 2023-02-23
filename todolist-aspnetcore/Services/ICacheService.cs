using StackExchange.Redis;

namespace todolistaspnetcore.Services
{
    public interface ICacheService
    {
        Task<long> AddToListAsync(string hostName, int port, string listKey, string value);
        Task<RedisValue[]> GetAllFromListAsync(string hostName, int port, string listKey);
        Task<string> GetFromListAsync(string hostName, int port, string listKey, string elementKey);
        Task<bool> KeyExpireAsync(string hostName, int port, string key, int expirySeconds);
        Task<long> RemoveFromListAsync(string hostName, int port, string listKey, string value);
        Task<bool> RemoveList(string hostName, int port, string listKey);
    }
}