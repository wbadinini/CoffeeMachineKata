using CoffeeMachineKata.Entities;

namespace CoffeeMachineKata.Logic
{
    public interface IEmailNotifier
    {
        void NotifyMissingDrink(DrinkCommand drink);
    }
}