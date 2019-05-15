using CoffeeMachineKata.Entities;

namespace CoffeeMachineKata.Logic
{
    public interface IPriceProvider
    {
        decimal GetPrice(DrinkCommand command);
    }
}