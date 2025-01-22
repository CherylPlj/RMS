﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RMS.Models;

#nullable disable

namespace RMS.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20250122052011_UserProf")]
    partial class UserProf
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RMS.Models.Lease", b =>
                {
                    b.Property<int>("LeaseID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LeaseID"));

                    b.Property<string>("LeaseAgreementFilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LeaseEndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LeaseStartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LeaseStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Active");

                    b.Property<int?>("TenantID")
                        .HasColumnType("int");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("LeaseID");

                    b.HasIndex("TenantID");

                    b.HasIndex("UnitId");

                    b.ToTable("Leases");
                });

            modelBuilder.Entity("RMS.Models.MaintenanceRequest", b =>
                {
                    b.Property<int>("RequestID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RequestID"));

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("RequestDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("RequestStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Pending");

                    b.Property<DateTime?>("ResolutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("StaffID")
                        .HasColumnType("int");

                    b.Property<int?>("TenantID")
                        .HasColumnType("int");

                    b.Property<int?>("UnitID")
                        .HasColumnType("int");

                    b.HasKey("RequestID");

                    b.HasIndex("StaffID");

                    b.HasIndex("TenantID");

                    b.HasIndex("UnitID");

                    b.ToTable("MaintenanceRequests");
                });

            modelBuilder.Entity("RMS.Models.Payment", b =>
                {
                    b.Property<int>("PaymentID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentID"));

                    b.Property<decimal?>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("LeaseID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("PaymentDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PaymentMethod")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Pending");

                    b.Property<string>("TransactionReference")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("PaymentID");

                    b.HasIndex("LeaseID");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("RMS.Models.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CurrentPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NewPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicture")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("RMS.Models.PropertyManager", b =>
                {
                    b.Property<int>("ManagerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ManagerID"));

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ManagerID");

                    b.HasIndex("UserId");

                    b.ToTable("PropertyManagers");
                });

            modelBuilder.Entity("RMS.Models.Staff", b =>
                {
                    b.Property<int>("StaffID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("StaffID"));

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StaffRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("StaffID");

                    b.HasIndex("UserId");

                    b.ToTable("Staffs");
                });

            modelBuilder.Entity("RMS.Models.Tenant", b =>
                {
                    b.Property<int>("TenantID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TenantID"));

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProfilePicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("TenantID");

                    b.HasIndex("UserId");

                    b.ToTable("Tenants");
                });

            modelBuilder.Entity("RMS.Models.Unit", b =>
                {
                    b.Property<int>("UnitID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnitID"));

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Location")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("NumberOfBathrooms")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfBedrooms")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfFloors")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfGarages")
                        .HasColumnType("int");

                    b.Property<int?>("NumberOfUnits")
                        .HasColumnType("int");

                    b.Property<decimal?>("PricePerMonth")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal?>("SecurityDeposit")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("State")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Town")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitOwner")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UnitStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Active");

                    b.Property<string>("UnitType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ZipCode")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UnitID");

                    b.ToTable("Units");
                });

            modelBuilder.Entity("RMS.Models.UnitImage", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<string>("FilePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("UnitId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("UnitId");

                    b.ToTable("UnitImages");
                });

            modelBuilder.Entity("RMS.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmPassword")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(true);

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("nvarchar(64)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("TermsAndConditions")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RMS.Models.Lease", b =>
                {
                    b.HasOne("RMS.Models.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantID");

                    b.HasOne("RMS.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitId");

                    b.Navigation("Tenant");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("RMS.Models.MaintenanceRequest", b =>
                {
                    b.HasOne("RMS.Models.Staff", "Staff")
                        .WithMany("MaintenanceRequests")
                        .HasForeignKey("StaffID");

                    b.HasOne("RMS.Models.Tenant", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantID");

                    b.HasOne("RMS.Models.Unit", "Unit")
                        .WithMany()
                        .HasForeignKey("UnitID");

                    b.Navigation("Staff");

                    b.Navigation("Tenant");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("RMS.Models.Payment", b =>
                {
                    b.HasOne("RMS.Models.Lease", "Lease")
                        .WithMany()
                        .HasForeignKey("LeaseID");

                    b.Navigation("Lease");
                });

            modelBuilder.Entity("RMS.Models.Profile", b =>
                {
                    b.HasOne("RMS.Models.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("RMS.Models.Profile", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("RMS.Models.PropertyManager", b =>
                {
                    b.HasOne("RMS.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RMS.Models.Staff", b =>
                {
                    b.HasOne("RMS.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RMS.Models.Tenant", b =>
                {
                    b.HasOne("RMS.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RMS.Models.UnitImage", b =>
                {
                    b.HasOne("RMS.Models.Unit", "Unit")
                        .WithMany("Images")
                        .HasForeignKey("UnitId");

                    b.Navigation("Unit");
                });

            modelBuilder.Entity("RMS.Models.Staff", b =>
                {
                    b.Navigation("MaintenanceRequests");
                });

            modelBuilder.Entity("RMS.Models.Unit", b =>
                {
                    b.Navigation("Images");
                });

            modelBuilder.Entity("RMS.Models.User", b =>
                {
                    b.Navigation("Profile")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
