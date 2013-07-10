using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMC.Core.Util.NativeWrapper2008;

namespace JPR
{
    public class IPR : WMCBusobj
    {
        public static int CONTEXT_UPDATE = 0;
        public static int CONTEXT_NEW_REQUEST = 1;
        public static int CONTEXT_MASS_accessFromBusiness = 2;

        private IntObj ipID;
        private String formID;
        private String userID;
        private String empID;
        private String model;
        private String make;
        private String vendorNum;
        private String size;
        private String deviceType;
        private String securityControls;
        private String osVersion;
        private String serialNumber;
        private String busJustType;
        private String busJustification;
        private String physLocation;
        private String securedAck1;
        private String securedAck2;
        private String securedAck3;
        private String securedAck4;
        private String securedAck5;
        private String securedAck6;
        private String securityAck1;
        private String securityAck2;
        private String securityAck3;
        private String securityAck4;
        private String securityAck5;
        private String securityAck6;
        private String securityAck7;
        private String securityAck8;
        private String securityAck9;
        private String renownOwnedType;
        private String renownOwnedCarrier;
        private String renownOwnedPhone;
        private String personOwnedType;
        private String personOwnedCarrier;
        private String personOwnedPhone;
        private String personOwnedSN;
        private String personOwnedOS;
        private String syncToCalendar;
        private String syncToContacts;
        private String syncToEmail;
        private String userAccessComputer;
        private String userAccessMedia;
        private String typeOfData;
        private String dataSample;
        private String dataStorage;
        private String chkServer;
        private String chkApps;
        private String chkLAN;
        private String chkWorkstation;
        private String accessToServer;
        private String accessToApp;
        private String accessToLAN;
        private String connectionType;
        private String accessFromHome;
        private String accessFromBusiness;
        private String employeeSignature;
        private DateTimeObj submitDate;
        private String fsupName;
        private String fsupApprove;
        private DateTimeObj fsupApproveDate;
        private String vphrName;
        private String vphrApprove;
        private DateTimeObj vphrApproveDate;
        private String rhcfoName;
        private String rhcfoApprove;
        private DateTimeObj rhcfoApproveDate;
        private String ipdName;
        private String ipdApprove;
        private DateTimeObj ipdApproveDate;
        private String rhcioName;
        private String rhcioApprove;
        private DateTimeObj rhcioApproveDate;
        private String approvalStatus;
        private String archive;
        private String comments;

