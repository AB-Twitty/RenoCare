using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Map_unit_sessions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Timetables_Dialysis_Units_UnitId",
                table: "Sessions_Timetables");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Sessions_Timetables",
                newName: "DialysisUnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Timetables_Dialysis_Units_DialysisUnitId",
                table: "Sessions_Timetables",
                column: "DialysisUnitId",
                principalTable: "Dialysis_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_Timetables_Dialysis_Units_DialysisUnitId",
                table: "Sessions_Timetables");

            migrationBuilder.RenameColumn(
                name: "DialysisUnitId",
                table: "Sessions_Timetables",
                newName: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_Timetables_Dialysis_Units_UnitId",
                table: "Sessions_Timetables",
                column: "UnitId",
                principalTable: "Dialysis_Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
