using Application.Interfaces;
using Application.Mediatr.Burger.Models;
using Application.Persistance;
using MediatR;

namespace Application.Mediatr.Burger.Queries
{
    public class BurgerOverviewQueryHandler : IRequestHandler<BurgerOverviewQuery, IEnumerable<BurgerResult>>
    {
        private readonly IBurgerRepository _burgerRepository;
        public BurgerOverviewQueryHandler(IJwtTokenGenerator tokenGenerator, IBurgerRepository burgerRepository)
        {
            _burgerRepository = burgerRepository;
        }

        public async Task<IEnumerable<BurgerResult>> Handle(BurgerOverviewQuery request, CancellationToken cancellationToken)
        {
            var burgersDomain = await _burgerRepository.GetAll();

            var burgers = new List<BurgerResult>();

            foreach (var burgerDomain in burgersDomain)
            {
                burgers.Add(new BurgerResult
                {
                    Description = burgerDomain.Description,
                    Name = burgerDomain.Name,
                });
            }

            return burgers;
        }
    }
}
