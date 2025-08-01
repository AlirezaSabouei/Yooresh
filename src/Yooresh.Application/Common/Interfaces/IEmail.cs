namespace Yooresh.Application.Common.Interfaces;

public interface IEmail
{
    public Task SendEmailAsync(string email, string subject, string body);
}
