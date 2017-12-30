using Examen_BoeykensCaroline_3IMDA.Entities;
using Microsoft.EntityFrameworkCore;

namespace Examen_BoeykensCaroline_3IMDA.Data
{
    public class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Car>().HasKey(c => c.Id);

            modelBuilder.Entity<CarOwner>().HasKey(co => new {co.OwnerId, co.CarId});

            modelBuilder.Entity<Owner>().HasKey(o => o.Id);
            modelBuilder.Entity<Owner>().HasMany(c => c.Car).WithOne(co => co.Owner);

            modelBuilder.Entity<Cartype>().HasKey(t => t.Id);
            modelBuilder.Entity<Car>().HasOne(t => t.Cartype).WithMany(c => c.Cars);
        }

        public DbSet<Owner> Owners { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Cartype> Cartype { get; set; }
    }
}
