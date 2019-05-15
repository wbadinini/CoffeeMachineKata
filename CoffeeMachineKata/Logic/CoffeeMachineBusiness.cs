using CoffeeMachineKata.Entities;
using CoffeeMachineKata.Repository;

namespace CoffeeMachineKata.Logic
{
    public class CoffeeMachineBusiness
    {
        private readonly IPriceProvider _priceProvider;
        private readonly IReportService _reportService;
        private readonly IEmailNotifier _emailNotifier;
        private readonly IBeverageQuantityChecker _beverageQuantityChecker;

        public CoffeeMachineBusiness(IBeverageQuantityChecker beverageQuantityChecker = null, IEmailNotifier emailNotifier = null)
        {
            _priceProvider = new PriceProvider();
            _reportService = new ReportService();
            _beverageQuantityChecker = beverageQuantityChecker;
            _emailNotifier = emailNotifier;
        }

        public Command CallDrinkMakerEntity(DrinkCommand command)
        {
            var price = _priceProvider.GetPrice(command);
            if (command.InsertedMoney < price)
            {
                var content = string.Format(CoffeeMachineMessages.NotEnoughFunds, price - command.InsertedMoney);
                return new MessageCommand
                {
                    MessageContent = content
                };
            }

            if (_beverageQuantityChecker.IsEmpty(command))
            {
                _emailNotifier.NotifyMissingDrink(command);
                command.Status = CommandStatusEnum.Shortage;

                var content = CoffeeMachineMessages.ShortageMessage;
                return new MessageCommand
                {
                    MessageContent = content
                };
            }
            else
            {
                DataRepository.SaveInRepository(command);
                command.Status = CommandStatusEnum.Valid;
            }

            return command;
        }

        public string PrintReport()
        {
            return _reportService.PrintReport(DataRepository.Repository);
        }
    }
}