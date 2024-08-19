using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadingCaseImages.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddAnatomyToPatientCase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_PatientCase_AnatomyId",
                table: "PatientCase",
                column: "AnatomyId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientCase_Anatomy_AnatomyId",
                table: "PatientCase",
                column: "AnatomyId",
                principalTable: "Anatomy",
                principalColumn: "AnatomyId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientCase_Anatomy_AnatomyId",
                table: "PatientCase");

            migrationBuilder.DropIndex(
                name: "IX_PatientCase_AnatomyId",
                table: "PatientCase");
        }
    }
}
