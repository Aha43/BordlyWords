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
        [Route("api/word")]
        public async Task<IActionResult> GetWordAsync([FromQuery] GetWordParam p)
        {
            var word = await _bordlyWordsApi.GetWordAsync(p).ConfigureAwait(false);
            return Ok(word);
        }

        [HttpGet]
        [Route("api/check")]
        public async Task<IActionResult> CheckWordAsync([FromQuery] CheckWordParam p)
        {
            var result = await _bordlyWordsApi.IsWordAsync(p).ConfigureAwait(false);
            return Ok(result);
        }

    }

}
