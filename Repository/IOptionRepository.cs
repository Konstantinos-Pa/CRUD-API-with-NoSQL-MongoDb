using MongoDB_Demo.Models;

namespace MongoDB_Demo.Repository
{
    public interface IOptionRepository
    {
        Task CreateOption(Option option);
        Task DeleteOption(string id);
        Task<List<Option>> GetAllOptions();
        Task<Option> GetOptionById(string id);
        Task UpdateOption(string id, Option option);
    }
}