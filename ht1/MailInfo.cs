using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ht1
{
    public static class MailInfo
    {
        // dictionary contains mail server name as a key, and server port as a value
        public static Dictionary<string, int> mailServers = new Dictionary<string, int>();

        static MailInfo() 
        {
            mailServers.Add("smtp.yandex.ru", 25);
            mailServers.Add("smtp.mail.ru", 25);
            mailServers.Add("smtp.gmail.com", 587);
        }

    }

    public static class VariablesClass
    {
        public static Dictionary<string, string> Senders { get; } = new Dictionary<string, string>
        {
            { "engineeringwithpassion@gmail.com", "f N @ u146 g O I u146 o I"},
            { "lokilisss@mail.ru", "c u131 B q c u133 N P u132 M"}
        };
    }
}
