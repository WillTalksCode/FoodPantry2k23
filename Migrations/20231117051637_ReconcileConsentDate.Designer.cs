﻿// <auto-generated />
using System;
using FoodPantry2k23;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodPantry2k23.Migrations
{
    [DbContext(typeof(FPContext))]
    [Migration("20231117051637_ReconcileConsentDate")]
    partial class ReconcileConsentDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FoodPantry2k23.Models.Household", b =>
                {
                    b.Property<int>("HouseHoldID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("HouseHoldID"));

                    b.Property<string>("Address1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AdminNotes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("ConsentFormOnFile")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("ConsentFormSigned")
                        .HasColumnType("datetime2");

                    b.Property<string>("StateProvince")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("VerbalConsentGiven")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("VerbalConsentGivenOn")
                        .HasColumnType("datetime2");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("HouseHoldID");

                    b.ToTable("Households");
                });

            modelBuilder.Entity("FoodPantry2k23.Models.Person", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("HouseHoldID")
                        .HasColumnType("int");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("People");
                });

            modelBuilder.Entity("FoodPantry2k23.Models.Referral", b =>
                {
                    b.Property<int>("ReferralID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReferralID"));

                    b.Property<int>("HouseHoldID")
                        .HasColumnType("int");

                    b.Property<DateTime>("ReferralDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Service")
                        .HasColumnType("int");

                    b.HasKey("ReferralID");

                    b.ToTable("Referrals");
                });
#pragma warning restore 612, 618
        }
    }
}