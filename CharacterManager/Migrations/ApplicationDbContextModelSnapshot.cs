﻿// <auto-generated />
using System;
using CharacterManager.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CharacterManager.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("CharacterManager.Models.Archetype", b =>
                {
                    b.Property<int>("ArchetypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Keywords")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArchetypeId");

                    b.ToTable("Archetype");
                });

            modelBuilder.Entity("CharacterManager.Models.ArchetypeAbility", b =>
                {
                    b.Property<int>("ArchetypeAbilityId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("ArchetypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ArchetypeAbilityId");

                    b.HasIndex("ArchetypeId")
                        .IsUnique();

                    b.ToTable("ArchetypeAbility");
                });

            modelBuilder.Entity("CharacterManager.Models.Attributes", b =>
                {
                    b.Property<int>("AttributesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Agility")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Fellowship")
                        .HasColumnType("int");

                    b.Property<int>("Initiative")
                        .HasColumnType("int");

                    b.Property<int>("Intellect")
                        .HasColumnType("int");

                    b.Property<int>("Strength")
                        .HasColumnType("int");

                    b.Property<int>("Toughness")
                        .HasColumnType("int");

                    b.Property<int>("Willpower")
                        .HasColumnType("int");

                    b.HasKey("AttributesId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("CharacterManager.Models.Character", b =>
                {
                    b.Property<int>("CharacterId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int?>("ArchetypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Tier")
                        .HasColumnType("int");

                    b.Property<int>("XP")
                        .HasColumnType("int");

                    b.HasKey("CharacterId");

                    b.HasIndex("ArchetypeId");

                    b.ToTable("Character");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.TalentLink", b =>
                {
                    b.Property<int>("TalentLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("TalentId")
                        .HasColumnType("int");

                    b.HasKey("TalentLinkId");

                    b.ToTable("TalentLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CharacterLinks.WargearLink", b =>
                {
                    b.Property<int>("WargearLinkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("WargearId")
                        .HasColumnType("int");

                    b.HasKey("WargearLinkId");

                    b.ToTable("WargearLink");
                });

            modelBuilder.Entity("CharacterManager.Models.CombatTraits", b =>
                {
                    b.Property<int>("CombatTraitsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("Resilence")
                        .HasColumnType("int");

                    b.Property<int>("Shock")
                        .HasColumnType("int");

                    b.Property<int>("Soak")
                        .HasColumnType("int");

                    b.Property<int>("Speed")
                        .HasColumnType("int");

                    b.Property<int>("Wounds")
                        .HasColumnType("int");

                    b.HasKey("CombatTraitsId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("CombatTraits");
                });

            modelBuilder.Entity("CharacterManager.Models.MentalTraits", b =>
                {
                    b.Property<int>("MentalTraitsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Conviction")
                        .HasColumnType("int");

                    b.Property<int>("Corruption")
                        .HasColumnType("int");

                    b.Property<int>("PassiveAwareness")
                        .HasColumnType("int");

                    b.Property<int>("Resolve")
                        .HasColumnType("int");

                    b.HasKey("MentalTraitsId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("MentalTraits");
                });

            modelBuilder.Entity("CharacterManager.Models.Skills", b =>
                {
                    b.Property<int>("SkillsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("Athletics")
                        .HasColumnType("int");

                    b.Property<int>("Awareness")
                        .HasColumnType("int");

                    b.Property<int>("Ballistic")
                        .HasColumnType("int");

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Cunning")
                        .HasColumnType("int");

                    b.Property<int>("Deception")
                        .HasColumnType("int");

                    b.Property<int>("Insight")
                        .HasColumnType("int");

                    b.Property<int>("Intimidation")
                        .HasColumnType("int");

                    b.Property<int>("Investigation")
                        .HasColumnType("int");

                    b.Property<int>("Leadership")
                        .HasColumnType("int");

                    b.Property<int>("Medicae")
                        .HasColumnType("int");

                    b.Property<int>("Persuasion")
                        .HasColumnType("int");

                    b.Property<int>("Pilot")
                        .HasColumnType("int");

                    b.Property<int>("Pyschic")
                        .HasColumnType("int");

                    b.Property<int>("Scholar")
                        .HasColumnType("int");

                    b.Property<int>("Stealth")
                        .HasColumnType("int");

                    b.Property<int>("Survival")
                        .HasColumnType("int");

                    b.Property<int>("Tech")
                        .HasColumnType("int");

                    b.Property<int>("Weapon")
                        .HasColumnType("int");

                    b.HasKey("SkillsId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("Skills");
                });

            modelBuilder.Entity("CharacterManager.Models.SocialTraits", b =>
                {
                    b.Property<int>("SocialTraitsId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("CharacterId")
                        .HasColumnType("int");

                    b.Property<int>("Influence")
                        .HasColumnType("int");

                    b.Property<int>("Wealth")
                        .HasColumnType("int");

                    b.HasKey("SocialTraitsId");

                    b.HasIndex("CharacterId")
                        .IsUnique();

                    b.ToTable("SocialTraits");
                });

            modelBuilder.Entity("CharacterManager.Models.Talent", b =>
                {
                    b.Property<int>("TalentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TalentId");

                    b.ToTable("Talent");
                });

            modelBuilder.Entity("CharacterManager.Models.Wargear", b =>
                {
                    b.Property<int>("WargearId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<int>("AP")
                        .HasColumnType("int");

                    b.Property<string>("Damage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Range")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salvo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Traits")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("WargearId");

                    b.ToTable("Wargear");
                });

            modelBuilder.Entity("CharacterManager.Models.ArchetypeAbility", b =>
                {
                    b.HasOne("CharacterManager.Models.Archetype", null)
                        .WithOne("Ability")
                        .HasForeignKey("CharacterManager.Models.ArchetypeAbility", "ArchetypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.Attributes", b =>
                {
                    b.HasOne("CharacterManager.Models.Character", null)
                        .WithOne("Attributes")
                        .HasForeignKey("CharacterManager.Models.Attributes", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.Character", b =>
                {
                    b.HasOne("CharacterManager.Models.Archetype", "Archetype")
                        .WithMany()
                        .HasForeignKey("ArchetypeId");

                    b.Navigation("Archetype");
                });

            modelBuilder.Entity("CharacterManager.Models.CombatTraits", b =>
                {
                    b.HasOne("CharacterManager.Models.Character", null)
                        .WithOne("CombatTraits")
                        .HasForeignKey("CharacterManager.Models.CombatTraits", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.MentalTraits", b =>
                {
                    b.HasOne("CharacterManager.Models.Character", null)
                        .WithOne("MentalTraits")
                        .HasForeignKey("CharacterManager.Models.MentalTraits", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.Skills", b =>
                {
                    b.HasOne("CharacterManager.Models.Character", null)
                        .WithOne("Skills")
                        .HasForeignKey("CharacterManager.Models.Skills", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.SocialTraits", b =>
                {
                    b.HasOne("CharacterManager.Models.Character", null)
                        .WithOne("SocialTraits")
                        .HasForeignKey("CharacterManager.Models.SocialTraits", "CharacterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CharacterManager.Models.Archetype", b =>
                {
                    b.Navigation("Ability");
                });

            modelBuilder.Entity("CharacterManager.Models.Character", b =>
                {
                    b.Navigation("Attributes");

                    b.Navigation("CombatTraits");

                    b.Navigation("MentalTraits");

                    b.Navigation("Skills");

                    b.Navigation("SocialTraits");
                });
#pragma warning restore 612, 618
        }
    }
}
