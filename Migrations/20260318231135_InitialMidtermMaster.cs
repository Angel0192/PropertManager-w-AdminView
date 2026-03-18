using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PropertyManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialMidtermMaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Properties",
                columns: table => new
                {
                    PropertyID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UnitNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyRent = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Properties", x => x.PropertyID);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceProjects",
                columns: table => new
                {
                    ProjectID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PropertyID = table.Column<int>(type: "int", nullable: false),
                    ProjectTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    AssignedVendor = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceProjects", x => x.ProjectID);
                    table.ForeignKey(
                        name: "FK_MaintenanceProjects_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tenants",
                columns: table => new
                {
                    TenantID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PropertyID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tenants", x => x.TenantID);
                    table.ForeignKey(
                        name: "FK_Tenants_Properties_PropertyID",
                        column: x => x.PropertyID,
                        principalTable: "Properties",
                        principalColumn: "PropertyID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkLogs",
                columns: table => new
                {
                    LogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: false),
                    ClockInTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClockOutTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GPSLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProofPhotoURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaterialsUsed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VendorSignature = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkLogs", x => x.LogID);
                    table.ForeignKey(
                        name: "FK_WorkLogs_MaintenanceProjects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "MaintenanceProjects",
                        principalColumn: "ProjectID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RentSchedules",
                columns: table => new
                {
                    ScheduleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantID = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: true),
                    BaseRent = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LateFeeOccured = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ReminderCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentSchedules", x => x.ScheduleID);
                    table.ForeignKey(
                        name: "FK_RentSchedules_Tenants_TenantID",
                        column: x => x.TenantID,
                        principalTable: "Tenants",
                        principalColumn: "TenantID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectID = table.Column<int>(type: "int", nullable: true),
                    ScheduleID = table.Column<int>(type: "int", nullable: true),
                    InvoiceDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsExported = table.Column<bool>(type: "bit", nullable: false),
                    RentScheduleScheduleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceID);
                    table.ForeignKey(
                        name: "FK_Invoices_MaintenanceProjects_ProjectID",
                        column: x => x.ProjectID,
                        principalTable: "MaintenanceProjects",
                        principalColumn: "ProjectID");
                    table.ForeignKey(
                        name: "FK_Invoices_RentSchedules_RentScheduleScheduleID",
                        column: x => x.RentScheduleScheduleID,
                        principalTable: "RentSchedules",
                        principalColumn: "ScheduleID");
                });

            migrationBuilder.CreateTable(
                name: "RentPayments",
                columns: table => new
                {
                    PaymentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleID = table.Column<int>(type: "int", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AmountPaid = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PaymentMethod = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransactionReference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RentScheduleScheduleID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentPayments", x => x.PaymentID);
                    table.ForeignKey(
                        name: "FK_RentPayments_RentSchedules_RentScheduleScheduleID",
                        column: x => x.RentScheduleScheduleID,
                        principalTable: "RentSchedules",
                        principalColumn: "ScheduleID");
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceID", "InvoiceDate", "IsExported", "ProjectID", "RentScheduleScheduleID", "ScheduleID", "TotalAmount" },
                values: new object[] { 2, new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, null, 2, 1200.00m });

            migrationBuilder.InsertData(
                table: "Properties",
                columns: new[] { "PropertyID", "Address", "MonthlyRent", "PropertyName", "UnitNum" },
                values: new object[,]
                {
                    { 1, "1420 Lincoln Ave, Newburgh, IN", 1200.00m, "Maple Grove", "101" },
                    { 2, "200 State St, Newburgh, IN", 1500.00m, "Riverview", "A2" },
                    { 3, "55 Highland Rd, Evansville, IN", 950.00m, "Hidden Creek", "10" }
                });

            migrationBuilder.InsertData(
                table: "RentPayments",
                columns: new[] { "PaymentID", "AmountPaid", "PaymentDate", "PaymentMethod", "RentScheduleScheduleID", "ScheduleID", "TransactionReference" },
                values: new object[,]
                {
                    { 1, 1200.00m, new DateTime(2026, 3, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "ACH", null, 2, "TXN778899" },
                    { 2, 500.00m, new DateTime(2026, 3, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "Card", null, 3, "TXN112233" },
                    { 3, 100.00m, new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cash", null, 2, "CASH-001" }
                });

            migrationBuilder.InsertData(
                table: "MaintenanceProjects",
                columns: new[] { "ProjectID", "AssignedVendor", "BidAmount", "ProjectTitle", "PropertyID", "Status" },
                values: new object[,]
                {
                    { 1, "RoofMaster LLC", 2850.00m, "Fix leaking roof", 1, 1 },
                    { 2, "CoolAir Services", 180.00m, "HVAC Inspection", 2, 4 },
                    { 3, "PlumbQuick", 320.00m, "Kitchen Faucet Repair", 1, 2 }
                });

            migrationBuilder.InsertData(
                table: "Tenants",
                columns: new[] { "TenantID", "Email", "FirstName", "LastName", "PhoneNum", "PropertyID" },
                values: new object[,]
                {
                    { 1, "archie@usi.edu", "Archie", "Bald", "812-555-0101", 1 },
                    { 2, "sarah.j@gmail.com", "Sarah", "Johnson", "812-555-0142", 1 },
                    { 3, "mike.r@yahoo.com", "Michael", "Rodriguez", "812-555-0789", 2 }
                });

            migrationBuilder.InsertData(
                table: "Invoices",
                columns: new[] { "InvoiceID", "InvoiceDate", "IsExported", "ProjectID", "RentScheduleScheduleID", "ScheduleID", "TotalAmount" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, null, null, 2850.00m },
                    { 3, new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, null, null, 180.00m }
                });

            migrationBuilder.InsertData(
                table: "RentSchedules",
                columns: new[] { "ScheduleID", "BaseRent", "Date", "LateFeeOccured", "ReminderCount", "Status", "TenantID" },
                values: new object[,]
                {
                    { 1, 1200.00m, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 0, 0, 1 },
                    { 2, 1200.00m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 0.00m, 0, 1, 2 },
                    { 3, 1500.00m, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 50.00m, 0, 2, 3 }
                });

            migrationBuilder.InsertData(
                table: "WorkLogs",
                columns: new[] { "LogID", "ClockInTime", "ClockOutTime", "GPSLocation", "MaterialsUsed", "ProjectID", "ProofPhotoURL", "VendorSignature" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 16, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), "37.94, -87.40", "Shingles, Tar", 1, "", "Signed-Digitally-01" },
                    { 2, new DateTime(2026, 3, 17, 9, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), "37.94, -87.41", "Air Filter", 2, "", "Signed-Digitally-02" },
                    { 3, new DateTime(2026, 3, 18, 13, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 18, 15, 0, 0, 0, DateTimeKind.Unspecified), "37.94, -87.40", "New Faucet, Teflon Tape", 3, "", "Signed-Digitally-03" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProjectID",
                table: "Invoices",
                column: "ProjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_RentScheduleScheduleID",
                table: "Invoices",
                column: "RentScheduleScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceProjects_PropertyID",
                table: "MaintenanceProjects",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_RentPayments_RentScheduleScheduleID",
                table: "RentPayments",
                column: "RentScheduleScheduleID");

            migrationBuilder.CreateIndex(
                name: "IX_RentSchedules_TenantID",
                table: "RentSchedules",
                column: "TenantID");

            migrationBuilder.CreateIndex(
                name: "IX_Tenants_PropertyID",
                table: "Tenants",
                column: "PropertyID");

            migrationBuilder.CreateIndex(
                name: "IX_WorkLogs_ProjectID",
                table: "WorkLogs",
                column: "ProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "RentPayments");

            migrationBuilder.DropTable(
                name: "WorkLogs");

            migrationBuilder.DropTable(
                name: "RentSchedules");

            migrationBuilder.DropTable(
                name: "MaintenanceProjects");

            migrationBuilder.DropTable(
                name: "Tenants");

            migrationBuilder.DropTable(
                name: "Properties");
        }
    }
}
