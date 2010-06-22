using System;
using StoreSample.Models;
using StoreSample.Services;
using StoreSample.Views;

namespace StoreSample.Presenter
{
    public class ProductsPresenter
    {
        private readonly ICatalogService _catalog;
        private readonly IProductsView _view;

        public ProductsPresenter(ICatalogService catalog, IProductsView view)
        {
            _catalog = catalog;
            _view = view;
            view.SetCategories(catalog.GetCategories());
            view.CategorySelected += (sender, args) => SelectCategory(args.Category);
        }

        private void SelectCategory(Category category)
        {
            _view.SetProducts(_catalog.GetProducts(category.Id));
        }

        public void PlaceOrder(Order order)
        {
            if (!_catalog.HasInventory(order.Product.Id, order.Quantity))
                return;

            try
            {
                _catalog.Remove(order.Product.Id, order.Quantity);
                order.Filled = true;
            }
            catch (InvalidOperationException)
            {
                // LOG?
            }
        }
    }
}