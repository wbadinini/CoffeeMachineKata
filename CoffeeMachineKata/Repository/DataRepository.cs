using System.Collections.Generic;
using CoffeeMachineKata.Entities;

namespace CoffeeMachineKata.Repository
{
    public static class DataRepository
    {
        public static List<DrinkCommand> Repository { get; set; } = new List<DrinkCommand>();

        public static void SaveInRepository(DrinkCommand command)
        {
            Repository.Add(command);
        }
    }
}
