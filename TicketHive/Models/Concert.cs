namespace TicketHive.Models
{
    public class Concert
    {
        //Primary key
        public int ConcertId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;

        public DateTime ConcertTime { get; set; }

        public DateTime PublishDate { get; set; }

        //Foreign keys
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }

        //Navigation property
        public Owner? Owner { get; set; } 
        public Category? Category { get; set; }

    }
}
