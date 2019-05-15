namespace CoffeeMachineKata.Entities
{
    public class TeaCommand : DrinkCommand
    {
        public override string ToString()
        {
            return $"T{(ExtraHot ? "h" : "")}:{NumberOfSugar}:0";
        }
    }
}