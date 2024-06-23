using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class dialysiUnitTableInit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Dialysis_Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsHDSupported = table.Column<bool>(type: "bit", nullable: false),
                    IsHDFSupported = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dialysis_Units", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dialysis_Units_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Dialysis_Units",
                columns: new[] { "Id", "Address", "City", "Country", "Description", "IsHDFSupported", "IsHDSupported", "Name", "PhoneNumber", "UserId" },
                values: new object[] { 1, "the street where the unit is located", "Paris", "France", "this is the description for the dialysis unit", true, true, "Dialysis unit name", "123456789", "30aaf317-be57-4870-9768-2af3599936v2" });

            migrationBuilder.CreateIndex(
                name: "IX_Dialysis_Units_UserId",
                table: "Dialysis_Units",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Dialysis_Units");
        }
    }
}
