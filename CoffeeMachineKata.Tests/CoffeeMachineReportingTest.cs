using CoffeeMachineKata.Entities;
using CoffeeMachineKata.Logic;
using Moq;
using NUnit.Framework;

namespace CoffeeMachineKata.Tests
{
    [TestFixture]
    class CoffeeMachineReportingTest
    {
        [Test]
        public void PrintReport_After_TreeCommand_AndGet_TotalAmount()
        {
            var beverageQuantityCheckerMock = new Mock<IBeverageQuantityChecker>();
            beverageQuantityCheckerMock.Setup(s => s.IsEmpty(It.IsAny<DrinkCommand>())).Returns(false);

            var coffeeMachineBusiness = new CoffeeMachineBusiness(beverageQuantityCheckerMock.Object);

            var coffeeCommand = new CoffeeCommand()
            {
                InsertedMoney = 1M
            };
            var coffeeCommand2 = new CoffeeCommand()
            {
                ExtraHot = true,
                InsertedMoney = 1M
            };
            var orangeJuiceCommand = new OrangeJuiceCommand()
            {
                InsertedMoney = 1M
            };

            coffeeMachineBusiness.CallDrinkMakerEntity(coffeeCommand);
            coffeeMachineBusiness.CallDrinkMakerEntity(coffeeCommand2);
            coffeeMachineBusiness.CallDrinkMakerEntity(orangeJuiceCommand);

            var report = coffeeMachineBusiness.PrintReport();

            Assert.AreEqual("Number of sold drinks: 3\nTotal Amount: 1.8", report);
        }
    }
}
