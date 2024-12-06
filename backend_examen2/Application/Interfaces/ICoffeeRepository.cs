using backend_examen2.Models;

namespace backend_examen2.Application.Interfaces
{
    public interface ICoffeeRepository
    {
        List<Coffee> GetCoffees();
    }
}