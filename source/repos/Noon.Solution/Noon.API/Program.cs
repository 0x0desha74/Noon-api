using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noon.API.Errors;
using Noon.API.Extensions;
using Noon.API.Helpers;
using Noon.API.Middlewares;
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

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("no connection string wes found");
            // Add services to the container.
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            builder.Services.AddControllers();

            builder.Services.AddSwaggerServices();
            builder.Services.AddApplicationServices();
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



            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}"); //Handling NotFound EndPoint
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
