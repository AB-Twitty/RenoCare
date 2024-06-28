using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class FK_Med_Req_Report_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Requests_Session_Reports_ReportId",
                table: "Medication_Requests",
                column: "ReportId",
                principalTable: "Session_Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Reports_Medication_Requests_MedicationRequestId",
                table: "Session_Reports",
                column: "MedicationRequestId",
                principalTable: "Medication_Requests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                 name: "FK_Medication_Requests_Session_Reports_ReportId",
                 table: "Medication_Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Reports_Medication_Requests_MedicationRequestId",
                table: "Session_Reports");
        }
    }
}
