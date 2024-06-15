﻿// <auto-generated />
using System;
using Euro24Tracker.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Euro24Tracker.Migrations
{
    [DbContext(typeof(Euro24TrackerContext))]
    [Migration("20240614145914_Migration7")]
    partial class Migration7
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0-preview.4.24267.1");

            modelBuilder.Entity("Euro24Tracker.Types.Ereignis", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EreignisTypId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Kommentar")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Minute")
                        .HasColumnType("INTEGER");

                    b.Property<int>("SpielId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("TorNationId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EreignisTypId");

                    b.HasIndex("SpielId");

                    b.HasIndex("TorNationId");

                    b.ToTable("Ereignisse");
                });

            modelBuilder.Entity("Euro24Tracker.Types.EreignisTyp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("EreignisTyp");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Gruppe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Gruppen");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Nation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Gegentore")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("GruppeId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("LogoLink")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("Punkte")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ShortName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int?>("Tore")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Torverhältnis")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("GruppeId");

                    b.ToTable("Nationen");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Spiel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Datetime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Gruppenphase")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Stadion")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Spiele");
                });

            modelBuilder.Entity("Euro24Tracker.Types.SpielNation", b =>
                {
                    b.Property<int>("SpielId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NationId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("Tore")
                        .HasColumnType("INTEGER");

                    b.HasKey("SpielId", "NationId");

                    b.HasIndex("NationId");

                    b.ToTable("SpielNation");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Ereignis", b =>
                {
                    b.HasOne("Euro24Tracker.Types.EreignisTyp", "EreignisTyp")
                        .WithMany("Ereignisse")
                        .HasForeignKey("EreignisTypId");

                    b.HasOne("Euro24Tracker.Types.Spiel", "Spiel")
                        .WithMany("Ereignisse")
                        .HasForeignKey("SpielId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Euro24Tracker.Types.Nation", "TorNation")
                        .WithMany("TorEreginisse")
                        .HasForeignKey("TorNationId");

                    b.Navigation("EreignisTyp");

                    b.Navigation("Spiel");

                    b.Navigation("TorNation");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Nation", b =>
                {
                    b.HasOne("Euro24Tracker.Types.Gruppe", "Gruppe")
                        .WithMany("Nationen")
                        .HasForeignKey("GruppeId");

                    b.Navigation("Gruppe");
                });

            modelBuilder.Entity("Euro24Tracker.Types.SpielNation", b =>
                {
                    b.HasOne("Euro24Tracker.Types.Nation", "Nation")
                        .WithMany()
                        .HasForeignKey("NationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Euro24Tracker.Types.Spiel", "Spiel")
                        .WithMany()
                        .HasForeignKey("SpielId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Nation");

                    b.Navigation("Spiel");
                });

            modelBuilder.Entity("Euro24Tracker.Types.EreignisTyp", b =>
                {
                    b.Navigation("Ereignisse");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Gruppe", b =>
                {
                    b.Navigation("Nationen");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Nation", b =>
                {
                    b.Navigation("TorEreginisse");
                });

            modelBuilder.Entity("Euro24Tracker.Types.Spiel", b =>
                {
                    b.Navigation("Ereignisse");
                });
#pragma warning restore 612, 618
        }
    }
}
