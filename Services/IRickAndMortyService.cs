using upswotProj.Models;

namespace upswotProj.Services
{
    public interface IRickAndMortyService
    {
        public Task<bool> checkPersonInEpisode(string personName,string episodeName);
        public Task<Character> GetCharacterByName(string personName);
    }

    
}
