namespace CoffeeMachineKata.Entities
{
    public class DrinkCommand : Command
    {
        public CommandStatusEnum Status { get; set; }
        public decimal InsertedMoney { get; set; }
        public bool ExtraHot { get; set; }
    }
}