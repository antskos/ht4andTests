using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ht1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            cbSenderSelect.ItemsSource = VariablesClass.Senders;
            cbSenderSelect.DisplayMemberPath = "Key";
            cbSenderSelect.SelectedValuePath = "Value";

            cbSmtp.ItemsSource = MailInfo.mailServers;
            cbSmtp.DisplayMemberPath = "Key";
            cbSmtp.SelectedValuePath = "Value";

            //DBclass db = new DBclass();
            //dgEmails.ItemsSource = db.Emails;
        }

        private void CloseCmd_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Clock_Click(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedItem = tabPlanner;
        }

        private void BtnSendLater_Click(object sender, RoutedEventArgs e)
        {
            SchedulerClass sc = new SchedulerClass();
            TimeSpan tsSendTime = timePickerBox.TimeInterval;
            DateTime dtSendDateTime = (cldSchedulDateTimes.SelectedDate ?? DateTime.Today).Add(tsSendTime);
            if (dtSendDateTime < DateTime.Now)
            {
                MessageBox.Show("Дата и время отправки писем не могут быть раньше, чем настоящее время" );
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(cbSenderSelect.Text,
                                                    EncrypterDll.EncrypterLite.Deencrypt(cbSenderSelect.SelectedValue.ToString(), 10));

            var locator = (ViewModel.ViewModelLocator)FindResource("Locator");
            //sc.SendEmails(dtSendDateTime, emailSender, (IQueryable<Email>)dgEmails.ItemsSource);
            sc.SendEmails(dtSendDateTime, emailSender, locator.Main.ChosedEmails);
        }

        private void BtnSendNow_Click(object sender, RoutedEventArgs e)
        {
            string strLogin = cbSenderSelect.Text;
            if (string.IsNullOrEmpty(strLogin))
            {
                MessageBox.Show("Выберите отправителя");
                return;
            }

            string strPassword = cbSenderSelect.SelectedValue.ToString();
            if (string.IsNullOrEmpty(strPassword))
            {
                MessageBox.Show("Укажите пароль отправителя");
                return;
            }
            EmailSendServiceClass emailSender = new EmailSendServiceClass(strLogin,
                                                EncrypterDll.EncrypterLite.Deencrypt(strPassword, 10));

            emailSender.StrSmtp = cbSmtp.Text;
            if (string.IsNullOrEmpty(emailSender.StrSmtp))
            {
                MessageBox.Show("Укажите SMTP сервер для отправителя");
                return;
            }
            emailSender.MailBody = new TextRange(LetterBody.Document.ContentStart, LetterBody.Document.ContentEnd).Text;
            if (string.IsNullOrEmpty(emailSender.MailBody))
            {
                emailSender.MailBody = "Empty message";
            }
            emailSender.MailSubject = new TextRange(LetterSubject.Document.ContentStart, LetterSubject.Document.ContentEnd).Text;
            if (string.IsNullOrEmpty(emailSender.MailSubject))
            {
                emailSender.MailBody = "No name";
            }

            var locator = (ViewModel.ViewModelLocator)FindResource("Locator");
            //emailSender.SendMails((IQueryable<Email>)dgEmails.ItemsSource);
            emailSender.SendMails(locator.Main.ChosedEmails);
        }

        private void CbSenderSelect_ContextMenuClosing(object sender, ContextMenuEventArgs e)
        {

        }

        private void TabSwitcherControl_btnNextClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex++;
        }

        private void TabSwitcherControl_btnPreviousClick(object sender, RoutedEventArgs e)
        {
            tabControl.SelectedIndex--;
        }

        private void AddLetter_Click(object sender, RoutedEventArgs e)
        {
            ListViewItem item = new ListViewItem();
            letters.Items.Add(item);
        }
    }
}
