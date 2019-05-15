namespace CoffeeMachineKata.Entities
{
    public class Command
    {
        public int NumberOfSugar { get; set; }
        public bool IsWithStick => NumberOfSugar > 0;
    }
}