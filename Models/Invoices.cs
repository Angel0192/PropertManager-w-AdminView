using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyManager.Models
{
    public class Invoices
    {
        public int? InvoiceID{get; set;}

        public int? ProjectID{get; set;} = null;

        public int? ScheduleID{get; set;} = null;

        public DateTime InvoiceDate{get; set;} = DateTime.Today;

        public decimal TotalAmount{get; set;}

        public bool isExported{get; set;} = 0;
    }
}