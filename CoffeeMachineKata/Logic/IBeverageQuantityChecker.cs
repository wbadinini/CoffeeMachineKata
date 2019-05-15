using CoffeeMachineKata.Entities;

namespace CoffeeMachineKata.Logic
{
    public interface IBeverageQuantityChecker
    {
        bool IsEmpty(DrinkCommand drink);
    }
}