namespace OrderProcessingLib
{
    public class Order
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public bool IsFilled { get; private set; }

        public Order(string productName, int quantity)
        {
            ProductName = productName;
            Quantity = quantity;
        }

        public void Fill(IWarehouse warehouse)
        {
            if (!warehouse.HasInventory(ProductName, Quantity))
                return;

            warehouse.Remove(ProductName, Quantity);
            IsFilled = true;
        }

        public override string ToString()
        {
            return string.Format("{0} ({1})", ProductName, Quantity);
        }
    }
}