using Application.Mediator.Authentication.Commands.Create;
using Application.Mediator.Authentication.Queries.Login;
using Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Rockstar.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        public AuthenticationController(
            IMediator mediator,
            ILogger<AuthenticationController> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = _mapper.Map<CreateUserCommand>(request);

            var result = await _mediator.Send(command);

            if(!result.IsSuccess)
            {
                var pd = BusinessProblemDetailsFactory.Create(
                     HttpContext,
                     result.Error!
                 );

                return new ObjectResult(pd)
                {
                    StatusCode = pd.Status
                };
            }

            var response = _mapper.Map<AuthenticationResponse>(result.Value);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var query = _mapper.Map<LoginQuery>(request);

            var result = await _mediator.Send(query);

            var response = _mapper.Map<AuthenticationResponse>(result);

            return Ok(response);
        }
    }
}