        public IntObj IpID { get { return ipID; } set { ipID = value; } }
        public String FormID { get { return formID; } set { formID = value; } }
        public String UserID { get { return userID; } set { userID = value; } }
        public String EmpID { get { return empID; } set { empID = value; } }
        public String Model { get { return model; } set { model = value; } }
        public String Make { get { return make; } set { make = value; } }
        public String VendorNum { get { return vendorNum; } set { vendorNum = value; } }
        public String Size { get { return size; } set { size = value; } }
        public String DeviceType { get { return deviceType; } set { deviceType = value; } }
        public String SecurityControls { get { return securityControls; } set { securityControls = value; } }
        public String OsVersion { get { return osVersion; } set { osVersion = value; } }
        public String SerialNumber { get { return serialNumber; } set { serialNumber = value; } }
        public String BusJustType { get { return busJustType; } set { busJustType = value; } }
        public String BusJustification { get { return busJustification; } set { busJustification = value; } }
        public String PhysLocation { get { return physLocation; } set { physLocation = value; } }
        public String SecuredAck1 { get { return securedAck1; } set { securedAck1 = value; } }
        public String SecuredAck2 { get { return securedAck2; } set { securedAck2 = value; } }
        public String SecuredAck3 { get { return securedAck3; } set { securedAck3 = value; } }
        public String SecuredAck4 { get { return securedAck4; } set { securedAck4 = value; } }
        public String SecuredAck5 { get { return securedAck5; } set { securedAck5 = value; } }
        public String SecuredAck6 { get { return securedAck6; } set { securedAck6 = value; } }
        public String SecurityAck1 { get { return securityAck1; } set { securityAck1 = value; } }
        public String SecurityAck2 { get { return securityAck2; } set { securityAck2 = value; } }
        public String SecurityAck3 { get { return securityAck3; } set { securityAck3 = value; } }
        public String SecurityAck4 { get { return securityAck4; } set { securityAck4 = value; } }
        public String SecurityAck5 { get { return securityAck5; } set { securityAck5 = value; } }
        public String SecurityAck6 { get { return securityAck6; } set { securityAck6 = value; } }
        public String SecurityAck7 { get { return securityAck7; } set { securityAck7 = value; } }
        public String SecurityAck8 { get { return securityAck8; } set { securityAck8 = value; } }
        public String SecurityAck9 { get { return securityAck9; } set { securityAck9 = value; } }
        public String RenownOwnedType { get { return renownOwnedType; } set { renownOwnedType = value; } }
        public String RenownOwnedCarrier { get { return renownOwnedCarrier; } set { renownOwnedCarrier = value; } }
        public String RenownOwnedPhone { get { return renownOwnedPhone; } set { renownOwnedPhone = value; } }
        public String PersonOwnedType { get { return personOwnedType; } set { personOwnedType = value; } }
        public String PersonOwnedCarrier { get { return personOwnedCarrier; } set { personOwnedCarrier = value; } }
        public String PersonOwnedPhone { get { return personOwnedPhone; } set { personOwnedPhone = value; } }
        public String PersonOwnedSN { get { return personOwnedSN; } set { personOwnedSN = value; } }
        public String PersonOwnedOS { get { return personOwnedOS; } set { personOwnedOS = value; } }
        public String SyncToCalendar { get { return syncToCalendar; } set { syncToCalendar = value; } }
        public String SyncToContacts { get { return syncToContacts; } set { syncToContacts = value; } }
        public String SyncToEmail { get { return syncToEmail; } set { syncToEmail = value; } }
        public String UserAccessComputer { get { return userAccessComputer; } set { userAccessComputer = value; } }
        public String UserAccessMedia { get { return userAccessMedia; } set { userAccessMedia = value; } }
        public String TypeOfData { get { return typeOfData; } set { typeOfData = value; } }
        public String DataSample { get { return dataSample; } set { dataSample = value; } }
        public String DataStorage { get { return dataStorage; } set { dataStorage = value; } }
        public String ChkServer { get { return chkServer; } set { chkServer = value; } }
        public String ChkApps { get { return chkApps; } set { chkApps = value; } }
        public String ChkLAN { get { return chkLAN; } set { chkLAN = value; } }
        public String ChkWorkstation { get { return chkWorkstation; } set { chkWorkstation = value; } }
        public String AccessToServer { get { return accessToServer; } set { accessToServer = value; } }
        public String AccessToApp { get { return accessToApp; } set { accessToApp = value; } }
        public String AccessToLAN { get { return accessToLAN; } set { accessToLAN = value; } }
        public String ConnectionType { get { return connectionType; } set { connectionType = value; } }
        public String AccessFromHome { get { return accessFromHome; } set { accessFromHome = value; } }
        public String AccessFromBusiness { get { return accessFromBusiness; } set { accessFromBusiness = value; } }
        public String EmployeeSignature { get { return employeeSignature; } set { employeeSignature = value; } }
        public DateTimeObj SubmitDate { get { return submitDate; } set { submitDate = value; } }
        public String FsupName { get { return fsupName; } set { fsupName = value; } }
        public String FsupApprove { get { return fsupApprove; } set { fsupApprove = value; } }
        public DateTimeObj FsupApproveDate { get { return fsupApproveDate; } set { fsupApproveDate = value; } }
        public String VphrName { get { return vphrName; } set { vphrName = value; } }
        public String VphrApprove { get { return vphrApprove; } set { vphrApprove = value; } }
        public DateTimeObj VphrApproveDate { get { return vphrApproveDate; } set { vphrApproveDate = value; } }
        public String RhcfoName { get { return rhcfoName; } set { rhcfoName = value; } }
        public String RhcfoApprove { get { return rhcfoApprove; } set { rhcfoApprove = value; } }
        public DateTimeObj RhcfoApproveDate { get { return rhcfoApproveDate; } set { rhcfoApproveDate = value; } }
        public String IpdName { get { return ipdName; } set { ipdName = value; } }
        public String IpdApprove { get { return ipdApprove; } set { ipdApprove = value; } }
        public DateTimeObj IpdApproveDate { get { return ipdApproveDate; } set { ipdApproveDate = value; } }
        public String RhcioName { get { return rhcioName; } set { rhcioName = value; } }
        public String RhcioApprove { get { return rhcioApprove; } set { rhcioApprove = value; } }
        public DateTimeObj RhcioApproveDate { get { return rhcioApproveDate; } set { rhcioApproveDate = value; } }
        public String ApprovalStatus { get { return approvalStatus; } set { approvalStatus = value; } }
        public String Archive { get { return archive; } set { archive = value; } }
        public String Comments { get { return comments; } set { comments = value; } }

