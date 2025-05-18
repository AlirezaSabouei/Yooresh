namespace Villages.Application.Common.Interfaces;

public interface IEmail
{
    public Task SendEmail(string email, string subject, string body);
}
