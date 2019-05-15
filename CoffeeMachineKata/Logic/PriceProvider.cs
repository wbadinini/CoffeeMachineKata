using CoffeeMachineKata.Entities;

namespace CoffeeMachineKata.Logic
{
    public class PriceProvider: IPriceProvider
    {
        public decimal GetPrice(DrinkCommand command)
        {
            switch (command)
            {
                case TeaCommand _:
                    return 0.4M;
                case OrangeJuiceCommand _:
                case CoffeeCommand _:
                    return 0.6M;
                case ChocolateCommand _:
                    return 0.5M;
                default:
                    return 0;
            }
        }
    }
}
