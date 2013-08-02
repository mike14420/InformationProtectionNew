using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Configuration;
using System.Text;

namespace InformationProtection.Models
{
    public class SendMailWrapper
    {
        bool useDevCode;


        public SendMailWrapper(bool isDevMachine)
        {
            useDevCode = isDevMachine;
        }

        public void SendMessage(MailMessage message, String locationCode)
        {
            String AdminEmailAddress;
            String Admin_1_EmailAddress;
            String WebmasterEmail;
            String EmailSubjectMsg;
            bool SendUserMessage = true;

            MailMessage ourMessage = new MailMessage();
            SmtpClient client = new System.Net.Mail.SmtpClient("cynnursing.whsnv.net");
            AdminEmailAddress = String.Empty;
            if (WebConfigurationManager.AppSettings["AdminEmail"] != null)
            {
                AdminEmailAddress = WebConfigurationManager.AppSettings["AdminEmail"].ToString();
            }
            Admin_1_EmailAddress = String.Empty;
            if (WebConfigurationManager.AppSettings["AdminEmail1"] != null)
            {
                Admin_1_EmailAddress = WebConfigurationManager.AppSettings["AdminEmail1"].ToString();
            }
            WebmasterEmail = String.Empty;
            if (WebConfigurationManager.AppSettings["WebmasterEmail"] != null)
            {
                WebmasterEmail = WebConfigurationManager.AppSettings["WebmasterEmail"].ToString();
            }
            EmailSubjectMsg = String.Empty;
            if (WebConfigurationManager.AppSettings["EmailSubjectMsg"] != null)
            {
                EmailSubjectMsg = WebConfigurationManager.AppSettings["EmailSubjectMsg"].ToString();
            }
            SendUserMessage = true;
            if (WebConfigurationManager.AppSettings["SendUserMessage"] != null)
            {
                bool.TryParse(WebConfigurationManager.AppSettings["SendUserMessage"].ToString(), out SendUserMessage);
            }

            // SEND the staff the message
            try
            {
                if (!String.IsNullOrEmpty(AdminEmailAddress))
                {
                    message.Bcc.Add(new MailAddress(AdminEmailAddress));
                }

                if (SendUserMessage)
                {
                    client.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("SendMailWrapper: Email Send Exception " + ex.Message);
            }

            if (useDevCode)
            {
                try
                {
                    String msgSubject = String.Format("{0} {1}", EmailSubjectMsg, locationCode);
                    if (!String.IsNullOrEmpty(AdminEmailAddress))
                    {
                        ourMessage.To.Add(new MailAddress(AdminEmailAddress));
                    }
                    if (!String.IsNullOrEmpty(Admin_1_EmailAddress))
                    {
                        ourMessage.To.Add(new MailAddress(Admin_1_EmailAddress));
                    }
                    // MAKE SURE AT LEAST SOME ONE GETS IT
                    if (ourMessage.To.Count == 0)
                    {
                        ourMessage.To.Add(new MailAddress("mharris@renown.org"));
                    }
                    ourMessage.From = new MailAddress(WebmasterEmail);
                    ourMessage.Subject = msgSubject;
                    ourMessage.IsBodyHtml = true;
                    StringBuilder toAddress = new StringBuilder();
                    StringBuilder ccAddress = new StringBuilder();
                    for (int index = 0; index < message.To.Count; index++)
                    {
                        toAddress.Append(String.Format("Mail Recipient({0})-{1}",
                            index, message.To[index]));
                    }
                    for (int index = 0; index < message.CC.Count; index++)
                    {
                        ccAddress.Append(String.Format("Mail Recipient({0})-{1}",
                            index, message.CC[index]));
                    }
                    ourMessage.Body =
                        String.Format("Subject:{0}<br><br>TO:{1}<br><br>CC:{2}<br><br>SUBJECT:{3}<br><br>BODY:{4}",
                        EmailSubjectMsg, toAddress.ToString(), ccAddress.ToString(), message.Subject, message.Body);

                    // SEND THE MESSAGE
                    client.Send(ourMessage);
                }
                catch (Exception ex)
                {
                    throw new Exception("SendMailWrapper: Email Send Exception " + ex.Message);
                }
            }


        }
    }
}