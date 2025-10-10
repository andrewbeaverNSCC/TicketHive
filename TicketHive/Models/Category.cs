using System.ComponentModel.DataAnnotations;

namespace TicketHive.Models
{
    public class Category
    {

        //Drop down

        //Primary key
        [Display(Name = "Category Id")]

        public int CategoryId { get; set; }

        [Display(Name = "Category Name")]
        public string CategoryName { get; set; } = string.Empty;

        //Navigation property
        public List<Concert>? Concert { get; set; }
    }
}
