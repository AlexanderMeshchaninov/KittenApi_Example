﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using WebApiDataLayer;

namespace WebApiMigrations.Migrations
{
    [DbContext(typeof(WebApiDataContext))]
    [Migration("20210802003136_NewMigrationV2")]
    partial class NewMigrationV2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("ClinicKitten", b =>
                {
                    b.Property<int>("ClinicsId")
                        .HasColumnType("integer");

                    b.Property<int>("KittensId")
                        .HasColumnType("integer");

                    b.HasKey("ClinicsId", "KittensId");

                    b.HasIndex("KittensId");

                    b.ToTable("ClinicKitten");
                });

            modelBuilder.Entity("WebApiDataLayer.Models.Clinic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("ClinicName")
                        .HasColumnType("text");

                    b.Property<int>("KittenId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Clinics");
                });

            modelBuilder.Entity("WebApiDataLayer.Models.Kitten", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Color")
                        .HasColumnType("text");

                    b.Property<string>("FeedName")
                        .HasColumnType("text");

                    b.Property<bool>("HasCertificate")
                        .HasColumnType("boolean");

                    b.Property<bool>("HasMedicalInspection")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("LastInspection")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NickName")
                        .HasColumnType("text");

                    b.Property<int>("Weigth")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Kittens");
                });

            modelBuilder.Entity("ClinicKitten", b =>
                {
                    b.HasOne("WebApiDataLayer.Models.Clinic", null)
                        .WithMany()
                        .HasForeignKey("ClinicsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApiDataLayer.Models.Kitten", null)
                        .WithMany()
                        .HasForeignKey("KittensId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
