using System;
using System.Collections.Generic;
using System.Text;
using WMC.Core.Util.NativeWrapper2008;

namespace WMC.Core.Employees.BusObj2008
{
    /// <summary>
    /// Summary description for Department.
    /// </summary>
    public class Department
    {
        public static int HH_CORP = 510;
        public static int HH_CORP2 = 500;
        public static int HH_CORP3 = 520;

        private IntObj dept_id;
        private String dept_name;
        private IntObj corp_no;
        private IntObj cost_center;

        public IntObj Dept_id { get { return dept_id; } set { dept_id = value; } }
        public String Dept_name { get { return dept_name; } set { dept_name = value; } }
        public IntObj Corp_no { get { return corp_no; } set { corp_no = value; } }
        public IntObj Cost_center { get { return cost_center; } set { cost_center = value; } }

        public bool IsHometownDept()
        {
            if (dept_id.ToString().Substring(0, 3) == HH_CORP.ToString())
            {
                return true;
            }

            if (dept_id.ToString().Substring(0, 3) == HH_CORP2.ToString())
            {
                return true;
            }

            if (dept_id.ToString().Substring(0, 3) == HH_CORP3.ToString())
            {
                return true;
            }

            return false;
        }

        public Department(IntObj deptId)
        {
            this.dept_id = deptId;
        }

        public Department()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}


