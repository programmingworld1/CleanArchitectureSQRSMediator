using Application.Mediator.Song.Commands;
using Application.Mediator.Song.Models;
using Application.Mediator.Song.Queries;
using Contracts.Song;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Middlewares;

namespace Rockstar.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/songs")]
    [Authorize]
    public class SongController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly BusinessProblemDetailsFactory _problemDetailsFactory;

        public SongController(
            IMediator mediator,
            IMapper mapper,
            BusinessProblemDetailsFactory problemDetailsFactory)
        {
            _mediator = mediator;
            _mapper = mapper;
            _problemDetailsFactory = problemDetailsFactory;
        }

        [HttpGet]
        // Document all possible outcomes of this endpoint in swagger
        [ProducesResponseType(typeof(GetSongsByGenreResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByGenre([FromQuery] GetSongsByGenreRequest request)
        {
            var query = _mapper.Map<GetSongsByGenreQuery>(request);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateSongRequest request)
        {
            var command = _mapper.Map<CreateSongCommand>(request);

            var result = await _mediator.Send(command);

            if (!result.IsSuccess)
            {
                var pd = _problemDetailsFactory.Create(HttpContext, result.Error!);

                return new ObjectResult(pd) 
                { 
                    StatusCode = pd.Status 
                };
            }

            return Created();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteSongCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
