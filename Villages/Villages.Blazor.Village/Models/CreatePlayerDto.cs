using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Yooresh.Blazor.Village.Models;

public class CreatePlayerDto
{
    [DisplayName(nameof(Name))]
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength: 50, ErrorMessage = "{0} length should be less than 50 characters)")]
    public string Name { get; set; }

    [DisplayName(nameof(Email))]
    [Required(ErrorMessage = "{0} is required")]
    [EmailAddress(ErrorMessage = "{0} must be valid")]
    public string Email { get; set; }

    [DisplayName(nameof(Password))]
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "{0} length should be between 5 and 50 characters)")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [DisplayName("Confirm Password")]
    [Required(ErrorMessage = "{0} is required")]
    [StringLength(maximumLength: 50, MinimumLength = 5, ErrorMessage = "{0} length should be between 5 and 50 characters")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password))]
    public string ConfirmPassword { get; set; }
}
