using Microsoft.AspNetCore.Mvc;
using Noon.API.Errors;
using Noon.API.Heplers;
using Noon.Core.Repositories;
using Noon.Repository;

namespace Noon.API.Extensions
{
    public static class ApplicationServicesExtensions
    {


        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
        
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddAutoMapper(typeof(MappingProfiles));


            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {

                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                         .SelectMany(P => P.Value.Errors)
                                                         .Select(E => E.ErrorMessage)
                                                         .ToArray();


                    var validationErrorResponse = new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(validationErrorResponse);
                };
            });
                return services;

        }



       
    }
}
