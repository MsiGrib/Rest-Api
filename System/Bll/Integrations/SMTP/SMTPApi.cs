using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using SMTP.Models;

namespace SMTP
{
    internal class SMTPApi
    {
        private readonly HostModel _host;

        public SMTPApi(HostModel host)
        {
            _host = host;
        }

        public async void SendAsync(string to, string subject, string body, bool isHtml = false)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress("Support bonchi:", _host.Username));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(isHtml ? "html" : "plain")
            {
                Text = body,
            };

            using var client = new SmtpClient();
            try
            {
                await client.ConnectAsync(_host.Host, _host.Port, _host.UseSsl
                    ? SecureSocketOptions.SslOnConnect
                    : SecureSocketOptions.StartTls);
                await client.AuthenticateAsync(_host.Username, _host.Password);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
            catch (SslHandshakeException) { }
            catch (SmtpCommandException) { }
            catch (SmtpProtocolException) { }
        }
    }
}