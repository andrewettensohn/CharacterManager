﻿// <auto-generated />
using System;
using CharacterManager.Sync.API.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CharacterManager.Sync.API.Migrations
{
    [DbContext(typeof(SyncDbContext))]
    partial class SyncDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CharacterManager.DAC.Models.SyncStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("ArchetypeLastSync")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("ArmorLastSync")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CharacterLastSync")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("GearLastSync")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("TalentLastSync")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("WeaponLastSync")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("SyncStatus");
                });

            modelBuilder.Entity("CharacterManager.Sync.API.Models.ArchetypeSync", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Json")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ArchetypeModels");
                });

            modelBuilder.Entity("CharacterManager.Sync.API.Models.ArmorSync", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Json")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ArmorModels");
                });

            modelBuilder.Entity("CharacterManager.Sync.API.Models.CharacterSync", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Json")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CharacterModels");
                });

            modelBuilder.Entity("CharacterManager.Sync.API.Models.GearSync", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Json")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("GearModels");
                });

            modelBuilder.Entity("CharacterManager.Sync.API.Models.TalentSync", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Json")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TalentModels");
                });

            modelBuilder.Entity("CharacterManager.Sync.API.Models.WeaponSync", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Json")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("WeaponModels");
                });
#pragma warning restore 612, 618
        }
    }
}
