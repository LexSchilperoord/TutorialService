﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TutorialService.Data;

#nullable disable

namespace TutorialService.Migrations
{
    [DbContext(typeof(TutorialServiceContext))]
    [Migration("20220603090321_Start")]
    partial class Start
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TutorialService.Models.Enrollment", b =>
                {
                    b.Property<int>("EnrollmentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnrollmentID"), 1L, 1);

                    b.Property<int>("ExampleID")
                        .HasColumnType("int");

                    b.Property<int?>("Grade")
                        .HasColumnType("int");

                    b.Property<int>("GroupID")
                        .HasColumnType("int");

                    b.HasKey("EnrollmentID");

                    b.HasIndex("ExampleID");

                    b.HasIndex("GroupID");

                    b.ToTable("Enrollment");
                });

            modelBuilder.Entity("TutorialService.Models.Example", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("Creation")
                        .HasColumnType("datetime2");

                    b.Property<string>("Creator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("nvarchar(60)");

                    b.Property<string>("Rating")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("Example");
                });

            modelBuilder.Entity("TutorialService.Models.Group", b =>
                {
                    b.Property<int>("GroupID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GroupID"), 1L, 1);

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("GroupID");

                    b.ToTable("Group");
                });

            modelBuilder.Entity("TutorialService.Models.Enrollment", b =>
                {
                    b.HasOne("TutorialService.Models.Example", "Example")
                        .WithMany("Enrollments")
                        .HasForeignKey("ExampleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TutorialService.Models.Group", "Group")
                        .WithMany("Enrollments")
                        .HasForeignKey("GroupID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Example");

                    b.Navigation("Group");
                });

            modelBuilder.Entity("TutorialService.Models.Example", b =>
                {
                    b.Navigation("Enrollments");
                });

            modelBuilder.Entity("TutorialService.Models.Group", b =>
                {
                    b.Navigation("Enrollments");
                });
#pragma warning restore 612, 618
        }
    }
}