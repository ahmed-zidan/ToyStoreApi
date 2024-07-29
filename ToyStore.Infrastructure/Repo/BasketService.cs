using StackExchange.Redis;
using System.Text.Json;
using ToyStore.Core.IRepository;
using ToyStore.Core.Models;

namespace ToyStore.Infrastructure.Repo
{
    public class BasketService : IBasketService
    {
        private readonly IDatabase _redis;
        public BasketService(IConnectionMultiplexer multiplexer)
        {
            _redis = multiplexer.GetDatabase();
        }
        public async Task<bool> deleteBasket(string basketId)
        {
           return await _redis.KeyDeleteAsync(basketId);
            
        }

        public async Task<Basket> getBasket(string basketId)
        {
            var data = await _redis.StringGetAsync(basketId);
            return data.IsNullOrEmpty?null : JsonSerializer.Deserialize<Basket>(data) ;
        }

        public async Task<Basket> updateBasket(Basket customerBasket)
        {
            var created = await _redis.StringSetAsync(customerBasket.Id, JsonSerializer.Serialize(customerBasket), TimeSpan.FromDays(30));

            return created == true ? await getBasket(customerBasket.Id) : null;
        }
    }
}
