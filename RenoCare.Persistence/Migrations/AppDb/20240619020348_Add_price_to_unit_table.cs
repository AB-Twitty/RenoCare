using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Add_price_to_unit_table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsHDSupported",
                table: "Dialysis_Units",
                newName: "IsHdSupported");

            migrationBuilder.RenameColumn(
                name: "IsHDFSupported",
                table: "Dialysis_Units",
                newName: "IsHdfSupported");

            migrationBuilder.AddColumn<double>(
                name: "HdPrice",
                table: "Dialysis_Units",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "HdfPrice",
                table: "Dialysis_Units",
                type: "float",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Dialysis_Units",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "HdPrice", "HdfPrice" },
                values: new object[] { 310.0, 340.0 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HdPrice",
                table: "Dialysis_Units");

            migrationBuilder.DropColumn(
                name: "HdfPrice",
                table: "Dialysis_Units");

            migrationBuilder.RenameColumn(
                name: "IsHdfSupported",
                table: "Dialysis_Units",
                newName: "IsHDFSupported");

            migrationBuilder.RenameColumn(
                name: "IsHdSupported",
                table: "Dialysis_Units",
                newName: "IsHDSupported");
        }
    }
}
