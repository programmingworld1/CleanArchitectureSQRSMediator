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
        private readonly IMapper _mapper;

        public ArtistsController(
            IMediator mediator,
            IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        [HttpGet("GetArtist")]
        public async Task<IActionResult> Import([FromQuery] ArtistFindRequest request)
        {
            var query = _mapper.Map<FindArtistQuery>(request);

            var result = await _mediator.Send(query);

            return Ok(result);
        }

        [HttpPut("Import")]
        public async Task<IActionResult> Import()
        {
            var command = new ImportLibraryCommand();

            await _mediator.Send(command);

            /*Use 202 Accepted when you start background tasks(queue, job, saga, workflow),
            the final status is not available yet,
            the result will only be available later,
            and the client has to poll for status updates or wait for a callback.*/

            /* you dont want user to have a spinner for 60 sec.
             * you dont want to retry a long running process
             * it will take a lot of processing power, will take the threads, scalability issue
             * youll get timeouts, especially with loadbalancers/proxies
             */

            /* you could return an ID with the Accepted (202) statusCode, so the client can use the ID on a different endpoint to check the status. Just an example */
            return Accepted();
        }

        [HttpPost("RegisterArtists")]
        public async Task<IActionResult> Register(List<ArtistRegisterRequest> request)
        {
            var items = _mapper.Map<List<CreateArtistItem>>(request);

            var command = new CreateArtistsCommand()
            {
                Artists = items
            };

            await _mediator.Send(command);

            return Ok();
        }

        [HttpPost("DeleteArtist")]
        public async Task<IActionResult> Delete(ArtistDeleteRequest request)
        {
            var command = _mapper.Map<DeleteArtistCommand>(request);

            await _mediator.Send(command);

            return Ok();
        }
    }
}
