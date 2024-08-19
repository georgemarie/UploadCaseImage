using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UploadingCaseImages.DB.Migrations
{
    /// <inheritdoc />
    public partial class AllModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Anatomy",
                columns: table => new
                {
                    AnatomyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AnatomyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Anatomy", x => x.AnatomyId);
                });

            migrationBuilder.CreateTable(
                name: "PatientCase",
                columns: table => new
                {
                    PatientCaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VisitDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AnatomyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientCase", x => x.PatientCaseId);
                });

            migrationBuilder.CreateTable(
                name: "CaseImage",
                columns: table => new
                {
                    CaseImageId = table.Column<int>(type: "int", nullable: false),
                    CaseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CasePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PatientCaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CaseImage", x => x.CaseImageId);
                    table.ForeignKey(
                        name: "FK_CaseImage_PatientCase_CaseImageId",
                        column: x => x.CaseImageId,
                        principalTable: "PatientCase",
                        principalColumn: "PatientCaseId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Anatomy");

            migrationBuilder.DropTable(
                name: "CaseImage");

            migrationBuilder.DropTable(
                name: "PatientCase");
        }
    }
}
