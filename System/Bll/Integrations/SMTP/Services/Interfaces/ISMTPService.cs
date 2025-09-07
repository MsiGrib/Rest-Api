using SMTP.Models;

namespace SMTP.Services.Interfaces
{
    public interface ISMTPService
    {
        public void SendPasswordReset(HostModel model, string userEmail, string baseUrl, string token);
    }
}