        public override void Validate(int context)
        {

            if (context == IPR.CONTEXT_NEW_REQUEST)
            {

                //check to see which request form is being completed and validate required fields 
                if (formID != null)
                {
                    if (formID == "1") //WIRELESS request form
                    {
                        if (model == null || model.Length < 1)
                        {
                            this.AddException("Please enter the Wireless Card Model");
                        }

                        if (vendorNum == null || vendorNum.Length < 1)
                        {
                            this.AddException("Please enter the Wireless Internet Vendor");
                        }

                        if (serialNumber == null || serialNumber.Length < 1)
                        {
                            this.AddException("Please enter the serial number of the Wireless Internet device");
                        }

                        if (busJustType == null || busJustType.Length < 1)
                        {
                            this.AddException("Please choose whether new or existing");
                        }

                        if (busJustification == null || busJustification.Length < 1)
                        {
                            this.AddException("Please enter the business justification");
                        }

                        if (physLocation == null || physLocation.Length < 1)
                        {
                            this.AddException("Please enter the secured physical location of the Wireless Internet device");
                        }

                        if (securedAck1 == null || securedAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all the Renown policy disclaimers");
                        }

                        if (securityAck1 == null || securityAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all security disclaimers");
                        }
                        if (employeeSignature == null || employeeSignature.Length < 1)
                        {
                            this.AddException("Employee Signature is required");
                        }
                    }

                    else if (formID == "2") //USB request form
                    {
                        if (model == null || model.Length < 1)
                        {
                            this.AddException("Please enter the USB Model");
                        }

                        if (size == null || size.Length < 1)
                        {
                            this.AddException("Please enter the USB Size");
                        }

                        if (serialNumber == null || serialNumber.Length < 1)
                        {
                            this.AddException("Please enter the serial number of the USB device");
                        }

                        if (busJustType == null || busJustType.Length < 1)
                        {
                            this.AddException("Please choose whether new or existing");
                        }

                        if (busJustification == null || busJustification.Length < 1)
                        {
                            this.AddException("Please enter the business justification");
                        }

                        if (physLocation == null || physLocation.Length < 1)
                        {
                            this.AddException("Please enter the secured physical location of the USB device");
                        }

                        if (securedAck1 == null || securedAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all the Renown policy disclaimers");
                        }

                        if (securityAck1 == null || securityAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all security disclaimers");
                        }

                        if (employeeSignature == null || employeeSignature.Length < 1)
                        {
                            this.AddException("Employee Signature is required");
                        }
                    }

                    else if (formID == "3") //CD/DVD BURNER request form
                    {
                        if (deviceType == null || deviceType.Length < 1)
                        {
                            this.AddException("Please choose the device type");
                        }

                        if (busJustification == null || busJustification.Length < 1)
                        {
                            this.AddException("Please enter the business justification");
                        }

                        if (typeOfData == null || typeOfData.Length < 1)
                        {
                            this.AddException("Please identify the type of information to be placed on the CD/DVD media");
                        }

                        if (dataStorage == null || dataStorage.Length < 1)
                        {
                            this.AddException("Please identify where the portable media will be stored");
                        }

                        if (serialNumber == null || serialNumber.Length < 1)
                        {
                            this.AddException("Please enter the serial number of the computer");
                        }

                        if (userAccessComputer == null || userAccessComputer.Length < 1)
                        {
                            this.AddException("Please provide the name of individual(s) who will have access to the computer");
                        }

                        if (userAccessMedia == null || userAccessMedia.Length < 1)
                        {
                            this.AddException("Please provide the name of individual(s) who will have access to the portable media");
                        }

                        if (physLocation == null || physLocation.Length < 1)
                        {
                            this.AddException("Please enter the secured physical location of the computer");
                        }

                        if (dataSample == null || dataSample.Length < 1)
                        {
                            this.AddException("Please indicate that the sample of information is included with the request");
                        }

                        if (employeeSignature == null || employeeSignature.Length < 1)
                        {
                            this.AddException("Employee Signature is required");
                        }
                    }

                    else if (formID == "4") //Cell Phone use request form
                    {
                        if (make == null || make.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone make");
                        }

                        if (model == null || model.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone Model");
                        }

                        if (busJustification == null || busJustification.Length < 1)
                        {
                            this.AddException("Please enter the business justification");
                        }

                        if (renownOwnedType == null || renownOwnedType.Length < 1)
                        {
                            this.AddException("Please choose whether new or existing");
                        }

                        if (renownOwnedCarrier == null || renownOwnedCarrier.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone carrier");
                        }

                        if (renownOwnedPhone == null || renownOwnedPhone.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone number");
                        }

                        if (securedAck1 == null || securedAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all the Renown policy disclaimers");
                        }
                        /*
                        if (securityAck1 == null || securityAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all security disclaimers");
                        }
                        */
                        if (employeeSignature == null || employeeSignature.Length < 1)
                        {
                            this.AddException("Employee Signature is required");
                        }
                    }

                    else if (formID == "5") //Cell Phone syncing request form
                    {
                        if (make == null || make.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone make");
                        }

                        if (model == null || model.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone Model");
                        }

                        if (busJustification == null || busJustification.Length < 1)
                        {
                            this.AddException("Please enter the business justification");
                        }

                        if (personOwnedType == null || personOwnedType.Length < 1)
                        {
                            this.AddException("Please choose whether new or existing");
                        }

                        if (personOwnedCarrier == null || personOwnedCarrier.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone carrier");
                        }

                        if (personOwnedPhone == null || personOwnedPhone.Length < 1)
                        {
                            this.AddException("Please enter the Cell Phone number");
                        }

                        if (personOwnedSN == null || personOwnedSN.Length < 1)
                        {
                            this.AddException("Please enter the serial number of the cell phone device");
                        }

                        if (personOwnedOS == null || personOwnedOS.Length < 1)
                        {
                            this.AddException("Please enter the mobile operating system");
                        }

                        if ((syncToCalendar == null || syncToCalendar.Length < 1) && (syncToContacts == null || syncToContacts.Length < 1) && (syncToEmail == null || syncToEmail.Length < 1))
                        {
                            this.AddException("You must select one Syncing option at a minimum.");
                        }

                        if (securedAck1 == null || securedAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all the Renown policy disclaimers");
                        }
                        /*
                        if (securityAck1 == null || securityAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all security disclaimers");
                        }
                        */
                        if (employeeSignature == null || employeeSignature.Length < 1)
                        {
                            this.AddException("Employee Signature is required");
                        }
                    }

                    else if (formID == "6") //LAPTOP request form
                    {
                        if (model == null || model.Length < 1)
                        {
                            this.AddException("Please enter the LAPTOP Model");
                        }

                        if (serialNumber == null || serialNumber.Length < 1)
                        {
                            this.AddException("Please enter the LAPTOP serial number");
                        }

                        if (busJustType == null || busJustType.Length < 1)
                        {
                            this.AddException("Please choose whether new or existing");
                        }

                        if (busJustification == null || busJustification.Length < 1)
                        {
                            this.AddException("Please enter the business justification");
                        }

                        if (physLocation == null || physLocation.Length < 1)
                        {
                            this.AddException("Please enter the secured physical location of the LAPTOP");
                        }

                        if (securedAck1 == null || securedAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all the Renown policy disclaimers");
                        }

                        if (securityAck1 == null || securityAck1.Length < 1)
                        {
                            this.AddException("You must acknowledge all security disclaimers");
                        }
                        if (employeeSignature == null || employeeSignature.Length < 1)
                        {
                            this.AddException("Employee Signature is required");
                        }
                    }

                    else if (formID == "7") //REMOTE ACCESS - INTERNAL request form
                    {
                        if (chkServer == "1")
                        {
                            if (accessToServer == null || accessToServer.Length < 1)
                            {
                                this.AddException("Please specify IP addresses and Server name(s)");
                            }
                        }

                        if (chkApps == "1")
                        {
                            if (accessToApp == null || accessToApp.Length < 1)
                            {
                                this.AddException("Please specify the name(s) of the executable file to be used");
                            }
                        }

                        if (chkLAN == "1")
                        {
                            if (accessToLAN == null || accessToLAN.Length < 1)
                            {
                                this.AddException("Please specify Share name(s), not just drive letter");
                            }
                        }

                        if (chkWorkstation == "1")
                        {
                            if (serialNumber == null || serialNumber.Length < 1)
                            {
                                this.AddException("Please specify the full computer name or serial number");
                            }
                        }

                        if (busJustification == null || busJustification.Length < 1)
                        {
                            this.AddException("Please specify the job duties that require you to have remote access");
                        }

                        if (securedAck3 == null || securedAck3.Length < 1)
                        {
                            this.AddException("Please check that you understand you are responsible for...");
                        }

                        if (securedAck4 == null || securedAck4.Length < 1)
                        {
                            this.AddException("Current pathes");
                        }

                        if (securedAck5 == null || securedAck5.Length < 1)
                        {
                            this.AddException("Current anti-virus");
                        }

                        if (securedAck5 == null || securedAck5.Length < 1)
                        {
                            this.AddException("Personal firewall");
                        }

                        if (securedAck1 == null || securedAck1.Length < 1)
                        {
                            this.AddException("Please check that everything written here is true to the best of your knowledge");
                        }

                        if (employeeSignature == null || employeeSignature.Length < 1)
                        {
                            this.AddException("Employee Signature is required");
                        }
                    }
                }
            }


            if (context == IPR.CONTEXT_UPDATE)
            {
                //place holder for edit error checking
            }

            if (context == IPR.CONTEXT_MASS_accessFromBusiness)
            {
                if (submitDate == null || submitDate.ToString() == "")
                {
                    this.AddException("Cut off date must be a recognized or valid date format: mm/dd/yyyy");
                }
                else
                {
                    Regex rx = new Regex(@"[0-9][0-9]\057[0-9][0-9]\057[0-9][0-9][0-9][0-9]");
                    if (!rx.IsMatch(submitDate.ToString()))
                    {
                        this.AddException("C date must be a recognized or valid date format: mm/dd/yyyy");
                    }
                }
            }
        }


        public override void Validate()
        {
            //place holder for general error cheching
        }


        public IPR()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
