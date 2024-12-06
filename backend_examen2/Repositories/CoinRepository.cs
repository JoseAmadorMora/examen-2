using System.Collections.Generic;
using backend_examen2.Application.Interfaces;
using backend_examen2.Models;

namespace backend_examen2.Repositories
{
    public class CoinRepository : ICoinRepository
    {
        private List<Coin> _coins = new List<Coin>
        {
            new Coin { Denomination = 500, Quantity = 20 },
            new Coin { Denomination = 100, Quantity = 30 },
            new Coin { Denomination = 50, Quantity = 50 },
            new Coin { Denomination = 25, Quantity = 25 }
        };

        public List<Coin> GetCoins()
        {
            return _coins;
        }

        public void SubtractCoins(int denomination, int quantity)
        {
            var coin = _coins.FirstOrDefault(c => c.Denomination == denomination);
            if (coin != null)
            {
                coin.Quantity -= quantity;
            }
        }
    }
}