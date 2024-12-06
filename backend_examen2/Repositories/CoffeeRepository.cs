using backend_examen2.Application.Interfaces;
using backend_examen2.Models;

namespace backend_examen2.Repositories
{
    public class CoffeeRepository : ICoffeeRepository
    {
        private List<Coffee> _coffees = new List<Coffee>
        {
            new Coffee { Name = "Americano", Price = 950, Quantity = 10 },
            new Coffee { Name = "Capuchino", Price = 1200, Quantity = 8 },
            new Coffee { Name = "Latte", Price = 1350, Quantity = 10 },
            new Coffee { Name = "Mocachino", Price = 1500, Quantity = 15 }
        };

        public List<Coffee> GetCoffees()
        {
            return _coffees;
        }
    }
}