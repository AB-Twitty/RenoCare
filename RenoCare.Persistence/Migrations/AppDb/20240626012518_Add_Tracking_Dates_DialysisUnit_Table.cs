using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class Add_Tracking_Dates_DialysisUnit_Table : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Dialysis_Units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastModifiedDate",
                table: "Dialysis_Units",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Dialysis_Units");

            migrationBuilder.DropColumn(
                name: "LastModifiedDate",
                table: "Dialysis_Units");
        }
    }
}
