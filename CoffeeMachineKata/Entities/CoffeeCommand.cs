namespace CoffeeMachineKata.Entities
{
    public class CoffeeCommand : DrinkCommand
    {
        public override string ToString()
        {
            return $"C{(ExtraHot ? "h" : "")}:{NumberOfSugar}:0";
        }
    }
}