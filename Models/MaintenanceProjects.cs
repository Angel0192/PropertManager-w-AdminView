using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyManager.Models
{

    public enum InvoiceStatus{Bid, Appoved, Work_Order, Invoiced, Closed}
    public class MaintenanceProjects
    {
        // ProjectID PK
        public int ProjectID{get; set;}

        // PropertyID FK
        public int PropertyID{get; set;}

        // Project Title
        public string ProjectTitle{get; set;}

        // Bid Amount
        public decimal BidAmount{get; set;}
        // Status
        public InvoiceStatus Status{get; set;}

        // Assigned Vendor
        public string AssignedVendor{get; set;}
    }
}