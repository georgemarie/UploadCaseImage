using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadingCaseImages.DB.Migrations
{
    /// <inheritdoc />
    public partial class FirstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientCase_Anatomy_AnatomyId",
                table: "PatientCase");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCase_Anatomy_AnatomyId",
                table: "PatientCase",
                column: "AnatomyId",
                principalTable: "Anatomy",
                principalColumn: "AnatomyId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientCase_Anatomy_AnatomyId",
                table: "PatientCase");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCase_Anatomy_AnatomyId",
                table: "PatientCase",
                column: "AnatomyId",
                principalTable: "Anatomy",
                principalColumn: "AnatomyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
