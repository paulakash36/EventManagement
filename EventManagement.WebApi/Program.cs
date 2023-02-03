using EventManagement.Dal;
using EventManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddDbContext<EventManagementDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("MsSqlConStr")));

            builder.Services.AddTransient<ICommonRepository<Employee>, CommonRepository<Employee>>();

            builder.Services.AddTransient<ICommonRepository<Event>, CommonRepository<Event>>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}