namespace OrderProcessingLib
{
    public interface IWarehouse
    {
        bool HasInventory(string productName, int quantity);
        void Remove(string productName, int quantity);
    }
}