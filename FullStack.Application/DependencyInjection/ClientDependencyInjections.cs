using FullStack.Application.UseCase.Queries;
using FullStack.ServiceModel;
using FuulStack_Test.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.Application.DependencyInjection
{
    public static class ClientDependencyInjections
    {
        public static IServiceCollection AddApplicationClients(this IServiceCollection services, IConfiguration configuration)
        {
            // configure DI for application services
            services.AddHttpContextAccessor();
            services.AddHttpClient<IApplicationClientFactory, ApplicationClientFactory>();
            services.AddMediatR(typeof(GetMoviesQuery.GetMoviesQueryCommandHandler).GetTypeInfo().Assembly);

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.MetadataPropertyHandling = MetadataPropertyHandling.Ignore;
                options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.None;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());

            }).ConfigureApiBehaviorOptions(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errorsInModelState = context.ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .ToDictionary(pair => pair.Key,
                            pair => pair.Value.Errors.Select(x => x.ErrorMessage))
                        .ToArray();

                    var errorViewModel = new ErrorViewModel();

                    //cycle through the errors and add to response
                    foreach (var (key, value) in errorsInModelState)
                    {
                        foreach (var subError in value)
                        {
                            errorViewModel.Errors.Add(new Error
                            {
                                Code = key,
                                Message = subError
                            });
                        }
                    }

                    var error = ResultViewModel.Fail(
                        errorViewModel,
                        $" {StatusCodes.Status400BadRequest.ToString()} : Error occured"
                    );

                    return new BadRequestObjectResult(error);

                };
            });

            #region swagger implementation
            services.AddSwaggerGen(swagger =>
            {
                //This is to generate the Default UI of Swagger Documentation    
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Movies - DeveloperPortal WebApi",
                    Description = "Movies Developer web api portal for third party consumption"
                });
               
                
            });
            #endregion

            return services;
        }
    }
}
