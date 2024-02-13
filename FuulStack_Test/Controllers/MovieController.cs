using Asp.Versioning;
using FullStack.Application.UseCase.Queries;
using FullStack.ServiceModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace FullStack.Test.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class MovieController : BaseController
    {
        /// <summary>
        /// this endpoint is used to search movies
        /// </summary>
        /// <returns></returns>
        [HttpGet("MovieSearch")]
        public async Task<ResultViewModel> GetMovieList([FromQuery] string? title = "", [FromQuery] string? id = "", [FromQuery] int PageNum = 1)
        {
            Logger.LogInformation($"Received request to search movies");
            var result = await this.Mediator.Send(new GetMoviesQuery(title, id, PageNum));
            Logger.LogInformation($"Finished retriveing movies");
            Logger.LogInformation("====================================================================================");
            return result;
        }
    }
}
