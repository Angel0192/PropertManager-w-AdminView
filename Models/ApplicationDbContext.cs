using System;
using Microsoft.EntityFrameworkCore;
using PropertyManager.Models;

namespace PropertyManager.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Property> Properties { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<RentSchedules> RentSchedules { get; set; }
        public DbSet<RentPayment> RentPayments { get; set; } 
        public DbSet<MaintenanceProjects> MaintenanceProjects { get; set; }
        public DbSet<WorkLogs> WorkLogs { get; set; }
        public DbSet<Invoices> Invoices { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Invoices>().Property(i => i.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<MaintenanceProjects>().Property(m => m.BidAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<RentPayment>().Property(r => r.AmountPaid).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<RentSchedules>().Property(r => r.BaseRent).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<RentSchedules>().Property(r => r.LateFeeOccured).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Invoices>()
                .HasOne(i => i.Project)
                .WithMany()
                .HasForeignKey(i => i.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<WorkLogs>()
                .HasOne(w => w.Project)
                .WithMany()
                .HasForeignKey(w => w.ProjectID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Property>().HasData(
                new Property { PropertyID = 1, PropertyName = "Maple Grove", Address = "1420 Lincoln Ave, Newburgh, IN", UnitNum = "101", MonthlyRent = 1200.00m },
                new Property { PropertyID = 2, PropertyName = "Riverview", Address = "200 State St, Newburgh, IN", UnitNum = "A2", MonthlyRent = 1500.00m },
                new Property { PropertyID = 3, PropertyName = "Hidden Creek", Address = "55 Highland Rd, Evansville, IN", UnitNum = "10", MonthlyRent = 950.00m }
            );

            modelBuilder.Entity<Tenant>().HasData(
                new Tenant { TenantID = 1, FirstName = "Archie", LastName = "Bald", Email = "archie@usi.edu", PhoneNum = "812-555-0101", PropertyID = 1 },
                new Tenant { TenantID = 2, FirstName = "Sarah", LastName = "Johnson", Email = "sarah.j@gmail.com", PhoneNum = "812-555-0142", PropertyID = 1 },
                new Tenant { TenantID = 3, FirstName = "Michael", LastName = "Rodriguez", Email = "mike.r@yahoo.com", PhoneNum = "812-555-0789", PropertyID = 2 }
            );

            modelBuilder.Entity<RentSchedules>().HasData(
                new RentSchedules { ScheduleID = 1, TenantID = 1, Date = DateTime.Parse("2026-04-01"), Status = RentStatus.Unpaid, BaseRent = 1200.00m },
                new RentSchedules { ScheduleID = 2, TenantID = 2, Date = DateTime.Parse("2026-03-01"), Status = RentStatus.Paid, BaseRent = 1200.00m },
                new RentSchedules { ScheduleID = 3, TenantID = 3, Date = DateTime.Parse("2026-03-01"), Status = RentStatus.Late, BaseRent = 1500.00m, LateFeeOccured = 50.00m }
            );

            modelBuilder.Entity<MaintenanceProjects>().HasData(
                new MaintenanceProjects { ProjectID = 1, PropertyID = 1, ProjectTitle = "Fix leaking roof", BidAmount = 2850.00m, Status = InvoiceStatus.Approved, AssignedVendor = "RoofMaster LLC" },
                new MaintenanceProjects { ProjectID = 2, PropertyID = 2, ProjectTitle = "HVAC Inspection", BidAmount = 180.00m, Status = InvoiceStatus.Closed, AssignedVendor = "CoolAir Services" },
                new MaintenanceProjects { ProjectID = 3, PropertyID = 1, ProjectTitle = "Kitchen Faucet Repair", BidAmount = 320.00m, Status = InvoiceStatus.Work_Order, AssignedVendor = "PlumbQuick" }
            );

            modelBuilder.Entity<WorkLogs>().HasData(
                new WorkLogs { LogID = 1, ProjectID = 1, ClockInTime = DateTime.Parse("2026-03-16 08:00:00"), ClockOutTime = DateTime.Parse("2026-03-16 12:00:00"), GPSLocation = "37.94, -87.40", MaterialsUsed = "Shingles, Tar", VendorSignature = "Signed-Digitally-01" },
                new WorkLogs { LogID = 2, ProjectID = 2, ClockInTime = DateTime.Parse("2026-03-17 09:00:00"), ClockOutTime = DateTime.Parse("2026-03-17 10:00:00"), GPSLocation = "37.94, -87.41", MaterialsUsed = "Air Filter", VendorSignature = "Signed-Digitally-02" },
                new WorkLogs { LogID = 3, ProjectID = 3, ClockInTime = DateTime.Parse("2026-03-18 13:00:00"), ClockOutTime = DateTime.Parse("2026-03-18 15:00:00"), GPSLocation = "37.94, -87.40", MaterialsUsed = "New Faucet, Teflon Tape", VendorSignature = "Signed-Digitally-03" }
            );

            modelBuilder.Entity<RentPayment>().HasData(
                new RentPayment { PaymentID = 1, ScheduleID = 2, PaymentDate = DateTime.Parse("2026-03-13"), AmountPaid = 1200.00m, PaymentMethod = "ACH", TransactionReference = "TXN778899" },
                new RentPayment { PaymentID = 2, ScheduleID = 3, PaymentDate = DateTime.Parse("2026-03-16"), AmountPaid = 500.00m, PaymentMethod = "Card", TransactionReference = "TXN112233" },
                new RentPayment { PaymentID = 3, ScheduleID = 2, PaymentDate = DateTime.Parse("2026-03-18"), AmountPaid = 100.00m, PaymentMethod = "Cash", TransactionReference = "CASH-001" }
            );

            modelBuilder.Entity<Invoices>().HasData(
                new Invoices { InvoiceID = 1, ProjectID = 1, ScheduleID = 3, InvoiceDate = DateTime.Parse("2026-03-18"), TotalAmount = 2850.00m, IsExported = false },
                new Invoices { InvoiceID = 2, ProjectID = null, ScheduleID = 2, InvoiceDate = DateTime.Parse("2026-03-08"), TotalAmount = 1200.00m, IsExported = true },
                new Invoices { InvoiceID = 3, ProjectID = 2, ScheduleID = 1, InvoiceDate = DateTime.Parse("2026-03-18"), TotalAmount = 180.00m, IsExported = false }
            );
        }
    }
}