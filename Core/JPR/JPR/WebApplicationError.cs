using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using WMC.Core.Employees.BusObj2008;
using WMC.Core.Util.NativeWrapper2008;

namespace JPR
{
    public class WebApplicationError
    {

        private IntObj id;
        private IntObj userId;
        private string userName;
        private DateTimeObj timeStamp;
        private string ipAddress;
        private string webPage;
        private string helpLink;
        private string source;
        private string message;
        private string stackTrace;
        private string targetSite;

        public IntObj Id { get { return id; } set { id = value; } }
        public IntObj UserId { get { return userId; } set { userId = value; } }
        public string UserName { get { return userName; } set { userName = value; } }
        public DateTimeObj TimeStamp { get { return timeStamp; } set { timeStamp = value; } }
        public string IpAddress { get { return ipAddress; } set { ipAddress = value; } }
        public string WebPage { get { return webPage; } set { webPage = value; } }
        public string HelpLink { get { return helpLink; } set { helpLink = value; } }
        public string Source { get { return source; } set { source = value; } }
        public string Message { get { return message; } set { message = value; } }
        public string StackTrace { get { return stackTrace; } set { stackTrace = value; } }
        public string TargetSite { get { return targetSite; } set { targetSite = value; } }

        public void SendError()
        {
            MailMessage m = new MailMessage();
            m.From = new MailAddress(ConfigurationManager.AppSettings["WebmasterEmail"]);
            m.To.Add(ConfigurationManager.AppSettings["AdminEmail"]);
            //m.CC.Add("jforman@hometownhealth.com");
            m.Subject = "Award Recognition Form Error";
            m.Body = "Application Error Id: " + id.ToString() + "<br>" + "Message: " + message + "<br>" + "User: " + userName;
            m.IsBodyHtml = true;
            //SmtpClient smtp = new SmtpClient("mail.whsnv.net");
            SmtpClient smtp = new SmtpClient("cynnursing.whsnv.net");
            smtp.Send(m);
        }

        public WebApplicationError(Exception theExc, Employee theEmp, string ip, string thePage)
        {
            id = null;
            if (theEmp != null)
            {
                userId = theEmp.Emp_id;
                userName = theEmp.Logon_name;
            }

            timeStamp = DateTimeObj.FromObj(DateTime.Now);
            ipAddress = ip;
            webPage = thePage;

            if (theExc != null)
            {
                helpLink = theExc.HelpLink;
                source = theExc.Source.ToString();
                message = theExc.Message;
                stackTrace = theExc.StackTrace;
                targetSite = theExc.TargetSite.ToString();
            }
        }

        public WebApplicationError()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
