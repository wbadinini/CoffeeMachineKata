namespace CoffeeMachineKata.Entities
{
    public class ChocolateCommand : DrinkCommand
    {
        public override string ToString()
        {
            return $"H{(ExtraHot ? "h" : "")}:{NumberOfSugar}:0";
        }
    }
}