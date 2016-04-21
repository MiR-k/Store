using System;
using System.Collections.Generic;
using TAiMStore.Domain;

namespace TAiMStore.Model.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<Product> Products { get; }
    }
}
