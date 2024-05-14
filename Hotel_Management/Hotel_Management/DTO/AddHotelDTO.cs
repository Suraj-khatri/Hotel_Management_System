using System.ComponentModel.DataAnnotations;

namespace Hotel_Management.DTO
{
    public class AddHotelDTO
    {
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "The hotel address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The hotel email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }
        [Required]
        public IFormFile Photo { get; set; }

       public string? PhotoUrl { get; set; }


    }
}
