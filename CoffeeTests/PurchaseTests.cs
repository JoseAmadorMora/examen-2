using backend_examen2.Application;
using backend_examen2.Application.Interfaces;
using backend_examen2.Models;
using Moq;

namespace CoffeeTests
{
    public class PurchaseTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MakePurchase_Returns_CorrectChange()
        {
            // Arrange
            var coinRepository = new Mock<ICoinRepository>();
            coinRepository.Setup(c => c.GetCoins()).Returns(new List<Coin>
            {
                new Coin { Denomination = 500, Quantity = 20 },
                new Coin { Denomination = 100, Quantity = 30 },
                new Coin { Denomination = 50, Quantity = 50 },
                new Coin { Denomination = 25, Quantity = 25 }
            });

            var coffeeRepository = new Mock<ICoffeeRepository>();
            coffeeRepository.Setup(c => c.GetCoffees()).Returns(new List<Coffee>
            {
                new Coffee { Name = "Americano", Price = 100, Quantity = 10 },
                new Coffee { Name = "Latte", Price = 150, Quantity = 5 },
                new Coffee { Name = "Cappuccino", Price = 200, Quantity = 10 }
            });

            var purchase = new PurchaseCommand(coffeeRepository.Object, coinRepository.Object);

            var purchaseRequest = new PurchaseRequest
            {
                Money = 500,
                Total = 200,
                Coffees = new List<CoffeeRequest>
                {
                    new CoffeeRequest { Name = "Americano", Quantity = 3 },
                    new CoffeeRequest { Name = "Latte", Quantity = 2 }
                }
            };

            var changeCoins = purchase.MakePurchase(purchaseRequest);

            var totalChange = changeCoins.Sum(c => c.Denomination * c.Quantity);

            Assert.AreEqual(purchaseRequest.Money - purchaseRequest.Total, totalChange);

        }
    }
}