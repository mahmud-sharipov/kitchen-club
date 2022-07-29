﻿// <auto-generated />
using System;
using KitchenClube.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KitchenClube.Migrations.SqlServer
{
    [DbContext(typeof(KitchenClubSqlServerContext))]
    [Migration("20220729040920_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KitchenClube.Models.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Foods", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("b3192fdd-c484-4558-a92c-85277076c389"),
                            Description = "1 ба 1",
                            Image = "images/osh.png",
                            IsActive = true,
                            Name = "Osh"
                        },
                        new
                        {
                            Id = new Guid("df7e0db4-4e07-405b-816f-9c1aee84ebd5"),
                            Description = "Бо гӯшти гӯспанд",
                            Image = "images/kazan.png",
                            IsActive = true,
                            Name = "Kazan-Kebab"
                        },
                        new
                        {
                            Id = new Guid("b82375b0-761d-4073-a019-9d8f737efdf4"),
                            Description = "Бо гӯшти гов",
                            Image = "images/jazza.png",
                            IsActive = true,
                            Name = "Jazza"
                        });
                });

            modelBuilder.Entity("KitchenClube.Models.Menu", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Menus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("e3e494c4-f4c8-41e1-a93e-e8eab21e5674"),
                            EndDate = new DateTime(2022, 7, 29, 9, 9, 20, 769, DateTimeKind.Local).AddTicks(1425),
                            StartDate = new DateTime(2022, 7, 29, 9, 9, 20, 769, DateTimeKind.Local).AddTicks(1418),
                            Status = 1
                        });
                });

            modelBuilder.Entity("KitchenClube.Models.MenuItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItems", (string)null);
                });

            modelBuilder.Entity("KitchenClube.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8571eca7-d8d3-4e78-a43d-f48eadc8ba4e"),
                            Email = "azizjon@gmail.com",
                            FullName = "Azizjon",
                            IsActive = true,
                            PasswordHash = "",
                            PhoneNumber = "+992 92 929 2992"
                        },
                        new
                        {
                            Id = new Guid("8a9c9de8-1da2-4ac7-a8ec-ac5715c57950"),
                            Email = "amirjon@gmail.com",
                            FullName = "Amirjon",
                            IsActive = true,
                            PasswordHash = "",
                            PhoneNumber = "+992 92 777 00 77"
                        },
                        new
                        {
                            Id = new Guid("5642ea1a-d5bb-4eda-8efe-89e1e794a788"),
                            Email = "karimjon@gamil.com",
                            FullName = "Karimjon",
                            IsActive = true,
                            PasswordHash = "",
                            PhoneNumber = "+992 92 888 77 66"
                        });
                });

            modelBuilder.Entity("KitchenClube.Models.UserMenuItemSelection", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("MenuitemId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Vote")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("MenuitemId");

                    b.HasIndex("UserId");

                    b.ToTable("UserMenuItemSelections", (string)null);
                });

            modelBuilder.Entity("KitchenClube.Models.MenuItem", b =>
                {
                    b.HasOne("KitchenClube.Models.Food", "Food")
                        .WithMany("MenuItems")
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("KitchenClube.Models.Menu", "Menu")
                        .WithMany("MenuItems")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Menu");
                });

            modelBuilder.Entity("KitchenClube.Models.UserMenuItemSelection", b =>
                {
                    b.HasOne("KitchenClube.Models.MenuItem", "Menuitem")
                        .WithMany("UserMenuItemSelections")
                        .HasForeignKey("MenuitemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KitchenClube.Models.User", "User")
                        .WithMany("MenuSelections")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Menuitem");

                    b.Navigation("User");
                });

            modelBuilder.Entity("KitchenClube.Models.Food", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("KitchenClube.Models.Menu", b =>
                {
                    b.Navigation("MenuItems");
                });

            modelBuilder.Entity("KitchenClube.Models.MenuItem", b =>
                {
                    b.Navigation("UserMenuItemSelections");
                });

            modelBuilder.Entity("KitchenClube.Models.User", b =>
                {
                    b.Navigation("MenuSelections");
                });
#pragma warning restore 612, 618
        }
    }
}