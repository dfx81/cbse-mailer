namespace MailUtils;

using System.Net;
using System.Net.Mail;

public class Mailer
{
    private readonly SmtpClient smtp;

    public Mailer(string host, int port, string user, string pass) {
        smtp = new SmtpClient(host) {
            Port = port,
            Credentials = new NetworkCredential(user, pass),
            EnableSsl = true,
        };
    }

    public void SendMail(string from, string to, string subject, string body) {
        MailMessage message = new MailMessage(from, to) {
            Subject = subject,
            Body = body,
            IsBodyHtml = true,
        };
        
        try
        {
            smtp.Send(message);
        } catch (SmtpException err)
        {
            Console.WriteLine("ERROR {0}: Problem Sending Email.", err.StatusCode);
        }
    }
}
