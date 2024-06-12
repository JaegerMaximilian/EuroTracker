using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EURO2024App.Types;

namespace Euro24Tracker.Data
{
    public class Euro24TrackerContext : DbContext
    {
        public Euro24TrackerContext (DbContextOptions<Euro24TrackerContext> options)
            : base(options)
        {
        }

       // public DbSet<Euro24Tracker.Types.Nation> Nation { get; set; } = default!;

        public DbSet<EURO2024App.Types.Nation> Nationen { get; set; }
        public DbSet<EURO2024App.Types.Gruppe> Gruppen { get; set; }
        public DbSet<EURO2024App.Types.Spiel> Spiele { get; set; }
        public DbSet<EURO2024App.Types.Ereignis> Ereignisse { get; set; }
        public DbSet<EURO2024App.Types.SpielNation> SpielNation { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1:n Beziehung zwischen Nation und Gruppe
            modelBuilder.Entity<Nation>()
                .HasOne(n => n.Gruppe)
                .WithMany(g => g.Nationen)
                .HasForeignKey(n => n.GruppeId);

            // n:m Beziehung zwischen Spiel und Nation
            modelBuilder.Entity<SpielNation>()
                .HasKey(sn => new { sn.SpielId, sn.NationId });

            modelBuilder.Entity<Spiel>()
              .HasMany(e => e.Nationen)
              .WithMany(e => e.Spiele)
              .UsingEntity<SpielNation>();

            // 1:n Beziehung zwischen Spiel und Ereignis
            modelBuilder.Entity<Ereignis>()
                .HasOne(e => e.Spiel)
                .WithMany(s => s.Ereignisse)
                .HasForeignKey(e => e.SpielId);

            // 1:n Beziehung zwischen Spiel und Ereignis
            modelBuilder.Entity<Ereignis>()
                .HasOne(e => e.EreignisTyp)
                .WithMany(s => s.Ereignisse)
                .HasForeignKey(e => e.EreignisTypId);
        }
        public DbSet<Euro24Tracker.Types.EreignisTyp> EreignisTyp { get; set; } = default!;
    }
}
