using System.Collections.Generic;

namespace TAiMStore.Domain
{
    public class Product:Entity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DescriptionSecond { get; set; }
        public decimal Price { get; set; }
        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }

        //связи
        public virtual Category Category { get; set; }
        //public virtual OrderProduct OrderProducts { get; set; }
    }
}
