namespace Yooresh.Village.WinForms.Players.Models;

[PropertyChanged.AddINotifyPropertyChangedInterface]
public class SignUpModel
{
    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string PasswordConfirmation { get; set; } = null!;
}