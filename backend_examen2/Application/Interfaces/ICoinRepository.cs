using backend_examen2.Models;

namespace backend_examen2.Application.Interfaces
{
    public interface ICoinRepository
    {
        List<Coin> GetCoins();
        void SubtractCoins(int denomination, int quantity);
    }
}