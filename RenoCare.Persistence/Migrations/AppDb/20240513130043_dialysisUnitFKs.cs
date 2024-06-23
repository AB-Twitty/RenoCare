using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class dialysisUnitFKs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Requests_Dialysis_Units_DialysisUnitId",
                table: "Medication_Requests",
                column: "DialysisUnitId",
                principalTable: "Dialysis_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Session_Reports_Dialysis_Units_DialysisUnitId",
                table: "Session_Reports",
                column: "DialysisUnitId",
                principalTable: "Dialysis_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Requests_Dialysis_Units_DialysisUnitId",
                table: "Medication_Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Session_Reports_Dialysis_Units_DialysisUnitId",
                table: "Session_Reports");

        }
    }
}
