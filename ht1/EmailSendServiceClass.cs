using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Collections.ObjectModel;

namespace ht1
{
    public class EmailSendServiceClass
    {
        #region vars
        private string mailSender;      // email, c которого будет рассылаться почта
        private string strPassword;     // пароль к email, с которого будет рассылаться почта
        
        public string StrSmtp { get; set; }          // выбор SMTP-сервера для отправки
        public string MailBody { get; set; }         // текст письма для отправки
        public string MailSubject { get; set; }      // тема письма для отправки
        #endregion

        public EmailSendServiceClass(string senderLogin, string senderPassword)
        {
            mailSender = senderLogin;
            strPassword = senderPassword;
            StrSmtp = "25";
            MailBody = "empty message";
            MailSubject = "without subject"; 
        }

        // проходим по списку рассылки и выбираем из списка почтовый адрес для отправки
        public void SendMails(ObservableCollection<Email> emails)
        {
            foreach (Email email in emails)
            {
                SendMail(email.Value);
            }
        }

        public void SendMail(string mailRecipient)
        {
            using (MailMessage mm = new MailMessage(mailSender, mailRecipient))
            {
                mm.IsBodyHtml = false;          // Не используем html в теле письма

                // Авторизуемся на smtp-сервере и отправляем письмо
                SmtpClient sc = new SmtpClient(StrSmtp, MailInfo.mailServers[StrSmtp]);
                sc.EnableSsl = true;
                sc.DeliveryMethod = SmtpDeliveryMethod.Network;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new NetworkCredential(mailSender, strPassword);

                try
                {
                    sc.Send(mm);
                }
                catch(Exception ex) 
                { 
                    MessageBox.Show("Невозможно отправить письмо " + ex.ToString());
                }
            }   // using
        }  // method SendMail(string mailRecipient)

    }
}
