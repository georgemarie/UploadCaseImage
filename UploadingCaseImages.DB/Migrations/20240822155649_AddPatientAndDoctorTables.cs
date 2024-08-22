using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadingCaseImages.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddPatientAndDoctorTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "PatientCase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                table: "PatientCase",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Doctor",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctor", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientCase_DoctorId",
                table: "PatientCase",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCase_PatientId",
                table: "PatientCase",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCase_Doctor_DoctorId",
                table: "PatientCase",
                column: "DoctorId",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCase_Patient_PatientId",
                table: "PatientCase",
                column: "PatientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientCase_Doctor_DoctorId",
                table: "PatientCase");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientCase_Patient_PatientId",
                table: "PatientCase");

            migrationBuilder.DropTable(
                name: "Doctor");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropIndex(
                name: "IX_PatientCase_DoctorId",
                table: "PatientCase");

            migrationBuilder.DropIndex(
                name: "IX_PatientCase_PatientId",
                table: "PatientCase");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "PatientCase");

            migrationBuilder.DropColumn(
                name: "PatientId",
                table: "PatientCase");
        }
    }
}
