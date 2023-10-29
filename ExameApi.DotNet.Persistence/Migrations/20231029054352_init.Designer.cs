﻿// <auto-generated />
using System;
using ExameApi.DotNet.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ExameApi.DotNet.Persistence.Migrations
{
    [DbContext(typeof(ExamContext))]
    [Migration("20231029054352_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.22")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ExamApi.DotNet.Domain.Entity.Exam", b =>
                {
                    b.Property<Guid>("IdExam")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("IdPatient")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("UrlLocations")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdExam");

                    b.HasIndex("IdPatient");

                    b.ToTable("ExamData");
                });

            modelBuilder.Entity("ExamApi.DotNet.Domain.Entity.Patient", b =>
                {
                    b.Property<Guid>("IdPatient")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Age")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("IdPatient");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("PatientData");
                });

            modelBuilder.Entity("ExamApi.DotNet.Domain.Entity.Exam", b =>
                {
                    b.HasOne("ExamApi.DotNet.Domain.Entity.Patient", "Patient")
                        .WithMany("ExamList")
                        .HasForeignKey("IdPatient")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("ExamApi.DotNet.Domain.Entity.Patient", b =>
                {
                    b.Navigation("ExamList");
                });
#pragma warning restore 612, 618
        }
    }
}
