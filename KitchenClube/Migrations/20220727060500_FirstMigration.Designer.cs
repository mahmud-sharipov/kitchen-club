﻿// <auto-generated />
using System;
using KitchenClube.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KitchenClube.Migrations
{
    [DbContext(typeof(KitchenClubContext))]
    [Migration("20220727060500_FirstMigration")]
    partial class FirstMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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
