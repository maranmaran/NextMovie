using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Domain.Entities;

namespace Domain
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<UserMovie> Movies { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //// seed test admin
            //var admin = new Admin()
            //{
            //    Id = Guid.Parse("0faee6ac-1772-4bbe-9990-a7d9a22dd529"),
            //    Username = "admin",
            //    PasswordHash = "8c6976e5b5410415bde908bd4dee15dfb167a9c873fc4bb8a81f6f2ab448a918"
            //};
            //modelBuilder.Entity<Admin>().HasData(admin);

            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }

    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            // IDesignTimeDbContextFactory is used usually when you execute EF Core commands like Add-Migration, Update-Database, and so on
            Console.WriteLine("Which environment you wish to operate with");
            var environment = (Console.ReadLine())?.ToLower().Trim();

            if (environment != "development" &&
                environment != "release" &&
                environment != "dev" &&
                environment != "prod")
            {
                throw new Exception("Environment can only be Development or Release");
            }

            if (environment == "dev" || environment == "development")
                environment = "Development";
            if (environment == "prod" || environment == "release")
                environment = "Release";


            // Build config
            var config = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../API"))
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment}.json", optional: true)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            // Here we create the DbContextOptionsBuilder manually.        
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(connectionString);

            // Create our DbContext.
            return new ApplicationDbContext(builder.Options);
        }
    }
}
