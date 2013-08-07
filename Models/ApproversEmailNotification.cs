using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Routing;
using IpDataProvider;
using IpModelData;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Models
{
    public class ApproversEmailNotification
    {
        private void SubmitRequest(String requestorsEmpId, String toEmail, String toName, String ApproverEmpID)
        {
            
            IpRequestorView ipRequestorView = new Models.IpRequestorView();
            IpRequestorViewData Requestor = ipRequestorView.GetRequestor(requestorsEmpId);
            String requestorsName = Requestor.FullName;

            MailMessage EmailMessage = new MailMessage();
            SendMailWrapper mailWrapper = new SendMailWrapper(true);

            String MikesComputer = WebConfigurationManager.AppSettings["MikesComputer"].ToString().ToLower();
            String ServerName = WebConfigurationManager.AppSettings["ServerName"].ToString().ToLower();
            String WebSiteName = WebConfigurationManager.AppSettings["WebSiteName"].ToString().ToLower();
            String WebmasterEmail = WebConfigurationManager.AppSettings["WebmasterEmail"].ToString().ToLower();
            Boolean SendDevMessage = false;
            SendDevMessage = bool.Parse(WebConfigurationManager.AppSettings["SendDevMessage"].ToString().ToLower());
            String ComputerName = System.Environment.MachineName.ToLower();

            // ADDRESSES
            EmailMessage.To.Add(toEmail);
            EmailMessage.From = new MailAddress(WebmasterEmail);
            EmailMessage.Subject = String.Format("Information Protection Form from {0}", toName);

            StringBuilder sbUri = new StringBuilder();
            if (ComputerName.ToLower() == MikesComputer)
            {
                //String HostName = System.Net.Dns.GetHostName();
                //string url = HttpContext.Current.Request.Url.AbsoluteUri;
                //string path = HttpContext.Current.Request.Url.AbsolutePath;
                //string ServerNamX = HttpContext.Current.Request.RawUrl;

                ServerName = HttpContext.Current.Request.Url.Authority;
                sbUri = new StringBuilder(String.Format("http://{0}/ApproversRequest/Approvers?ApproverEmpID={1}", 
                    ServerName, ApproverEmpID));
            }
            else
            {
                sbUri = new StringBuilder(String.Format("http://{0}/{1}/ApproversRequest/Approvers?ApproverEmpID={2}", 
                    ServerName, WebSiteName, ApproverEmpID));
            }

            StringBuilder sb = new StringBuilder();
            sb.Append(String.Format("{0} has submitted a new Information Protection request form(s). The user id is {1}. ",
                requestorsName, requestorsEmpId));
            sb.Append(String.Format("To view/approve this request,<br><a href=\"{0}\"> Please click here</a>",
                sbUri.ToString()));
            EmailMessage.IsBodyHtml = true;
            EmailMessage.Body = sb.ToString();

            mailWrapper.SendMessage(EmailMessage, String.Empty);

        }


        public bool SubmitRequestToNextApprover(String RequestId)
        {
            // NOW Send notification to next approver
            ApproversEmailNotification approversEmailNotification = new ApproversEmailNotification();
            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApprovalRequestDbAccess approvalRequestDbAccess = new ApprovalRequestDbAccess(connectionString);
            IpApprovalRequest request = approvalRequestDbAccess.GetApprovalRequest(RequestId);
            ApproversEmailNotification Notification = new ApproversEmailNotification();
            IpApprovalRequestViewData ourRequest = IpApprovalRequestView.Convert(request);
            IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();

            ipApprovalRequestView.AddOtherProperties(ourRequest);

            bool retValue = false;
            String requestorsEmpId = ourRequest.RequuestorsEmpId;
            if (ourRequest.IsFirstSupApprovalNextPending())
            {
                SubmitRequest(requestorsEmpId, request.FirstSupEmail, request.FirstSupName, request.FirstSupEmpId.ToString());
            }
            if (ourRequest.IsSecondSupApprovalNextPending())
            {
                SubmitRequest(requestorsEmpId, request.SecondSupEmail, request.SecondSupName, request.SecondSupEmpId.ToString());
            }
            if (ourRequest.IsVpHrApprovalNextPending())
            {
                SubmitRequest(requestorsEmpId, request.VphrEmail, request.VpHrName, request.VpHrApproverEmpId.ToString());
            }
            if (ourRequest.IsRhCfoApprovalNextPending())
            {
                SubmitRequest( requestorsEmpId, request.RhCfoEmail, request.RhCfoName, request.RhCfoApproverEmpId.ToString());
            }
            if (ourRequest.IsIpdApprovalNextPending())
            {
                SubmitRequest(requestorsEmpId, request.IpdEmail, request.IpdName, request.IpdApproverEmpId.ToString());
            }
            if (ourRequest.IsCioApprovalNextPending())
            {
                SubmitRequest(requestorsEmpId, request.CioEmail, request.CioName, request.CioApproverEmpId.ToString());
            }
            return retValue;
        }

        //public bool SubmitRequestToNextApprover(String requestId)
        //{
        //    bool retValue = false;

        //    IpApprovalRequestView ipApprovalRequestView = new IpApprovalRequestView();
        //    IpApprovalRequestViewData request = ipApprovalRequestView.GetApprovalRequest(requestId);
        //    SubmitRequestToNextApprover(request);

        //    return retValue;
        //}


        private void SendApprovedEmail(String toEmail, String subject, String body)
        {


            MailMessage EmailMessage = new MailMessage();
            SendMailWrapper mailWrapper = new SendMailWrapper(true);
            String WebmasterEmail = WebConfigurationManager.AppSettings["WebmasterEmail"].ToString().ToLower();
            String ComputerName = System.Environment.MachineName.ToLower();
            // ADDRESSES
            EmailMessage.To.Add(toEmail);
            EmailMessage.From = new MailAddress(WebmasterEmail);
            EmailMessage.Body = body;
            EmailMessage.IsBodyHtml = true;
            mailWrapper.SendMessage(EmailMessage, String.Empty);

        }

        public bool SendNotificationRequestApproved(IpApprovalRequestViewData request)
        {
            bool retValue = false;
            int requestorsId = request.IpRequestorId;
            String requestorsEmpId = request.RequuestorsEmpId;
            StringBuilder messageBody = new StringBuilder();
            StringBuilder sbUri = new StringBuilder();

            IpRequestorView ipRequestorView = new Models.IpRequestorView();
            IpRequestorViewData Requestor = ipRequestorView.GetRequestor(requestorsEmpId);

            if (requestorsId > 0)
            {
                String newState = request.ApprovedStatus;
                IpApprover.ApproveState newStateEnum;
                Enum.TryParse(newState, out newStateEnum);

                String MikesComputer = WebConfigurationManager.AppSettings["MikesComputer"].ToString().ToLower();
                String ServerName = WebConfigurationManager.AppSettings["ServerName"].ToString().ToLower();
                String WebSiteName = WebConfigurationManager.AppSettings["WebSiteName"].ToString().ToLower();
                String WebmasterEmail = WebConfigurationManager.AppSettings["WebmasterEmail"].ToString().ToLower();
                String ComputerName = System.Environment.MachineName.ToLower();
                // SUBJET
                String subject = String.Format("Information Protection Form from {0} EmpID={1}", Requestor.FullName, Requestor.EmpID);

                // BODY
                if (newStateEnum == IpApprover.ApproveState.approved)
                {
                    messageBody.Append(String.Format("Your Information Protection Request has been approved for a request type of: {0}",
                        request.RequestType));
                }
                if (newStateEnum == IpApprover.ApproveState.resubmit)
                {
                    messageBody.Append(String.Format("Regarding your Information Protection Request of type: {0}. ",
                        request.RequestType));
                    messageBody.Append(String.Format("You have been asked to resubmit."));

                }
                if (newStateEnum == IpApprover.ApproveState.rejected)
                {
                    messageBody.Append(String.Format("Regarding your Information Protection Request of type: {0}. ",
                        request.RequestType));
                    messageBody.Append(String.Format("The Request has been rejected."));

                }
                if (newStateEnum == IpApprover.ApproveState.pending)
                {
                    messageBody.Append(String.Format("Regarding your Information Protection Request of type: {0}. ",
                        request.RequestType));
                    messageBody.Append(String.Format("The Request has been submitted."));

                }

                // build link for the body
                if (ComputerName.ToLower() == MikesComputer)
                {
                    ServerName = HttpContext.Current.Request.Url.Authority;
                    sbUri = new StringBuilder(String.Format("http://{0}/UsersView?ApproverEmpID={1}",
                        ServerName, Requestor.EmpID));
                }
                else
                {
                    sbUri = new StringBuilder(String.Format("http://{0}/{1}/UsersView?ApproverEmpID={2}",
                        ServerName, WebSiteName, Requestor.EmpID));
                }
                messageBody.Append(String.Format("<br/> To view,<br><a href=\"{0}\"> Please click here</a>",
                    sbUri.ToString()));

                SendApprovedEmail(Requestor.Email, subject, messageBody.ToString());
            }
            return retValue;
        }
    }
}
