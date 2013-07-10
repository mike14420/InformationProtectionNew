using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IpModelData;

namespace IpRequestDataAccess
{
    public class RemoteAccessConfiguration : EntityTypeConfiguration<RemoteAccess>
    {
        public RemoteAccessConfiguration()
        {
        }
    }
}
