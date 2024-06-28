using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Map_amenities_units : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenities_Units_Mapping",
                columns: table => new
                {
                    AmenitiesId = table.Column<int>(type: "int", nullable: false),
                    DialysisUnitsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenities_Units_Mapping", x => new { x.AmenitiesId, x.DialysisUnitsId });
                    table.ForeignKey(
                        name: "FK_Amenities_Units_Mapping_Amenities_AmenitiesId",
                        column: x => x.AmenitiesId,
                        principalTable: "Amenities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Amenities_Units_Mapping_Dialysis_Units_DialysisUnitsId",
                        column: x => x.DialysisUnitsId,
                        principalTable: "Dialysis_Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Amenities_Units_Mapping_DialysisUnitsId",
                table: "Amenities_Units_Mapping",
                column: "DialysisUnitsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Amenities_Units_Mapping");
        }
    }
}
