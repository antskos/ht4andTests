using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using ht1.Services;
using GalaSoft.MvvmLight.Command;
using System.Linq;
using System;
using System.Collections;
using System.Collections.Generic;

namespace ht1.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        ObservableCollection<Email> _Emails;
        ObservableCollection<Email> _ChosedEmails;

        public ObservableCollection<Email> Emails           // список имеющихся получателей писем
        {
            get { return _Emails; }
            set
            {
                _Emails = value;
                RaisePropertyChanged(nameof(Emails));
            }
        }

        public ObservableCollection<Email> ChosedEmails       // список выбранных получателей писем      
        {
            get { return _ChosedEmails; }
            set
            {
                _ChosedEmails = value;
                RaisePropertyChanged(nameof(ChosedEmails));
            }
        }

        Email _EmailInfo;
        public Email EmailInfo
        {
            get { return _EmailInfo; }
            set
            {
                _EmailInfo = value;
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }


        IDataAccessService _serviceProxy;

        public RelayCommand<object> AddRecipientsCommand { get; set; }
        public RelayCommand<object> DeleteRecipientsCommand { get; set; }

        public RelayCommand<Email> SaveCommand { get; set; }

        public RelayCommand<string> FindByNameCommand { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataAccessService servProxy)
        {
            _serviceProxy = servProxy;
            Emails = new ObservableCollection<Email>();
            ChosedEmails = new ObservableCollection<Email>();
            EmailInfo = new Email();

            GetEmails();

            AddRecipientsCommand = new RelayCommand<object>(AddRecipients);
            DeleteRecipientsCommand = new RelayCommand<object>(DeleteRecipients);
            SaveCommand = new RelayCommand<Email>(SaveEmail);
            FindByNameCommand = new RelayCommand<string>(FindByName);

            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
        }

        private void AddRecipients(object selectedMails)      //команда, добавляет в список получателей пользователей
        {
            if (selectedMails != null)
            {
                var myRecipients = selectedMails as IList;
                List<Email> myList = myRecipients.Cast<Email>().ToList();
                foreach (var item in myList)
                {
                    if (!ChosedEmails.Contains(item)) ChosedEmails.Add(item);
                }
            }
        }

        private void DeleteRecipients(object selectedMails)      //команда, убирает получателей писем из списка
        {
            //if (selectedMails != null)
            //{
            //    var myRecipients = selectedMails as IList;
            //    List<Email> myList = myRecipients.Cast<Email>().ToList();
            //    for (int i = myList.Count - 1; i >= 0; i--)
            //    {
            //        ChosedEmails.Remove(myList[i]);
            //    }
            //}
        }

        void FindByName(string txt)
        {
            Emails.Clear();
            GetEmails();
            var findedList = Emails.Where(em => em.Name.Contains(txt));
            Emails = new ObservableCollection<Email>(findedList);
        }

        public string PrintedName { get; set; }

        void GetEmails()
        {
            Emails.Clear();

            foreach (var item in _serviceProxy.GetEmails())
            {
                Emails.Add(item);
            }
            //if (!String.IsNullOrEmpty(PrintedName)) 
            //{
            //    var newCollection = _serviceProxy.GetEmails().Where(em => em.Name.Contains(PrintedName));
            //    foreach (var item in newCollection)
            //    {
            //        Emails.Add(item);
            //    }
            //}
            //else
            //{
            //    foreach (var item in _serviceProxy.GetEmails())
            //    {
            //        Emails.Add(item);
            //    }
            //}
        }

        void SaveEmail(Email email)
        {
            EmailInfo.Id = _serviceProxy.CreateEmail(email);
            if (EmailInfo.Id != 0)
            {
                Emails.Add(EmailInfo);
                RaisePropertyChanged(nameof(EmailInfo));
            }
        }
    }
}