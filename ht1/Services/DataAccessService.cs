using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace ht1.Services
{
    public interface IDataAccessService
    {
        ObservableCollection<Email> GetEmails();

        int CreateEmail(Email email);
    }

    class DataAccessService : IDataAccessService
    {
        EmailsDataContext context;
        public DataAccessService()
        {
            context = new EmailsDataContext();
        }
        public ObservableCollection<Email> GetEmails()
        {
            ObservableCollection<Email> Emails = new ObservableCollection<Email>();

            foreach (var item in context.EmailsTable)
            {
                Emails.Add(item);
            }
            return Emails;
        }

        public int CreateEmail(Email email) 
        {
            context.EmailsTable.InsertOnSubmit(email);
            context.SubmitChanges();
            return email.Id;
        }
    }
}
