using BordlyWords.Specification.Api;
using Microsoft.AspNetCore.Mvc;

namespace BordlyWordsWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class InfoController : ControllerBase
    {
        private readonly IBordlyWordsApi _bordlyWordsApi;

        public InfoController(IBordlyWordsApi bordlyWordsApi) => _bordlyWordsApi = bordlyWordsApi;

        [HttpGet]
        [Route("api/info")]
        public async Task<IActionResult> GetInfoAsync()
        {
            var info = await _bordlyWordsApi.GetInfo().ConfigureAwait(false);
            return Ok(info);
        }

    }

}
