namespace TicketHive.Models
{
    public class Owner
    {
        //Primary key
        public int OwnerId { get; set; }

        public string OwnerName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
