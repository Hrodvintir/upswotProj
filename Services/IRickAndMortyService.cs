using upswotProj.Models;

namespace upswotProj.Services
{
    public interface IRickAndMortyService
    {
        public Task<bool> CheckByNameAndEpisode(string personName,string episodeName);
        public Task<Person> GetByName(string personName);
    }
}
