using System.Collections.Generic;
using TAiMStore.Domain;

namespace TAiMStore.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<Product> Products { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}