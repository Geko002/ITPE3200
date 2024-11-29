using Microsoft.AspNetCore.Identity.UI.Services;
using System.Threading.Tasks;

namespace PixNote.Services
{
    public class DummyEmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            // Log the email attempt to the console or any other logger you use
            System.Console.WriteLine($"Sending email to: {email}");
            System.Console.WriteLine($"Subject: {subject}");
            System.Console.WriteLine($"Message: {message}");
            
            // Simulate a successful email send by returning a completed task
            return Task.CompletedTask;
        }
    }
}
