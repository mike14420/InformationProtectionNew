using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpModelData;

namespace IpRequestDataAccess
{
    public class CellPhoneConfiguration : EntityTypeConfiguration<CellPhoneDevice>
    {
        public CellPhoneConfiguration()
        {
            Property(d => d.Make).IsRequired();
            Property(d => d.Model).IsRequired();
        }
    }
}
