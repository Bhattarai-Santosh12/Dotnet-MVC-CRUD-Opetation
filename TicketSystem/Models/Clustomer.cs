using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class Customer
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        
        public string Title { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(1000, ErrorMessage = "Name cannot be longer than 100 characters")]
        public string Description { get; set; }


      //  public string Image { get; set; }
      
        public bool Status { get; set; }

        [Required(ErrorMessage = "CreatedBy is required")]
        public string CreatedBy { get; set; }
     
        public DateTime Created_on { get; set; } = DateTime.Now;
        
        public DateTime last_updated_on { get; set; }= DateTime.Now;
    }
}
