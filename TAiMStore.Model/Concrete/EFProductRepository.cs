using System.Collections.Generic;
using TAiMStore.Domain;
using TAiMStore.Model.Abstract;

namespace TAiMStore.Model.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Product> Products
        {
            get { return context.Products; }
        }
    }
}