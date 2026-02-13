using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_Demo.Models;

namespace MongoDB_Demo.Repository
{
    public class OptionRepository : IOptionRepository
    {
        private readonly IMongoCollection<Option> _option;

        public OptionRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _option = database.GetCollection<Option>("Options");
        }

        public async Task<List<Option>> GetAllOptions()
        {
            return await _option.Find(o => true).ToListAsync();
        }

        public async Task<Option> GetOptionById(string id)
        {
            return await _option.Find(o => o.Id == id).FirstOrDefaultAsync();
        }

        public async Task CreateOption(Option option)
        {
            await _option.InsertOneAsync(option);
        }

        public async Task UpdateOption(string id, Option option)
        {
            option.Id = id; 
            await _option.ReplaceOneAsync(o => o.Id == id, option);
        }

        public async Task DeleteOption(string id)
        {
            await _option.DeleteOneAsync(o => o.Id == id);
        }
    }
}
