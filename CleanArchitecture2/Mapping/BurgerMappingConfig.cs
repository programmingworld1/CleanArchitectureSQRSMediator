using Application.Mediatr.Burger.Commands;
using Application.Mediatr.Burger.Models;
using Contracts.Burger;
using Mapster;

namespace CleanArchitecture2.Mapping
{
    public class BurgerMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<BurgerRegisterRequest, BurgerRegisterCommand>();

            config.NewConfig<BurgerResult, BurgerResponse>();
        }
    }
}
