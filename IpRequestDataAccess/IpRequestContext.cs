using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IpModelData;

namespace IpRequestDataAccess
{
    public class IpRequestContext : DbContext
    {
        // Request by types
        public DbSet<CellPhoneDevice>     CellPhoneDevices { get; set; }
        public DbSet<CdBurnerDevice>      CdburnerDevice { get; set; }
        public DbSet<CellPhoneSyncDevice> CellPhoneSyncDevice { get; set; }
        public DbSet<LapTopDevice>        LapTopDevice { get; set; }
        public DbSet<RemoteAccess>        RemoteAccess { get; set; }
        public DbSet<UsbDevice>           UsbDevices { get; set; }
        public DbSet<WirelessDevice>      WirelessDevices { get; set; }

        public DbSet<IpApprovalRequest> Requests { get; set; }
        public DbSet<IpRequestor>       IpRequestor { get; set; }
        public DbSet<IpApprover>        IpApprover { get; set; }
        public DbSet<AdminUser>         AdminUsers { get; set; }
        public DbSet<Error>             Errors { get; set; }      
        public IpRequestContext() : base("IpRequestContext")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new IpRequestorConfiguration());
            modelBuilder.Configurations.Add(new IpApprovalRequestConfiguration());
            modelBuilder.Configurations.Add(new CdBurnerDeviceConfiguration());
            modelBuilder.Configurations.Add(new CellPhoneConfiguration());
            modelBuilder.Configurations.Add(new CellPhoneSyncDeviceConfiguration());
            modelBuilder.Configurations.Add(new IpApproverConfiguration());
            modelBuilder.Configurations.Add(new LapTopDeviceConfiguration());
            modelBuilder.Configurations.Add(new RemoteAccessConfiguration());
            modelBuilder.Configurations.Add(new WirelessDeviceConfiguration());
            modelBuilder.Configurations.Add(new UsbConfiguration());
            modelBuilder.Configurations.Add(new AdminUserConfiguration());
            modelBuilder.Configurations.Add(new ErrorConfiguration());

            
        }
    }
}
