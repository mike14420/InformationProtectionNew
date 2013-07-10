using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpModelData;

namespace IpRequestDataAccess
{
    public class IpRequestorConfiguration : EntityTypeConfiguration<IpRequestor>
    {
        public IpRequestorConfiguration()
        {

            Property(d => d.Fname).IsRequired();
            Property(d => d.Lname).IsRequired();
            Property(d => d.EmpID).IsRequired();
        }
    }
}
