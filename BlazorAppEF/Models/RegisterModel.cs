using System.ComponentModel.DataAnnotations;

namespace BlazorAppEF.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "UserName is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        [Phone]
        [Required(ErrorMessage = "PhoneNumber is required")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage = "FirstName is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }
        public string? Father { get; set; }
    }
}
