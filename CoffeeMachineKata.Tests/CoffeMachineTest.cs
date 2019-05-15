using CoffeeMachineKata.Entities;
using CoffeeMachineKata.Logic;
using Moq;
using NUnit.Framework;

namespace CoffeeMachineKata.Tests
{
    [TestFixture]
    class CoffeeMachineTest
    {
        [Test]
        public void Make_OneTea_With_OneSugar_With_Stick()
        {
            var teaCommand = new TeaCommand()
            {
                NumberOfSugar = 1
            };

            Assert.AreEqual("T:1:0", teaCommand.ToString());
        }

        [Test]
        public void Make_OneCoffee_With_NoSugar_AndNo_Stick()
        {
            var coffeeCommand = new CoffeeCommand();

            Assert.AreEqual("C:0:0", coffeeCommand.ToString());
        }

        [Test]
        public void Make_OneChocolate_With_TwoSugar_With_Stick()
        {
            var chocolateCommand = new ChocolateCommand()
            {
                NumberOfSugar = 2
            };

            Assert.AreEqual("H:2:0", chocolateCommand.ToString());
        }

        [Test]
        public void Make_Hi_MessageCommand()
        {
            var messageCommand = new MessageCommand()
            {
                MessageContent = "Hi Drink Maker"
            };

            Assert.AreEqual("M:Hi Drink Maker", messageCommand.ToString());
        }

        [Test]
        public void Make_Coffee_With_ZeroEuro_ReturnAMessageCommand_With_The_60Cent_Missing()
        {
            var coffeeCommand = new CoffeeCommand()
            {
                InsertedMoney = 0M
            };

            var coffeeMachineBusiness = new CoffeeMachineBusiness();
            var result = coffeeMachineBusiness.CallDrinkMakerEntity(coffeeCommand);

            Assert.IsTrue(result is MessageCommand);
            Assert.AreEqual("Not enough money is provided, the amount of money missing is: 0.6", (result as MessageCommand).MessageContent);
        }

        [Test]
        public void Make_Coffee_With_3Cent_ReturnAMessageCommand_With_The_3Cent_Missing()
        {
            var coffeeCommand = new CoffeeCommand()
            {
                InsertedMoney = 0.3M
            };

            var coffeeMachineBusiness = new CoffeeMachineBusiness();
            var result = coffeeMachineBusiness.CallDrinkMakerEntity(coffeeCommand);

            Assert.IsTrue(result is MessageCommand);
            Assert.AreEqual("Not enough money is provided, the amount of money missing is: 0.3", (result as MessageCommand).MessageContent);
        }

        [Test]
        public void Make_Coffee_With_60Cent_ReturnA_Valid_Command()
        {
            var beverageQuantityCheckerMock = new Mock<IBeverageQuantityChecker>();
            beverageQuantityCheckerMock.Setup(s => s.IsEmpty(It.IsAny<DrinkCommand>())).Returns(false);


            var coffeeCommand = new CoffeeCommand()
            {
                InsertedMoney = 0.6M
            };

            var coffeeMachineBusiness = new CoffeeMachineBusiness(beverageQuantityCheckerMock.Object);
            var result = coffeeMachineBusiness.CallDrinkMakerEntity(coffeeCommand);

            Assert.IsTrue(result is CoffeeCommand);
            Assert.AreEqual(CommandStatusEnum.Valid, (result as DrinkCommand).Status);
        }

        [Test]
        public void Make_OrangeJuice()
        {
            var orangeJuiceCommand = new OrangeJuiceCommand();

            Assert.AreEqual("O::", orangeJuiceCommand.ToString());
        }

        [Test]
        public void Make_OrangeJuice_With_30Cent_ReturnA_NotEnoughFundsMessage_And_MissingMoney()
        {
            var orangeJuiceCommand = new OrangeJuiceCommand()
            {
                InsertedMoney = 0.3M
            };

            var coffeeMachineBusiness = new CoffeeMachineBusiness();
            var result = coffeeMachineBusiness.CallDrinkMakerEntity(orangeJuiceCommand);

            Assert.IsTrue(result is MessageCommand);
            Assert.AreEqual("Not enough money is provided, the amount of money missing is: 0.3", (result as MessageCommand).MessageContent);
        }

        [Test]
        public void Make_OrangeJuice_With_60Cent_ReturnA_Valid_Command()
        {
            var beverageQuantityCheckerMock = new Mock<IBeverageQuantityChecker>();
            beverageQuantityCheckerMock.Setup(s => s.IsEmpty(It.IsAny<DrinkCommand>())).Returns(false);

            var orangeJuiceCommand = new OrangeJuiceCommand()
            {
                InsertedMoney = 0.6M
            };

            var coffeeMachineBusiness = new CoffeeMachineBusiness(beverageQuantityCheckerMock.Object);
            var result = coffeeMachineBusiness.CallDrinkMakerEntity(orangeJuiceCommand);

            Assert.IsTrue(result is OrangeJuiceCommand);
            Assert.AreEqual(CommandStatusEnum.Valid, (result as DrinkCommand).Status);
        }

        [Test]
        public void Make_OneExtraHotChocolate_With_TwoSugar_With_Stick()
        {
            var chocolateCommand = new ChocolateCommand()
            {
                NumberOfSugar = 2,
                ExtraHot = true
            };

            Assert.AreEqual("Hh:2:0", chocolateCommand.ToString());
        }

        [Test]
        public void Make_ExtraHotOneTea_With_OneSugar_With_Stick()
        {
            var teaCommand = new TeaCommand()
            {
                NumberOfSugar = 1,
                ExtraHot = true
            };

            Assert.AreEqual("Th:1:0", teaCommand.ToString());
        }

        [Test]
        public void Make_OneExtraHotCoffee_With_NoSugar_AndNo_Stick()
        {
            var coffeeCommand = new CoffeeCommand()
            {
                ExtraHot = true
            };

            Assert.AreEqual("Ch:0:0", coffeeCommand.ToString());
        }

        [Test]
        public void Order_ACoffee_With_Shortage_Display_Message()
        {
            var emailNotifierMock = new Mock<IEmailNotifier>();
            emailNotifierMock.Setup(s => s.NotifyMissingDrink(It.IsAny<DrinkCommand>()));

            var beverageQuantityCheckerMock = new Mock<IBeverageQuantityChecker>();
            beverageQuantityCheckerMock.Setup(s => s.IsEmpty(It.IsAny<DrinkCommand>())).Returns(true);

            var coffeeMachineBusiness = new CoffeeMachineBusiness(beverageQuantityCheckerMock.Object, emailNotifierMock.Object);

            var coffeeCommand = new CoffeeCommand()
            {
                InsertedMoney = 1M
            };

            var result = coffeeMachineBusiness.CallDrinkMakerEntity(coffeeCommand) as MessageCommand;
            
            Assert.IsTrue(result != null);
            Assert.AreEqual(CoffeeMachineMessages.ShortageMessage, result.MessageContent);
        }
    }
}
