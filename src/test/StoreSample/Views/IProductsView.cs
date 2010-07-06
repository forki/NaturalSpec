using System;
using System.Collections.Generic;
using StoreSample.Models;

namespace StoreSample.Views
{
    public interface IProductsView
    {
        event EventHandler<CategoryEventArgs> CategorySelected;
        void SetCategories(IEnumerable<Category> categories);
        void SetProducts(IEnumerable<Product> products);
    }
}