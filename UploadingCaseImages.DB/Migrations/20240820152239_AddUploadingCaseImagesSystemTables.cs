using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadingCaseImages.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddUploadingCaseImagesSystemTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anatomy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anatomy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientCase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnatomyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientCase_Anatomy_AnatomyId",
                        column: x => x.AnatomyId,
                        principalTable: "Anatomy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CaseImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientCaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CaseImage_PatientCase_PatientCaseId",
                        column: x => x.PatientCaseId,
                        principalTable: "PatientCase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CaseImage_PatientCaseId",
                table: "CaseImage",
                column: "PatientCaseId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientCase_AnatomyId",
                table: "PatientCase",
                column: "AnatomyId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CaseImage");

            migrationBuilder.DropTable(
                name: "PatientCase");

            migrationBuilder.DropTable(
                name: "Anatomy");
        }
    }
}
