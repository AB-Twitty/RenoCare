using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class MedicationStatusColorUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 1,
                column: "LabelClass",
                value: "#f0ad4e");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 2,
                column: "LabelClass",
                value: "#20809D");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 3,
                column: "LabelClass",
                value: "#5cb85c");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 4,
                column: "LabelClass",
                value: "#A72925");

            migrationBuilder.UpdateData(
                table: "Medication_Request_Status",
                keyColumn: "Id",
                keyValue: 5,
                column: "LabelClass",
                value: "#d9534f");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
        }
    }
}
