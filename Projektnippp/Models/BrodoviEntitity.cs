namespace Projektnippp.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BrodoviEntitity : DbContext
    {
        public BrodoviEntitity()
            : base("name=BrodoviEntitity")
        {
        }

        public virtual DbSet<Brod> Brods { get; set; }
        public virtual DbSet<Karta> Kartas { get; set; }
        public virtual DbSet<Putnik> Putniks { get; set; }
        public virtual DbSet<Racun> Racuns { get; set; }
        public virtual DbSet<Voznja> Voznjas { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Putnik>()
                .HasMany(e => e.Kartas)
                .WithOptional(e => e.Putnik1)
                .HasForeignKey(e => e.Putnik);

            modelBuilder.Entity<Racun>()
                .Property(e => e.Iznos)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Voznja>()
                .Property(e => e.Cijena)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Voznja>()
                .HasMany(e => e.Kartas)
                .WithOptional(e => e.Voznja1)
                .HasForeignKey(e => e.Voznja);
        }
    }
}
