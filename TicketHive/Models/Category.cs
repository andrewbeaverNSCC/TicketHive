namespace TicketHive.Models
{
    public class Category
    {
        //Primary key
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;

        //Navigation property
        public List<Concert>? Concert { get; set; }
    }
}
