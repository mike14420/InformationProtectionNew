using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMC.Core.Util.NativeWrapper2008;

namespace JPR
{
    public class IPEmp : WMCBusobj
    {
        public static int CONTEXT_UPDATE = 0;
        public static int CONTEXT_NEW_REQUEST = 1;
        public static int CONTEXT_MASS_ARCHIVE = 2;

        private IntObj userID;
        private String userName;
        private String fName;
        private String lName;
        private String mName;
        private String jobTitle;
        private String empID;
        private String deptID;
        private String deptName;
        private String empPhone;
        private String empEmail;
        private String empStatus;
        private DateTimeObj subDate;


        public IntObj UserID { get { return userID; } set { userID = value; } }
        public String UserName { get { return userName; } set { userName = value; } }
        public String FName { get { return fName; } set { fName = value; } }
        public String LName { get { return lName; } set { lName = value; } }
        public String MName { get { return mName; } set { mName = value; } }
        public String JobTitle { get { return jobTitle; } set { jobTitle = value; } }
        public String EmpID { get { return empID; } set { empID = value; } }
        public String DeptID { get { return deptID; } set { deptID = value; } }
        public String EmpStatus { get { return empStatus; } set { empStatus = value; } }
        public String DeptName { get { return deptName; } set { deptName = value; } }
        public String EmpPhone { get { return empPhone; } set { empPhone = value; } }
        public String EmpEmail { get { return empEmail; } set { empEmail = value; } }
        public DateTimeObj SubDate { get { return subDate; } set { subDate = value; } }


        public override void Validate(int context)
        {

            if (context == IPR.CONTEXT_NEW_REQUEST)
            {
                /*
                if (empStatus == null || empStatus.Length < 1)
                {
                    this.AddException("Please specify whether you are an exempt or a non-exempt employee.");
                }*/
                if (empID == null || empID.Length < 1)
                {
                    this.AddException("Please enter your employee ID");
                }

                if (fName == null || fName.Length < 1)
                {
                    this.AddException("Form user's first name is required");
                }

                if (lName == null || lName.Length < 1)
                {
                    this.AddException("Form user's last name is required");
                }

                if (jobTitle == null || jobTitle.Length < 1)
                {
                    this.AddException("Please enter your job title");
                }

                if (deptName == null || deptName.Length < 1)
                {
                    this.AddException("Please enter your department name");
                }

                if (deptID == null || deptID.Length < 1)
                {
                    this.AddException("Please enter your department number");
                }

                if (empPhone == null || empPhone.Length < 1)
                {
                    this.AddException("Please enter your phone number");
                }

            }
        }


        public override void Validate()
        {
            //place holder for general error cheching
        }


        public IPEmp()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
