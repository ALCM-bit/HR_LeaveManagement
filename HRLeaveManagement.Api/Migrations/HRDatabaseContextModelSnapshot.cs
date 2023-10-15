﻿// <auto-generated />
using System;
using HRLeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace HRLeaveManagement.Api.Migrations
{
    [DbContext(typeof(HRDatabaseContext))]
    partial class HRDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("HRLeaveManagement.Domain.LeaveAllocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("EmployeeId")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("NumberOfDays")
                        .HasColumnType("NUMBER(10)");

                    b.Property<int>("Period")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("Id");

                    b.HasIndex("LeaveTypeId");

                    b.ToTable("LeaveAllocations");
                });

            modelBuilder.Entity("HRLeaveManagement.Domain.LeaveRequest", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool?>("Approved")
                        .HasColumnType("NUMBER(1)");

                    b.Property<bool>("Cancelled")
                        .HasColumnType("NUMBER(1)");

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("DateRequested")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("LeaveTypeId")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("RequestComments")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("RequestingEmployeeId")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TIMESTAMP(7)");

                    b.HasKey("Id");

                    b.HasIndex("LeaveTypeId");

                    b.ToTable("LeaveRequests");
                });

            modelBuilder.Entity("HRLeaveManagement.Domain.LeaveType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime?>("DateCreated")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime?>("DateModified")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("Defaultdays")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)");

                    b.HasKey("Id");

                    b.ToTable("LeaveTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateCreated = new DateTime(2023, 10, 15, 18, 45, 19, 290, DateTimeKind.Local).AddTicks(5005),
                            DateModified = new DateTime(2023, 10, 15, 18, 45, 19, 290, DateTimeKind.Local).AddTicks(5019),
                            Defaultdays = 10,
                            Name = "Vacation"
                        });
                });

            modelBuilder.Entity("HRLeaveManagement.Domain.LeaveAllocation", b =>
                {
                    b.HasOne("HRLeaveManagement.Domain.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaveType");
                });

            modelBuilder.Entity("HRLeaveManagement.Domain.LeaveRequest", b =>
                {
                    b.HasOne("HRLeaveManagement.Domain.LeaveType", "LeaveType")
                        .WithMany()
                        .HasForeignKey("LeaveTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LeaveType");
                });
#pragma warning restore 612, 618
        }
    }
}