using System.ComponentModel.DataAnnotations;

namespace CI_Platform.Models
{
    public class ForgetModel
    {
        [Required(ErrorMessage ="please enter password")]
        public string? Email { get; set; }

    }
}
