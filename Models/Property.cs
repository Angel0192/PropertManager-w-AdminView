using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PropertyManager.Models
{
    public class Property
    {
        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();

        [Key]
        public int PropertyID { get; set; }

        [Required(ErrorMessage = "Property Name is required")]
        [Display(Name = "Property Name")]
        public required string PropertyName { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public required string Address { get; set; }

        public string? UnitNum { get; set; } 

        [Required(ErrorMessage = "Rent amount is required")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal MonthlyRent { get; set; }
    }
}