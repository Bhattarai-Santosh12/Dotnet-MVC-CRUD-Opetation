using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace TicketSystem.Models
{
    public class AddCustomerViewModel
    {
       
        [Required(ErrorMessage = "Name is required")]
        [StringLength(1000, ErrorMessage = "Name cannot be longer than 1000 characters")]
        public string Title { get; set; }

        public string Description { get; set; }

        // Uncomment and configure if you plan to handle file uploads
        // public IFormFile Image { get; set; }

        public bool Status { get; set; }

        [Required]
        public string CreatedBy { get; set; }

        public DateTime Created_on { get; set; } = DateTime.Now;

        public DateTime last_updated_on { get; set; } = DateTime.Now;
    }
}
