using Application.Mediator.Artist.Commands;
using Application.Mediator.Artist.Models;
using Application.Mediator.Artist.Queries;
using Application.Mediator.LibraryImporter.Commands;
using Contracts.Artist;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Rockstar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ArtistsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ArtistsController> _logger;
        private readonly IMapper _mapper;

        public ArtistsController(
            IMediator mediator,
            ILogger<ArtistsController> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet("GetArtist")]
        public async Task<IActionResult> Import([FromQuery] ArtistFindRequest request)
        {
            var query = _mapper.Map<ArtistFindQuery>(request);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("Import")]
        public async Task<IActionResult> Import()
        {
            var command = new ImportLibraryCommand();

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("RegisterArtists")]
        public async Task<IActionResult> Register(List<ArtistRegisterRequest> request)
        {
            var items = _mapper.Map<List<ArtistRegisterItem>>(request);

            var command = new ArtistsRegisterCommand()
            {
                Artists = items
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("DeleteArtist")]
        public async Task<IActionResult> Delete(ArtistDeleteRequest request)
        {
            var command = _mapper.Map<ArtistDeleteCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }
    }
}
