using System.ComponentModel.DataAnnotations;
using Yooresh.Village.Validations;

namespace Yooresh.Village.Models.Players;

public class SignUpDto : ModelBase
{
    [EmailAddress(ErrorMessage = "Valid Email is required")]
    [Required(ErrorMessage = "Valid Email is required")]
    public string Email { get; set; } = string.Empty;

    [PasswordStrength(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [Required(ErrorMessage = "Name is required")]
    public string Name { get; set; }
}
