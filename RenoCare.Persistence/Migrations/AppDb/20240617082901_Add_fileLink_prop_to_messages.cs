using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Add_fileLink_prop_to_messages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileLink",
                table: "Messages",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileLink",
                table: "Messages");
        }
    }
}
