using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class medicationRequestReportFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReportId",
                table: "Medication_Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Requests_Session_Reports_ReportId",
                table: "Medication_Requests",
                column: "ReportId",
                principalTable: "Session_Reports",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Requests_Session_Reports_ReportId",
                table: "Medication_Requests");

            migrationBuilder.DropColumn(
                name: "ReportId",
                table: "Medication_Requests");
        }
    }
}
