using Application.Mediatr.Authentication.Commands.Register;
using Application.Mediatr.Burger.Commands;
using Application.Mediatr.Burger.Models;
using Application.Mediatr.Burger.Queries;
using Contracts.Authentication;
using Contracts.Burger;
using Domain.Entities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace CleanArchitecture2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class BurgersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly IMapper _mapper;

        public BurgersController(
            IMediator mediator,
            ILogger<AuthenticationController> logger,
            IMapper mapper)
        {
            _mediator = mediator;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> ListBurgers()
        {
            var query = new BurgerOverviewQuery();

            var result = await _mediator.Send(query);

            var burgers = _mapper.Map<List<BurgerResponse>>(result);

            return Ok(burgers);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(BurgerRegisterRequest request)
        {
            var command = _mapper.Map<BurgerRegisterCommand>(request);

            var result = await _mediator.Send(command);

            var response = _mapper.Map<BurgerResponse>(result);

            return Ok(response);
        }
    }
}
