using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB_Demo.Models;

namespace MongoDB_Demo.Repository
{
    public class PollRepository : IPollRepository
    {
        public readonly IMongoCollection<Poll> _poll;

        public PollRepository(IMongoClient mongoClient, IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _poll = database.GetCollection<Poll>("Polls");
        }

        public async Task<List<Poll>> GetAllPolls()
        {
            return await _poll.Find(p => true).ToListAsync();
        }

        public async Task<Poll> GetPollById(string id)
        {
            return await _poll.Find(p => p.id == id).FirstOrDefaultAsync();
        }

        public async Task CreatePoll(Poll poll)
        {
            await _poll.InsertOneAsync(poll);
        }

        public async Task UpdatePoll(string id, Poll poll)
        {
            poll.id = id;
            await _poll.ReplaceOneAsync(p => p.id == id, poll);
        }

        public async Task DeletePoll(string id)
        {
            await _poll.DeleteOneAsync(p => p.id == id);
        }
    }
}
