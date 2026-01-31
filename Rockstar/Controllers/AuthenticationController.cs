using Application.Mediator.Authentication.Commands.Create;
using Application.Mediator.Authentication.Queries.Login;
using Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Rockstar.Middlewares;

namespace Rockstar.Controllers
{
    [ApiController]
    [Route("api/authentication")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly BusinessProblemDetailsFactory _problemDetailsFactory;

        public AuthenticationController(
            IMediator mediator,
            IMapper mapper,
            BusinessProblemDetailsFactory problemDetailsFactory)
        {
            _mediator = mediator;
            _mapper = mapper;
            _problemDetailsFactory = problemDetailsFactory;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status409Conflict)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);

            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
            {
                var pd = _problemDetailsFactory.Create(
                     HttpContext,
                     result.Error!
                 );

                return new ObjectResult(pd)
                {
                    StatusCode = pd.Status
                };
            }

            var response = _mapper.Map<AuthenticationResponse>(result.Value!);

            return Ok(response);
        }

        [HttpPost("login")]
        [ProducesResponseType(typeof(AuthenticationResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);

            var result = await _mediator.Send(query);

            var response = _mapper.Map<AuthenticationResponse>(result);

            return Ok(response);
        }
    }
}
