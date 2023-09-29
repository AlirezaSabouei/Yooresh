using Yooresh.Application.Common.Interfaces;
using System.Net.Mail;
using System.Net;

namespace Yooresh.Infrastructure.EmailTools;

public class Email : IEmail
{
    private readonly string _senderEmail;
    private readonly string _senderPassword;

    public Email(string senderEmail,string senderPassword)
    {
        _senderEmail = senderEmail;
        _senderPassword = senderPassword;
    }
    public async Task SendEmail(string email,string subject, string body)
    {
        // Create a new MailMessage object
        MailMessage mail = new(_senderEmail, email)
        {
            // Set the subject and body of the email
            Subject = subject,
            Body = body
        };

        // Create an SMTP client to send the email
        SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587, // Port for Gmail SMTP
            Credentials = new NetworkCredential(_senderEmail, _senderPassword),
            EnableSsl = true, // Use SSL for secure connection
        };

        try
        {
            // Send the email
            smtpClient.Send(mail);
        }
        finally
        {
            // Dispose of resources
            mail.Dispose();
            smtpClient.Dispose();
        }
    }
}
