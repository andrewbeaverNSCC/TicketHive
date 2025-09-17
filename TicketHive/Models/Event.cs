namespace TicketHive.Models
{
    public class Event
    {
        //Primary key
        public int EventId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        
        public string Location { get; set; } = string.Empty;

        public DateOnly EventDate { get; set; }
        public TimeOnly EventTime { get; set; }
        public DateTime PublishDate { get; set; }

        //Foreign keys
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }

        public Owner? Owner { get; set; }
        public Category? Category { get; set; }

    }
}
