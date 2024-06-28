using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Init_Virus_Tables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Viruses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Abbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Viruses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DialysisUnits_Accepting_Viruses_Mapping",
                columns: table => new
                {
                    AcceptingVirusesId = table.Column<int>(type: "int", nullable: false),
                    DialysisUnitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DialysisUnits_Accepting_Viruses_Mapping", x => new { x.AcceptingVirusesId, x.DialysisUnitsId });
                    table.ForeignKey(
                        name: "FK_DialysisUnits_Accepting_Viruses_Mapping_Dialysis_Units_DialysisUnitsId",
                        column: x => x.DialysisUnitsId,
                        principalTable: "Dialysis_Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DialysisUnits_Accepting_Viruses_Mapping_Viruses_AcceptingVirusesId",
                        column: x => x.AcceptingVirusesId,
                        principalTable: "Viruses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients_With_Viruses_Mapping",
                columns: table => new
                {
                    PatientsId = table.Column<int>(type: "int", nullable: false),
                    VirusesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients_With_Viruses_Mapping", x => new { x.PatientsId, x.VirusesId });
                    table.ForeignKey(
                        name: "FK_Patients_With_Viruses_Mapping_Patients_PatientsId",
                        column: x => x.PatientsId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_With_Viruses_Mapping_Viruses_VirusesId",
                        column: x => x.VirusesId,
                        principalTable: "Viruses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Viruses",
                columns: new[] { "Id", "Abbreviation", "Description", "Name" },
                values: new object[] { 1, "HIV", "HIV attacks the body’s immune system, specifically the CD4 cells (T cells), which help the immune system fight off infections. If left untreated, HIV reduces the number of these cells, making the body more vulnerable to infections and certain cancers.", "Human Immunodeficiency Virus" });

            migrationBuilder.InsertData(
                table: "Viruses",
                columns: new[] { "Id", "Abbreviation", "Description", "Name" },
                values: new object[] { 2, "HBV", "HBV is a virus that infects the liver, causing inflammation and potentially leading to serious conditions such as liver cirrhosis or liver cancer. It is transmitted through contact with infectious body fluids, such as blood, semen, and vaginal fluids.", "Hepatitis B Virus" });

            migrationBuilder.InsertData(
                table: "Viruses",
                columns: new[] { "Id", "Abbreviation", "Description", "Name" },
                values: new object[] { 3, "HCV", "HCV is a liver infection caused by the hepatitis C virus. It can lead to chronic liver disease, including cirrhosis and liver cancer. HCV is primarily spread through contact with blood from an infected person.", "Hepatitis C Virus" });

            migrationBuilder.CreateIndex(
                name: "IX_DialysisUnits_Accepting_Viruses_Mapping_DialysisUnitsId",
                table: "DialysisUnits_Accepting_Viruses_Mapping",
                column: "DialysisUnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_With_Viruses_Mapping_VirusesId",
                table: "Patients_With_Viruses_Mapping",
                column: "VirusesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DialysisUnits_Accepting_Viruses_Mapping");

            migrationBuilder.DropTable(
                name: "Patients_With_Viruses_Mapping");

            migrationBuilder.DropTable(
                name: "Viruses");
        }
    }
}
