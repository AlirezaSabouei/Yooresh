using System.ComponentModel.DataAnnotations;
using Yooresh.Village.Validations;

namespace Yooresh.Village.Models.Accounts;

public class SignUpDto : ModelBase
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [PasswordStrength(ErrorMessage = "The Password field is required")]
    public string Password { get; set; }

    [Required]
    public string Name { get; set; }
}
