using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Diagnostics;
using System.Configuration;

namespace DataLayer
{
    public static class ExceptionHandler
    {
        static MailMessage mail = new MailMessage();
        static SmtpClient s = new SmtpClient();
        public static void write(Exception e)
        {
            string sSource;
            string sEvent;

            sSource = "CotagAdministration";
            sEvent = e.ToString();

            if (!EventLog.SourceExists(sSource))
            {
                EventLog.CreateEventSource(sSource, "Application");
            }

            EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Error, 2);
            EmailException(e);
        }

        public static void writeInfo(string s)
        {
            string sSource;
            string sEvent;

            sSource = "CotagAdministration";
            sEvent = s;

            if (!EventLog.SourceExists(sSource)) EventLog.CreateEventSource(sSource, "Application");

            EventLog.WriteEntry(sSource, sEvent, EventLogEntryType.Information, 2);
        }

        private static void EmailException(Exception ex)
        {
            if (bool.Parse(ConfigurationManager.AppSettings["sendMail"].ToString()))
            {
                try
                {
                    s.Host = ConfigurationManager.AppSettings["mailServer"].ToString();
                    if (ConfigurationManager.AppSettings["mailTo"].ToString().Contains("|"))
                    {
                        int mailNo = ConfigurationManager.AppSettings["mailTo"].ToString().Split('|').Length;
                        for (int i = 0; i < mailNo; )
                        {
                            s.Send(ConfigurationManager.AppSettings["mailFrom"].ToString(), ConfigurationManager.AppSettings["mailTo"].ToString().Split('|')[i], "An excption occured", ex.ToString());
                            i++;
                        }
                    }
                    else
                    {
                        s.Send(ConfigurationManager.AppSettings["mailFrom"].ToString(), ConfigurationManager.AppSettings["mailTo"].ToString(), "An excption occured", ex.ToString());
                    }
                }
                catch (Exception e)
                {
                    write(e);
                }
            }
        }

    }
}