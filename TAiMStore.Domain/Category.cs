using System.Collections.Generic;

namespace TAiMStore.Domain
{
    public class Category: Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // связи
        public ICollection<Product> Products { get; set; }
    }
}
