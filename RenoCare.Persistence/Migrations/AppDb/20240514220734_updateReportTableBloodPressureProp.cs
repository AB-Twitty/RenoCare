using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class updateReportTableBloodPressureProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DuringDiastolicBloodPressure",
                table: "Session_Reports");

            migrationBuilder.DropColumn(
                name: "DuringSystolicBloodPressure",
                table: "Session_Reports");

            migrationBuilder.DropColumn(
                name: "PostDiastolicBloodPressure",
                table: "Session_Reports");

            migrationBuilder.RenameColumn(
                name: "PreSystolicBloodPressure",
                table: "Session_Reports",
                newName: "PreBloodPressure");

            migrationBuilder.RenameColumn(
                name: "PreDiastolicBloodPressure",
                table: "Session_Reports",
                newName: "PostBloodPressure");

            migrationBuilder.RenameColumn(
                name: "PostSystolicBloodPressure",
                table: "Session_Reports",
                newName: "DuringBloodPressure");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PreBloodPressure",
                table: "Session_Reports",
                newName: "PreSystolicBloodPressure");

            migrationBuilder.RenameColumn(
                name: "PostBloodPressure",
                table: "Session_Reports",
                newName: "PreDiastolicBloodPressure");

            migrationBuilder.RenameColumn(
                name: "DuringBloodPressure",
                table: "Session_Reports",
                newName: "PostSystolicBloodPressure");

            migrationBuilder.AddColumn<string>(
                name: "DuringDiastolicBloodPressure",
                table: "Session_Reports",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DuringSystolicBloodPressure",
                table: "Session_Reports",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PostDiastolicBloodPressure",
                table: "Session_Reports",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");
        }
    }
}
