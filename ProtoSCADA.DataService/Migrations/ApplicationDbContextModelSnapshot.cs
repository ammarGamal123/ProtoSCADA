﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProtoSCADA.Data.Context;

#nullable disable

namespace ProtoSCADA.DataService.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Alert", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<byte>("Condition")
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("MachineID")
                        .HasColumnType("int");

                    b.Property<float>("ThersholdValue")
                        .HasColumnType("real");

                    b.Property<int?>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.HasIndex("UserID");

                    b.ToTable("Alerts", (string)null);
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MachineID")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.HasIndex("UserID");

                    b.ToTable("Events", (string)null);
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Factory", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Factorys", (string)null);
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Machine", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("FactoryID")
                        .HasColumnType("int");

                    b.Property<DateTime>("LastMaintance")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("Machines", (string)null);
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Metric", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("MachineID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<float>("Value")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("MachineID");

                    b.ToTable("Metrics", (string)null);
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("FactoryID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("FactoryID");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Alert", b =>
                {
                    b.HasOne("ProtoSCADA.Entities.Entities.Machine", "Machine")
                        .WithMany("Alerts")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProtoSCADA.Entities.Entities.User", null)
                        .WithMany("Alerts")
                        .HasForeignKey("UserID");

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Event", b =>
                {
                    b.HasOne("ProtoSCADA.Entities.Entities.Machine", "Machine")
                        .WithMany("Events")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProtoSCADA.Entities.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");

                    b.Navigation("User");
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Machine", b =>
                {
                    b.HasOne("ProtoSCADA.Entities.Entities.Factory", "Factory")
                        .WithMany("Machines")
                        .HasForeignKey("FactoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Factory");
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Metric", b =>
                {
                    b.HasOne("ProtoSCADA.Entities.Entities.Machine", "Machine")
                        .WithMany("Metrics")
                        .HasForeignKey("MachineID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Machine");
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.User", b =>
                {
                    b.HasOne("ProtoSCADA.Entities.Entities.Factory", null)
                        .WithMany("Users")
                        .HasForeignKey("FactoryID");
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Factory", b =>
                {
                    b.Navigation("Machines");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.Machine", b =>
                {
                    b.Navigation("Alerts");

                    b.Navigation("Events");

                    b.Navigation("Metrics");
                });

            modelBuilder.Entity("ProtoSCADA.Entities.Entities.User", b =>
                {
                    b.Navigation("Alerts");
                });
#pragma warning restore 612, 618
        }
    }
}
