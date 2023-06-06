using System.ComponentModel.DataAnnotations;

namespace EF_PCStore.API.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage ="UserName is required")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
