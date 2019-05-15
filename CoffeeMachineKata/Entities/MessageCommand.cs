namespace CoffeeMachineKata.Entities
{
    public class MessageCommand : Command
    {
        public string MessageContent { get; set; }

        public override string ToString()
        {
            return $"M:{MessageContent}";
        }
    }
}