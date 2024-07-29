﻿
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;
using ToyStore.Api.Errors;
using ToyStore.Api.Helpers;
using ToyStore.Core.IRepository;
using ToyStore.Infrastructure.Repo;

namespace ToyStore.Api.Extensions
{
    public static class ApplicationExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services,IConfiguration configuration) {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IBasketService, BasketService>();
            services.AddAutoMapper(typeof(AppMapper));
            services.Configure<ApiBehaviorOptions>(opt =>
            {
                opt.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState.Where(x => x.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                    var errorResponse = new APiValidationErrorResponse();
                    errorResponse.Errors = errors;
                    return new BadRequestObjectResult(errorResponse);
                };
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", pol =>
                {
                    pol.AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithOrigins("http://localhost:5255");
                });
            });

            return services;
        }
    }
}
