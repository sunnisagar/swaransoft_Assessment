using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Web.Mvc;

namespace swaransoft_Assessment.Models
{
    public class dbContext : DbContext
    {
        public dbContext() : base("dbContext_Connection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CascadingModel>()
                .HasKey(c => c.CityId)
                .Property(c => c.CityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<SelectListItem>()
                .HasKey(s => s.Selected)
                .Property(s => s.Selected)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Student> Students { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<State> States { get; set; }
    }
}