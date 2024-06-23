using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class reportTableInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Session_Reports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DialysisUnitId = table.Column<int>(type: "int", nullable: false),
                    MedicationRequestId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Nephrologist = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    DialysisDuration = table.Column<double>(type: "float", nullable: false),
                    DialysisFrequency = table.Column<int>(type: "int", nullable: false),
                    VascularAccessType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    DialyzerType = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    PreWeight = table.Column<double>(type: "float", nullable: false),
                    PostWeight = table.Column<double>(type: "float", nullable: false),
                    PreSystolicBloodPressure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DuringSystolicBloodPressure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PostSystolicBloodPressure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PreDiastolicBloodPressure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DuringDiastolicBloodPressure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    PostDiastolicBloodPressure = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    DryWeight = table.Column<double>(type: "float", nullable: false),
                    HeartRate = table.Column<int>(type: "int", nullable: false),
                    PreUrea = table.Column<double>(type: "float", nullable: false),
                    PostUrea = table.Column<double>(type: "float", nullable: false),
                    UreaReductionRatio = table.Column<double>(type: "float", nullable: false),
                    TotalFluidRemoval = table.Column<double>(type: "float", nullable: false),
                    FluidRemovalRate = table.Column<double>(type: "float", nullable: false),
                    UrineOutput = table.Column<double>(type: "float", nullable: false),
                    Kt_V = table.Column<double>(type: "float", nullable: false),
                    Creatinine = table.Column<double>(type: "float", nullable: false),
                    Potassium = table.Column<double>(type: "float", nullable: false),
                    Hemoglobin = table.Column<double>(type: "float", nullable: false),
                    Hematocrit = table.Column<double>(type: "float", nullable: false),
                    Albumin = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Session_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Session_Reports_Medication_Requests_MedicationRequestId",
                        column: x => x.MedicationRequestId,
                        principalTable: "Medication_Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Session_Reports_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Session_Reports");
        }
    }
}
