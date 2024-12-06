using backend_examen2.Application.Interfaces;
using backend_examen2.Models;
using backend_examen2.Repositories;

namespace backend_examen2.Application
{

    public class CoffeeQuery
    {
        private readonly ICoffeeRepository _coffeeRepository;

        public CoffeeQuery(ICoffeeRepository coffeeRepository)
        {
            _coffeeRepository = coffeeRepository;
        }

        public List<Coffee> GetCoffees()
        {
            return _coffeeRepository.GetCoffees();
        }
    }
}