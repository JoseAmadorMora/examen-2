namespace backend_examen2.Models
{
    public class PurchaseRequest
    {
        public int Total { get; set; }
        public int Money { get; set; }
        public List<CoffeeRequest> Coffees { get; set; }
    }
}