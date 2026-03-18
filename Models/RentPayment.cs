using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PropertyManager.Models
{
    public class RentPayment
    {
        // PaymentID
        [Key]
        public int PaymentID {get; set;}

        // ScheduleID
        public int ScheduleID{get; set;}

        // PaymentDate
        public DateTime PaymentDate{get; set;} = DateTime.Today;

        // Amount Paid
        public decimal AmountPaid{get; set;}

        // Payment Method
        public string PaymentMethod{get; set;}

        //Transaction Reference
        public string TransactionReference{get;set;}

        public RentSchedules? RentSchedule {get; set;}
    }
}