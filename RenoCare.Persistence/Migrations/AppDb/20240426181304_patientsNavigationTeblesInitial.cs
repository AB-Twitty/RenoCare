using Microsoft.EntityFrameworkCore.Migrations;

namespace RenoCare.Persistence.Migrations.AppDb
{
    public partial class patientsNavigationTeblesInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Diabetes_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diabetes_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Hypertension_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hypertension_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Smoking_Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Smoking_Status", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    KidneyFailureCause = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiabetesTypeId = table.Column<int>(type: "int", nullable: false),
                    HypertensionTypeId = table.Column<int>(type: "int", nullable: false),
                    SmokingStatusId = table.Column<int>(type: "int", nullable: true),
                    DeletionReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Diabetes_Types_DiabetesTypeId",
                        column: x => x.DiabetesTypeId,
                        principalTable: "Diabetes_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Hypertension_Types_HypertensionTypeId",
                        column: x => x.HypertensionTypeId,
                        principalTable: "Hypertension_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patients_Smoking_Status_SmokingStatusId",
                        column: x => x.SmokingStatusId,
                        principalTable: "Smoking_Status",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Diabetes_Types",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "No diabetes and fall within the normal range of blood sugar levels.", "Non-diabetic" },
                    { 2, "Type 1 diabetes is where the blood glucose (sugar) level is too high because the body can’t make a hormone called insulin. The body still breaks down the carbohydrate from food and drink and turns it into glucose. But when the glucose enters the bloodstream, there’s no insulin to allow it into the body’s cells. More and more glucose then builds up in the bloodstream, leading to high blood sugar levels.", "Type 1 diabetes" },
                    { 3, "Type 2 diabetes is where the insulin the pancreas makes can’t work properly, or the pancreas can’t make enough insulin. This means the blood glucose (sugar) levels keep rising. Having type 2 diabetes without treatment means that high sugar levels in the blood can seriously damage parts of the body, including the eyes, heart and feet. These are called the complications of diabetes. But with the right treatment and care, the patient can live well with type 2 diabetes and reduce the risk of developing them.", "Type 2 diabetes" },
                    { 4, "Gestational diabetes is diabetes that can develop during pregnancy. It affects women who haven't been affected by diabetes before. It means she has high blood sugar and needs to take extra care of herself and her bump. This will include eating well and keeping active. It usually goes away again after giving birth. It is usually diagnosed from a blood test 24 to 28 weeks into pregnancy.", "Gestational diabetes" },
                    { 5, "Monogenic diabetes is a rare condition, different from both type 1 and type 2 diabetes. It’s caused by a mutation in a single gene. If a parent has this mutation, their children have a 50p per cent chance of inheriting it. In some cases of monogenic diabetes, the condition can be managed with specific tablets and doesn’t require insulin treatment.", "Monogenic diabetes" }
                });

            migrationBuilder.InsertData(
                table: "Hypertension_Types",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Systolic blood pressure less than 120 mm Hg and diastolic blood pressure less than 80 mm Hg.", "Normal" },
                    { 2, "Systolic blood pressure between 120-129 mm Hg and diastolic blood pressure less than 80 mm Hg.", "Elevated" },
                    { 3, "Systolic blood pressure consistently ranging from 130-139 mm Hg or diastolic blood pressure consistently ranging from 80-89 mm Hg.", "Hypertension Stage 1" },
                    { 4, "Systolic blood pressure of 140 mm Hg or higher or diastolic blood pressure of 90 mm Hg or higher.", "Hypertension Stage 2" },
                    { 5, " Blood pressure readings exceeding 180/120 mm Hg, requiring immediate medical attention.", "Hypertensive Crisis:" }
                });

            migrationBuilder.InsertData(
                table: "Smoking_Status",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Individuals who have never smoked.", "Non Smoker" },
                    { 2, "Individuals who used to smoke but have successfully quit.", "Former Smoker" },
                    { 3, "Individuals who currently smoke.", "Current Smoker" }
                });

            migrationBuilder.InsertData(
                table: "Patients",
                columns: new[] { "Id", "DeletionReason", "DiabetesTypeId", "HypertensionTypeId", "KidneyFailureCause", "SmokingStatusId", "UserId" },
                values: new object[] { 1, null, 1, 1, "Hypertension", null, "a6d6f491-1957-4e70-98c7-997eb0d3255f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Diabetes_Types");

            migrationBuilder.DropTable(
                name: "Hypertension_Types");

            migrationBuilder.DropTable(
                name: "Smoking_Status");
        }
    }
}
