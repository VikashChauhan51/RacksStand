 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailFactory
{
    public  class Email
    {
        //smtp.gmail.com	25, 587	TLS
        //smtp.gmail.com	465	SSL
     
        public static void TrySendEmail(Credentials credentials,string from, string subject, string body, List<string> toList, List<string> ccList, List<string> bcclist, List<string> attachments)
        {
            try
            {
                if (credentials == null) return;
                using (SmtpClient SmtpServer = new SmtpClient(credentials.Host))
                {
                    using (MailMessage mail = new MailMessage())
                    {
                        mail.From = new MailAddress(from);

                        mail.Subject = subject;
                        mail.Body = body;
                        mail.IsBodyHtml = credentials.IsBodyHtml;
                        foreach (var toRecipients in toList)
                        {
                            mail.To.Add(toRecipients);
                        }
                        foreach (var ccRecipients in ccList ?? new List<string>())
                        {
                            mail.CC.Add(ccRecipients);
                        }
                        foreach (var bccRecipients in bcclist ?? new List<string>())
                        {
                            mail.Bcc.Add(bccRecipients);
                        }
                        foreach (var filePath in attachments ?? new List<string>())
                        {
                            mail.Attachments.Add(new System.Net.Mail.Attachment(filePath));
                        }
                        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                        SmtpServer.Port = credentials.Port;
                        SmtpServer.EnableSsl = credentials.EnableSsl;
                        SmtpServer.Credentials = new System.Net.NetworkCredential(credentials.UserName, credentials.Password);
                        SmtpServer.Send(mail);
                    }
                }

            }
            catch
            {
                return;
            }

        }
        public static void TrySendAsyncEmail(Credentials credentials,string from, string subject, string body, List<string> toList, List<string> ccList, List<string> bcclist, List<string> attachments)
        {
            try
            {
                if (credentials == null) return;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(credentials.Host);
                mail.From = new MailAddress(from);

                mail.Subject = subject;
                mail.Body = body;
                mail.IsBodyHtml = credentials.IsBodyHtml;
                foreach (var toRecipients in toList)
                {
                    mail.To.Add(toRecipients);
                }
                foreach (var ccRecipients in ccList ?? new List<string>())
                {
                    mail.CC.Add(ccRecipients);
                }
                foreach (var bccRecipients in bcclist ?? new List<string>())
                {
                    mail.Bcc.Add(bccRecipients);
                }
                foreach (var filePath in attachments ?? new List<string>())
                {
                    mail.Attachments.Add(new System.Net.Mail.Attachment(filePath));
                }

                SmtpServer.Port = credentials.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(credentials.UserName, credentials.Password);
                SmtpServer.EnableSsl = credentials.EnableSsl;
                SmtpServer.SendMailAsync(mail);

            }
            catch
            {
                return;
            }
        }


    }
}
