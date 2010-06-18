namespace StoreSample.Models
{
    public class Order
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public bool Filled { get; set; }
    }
}