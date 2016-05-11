using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAiMStore.Domain;
using TAiMStore.Model.Abstract;

namespace TAiMStore.Model.Concrete
{
    public class EFDCategoryRepository : ICategoryRepository
    {

        StoreContext context = new StoreContext();

        public IEnumerable<Category> Categories
        {
            get { return context.Categories; }
        }
    }
}
