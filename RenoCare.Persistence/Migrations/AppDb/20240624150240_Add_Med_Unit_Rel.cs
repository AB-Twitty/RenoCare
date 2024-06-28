using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Add_Med_Unit_Rel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Medication_Requests_DialysisUnitId",
                table: "Medication_Requests",
                column: "DialysisUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Medication_Requests_DialysisUnitId",
                table: "Medication_Requests");
        }
    }
}
