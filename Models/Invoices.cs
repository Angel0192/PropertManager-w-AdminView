using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PropertyManager.Models
{
    public class Invoices
    {
        [Key]
        public int? InvoiceID{get; set;}

        public int? ProjectID{get; set;} = null;

        public int? ScheduleID{get; set;} = null;

        public DateTime InvoiceDate{get; set;} = DateTime.Today;

        public decimal TotalAmount{get; set;}

        public bool IsExported{get; set;} = false;

        public virtual MaintenanceProjects? Project { get; set; } = default!;
        public virtual RentSchedules? RentSchedule { get; set; } = default!;
    }
}