using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyManager.Models
{
    public class Tenant
    {
        public Property? Property { get; set; } 
        
        [Key]
        public int TenantID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNum { get; set; }

        public int PropertyID { get; set; }
    }
}