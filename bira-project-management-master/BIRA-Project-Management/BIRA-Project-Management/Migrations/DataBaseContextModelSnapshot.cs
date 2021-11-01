﻿// <auto-generated />
using BIRA_Project_Management.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BIRA_Project_Management.Migrations
{
    [DbContext(typeof(DataBaseContext))]
    partial class DataBaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.11");

            modelBuilder.Entity("BIRA_Project_Management.Models.Issue", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Assignee")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<string>("Reporter")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<string>("Type")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("Issue");
                });

            modelBuilder.Entity("BIRA_Project_Management.Models.Project", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Creator")
                        .HasColumnType("text");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("BIRA_Project_Management.Models.Issue", b =>
                {
                    b.HasOne("BIRA_Project_Management.Models.Project", "Project")
                        .WithMany("issues")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Project");
                });

            modelBuilder.Entity("BIRA_Project_Management.Models.Project", b =>
                {
                    b.Navigation("issues");
                });
#pragma warning restore 612, 618
        }
    }
}
