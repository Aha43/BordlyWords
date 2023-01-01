using BordlyWords.DefaultInfrastructure.Domain.Param;
using BordlyWords.Specification.Api;
using Microsoft.AspNetCore.Mvc;

namespace BordlyWordsWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class WordController : ControllerBase
    {
        private readonly IBordlyWordsApi _bordlyWordsApi;

        public WordController(IBordlyWordsApi bordlyWordsApi) => _bordlyWordsApi = bordlyWordsApi;

        [HttpGet]
        [Route("api/pick")]
        public async Task<IActionResult> PickAsync([FromQuery] PickWordParam p)
        {
            var word = await _bordlyWordsApi.Pick(p).ConfigureAwait(false);
            return Ok(word);
        }

        [HttpGet]
        [Route("api/check")]
        public async Task<IActionResult> CheckAsync([FromQuery] CheckWordParam p)
        {
            var result = await _bordlyWordsApi.Check(p).ConfigureAwait(false);
            return Ok(result);
        }

    }

}
