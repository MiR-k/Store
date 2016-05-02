using System.Collections.Generic;
using TAiMStore.Domain;

namespace TAiMStore.Model.ViewModels
{
    public class ProductsViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }
    }
}
