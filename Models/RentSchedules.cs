using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyManager.Models
{
    public enum RentStatus {Unpaid, Paid, Late, Partial}
    public class RentSchedules
    {
        // Schedule ID PK
        [Key]
        public int ScheduleID {get;set;}

        // Tenant ID FK
        public int TenantID{get; set;}

        // Due Date
        public DateTime Date{get;set;}

        // Status (Unpaid, Paid, Late, Partial) : Default = unpaid
        public RentStatus? Status{get; set;} = RentStatus.Unpaid;
        // Base Rent
        public decimal BaseRent{get; set;}

        // Late Fee Occured
        public decimal LateFeeOccured{get; set;} = 0.00m;

        // Reminder Count : Default = 0
        public int ReminderCount{get; set;} = 0;

        public Tenant? Tenant { get; set; }
        
    }
}