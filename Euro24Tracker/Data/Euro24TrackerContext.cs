using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Euro24Tracker.Types;

namespace Euro24Tracker.Data
{
    public class Euro24TrackerContext : DbContext
    {
        public Euro24TrackerContext (DbContextOptions<Euro24TrackerContext> options)
            : base(options)
        {
        }

       // public DbSet<Euro24Tracker.Types.Nation> Nation { get; set; } = default!;

        public DbSet<Nation> Nationen { get; set; }
        public DbSet<Gruppe> Gruppen { get; set; }
        public DbSet<Spiel> Spiele { get; set; }
        public DbSet<Ereignis> Ereignisse { get; set; }
        public DbSet<SpielNation> SpielNationen { get; set; }

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

            modelBuilder.Entity<SpielNation>()
                .HasOne(sn => sn.Spiel)
                .WithMany(s => s.SpielNationen)
                .HasForeignKey(sn => sn.SpielId);

            modelBuilder.Entity<SpielNation>()
                .HasOne(sn => sn.Nation)
                .WithMany(n => n.SpieleNation)
                .HasForeignKey(sn => sn.NationId);

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
    }
}
