using Application.Mediatr.Authentication.Commands.Register;
using Application.Mediatr.Authentication.Models;
using Application.Mediatr.Burger.Models;
using Application.Persistance;
using MediatR;

namespace Application.Mediatr.Burger.Commands
{
    public class BurgerRegisterCommandHandler : IRequestHandler<BurgerRegisterCommand, BurgerResult>
    {
        private readonly IBurgerRepository _burgerRepository;
        public BurgerRegisterCommandHandler(IBurgerRepository burgerRepository)
        {
            _burgerRepository = burgerRepository;
        }

        public async Task<BurgerResult> Handle(BurgerRegisterCommand command, CancellationToken cancellationToken)
        {
            var burg = new Domain.Entities.Burger()
            {
                Description = command.Description,
                Name = command.Name,
            };

            await _burgerRepository.Add(burg);
            await _burgerRepository.CommitAsync();

            return new BurgerResult()
            {
                Description = command.Description,
                Name = command.Name,
            };
        }
    }
}
