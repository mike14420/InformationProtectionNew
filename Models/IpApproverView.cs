using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using IpDataProvider;
using IpModelData;
using JPR;
using WMC.Core.Employees.BusObj2008;

namespace InformationProtection.Models
{
    public class IpApproverView
    {


        /// <summary>
        /// From a list of approval request we will generate a list of all approvers
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        internal List<IpApproverViewData> GetAllApproversFromApprovers()
        {
            List<IpApproverViewData> approvers;

            String connectionString = WebConfigurationManager.ConnectionStrings["IpRequest"].ConnectionString;
            ApproversDbAcess approversDb = new ApproversDbAcess(connectionString);
            List<IpApprover> retData = approversDb.GetAllByRole(Roles.RoleNameEnum.approver.ToString());
            approvers = Convert(retData);

            return approvers;
        }

        private IpApproverViewData CreateApprover(string firstSupEmpId)
        {
            IpApproverViewData thisEmp = new IpApproverViewData();

            SQLRFSDP dp = null;
            Employee DbEmp = null;
            dp = new SQLRFSDP();
            int EmpIDint = 0;
            int.TryParse(firstSupEmpId, out EmpIDint);
            DbEmp = dp.GetEmployeeById(EmpIDint);
            if (DbEmp != null)
            {
                thisEmp.EmailAddress = DbEmp.Email;
                thisEmp.Title = DbEmp.JobTitle;
                thisEmp.Name = String.Format("{0} {1}", DbEmp.FirstName, DbEmp.LastName);;
                thisEmp.EmpID = DbEmp.Emp_id.IntValue;
                thisEmp.ApproverLevel = IpApproverViewData.ApproverLevelEnum.other;
            }
            return thisEmp;
        }


        public static List<IpApprover> Convert(List<IpApproverViewData> ourData)
        {
            List<IpApprover> retData = new List<IpApprover>();
            if (ourData != null)
            {
                foreach (IpApproverViewData device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }

        public static List<IpApproverViewData> Convert(List<IpApprover> ourData)
        {
            List<IpApproverViewData> retData = new List<IpApproverViewData>();
            if (ourData != null)
            {
                foreach (IpApprover device in ourData)
                {
                    retData.Add(Convert(device));
                }
            }
            return retData;
        }
        public static IpApproverViewData Convert(IpApprover data)
        {
            IpApproverViewData retData = new IpApproverViewData();
            if (data != null)
            {
                IpApproverViewData.ApproverLevelEnum ourlevel;
                Enum.TryParse(data.ApproverLevel, out ourlevel);
                retData.ApproverLevel = ourlevel;
                retData.EmailAddress = data.EmailAddress;
                retData.IpApproverId = data.IpApproverId;
                retData.Name = data.Name;
                retData.Title = data.Title;
                retData.EmpID = data.EmpID;
            }

            return retData;
        }
        public static IpApprover Convert(IpApproverViewData data)
        {
            IpApprover retData = new IpApprover();

            retData.ApproverLevel = data.ApproverLevel.ToString();
            retData.EmailAddress = data.EmailAddress;
            retData.IpApproverId = data.IpApproverId;
            retData.Name = data.Name;
            retData.Title = data.Title;
            retData.EmpID = data.EmpID;

            return retData;
        }


    }
}