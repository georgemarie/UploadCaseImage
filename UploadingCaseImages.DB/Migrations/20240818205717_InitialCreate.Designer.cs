﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UploadingCaseImages.DB;

#nullable disable

namespace UploadingCaseImages.DB.Migrations
{
    [DbContext(typeof(UploadingCaseImagesContext))]
    [Migration("20240818205717_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("UploadingCaseImages.DB.Model.AnatomyModel", b =>
                {
                    b.Property<int>("AnatomyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AnatomyId"));

                    b.Property<int>("BodyPart")
                        .HasColumnType("int");

                    b.Property<int>("CaseId")
                        .HasColumnType("int");

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("AnatomyId");

                    b.HasIndex("CaseId");

                    b.ToTable("AnatomyModel");
                });

            modelBuilder.Entity("UploadingCaseImages.DB.Model.Case", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AnatomyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("CaseDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorNotes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PatientId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Case");
                });

            modelBuilder.Entity("UploadingCaseImages.DB.Model.AnatomyModel", b =>
                {
                    b.HasOne("UploadingCaseImages.DB.Model.Case", "Case")
                        .WithMany("ImageUrls")
                        .HasForeignKey("CaseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Case");
                });

            modelBuilder.Entity("UploadingCaseImages.DB.Model.Case", b =>
                {
                    b.Navigation("ImageUrls");
                });
#pragma warning restore 612, 618
        }
    }
}
