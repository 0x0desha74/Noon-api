using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noon.API.Errors;
using Noon.API.Extensions;
using Noon.API.Helpers;
using Noon.API.Middlewares;
using Noon.Core.Entities.Identity;
using Noon.Core.Repositories;
using Noon.Repository;
using Noon.Repository.Data;
using Noon.Repository.Identity;
using StackExchange.Redis;

namespace Noon.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
             
            //Allow Dependency Injection Of ApplicationDbContext
            builder.Services.AddDbContext<StoreContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("no connection string wes found");
                options.UseSqlServer(connectionString);
            });


            //Allow Dependency Injection Of Redis
            builder.Services.AddSingleton<IConnectionMultiplexer>(S =>
            {
                var cs = builder.Configuration.GetConnectionString("Redis");
                return ConnectionMultiplexer.Connect(cs);
            });


            //Allow Dependency Injection Of AppIdentityDbContext
            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                var identityConnectionString = builder.Configuration.GetConnectionString("IdentityConnection") ?? throw new InvalidOperationException("No Connection String Was Found");
                options.UseSqlServer(identityConnectionString);
            });


            //Application Services
            builder.Services.AddApplicationServices();
            builder.Services.AddIdentityServices(builder.Configuration);
            builder.Services.AddSwaggerServices();
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().WithOrigins(builder.Configuration["FrontBaseUrl"]);
                });
            });
            var app = builder.Build();




            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = services.GetRequiredService<StoreContext>(); //Creating object form dbContext explicitly
                await dbContext.Database.MigrateAsync(); //Apply Migrations
                await StoreContextSeed.SeedDataAsync(dbContext); //Seeding Data

                var identityDbContext = services.GetRequiredService<AppIdentityDbContext>();
                await identityDbContext.Database.MigrateAsync();

                //Seeding data of the first user
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsync(userManager);

            }
            catch (Exception ex)
            {

                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error Occurred During Applying the Migration");
            }



            #region Configur Kestrel Middlewares
            app.UseMiddleware<ExceptionMiddleware>();
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerMiddlewares();
            }

            app.UseStatusCodePagesWithReExecute("/errors/{0}"); //Handling NotFound EndPoint
            app.UseHttpsRedirection();
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            #endregion

            app.UseStaticFiles();
            app.MapControllers();

            app.Run();
        }
    }
}
