using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using EventManagement.Models;
 
namespace EventManagement.Dal
{
    public class EventManagementDbContext : DbContext
    {
        public EventManagementDbContext()
        {

        }

        public EventManagementDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                options.UseSqlServer("Data Source = localhost; Initial Catalog = EventMgtDb; Integrated Security = true; TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee() { EmployeeId = 1, EmployeeName = "AK PAUL", Address = "Kenchenhalli", City = "Bangalore", Country = "India", Zipcode = "560098", Phone = "+91 1234567890", Email = "paulak@gmail.com", Skillsets = ".NET Developer", Avatar = "/images/ak.jpg" },
                new Employee() { EmployeeId = 2, EmployeeName = "K PAUL", Address = "Narayanpur", City = "Balurghat", Country = "India", Zipcode = "733101", Phone = "+91 1235567890", Email = "paulk@gmail.com", Skillsets = "People Management", Avatar = "/images/k.jpg" },
                new Employee() { EmployeeId = 3, EmployeeName = "AB PAUL", Address = "Kenchenhalli", City = "Bangalore", Country = "India", Zipcode = "560098", Phone = "+91 12333567890", Email = "paulab@gmail.com", Skillsets = "Trainer and Consultant", Avatar = "/images/ab.jpg" }
                );

            modelBuilder.Entity<Role>().HasData(
                new Role() { RoleId = 1, RoleName = "Employee", RoleDescription = "Employee of Organisation" },
                new Role() { RoleId = 2, RoleName = "HR", RoleDescription = "HR of Organisation" }
                );

        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}