using System.Collections.Generic;

namespace TAiMStore.Domain
{
    public class Order : Entity
    {
        public int Id { get; set; }
        
        //
        public virtual User User { get; set; }
        public virtual PaymentType Payment { get; set; }
        public virtual OrderProduct OrderProduct { get; set; }
    }
}
