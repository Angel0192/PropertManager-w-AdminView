using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PropertyManager.Models
{
    public class WorkLogs
    {
        // LogID PK
        [Key]
        public int LogID{get; set;}

        // Project ID FK
        public int ProjectID{get; set;}

        // Clock In Time
        public DateTime ClockInTime{get; set;}

        // Clock Out Time
        public DateTime ClockOutTime{get; set;}

        // GPS Location
        public string GPSLocation{get; set;} = string.Empty;

        // ProofPhotoURL
        public string ProofPhotoURL{get; set;} = string.Empty;

        // Materials Used
        public string MaterialsUsed{get; set;}

        // Vendor Signature
        public string VendorSignature{get; set;}
        
        public MaintenanceProjects? Project { get; set; }
    }
}