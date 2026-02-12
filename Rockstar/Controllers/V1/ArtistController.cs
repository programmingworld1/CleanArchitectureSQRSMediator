using Application.Mediator.Artist.Commands;
using Application.Mediator.Artist.Models;
using Application.Mediator.Artist.Queries;
using Application.Mediator.LibraryImporter.Commands;
using Contracts.Artist;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Middlewares;

namespace Rockstar.Controllers.V1
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/artists")]
    [Authorize]
    public class ArtistController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly BusinessProblemDetailsFactory _problemDetailsFactory;

        public ArtistController(
            IMediator mediator,
            IMapper mapper,
            BusinessProblemDetailsFactory problemDetailsFactory)
        {
            _mediator = mediator;
            _mapper = mapper;
            _problemDetailsFactory = problemDetailsFactory;
        }

        [HttpGet]
        [ProducesResponseType(typeof(FindArtistResult), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] FindArtistRequest request)
        {
            var query = _mapper.Map<FindArtistQuery>(request);

            var result = await _mediator.Send(query);
            
            // Check IsSuccess, niet null
            if (!result.IsSuccess)
            {
                var pd = _problemDetailsFactory.Create(HttpContext, result.Error!);

                return new ObjectResult(pd)
                {
                    StatusCode = pd.Status
                };
            }

            return Ok(result.Value);
        }

        [HttpPut("import")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(List<CreateArtistRequest> request)
        {
            var items = _mapper.Map<List<CreateArtistItem>>(request);

            var command = new CreateArtistsCommand(items);

            await _mediator.Send(command);

            return Created();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(int id, UpdateArtistRequest request)
        {
            var command = new UpdateArtistCommand(id, request.Name, request.RowVersion);

            await _mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            var command = new DeleteArtistCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }

        // Restfull = API that follows the REST-principles.
        // Use the correct HTTP-method: GET (only read), PUT(replace fully), PATCH(replace partly), POST (create or an action), DELETE
        // API should be stateless, so each requestcontains all info the server needs
        // in the route, use plural "api/artists instead of api/artist"
        // Correct status codes:
            //•	200 OK - Succesvolle GET/PUT/PATCH
            //•	201 Created - Resource succesvol aangemaakt(bij POST)
            //•	204 No Content - Succesvolle DELETE(geen body)
            //•	400 Bad Request - Validatie error
            //•	401 Unauthorized - Niet geauthenticeerd
            //•	403 Forbidden - Niet geautoriseerd
            //•	404 Not Found - Resource niet gevonden
            //•	409 Conflict - Conflict(bv.duplicate)
            //•	422 Unprocessable Entity - Validatie error(alternatief voor 400)
            //•	500 Internal Server Error - Server fout
        // 201 Created: provide location and body (recommended)
        // No state change via GET
        // Delete/GET/PUT needs to be indempotent: result is always the same if you call it multiple times
        // query parameter for filtering and paging
        // No server side sessions state
        // versioning
        // Lower naming routes
        // Self descriptive: ProblemDetails

    }
}
