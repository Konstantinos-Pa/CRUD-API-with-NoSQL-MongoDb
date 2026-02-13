using MongoDB_Demo.Models;

namespace MongoDB_Demo.Repository
{
    public interface IPollRepository
    {
        Task CreatePoll(Poll poll);
        Task DeletePoll(string id);
        Task<List<Poll>> GetAllPolls();
        Task<Poll> GetPollById(string id);
        Task UpdatePoll(string id, Poll poll);
    }
}