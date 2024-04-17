using System.Net.Mail;
using LSP.Business.Abstract;
using LSP.Entity.DTO.Configuration;

namespace LSP.Entity.DTO.MailSmsDtos
{
    public class MailManager : IMailService
    {
        private readonly AppSettings _appSettings;

        public MailManager(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void Send(string email, string message, string subject)
        {
            var host = _appSettings.MailSettings.EmailHost;
            var port = _appSettings.MailSettings.EmailPort;
            var smtpUsername = _appSettings.MailSettings.EmailSmtpUserName;
            var smtpPassword = _appSettings.MailSettings.EmailSmtpPassword;

            SmtpClient client = new(host);
            client.Port = port;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            var fromAddress = new MailAddress(_appSettings.MailSettings.EmailFromAddress, _appSettings.MailSettings.EmailFromTitle, System.Text.Encoding.UTF8);
            var toAddress = new MailAddress(email, email, System.Text.Encoding.UTF8);
            var mail = new MailMessage(fromAddress, toAddress);
            mail.Body = message;

            mail.IsBodyHtml = true;
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

            client.Send(mail);
        }

        public bool WithdrawBalanceInfo(string info)
        {
            string message = info;
            string subject = "WITHDRAW BALANCE ERROR";

            string host = _appSettings.MailSettings.EmailHost;
            int port = _appSettings.MailSettings.EmailPort;
            string smtpUsername = _appSettings.MailSettings.EmailSmtpUserName;
            string smtpPassword = _appSettings.MailSettings.EmailSmtpPassword;

            SmtpClient client = new(host);
            client.Port = port;
            client.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;

            client.Credentials = new System.Net.NetworkCredential(smtpUsername, smtpPassword);
            client.EnableSsl = true;

            MailAddress fromAddress = new MailAddress(_appSettings.MailSettings.EmailFromAddress, "Withdraw Balance Error Occured", System.Text.Encoding.UTF8);
            MailAddress toAddress = new MailAddress("ak.bahadir@hotmail.com", "ak.bahadir@hotmail.com", System.Text.Encoding.UTF8);
            MailMessage m = new MailMessage(fromAddress, toAddress);
            m.CC.Add("ak.bahadir@hotmail.com");
            m.Body = message;
            m.IsBodyHtml = true;
            m.BodyEncoding = System.Text.Encoding.UTF8;
            m.Subject = subject;
            m.SubjectEncoding = System.Text.Encoding.UTF8;

            client.Send(m);
            return true;
        }
    }
}