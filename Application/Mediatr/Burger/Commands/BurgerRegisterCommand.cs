using Application.Mediatr.Burger.Models;
using MediatR;

namespace Application.Mediatr.Burger.Commands
{
    public class BurgerRegisterCommand : IRequest<BurgerResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
