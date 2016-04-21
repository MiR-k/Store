using System.Collections.Generic;

namespace TAiMStore.Domain
{
    public  class Role : Entity
    {
        public int Id { get; set; }
        public string NameRole { get; set; }

        //связи
        public ICollection<User> Users { get; set; }
    }
}
