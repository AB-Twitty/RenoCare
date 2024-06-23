using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class initMedicationRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Medication_Request_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication_Request_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medication_Request_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication_Request_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medication_Requests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DialysisUnitId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AppointmentHour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientProblem = table.Column<string>(type: "text", nullable: true),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    TypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medication_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medication_Requests_Medication_Request_Status_StatusId",
                        column: x => x.StatusId,
                        principalTable: "Medication_Request_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medication_Requests_Medication_Request_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Medication_Request_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Medication_Requests_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Medication_Request_Status",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Indicates that the medication request is pending / awaiting to be reviewed by the healthcare provider.", "Pending" },
                    { 2, "Indicates that the medication request is upcoming / reviewed by the healthcare provider and approved it.", "Upcoming" },
                    { 3, "Indicates that the medication request is completed.", "Completed" },
                    { 4, "Indicates that the medication request is rejected / reviewed by the healthcare provider and declined it.", "Rejected" },
                    { 5, "Indicates that the medication request is either cancelled by the patient or its time has passed without reviewing it.", "Cancelled" }
                });

            migrationBuilder.InsertData(
                table: "Medication_Request_Types",
                columns: new[] { "Id", "Description", "IsActive", "Name" },
                values: new object[,]
                {
                    { 1, "Book for only one time.", true, "Just Once" },
                    { 2, "Automatically book the same medication request every week.", true, "Weekly" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Medication_Requests");

            migrationBuilder.DropTable(
                name: "Medication_Request_Status");

            migrationBuilder.DropTable(
                name: "Medication_Request_Types");
        }
    }
}
