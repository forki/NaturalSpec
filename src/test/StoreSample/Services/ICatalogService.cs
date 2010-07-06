using System.Collections.Generic;
using StoreSample.Models;

namespace StoreSample.Services
{
    public interface ICatalogService
    {
        IEnumerable<Category> GetCategories();
        IEnumerable<Product> GetProducts(int categoryId);
        bool HasInventory(int productId, int quantity);
        void Remove(int productId, int quantity);
    }
}