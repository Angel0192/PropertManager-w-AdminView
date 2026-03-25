using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyManager.Migrations
{
    /// <inheritdoc />
    public partial class FinalSeedFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "ScheduleID",
                value: null);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 2,
                columns: new[] { "InvoiceDate", "ProjectID" },
                values: new object[] { new DateTime(2026, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), null });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 3,
                column: "InvoiceDate",
                value: new DateTime(2026, 3, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "ScheduleID",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 2,
                columns: new[] { "InvoiceDate", "ProjectID" },
                values: new object[] { new DateTime(2026, 3, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 });

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 3,
                column: "InvoiceDate",
                value: new DateTime(2026, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
