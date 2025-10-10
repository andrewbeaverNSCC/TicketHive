using System.ComponentModel.DataAnnotations;

namespace TicketHive.Models
{

    //Drop down
    public class Owner
    {
        //Primary key
        [Display(Name = "Owner Id")]
        public int OwnerId { get; set; }

        [Display(Name = "Owner Name")]
        public string OwnerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //Navigation property
        public List<Concert>? Concert { get; set; }
    }
}
