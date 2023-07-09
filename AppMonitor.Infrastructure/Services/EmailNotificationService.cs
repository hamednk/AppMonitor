using AppMonitor.Application.Services;
using AppMonitor.Domain.Entities;

using System.Net.Mail;

namespace AppMonitor.Infrastructure.Services
{
    public class EmailNotificationSettings
    {
        public string Sender { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class EmailNotificationService : INotificationService
    {
        private readonly string _sender;
        private readonly string _password;
        private readonly string _host;
        private readonly int _port;

        public EmailNotificationService(string sender, string password, string host, int port)
        {
            _sender = sender;
            _password = password;
            _host = host;
            _port = port;
        }

        public async Task SendNotificationAsync(TargetApp app)
        {

            var message = new MailMessage();
            message.From = new MailAddress(_sender);
            message.To.Add(new MailAddress(app.User.Email));
            message.Subject = $"App Monitor Alert: {app.Name} is down";
            message.Body = $"The app {app.Name} with URL {app.Url} is down. Please check it as soon as possible.";


            var client = new SmtpClient(_host, _port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_sender, _password);


            await client.SendMailAsync(message);
        }
    }
}