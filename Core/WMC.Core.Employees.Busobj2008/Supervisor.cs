using System;
using System.Collections.Generic;
using System.Text;
using WMC.Core.Util.NativeWrapper2008 ;

namespace WMC.Core.Employees.BusObj2008
{
    /// <summary>
    /// Summary description for Supervisor.
    /// </summary>
    public class Supervisor
    {
        private IntObj sup_id;
        private String name;
        private String email;
        private bool isTerminated;

        public IntObj Sup_id { get { return sup_id; } set { sup_id = value; } }
        public String Name { get { return name; } set { name = value; } }
        public String Email { get { return email; } set { email = value; } }
        public bool IsTerminated { get { return isTerminated; } set { isTerminated = value; } }

        public Supervisor(Employee thisEmp)
        {
            if (thisEmp != null)
            {
                this.sup_id = thisEmp.Emp_id;
                this.name = thisEmp.Display_name;
                this.email = thisEmp.Email;
                this.isTerminated = thisEmp.IsTerminated;
            }
            else
            {
                this.sup_id = new IntObj(00000);
                this.name = "N/A";
            }
        }

        public Supervisor()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}

