using backend_examen2.Application.Interfaces;
using backend_examen2.Models;
using backend_examen2.Repositories;

namespace backend_examen2.Application
{
    public class PurchaseCommand
    {
        private readonly ICoffeeRepository _coffeeRepository;
        private readonly ICoinRepository _coinRepository;

        public PurchaseCommand(ICoffeeRepository coffeeRepository, ICoinRepository coinRepository)
        {
            _coffeeRepository = coffeeRepository;
            _coinRepository = coinRepository;
        }

        public List<Coin> MakePurchase(PurchaseRequest request)
        {

            List<Coin> purchaseChange = new List<Coin>();
            if (request.Money < request.Total)
            {
                throw new Exception("Dinero insuficiente para realizar la compra");
            }



            if (request.Money != request.Total)
            {
                var coins = _coinRepository.GetCoins();
                purchaseChange = CalculateChange(coins, request.Money, request.Total);
                SubtractCoins(purchaseChange);
            }

            var coffees = _coffeeRepository.GetCoffees();

            foreach (var coffeeRequest in request.Coffees)
            {
                var coffee = coffees.FirstOrDefault(c => c.Name == coffeeRequest.Name);
                if (coffee == null || coffee.Quantity < coffeeRequest.Quantity)
                {
                    throw new Exception($"Cantidad insuficiente de café: {coffeeRequest.Name}");
                }
                coffee.Quantity -= coffeeRequest.Quantity;
            }

            return purchaseChange;
        }

        private List<Coin> CalculateChange(List<Coin> coins, decimal money, decimal total)
        {
            var changeAmount = money - total;
            var change = new List<Coin>();

            foreach (var coin in coins.OrderByDescending(c => c.Denomination))
            {
                if (changeAmount <= 0) break;

                var coinCount = (int)(changeAmount / coin.Denomination);
                if (coinCount > 0)
                {
                    var quantityToUse = Math.Min(coinCount, coin.Quantity);
                    change.Add(new Coin { Denomination = coin.Denomination, Quantity = quantityToUse });
                    changeAmount -= coin.Denomination * quantityToUse;
                }
            }

            if (changeAmount > 0)
            {
                throw new Exception("No se puede proporcionar el vuelto exacto con las monedas disponibles");
            }

            return change;
        }

        private void SubtractCoins(List<Coin> change)
        {
            foreach (var coin in change)
            {
                _coinRepository.SubtractCoins(coin.Denomination, coin.Quantity);
            }
        }
    }
}