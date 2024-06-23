using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class MedicationStatusLabels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LabelClass",
                table: "Medication_Request_Status",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "LabelClass",
                value: "label-warning");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "LabelClass",
                value: "label-info");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "LabelClass",
                value: "label-success");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "LabelClass",
                value: "label-warning{background-color:#7B4C09}");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "LabelClass",
                value: "label-danger");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Types",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: true);

            migrationBuilder.UpdateData(
                table: "Medication_Request_Types",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelClass",
                table: "Medication_Request_Status");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Types",
                keyColumn: "Id",
                keyValue: 1,
                column: "IsActive",
                value: false);

            migrationBuilder.UpdateData(
                table: "Medication_Request_Types",
                keyColumn: "Id",
                keyValue: 2,
                column: "IsActive",
                value: false);
        }
    }
}
