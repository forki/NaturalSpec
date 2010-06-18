using System;
using StoreSample.Models;

namespace StoreSample.Views
{
    public class CategoryEventArgs : EventArgs
    {
        public CategoryEventArgs(Category category)
        {
            this.Category = category;
        }

        public Category Category { get; private set; }
    }
}