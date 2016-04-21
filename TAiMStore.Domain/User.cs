using System.Collections.Generic;

namespace TAiMStore.Domain
{
    public class User : Entity
    {
        public int Id { get; set; }
        public string LoginName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? isActivate { get; set; }

        //связи
        public virtual Role Role { get; set; }
        public virtual Order Order { get; set; }
        public Contacts Contacts { get; set; }
    }
}
