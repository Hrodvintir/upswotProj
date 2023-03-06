using System.Data;
using System.Web;
using Newtonsoft.Json;
using upswotProj.Models;

namespace upswotProj.Services
{
    public class RickAndMortyService : IRickAndMortyService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public RickAndMortyService(IHttpClientFactory _httpClientFactory)
        {
            this._httpClientFactory = _httpClientFactory;
        }
        public async Task<Character> GetCharacterByName(string personName)
        {
            

            throw new NotImplementedException();
        }

        public async Task<bool> checkPersonInEpisode(string personName, string episodeName)
        {
            var person = await GetPersonByName(personName);
            var episodeCode = await GetEpisodeCodeByName(episodeName);
            
            var episodeUrl = $"https://rickandmortyapi.com/api/episode?name={episodeCode}";
            return person.episode.Contains(episodeUrl);
        }

        private async Task<Person> GetPersonByName(string personName)
        {
            var builder = new UriBuilder("https://rickandmortyapi.com/api/character");
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["name"] = personName;

            builder.Query = query.ToString();
            var url = builder.ToString();
            
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);

            var str = await response.Content.ReadAsStringAsync();
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(str)!;

            var toReturn = new Person
            {
                Id = dynamicObject.result.id,
                name = dynamicObject.results.name,
                status = dynamicObject.results.status,
                species = dynamicObject.results.species,
                type = dynamicObject.results.type,
                origin = new Origin()
                {
                    name = dynamicObject.results.origin.name,
                    url = dynamicObject.results.origin.url,
                },
                episode = (IEnumerable<string>)dynamicObject.results.episode
            };
            return toReturn;
        }

        private async Task<string> GetEpisodeCodeByName(string episodeName)
        {
            var builder = new UriBuilder("https://rickandmortyapi.com/api/episode");
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["name"] = episodeName;

            builder.Query = query.ToString();
            var url = builder.ToString();
            
            var client = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync(url).ConfigureAwait(false);

            var str = await response.Content.ReadAsStringAsync();
            
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(str)!;
            var episodeCode = dynamicObject.results.episode.ToString();
            
            return Convert.ToString(episodeCode);
        }
    }
}
