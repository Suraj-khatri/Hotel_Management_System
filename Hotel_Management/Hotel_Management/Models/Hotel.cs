using System.ComponentModel.DataAnnotations;

namespace Hotel_Management.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "The hotel name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The hotel address is required.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "The hotel email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        public string PhotoUrl { get; set; }

    }
}
