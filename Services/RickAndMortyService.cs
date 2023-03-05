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
        public async Task<bool> CheckByNameAndEpisode(string personName, string episodeName)
        {
            var episodId = 15;
            var episodUrl = ($"https://rickandmortyapi.com/api/episode?name={1}", episodId);

            throw new NotImplementedException();
        }

        public async Task<Person> GetByName(string personName)
        {
            var builder = new UriBuilder("https://rickandmortyapi.com/api/character");
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["name"] = personName;

            builder.Query = query.ToString();
            var url = builder.ToString();

            //var parameters = new Dictionary<string, string>() {{"name",$"{personName}"}};
            //var encodedParams = new FormUrlEncodedContent(parameters);
            
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
                }
            };
            throw new NotImplementedException();
        }
    }
}
