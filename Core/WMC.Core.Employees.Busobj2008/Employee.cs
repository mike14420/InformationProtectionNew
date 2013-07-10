using System;
using System.Collections.Generic;
using System.Text;
using WMC.Core.Util.NativeWrapper2008;

namespace WMC.Core.Employees.BusObj2008
{
	/// <summary>
	/// Summary description for Employee.
	/// </summary>
	public class Employee
	{
		private String display_name;
		private String name;
        private String firstName;
        private String lastName;
        private String middleName;
		private IntObj emp_id;
        private IntObj supId;
        private IntObj techType;
		private String email;
		private String logon_name;
		private String jobTitle;
        private String nickName;
		private bool isTerminated;

		private Supervisor supervisor;
		private Department department;

		public String Display_name{get{return display_name;}set{display_name=value;}}
		public String FirstName{get{return firstName;}set{firstName=value;}}
        public String LastName { get { return lastName; } set { lastName = value; } }
        public String MiddleName { get { return middleName; } set { middleName = value; } }
        public String Name { get { return name; } set { name = value; } }
		public IntObj Emp_id{get{return emp_id;}set{emp_id=value;}}
        public IntObj SupId { get { return supId; } set { supId = value; } }
        public IntObj TechType { get { return techType; } set { techType = value; } }
		public String Email{get{return email;}set{email=value;}}
		public String Logon_name{get{return logon_name;}set{logon_name=value;}}
		public String JobTitle{get{return jobTitle;}set{jobTitle=value;}}
        public String NickName { get { return nickName; } set { nickName = value; } }
		public bool IsTerminated{get{return isTerminated;}set{isTerminated=value;}}

		public Supervisor Supervisor{get{return supervisor;}set{supervisor=value;}}
		public Department Department{get{return department;}set{department=value;}}

		public Employee(Supervisor sup)
		{
			if(sup != null)
			{
				if(sup.Sup_id == IntObj.FromObj(00000) || sup.Name == "N/A")
				{
					this.Emp_id = sup.Sup_id;
					this.Name = sup.Name;
					this.Display_name = sup.Name;
					this.IsTerminated = true;
				}
				else
				{
					this.Emp_id = sup.Sup_id;
					this.Name = sup.Name;
					this.Display_name = sup.Name;
					this.isTerminated = sup.IsTerminated;
				}
			}
		}

		public Employee()
		{
			//
			// TODO: Add constructor logic here
			//
		}
	}
}



