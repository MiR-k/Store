using System;
using System.Collections.Generic;
using TAiMStore.Domain;

namespace TAiMStore.Model.Abstract
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
    }
}
