using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using upswotProj.Services;

namespace upSwotProject.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RickAndMortyController : ControllerBase
    {
        private readonly IRickAndMortyService _rickAndMortyService;

        public RickAndMortyController(IRickAndMortyService _rickAndMortyService)
        {
            this._rickAndMortyService = _rickAndMortyService;
        }
        [HttpPost]
        [Route("/api/v1/check-person")]
        public IActionResult CheckByNameAndEpisode(string personName, string episodeName)
        {
            _rickAndMortyService.GetByName(personName);
            //Check if person has an episode with name @episodeName
            return NotFound();
        }

        [HttpGet]
        [Route("/api/v1/person")]
        public async Task<IActionResult> CheckByName(string name)
        {
            var person = await _rickAndMortyService.GetByName(name);
            //Return Persong obj
            return NotFound();
        }
    }
}