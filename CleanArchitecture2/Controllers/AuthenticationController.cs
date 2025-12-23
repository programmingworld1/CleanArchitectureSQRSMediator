using Application.Mediatr.Authentication.Commands.Register;
using Application.Mediatr.Authentication.Queries.Login;
using Contracts.Authentication;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : ControllerBase
    {
        //private readonly IAuthenticationCommandService _authenticationCommandService;
        //private readonly IAuthenticationQueryService _authenticationQueryService;

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
            //var res = _authenticationCommandService.Register(
            //    request.FirstName, 
            //    request.LastName, 
            //    request.Email, 
            //    request.Password);

            var command = _mapper.Map<RegisterCommand>(request);

            var result = await _mediator.Send(command);

            var response = _mapper.Map<AuthenticationResponse>(result);

            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            //var res = _authenticationQueryService.Login(
            //    request.Email,
            //    request.Password);

            var query = _mapper.Map<LoginQuery>(request);

            var result = await _mediator.Send(query);

            var response = _mapper.Map<AuthenticationResponse>(result);

            return Ok(response);
        }
    }
}
