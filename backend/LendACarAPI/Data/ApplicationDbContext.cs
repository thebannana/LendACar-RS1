using LendACarAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;



namespace LendACarAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
        DbContextOptions options) : base(options)
        { }

        public DbSet<City> Cities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<CompanyEmployee> CompanyEmployees { get; set; }
        public DbSet<CompanyReview> CompanyReviews { get; set; }
        public DbSet<CompanyPosition> CompanyPositions { get; set; }
        public DbSet<Person> Person { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserReview> UserReviews { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleCategory> VehicleCategories { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<VehicleReview> VehicleReviews { get; set; }
        public DbSet<VehicleImage> VehicleImages { get; set; }
        public DbSet<WorkingHour> WorkingHours { get; set; }
        public DbSet<DriversLicense> DriversLicense { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CompanyReview>()
                .HasKey(x => new { x.ReviewerId, x.CompanyId });
            modelBuilder.Entity<UserReview>()
                .HasKey(x => new { x.ReviewerId, x.UserId });
            modelBuilder.Entity<VehicleReview>()
                .HasKey(x => new { x.ReviewerId, x.VehicleId });

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>().UseTpcMappingStrategy();

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            }

            // opcija kod nasljeđivanja
            // modelBuilder.Entity<NekaBaznaKlasa>().UseTpcMappingStrategy();
        }
    }
}
