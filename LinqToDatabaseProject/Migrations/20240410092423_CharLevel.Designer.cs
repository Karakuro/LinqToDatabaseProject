﻿// <auto-generated />
using System;
using LinqToDatabaseProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LinqToDatabaseProject.Migrations
{
    [DbContext(typeof(GameDbContext))]
    [Migration("20240410092423_CharLevel")]
    partial class CharLevel
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LinqToDatabaseProject.Data.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CharacterId"));

                    b.Property<int>("CharacterLevel")
                        .HasColumnType("int");

                    b.Property<int>("LifePoints")
                        .HasColumnType("int");

                    b.Property<string>("Nickname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("CharacterId");

                    b.HasIndex("PlayerId");

                    b.ToTable("Characters");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Inventory", b =>
                {
                    b.Property<int>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryId"));

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<bool?>("IsPrimaryWeapon")
                        .HasColumnType("bit");

                    b.Property<int>("ItemCount")
                        .HasColumnType("int");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.HasKey("InventoryId");

                    b.HasIndex("CharacterId");

                    b.HasIndex("ItemId");

                    b.ToTable("Inventories");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Item", b =>
                {
                    b.Property<int>("ItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemId"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsWeapon")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RarityId")
                        .HasColumnType("int");

                    b.HasKey("ItemId");

                    b.HasIndex("RarityId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Player", b =>
                {
                    b.Property<int>("PlayerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerId"));

                    b.Property<int>("AccountLevel")
                        .HasColumnType("int");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("Credits")
                        .HasColumnType("bigint");

                    b.HasKey("PlayerId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Rarity", b =>
                {
                    b.Property<int>("RarityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RarityId"));

                    b.Property<float>("DropRate")
                        .HasColumnType("real");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RarityId");

                    b.ToTable("Rarities");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Character", b =>
                {
                    b.HasOne("LinqToDatabaseProject.Data.Player", "Player")
                        .WithMany("Characters")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Inventory", b =>
                {
                    b.HasOne("LinqToDatabaseProject.Data.Character", "Character")
                        .WithMany("Inventory")
                        .HasForeignKey("CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LinqToDatabaseProject.Data.Item", "Item")
                        .WithMany("Inventory")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Character");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Item", b =>
                {
                    b.HasOne("LinqToDatabaseProject.Data.Rarity", "Rarity")
                        .WithMany("Items")
                        .HasForeignKey("RarityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rarity");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Character", b =>
                {
                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Item", b =>
                {
                    b.Navigation("Inventory");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Player", b =>
                {
                    b.Navigation("Characters");
                });

            modelBuilder.Entity("LinqToDatabaseProject.Data.Rarity", b =>
                {
                    b.Navigation("Items");
                });
#pragma warning restore 612, 618
        }
    }
}
