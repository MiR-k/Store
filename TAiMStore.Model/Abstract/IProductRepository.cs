using System;
using System.Collections.Generic;
using TAiMStore;

namespace TAiMStore.Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
