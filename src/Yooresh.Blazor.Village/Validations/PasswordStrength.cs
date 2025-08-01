using System.ComponentModel.DataAnnotations;

namespace Yooresh.Village.Validations;

public class PasswordStrength : ValidationAttribute
{
    public override bool IsValid(object? value)
    {
        if (value == null)
        {
            return false;
        }
        if (string.IsNullOrWhiteSpace(value!.ToString()!))
        {
            return false;
        }
        return value!.ToString()!.Length > 5 &&
               value!.ToString()!.Any(c => char.IsLetter(c)) &&
               value!.ToString()!.Any(c => char.IsDigit(c));
    }
}
