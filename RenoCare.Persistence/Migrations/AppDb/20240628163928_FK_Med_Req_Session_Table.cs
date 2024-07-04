using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class FK_Med_Req_Session_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentHour",
                table: "Medication_Requests");

            migrationBuilder.AddColumn<int>(
                name: "SessionId",
                table: "Medication_Requests",
                type: "int",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Medication_Requests_SessionId",
                table: "Medication_Requests",
                column: "SessionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medication_Requests_Sessions_Timetables_SessionId",
                table: "Medication_Requests",
                column: "SessionId",
                principalTable: "Sessions_Timetables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medication_Requests_Sessions_Timetables_SessionId",
                table: "Medication_Requests");

            migrationBuilder.DropIndex(
                name: "IX_Medication_Requests_SessionId",
                table: "Medication_Requests");

            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "Medication_Requests");

            migrationBuilder.AddColumn<string>(
                name: "AppointmentHour",
                table: "Medication_Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
