using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class ProfileViewModel
    {
        public string UserId { get; set; }

        [Required(ErrorMessage = "Full Name is required.")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }

        [Phone(ErrorMessage = "Invalid Phone Number.")]
        public string Phone { get; set; }
    }
}
