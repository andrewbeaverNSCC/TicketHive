using System.ComponentModel.DataAnnotations;

namespace TicketHive.Models
{
    public class Concert
    {
        //Primary key
        [Display(Name = "Concert Id")]
        public int ConcertId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // The concerts's filename on the server
        public string Filename { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        
        [Display(Name = "Concert Time")]
        public DateTime ConcertTime { get; set; }

        [Display(Name = "Published")]
        public DateTime PublishDate { get; set; }

        //Foreign keys
        public int OwnerId { get; set; }
        public int CategoryId { get; set; }

        //Navigation property
        public Owner? Owner { get; set; } 
        public Category? Category { get; set; }

    }
}
