using System.Collections.Generic;
using CoffeeMachineKata.Entities;

namespace CoffeeMachineKata.Logic
{
    public class ReportService : IReportService
    {
        private readonly IPriceProvider _priceProvider;

        public ReportService(IPriceProvider priceProvider = null)
        {
            _priceProvider = new PriceProvider();
        }

        public string PrintReport(List<DrinkCommand> repository)
        {
            var commandCounter = 0;
            var totalAmount = 0M;

            foreach (var drinkCommand in repository)
            {
                commandCounter++;
                totalAmount += _priceProvider.GetPrice(drinkCommand);
            }

            return string.Format(CoffeeMachineMessages.ReportingFormat, commandCounter, totalAmount);
        }
    }
}