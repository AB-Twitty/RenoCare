using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Messges_add_isFile_prop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFile",
                table: "Messages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFile",
                table: "Messages");
        }
    }
}
