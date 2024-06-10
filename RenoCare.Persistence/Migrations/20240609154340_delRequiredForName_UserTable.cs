using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations
{
    public partial class delRequiredForName_UserTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eb1897c-7ba1-4595-b37c-f48bcd61e033",
                column: "ConcurrencyStamp",
                value: "47d3c41c-b3f0-4e2d-82b5-5e7b8a560c99");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eb1897c-7ba1-4595-b37c-f48bcd61e034",
                column: "ConcurrencyStamp",
                value: "d9afe1a3-c1a2-4419-a1a4-6380e583d1bc");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eb1897c-7ba1-4595-b37c-f48bcd61e035",
                column: "ConcurrencyStamp",
                value: "9a65a2ca-3d66-43fb-8968-3d39d53a824a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6d6f491-1957-4e70-98c7-997eb0d3255f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "78769d48-3a91-4966-b874-10b10eb2b166", "AQAAAAEAACcQAAAAEFfiGHgOTR1hLc8sh4IuG5gDSm25F2N0Bf7aQsrSd2AudAaZSt6Pdus+cPXk6j4DQw==", "15ce9511-3332-44c2-9d2f-438858af8296" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6d6f491-1957-4e70-98c7-997eb0d3256f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "170fc1e8-91cc-45b2-910c-043833720932", "AQAAAAEAACcQAAAAEPm6cCvdJDiHoHINkvUtQ1UajzdiwDhYLVURycEyFd1KIzkARblfq23EK5Awnd62Sw==", "46fcf3bd-5c81-40e9-8968-69aacf67e8e7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(25)",
                maxLength: 25,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(25)",
                oldMaxLength: 25,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eb1897c-7ba1-4595-b37c-f48bcd61e033",
                column: "ConcurrencyStamp",
                value: "8a7ce36e-2729-429e-90e0-1902c57ba999");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eb1897c-7ba1-4595-b37c-f48bcd61e034",
                column: "ConcurrencyStamp",
                value: "e1219ab8-067e-43d3-8103-c3aca311a3f7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5eb1897c-7ba1-4595-b37c-f48bcd61e035",
                column: "ConcurrencyStamp",
                value: "3c343d9f-dea9-43b0-a930-a2e5500d5e1c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6d6f491-1957-4e70-98c7-997eb0d3255f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cfcdb9a2-8e3c-4f30-a9cb-e592cf15a8bb", "AQAAAAEAACcQAAAAELc3JRHBeVrUzZ8Q30Lxf0zvnnS+4sa2fV5Aej552P8syd+IjogCFP+PbjmoguV7Hw==", "78782f45-c77a-43f0-8bc2-fac5c89892cf" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a6d6f491-1957-4e70-98c7-997eb0d3256f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3804f2ea-fc13-4a05-b2d8-de7acab327ef", "AQAAAAEAACcQAAAAEC3wvkgOqj3fd0MKSxURtsi31c/NoWh+sEmAOSl9CwGdj44v7pF7VDCh0qcrWmLR+A==", "5af446eb-de3f-4ce7-99dc-403489e7b34b" });
        }
    }
}
