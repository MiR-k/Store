namespace TAiMStore.Domain
{
    public class OrderProduct : Entity
    {
        public int Id { get; set; }
        public int Count { get; set; }
        
        //связи
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
