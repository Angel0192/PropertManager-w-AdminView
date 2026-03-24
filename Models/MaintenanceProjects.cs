using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PropertyManager.Models
{

    public enum InvoiceStatus { Bid, Approved, Work_Order, Invoiced, Closed }
    public class MaintenanceProjects
    {
        // ProjectID PK
        [Key]
        public int ProjectID { get; set; }

        // PropertyID FK
        public int PropertyID { get; set; }

        // Project Title
        public string ProjectTitle { get; set; }

        // Bid Amount
        public decimal BidAmount { get; set; }
        // Status
        public InvoiceStatus Status { get; set; }

        // Assigned Vendor
        public string AssignedVendor { get; set; }

        public Property? Property { get; set; }
    }
}