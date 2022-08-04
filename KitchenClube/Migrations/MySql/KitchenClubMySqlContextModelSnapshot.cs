﻿// <auto-generated />
using System;
using KitchenClube.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KitchenClube.Migrations.MySql
{
    [DbContext(typeof(KitchenClubMySqlContext))]
    partial class KitchenClubMySqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("KitchenClube.Models.Food", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Description")
                        .HasColumnType("longtext");

                    b.Property<string>("Image")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Foods", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("f12f0134-8043-4cb8-9590-da79c738cf52"),
                            Description = "1 ба 1",
                            Image = "images/osh.png",
                            IsActive = true,
                            Name = "Osh"
                        },
                        new
                        {
                            Id = new Guid("a7ae556f-178c-4e07-a0b0-bd4f431c6dd9"),
                            Description = "Бо гӯшти гӯспанд",
                            Image = "images/kazan.png",
                            IsActive = true,
                            Name = "Kazan-Kebab"
                        },
                        new
                        {
                            Id = new Guid("3f6a9572-91b4-4f92-81cd-a2037de19de4"),
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
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Menus", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("fbb70259-8cb1-4b3d-9da6-4a0b0586d45b"),
                            EndDate = new DateTime(2022, 7, 29, 11, 23, 56, 118, DateTimeKind.Local).AddTicks(623),
                            StartDate = new DateTime(2022, 7, 29, 11, 23, 56, 118, DateTimeKind.Local).AddTicks(606),
                            Status = 1
                        });
                });

            modelBuilder.Entity("KitchenClube.Models.MenuItem", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Day")
                        .HasColumnType("datetime(6)");

                    b.Property<Guid>("FoodId")
                        .HasColumnType("char(36)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("MenuId")
                        .HasColumnType("char(36)");

                    b.HasKey("Id");

                    b.HasIndex("FoodId");

                    b.HasIndex("MenuId");

                    b.ToTable("MenuItems", (string)null);
                });

            modelBuilder.Entity("KitchenClube.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Email")
                        .HasColumnType("longtext");

                    b.Property<string>("FullName")
                        .HasColumnType("longtext");

                    b.Property<bool>("IsActive")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("longtext");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("01869113-04bb-4a83-ab3c-e5d9dba45d3c"),
                            Email = "azizjon@gmail.com",
                            FullName = "Azizjon",
                            IsActive = true,
                            PasswordHash = "",
                            PhoneNumber = "+992 92 929 2992"
                        },
                        new
                        {
                            Id = new Guid("5b5de6cc-a6fb-4433-ac37-8a815a81a595"),
                            Email = "amirjon@gmail.com",
                            FullName = "Amirjon",
                            IsActive = true,
                            PasswordHash = "",
                            PhoneNumber = "+992 92 777 00 77"
                        },
                        new
                        {
                            Id = new Guid("2120d68e-8b8e-4fad-9348-ca0afef4c5af"),
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
                        .HasColumnType("char(36)");

                    b.Property<Guid>("MenuitemId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("char(36)");

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
