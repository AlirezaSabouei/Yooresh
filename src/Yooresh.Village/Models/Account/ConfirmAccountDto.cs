using System.ComponentModel.DataAnnotations;

namespace Yooresh.Village.Models.Account;

public class ConfirmAccountDto : ModelBase
{
    public Guid PlayerId { get; set; }

    [Required(ErrorMessage = "Confirmation Code is required")]
    public string ConfirmationCode { get; set; }
}
