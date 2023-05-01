using CI_Platform.Entities.DataModels;
using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage="Email is required")]
        public string?Email { get; set; }
        [Required(ErrorMessage ="Password is required")]
        public string? Password { get; set; }
        public List<Banner>? banners { get; set; }
    }
}
