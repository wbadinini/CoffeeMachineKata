using System.Collections.Generic;
using CoffeeMachineKata.Entities;

namespace CoffeeMachineKata.Logic
{
    public interface IReportService
    {
        string PrintReport(List<DrinkCommand> repository);
    }
}