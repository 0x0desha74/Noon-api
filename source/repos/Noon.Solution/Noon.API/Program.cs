using Microsoft.EntityFrameworkCore;
using Noon.API.Heplers;
using Noon.Core.Repositories;
using Noon.Repository;
using Noon.Repository.Data;

namespace Noon.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
        
            #region Configure Services
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("no connection string wes found");
            // Add services to the container.
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(); 
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(typeof(MappingProfiles));
            #endregion


            var app = builder.Build();
            
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>(); //Creating object form dbContext explicitly
                await dbContext.Database.MigrateAsync(); //Apply Migrations
                await StoreContextSeed.SeedDataAsync(dbContext); //Seeding Data
            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred During Applying the Migration");
            }



            #region Configure Kestrel Middelwares
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
