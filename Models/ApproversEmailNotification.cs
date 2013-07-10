using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using IpModelData;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Models
{
    public class ApproversEmailNotification
    {
        public void SubmitRequest(String toEmail, String toName, String fromEmpID)
        {
            MailMessage EmailMessage = new MailMessage();
            SendMailWrapper mailWrapper = new SendMailWrapper(true);

            EmployeeView EmpModel = new EmployeeView();
            Employee emp = EmpModel.DbGetEmployeeByEmpId(fromEmpID);
            if (emp != null)
            {
                EmailMessage.To.Add(toEmail);
                EmailMessage.From = new MailAddress(emp.Email);
                EmailMessage.Subject = String.Format("Information Protection Form from {0} EmpID={1}",
                   toName, toEmail);

                String MikesComputer = WebConfigurationManager.AppSettings["MikesComputer"].ToString().ToLower();
                String ServerName = WebConfigurationManager.AppSettings["ServerName"].ToString().ToLower();
                String WebSiteName = WebConfigurationManager.AppSettings["WebSiteName"].ToString().ToLower();

                String ComputerName = System.Environment.MachineName.ToLower();
                Boolean SendDevMessage = false;

                SendDevMessage = bool.Parse(WebConfigurationManager.AppSettings["SendDevMessage"].ToString().ToLower());


                if (ComputerName.ToLower() == MikesComputer)
                {
                    String HostName = System.Net.Dns.GetHostName();
                    string url = HttpContext.Current.Request.Url.AbsoluteUri;
                    string path = HttpContext.Current.Request.Url.AbsolutePath;
                    ServerName = HttpContext.Current.Request.Url.Authority;
                    string ServerNamX = HttpContext.Current.Request.RawUrl;
                }

                StringBuilder sbUri = new StringBuilder(String.Format("http://{0}/{1}/ApproversView?Approver=firstsup&EmpID={2}", ServerName, WebSiteName, toEmail));

                StringBuilder sb = new StringBuilder();
                sb.Append(String.Format("{0} has submitted a new Information Protection request form(s). The user id is {1}",
                    emp.Display_name, emp.Display_name));
                sb.Append(String.Format("To view/approve this request,<br><a href=\"{0}\"> Please click here</a>",
                    sbUri.ToString()));

                EmailMessage.Body = sb.ToString();

            }
            mailWrapper.SendMessage(EmailMessage, String.Empty);

        }


        public bool SubmitRequestToNextApprover(String EmpID, IpApprovalRequestViewData request)
        {
            bool retValue = false;
            if (request.IsFirstSupApprovalNextPending())
            {
                SubmitRequest(request.FirstSupEmail, request.FirstSupName, EmpID);
            }
            if (request.IsSecondSupApprovalNextPending())
            {
                SubmitRequest(request.SecondSupEmail, request.FirstSupName, EmpID);
            }
            if (request.IsVpHrApprovalNextPending())
            {
                SubmitRequest(request.VphrEmail, request.FirstSupName, EmpID);
            }
            if (request.IsRhCfoApprovalNextPending())
            {
                SubmitRequest(request.RhCfoEmail, request.FirstSupName, EmpID);
            }
            if (request.IsIpdApprovalNextPending())
            {
                SubmitRequest(request.IpdEmail, request.FirstSupName, EmpID);
            }
            if (request.IsCioApprovalNextPending())
            {
                SubmitRequest(request.CioEmail, request.FirstSupName, EmpID);
            }
            return retValue;
        }
    }
}
