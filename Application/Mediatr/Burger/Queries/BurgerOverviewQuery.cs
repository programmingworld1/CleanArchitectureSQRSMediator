using Application.Mediatr.Burger.Models;
using MediatR;

namespace Application.Mediatr.Burger.Queries
{
    public class BurgerOverviewQuery : IRequest<IEnumerable<BurgerResult>>
    {
    }
}
