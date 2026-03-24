using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PropertyManager.Migrations
{
    /// <inheritdoc />
    public partial class EnableProjectCascadeDeletes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MaintenanceProjects_ProjectID",
                table: "Invoices");

            migrationBuilder.AddColumn<int>(
                name: "ProjectID1",
                table: "WorkLogs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectID1",
                table: "Invoices",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 1,
                column: "ProjectID1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 2,
                column: "ProjectID1",
                value: null);

            migrationBuilder.UpdateData(
                table: "Invoices",
                keyColumn: "InvoiceID",
                keyValue: 3,
                column: "ProjectID1",
                value: null);

            migrationBuilder.UpdateData(
                table: "WorkLogs",
                keyColumn: "LogID",
                keyValue: 1,
                column: "ProjectID1",
                value: null);

            migrationBuilder.UpdateData(
                table: "WorkLogs",
                keyColumn: "LogID",
                keyValue: 2,
                column: "ProjectID1",
                value: null);

            migrationBuilder.UpdateData(
                table: "WorkLogs",
                keyColumn: "LogID",
                keyValue: 3,
                column: "ProjectID1",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_WorkLogs_ProjectID1",
                table: "WorkLogs",
                column: "ProjectID1");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_ProjectID1",
                table: "Invoices",
                column: "ProjectID1");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MaintenanceProjects_ProjectID",
                table: "Invoices",
                column: "ProjectID",
                principalTable: "MaintenanceProjects",
                principalColumn: "ProjectID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MaintenanceProjects_ProjectID1",
                table: "Invoices",
                column: "ProjectID1",
                principalTable: "MaintenanceProjects",
                principalColumn: "ProjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkLogs_MaintenanceProjects_ProjectID1",
                table: "WorkLogs",
                column: "ProjectID1",
                principalTable: "MaintenanceProjects",
                principalColumn: "ProjectID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MaintenanceProjects_ProjectID",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_MaintenanceProjects_ProjectID1",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkLogs_MaintenanceProjects_ProjectID1",
                table: "WorkLogs");

            migrationBuilder.DropIndex(
                name: "IX_WorkLogs_ProjectID1",
                table: "WorkLogs");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_ProjectID1",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "ProjectID1",
                table: "WorkLogs");

            migrationBuilder.DropColumn(
                name: "ProjectID1",
                table: "Invoices");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_MaintenanceProjects_ProjectID",
                table: "Invoices",
                column: "ProjectID",
                principalTable: "MaintenanceProjects",
                principalColumn: "ProjectID");
        }
    }
}
