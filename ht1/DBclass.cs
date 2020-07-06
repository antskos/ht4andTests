using System.Linq;

namespace ht1
{
    /// <summary>
    /// Класс, который отвечает за работу с базой данных
    /// </summary>
    class DBclass
    {
        private EmailsDataContext emails = new EmailsDataContext();
        public IQueryable<Email> Emails
        {
            get
            {
                return from c in emails.EmailsTable select c;
            }
        }
    }
}
