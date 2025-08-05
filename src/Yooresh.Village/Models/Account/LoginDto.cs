using System.ComponentModel.DataAnnotations;

namespace Yooresh.Village.Models.Account;

public class LoginDto : ModelBase
{
    [Required, EmailAddress]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; }
}
