namespace TAiMStore.Domain
{
    public class Contacts: Entity
    {
        public int Id { get; set; }
        public string PersonFullName { get; set; }
        public string Organization { get; set; }
        public int PostZip { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string House { get; set; }
        public string Room { get; set; }
        public string Telephone { get; set; }
        
        //связи
        public User User { get; set; }
    }
}
