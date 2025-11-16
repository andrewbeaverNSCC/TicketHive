using System.ComponentModel.DataAnnotations;

namespace TicketHive.Models
{
    public class Purchase
    {
        [Display(Name = "Purchase Id")]
        public int PurchaseId { get; set; }

        [Display(Name = "Number of Tickets")]
        public int NumOfTickets { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        //Credit Card Payment details
        [Display(Name = "Name on Card")]
        public string NameOnCard { get; set; } = string.Empty;

        [Display(Name = "Card Number")]
        public int CardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        public int ExpirationDate { get; set; }

        [Display(Name = "CVV")]
        public int CVV { get; set; }

        // Foreign key to Concert
        public int ConcertId { get; set; }

        // Navigation to the principal entity
        public Concert? Concert { get; set; }
    }
}
