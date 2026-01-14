using Application.Mediator.Artist.Commands;
using Application.Mediator.Artist.Models;
using Application.Mediator.Artist.Queries;
using Application.Mediator.Song.Commands;
using Application.Mediator.Song.Queries;
using Contracts.Artist;
using Contracts.Song;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rockstar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class SongController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SongController> _logger;
        private readonly IMapper _mapper;

        public SongController(
            IMediator mediator,
            ILogger<SongController> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetSongsOnGenre")]
        public async Task<IActionResult> Import([FromQuery] SongsGetOnGenreRequest request)
        {
            var query = _mapper.Map<GetSongsByGenreQuery>(request);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("RegisterSong")]
        public async Task<IActionResult> Register(SongRegisterRequest request)
        {
            var command = _mapper.Map<SongRegisterCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("DeleteSong")]
        public async Task<IActionResult> Delete(SongDeleteRequest request)
        {
            var command = _mapper.Map<SongDeleteCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }
    }
}
