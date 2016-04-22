using System.Collections.Generic;
using TAiMStore.Domain;

namespace TAiMStore.Models
{
    public class ProductsListView
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string CurrentCategory { get; set; }

    }
}
