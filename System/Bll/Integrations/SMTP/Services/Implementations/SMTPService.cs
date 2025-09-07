using SMTP.Models;
using SMTP.Services.Interfaces;

namespace SMTP.Services.Implementations
{
    internal class SMTPService : ISMTPService
    {
        public void SendPasswordReset(HostModel model, string userEmail, string baseUrl, string token)
        {
            var api = new SMTPApi(model);

            string subject = $"Title";
            string confirmationUrl = $"{baseUrl}/" +
                         $"?email={Uri.EscapeDataString(userEmail)}" +
                         $"&token={Uri.EscapeDataString(token)}";

            string body = $@"
                <html>
                  <body style='font-family: Arial, sans-serif; background-color: #f0f2ff; padding: 40px 20px;'>

                  </body>
                </html>";

            api.SendAsync(userEmail, subject, body, true);
        }
    }
}