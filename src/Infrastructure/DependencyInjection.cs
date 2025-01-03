﻿using Domain.Repositories;
using Infrastructure.Api.CategoryRepository;
using Infrastructure.Api.CoffeeRepository;
using Infrastructure.Api.ShopRepository;
using Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            //AutoMapper

            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            // Database

            services.AddDbContext<DatabaseContext>();
            var dbContext = new DatabaseContext();
            dbContext.Database.EnsureCreated();
            dbContext.Dispose();

            // Database Initializer

            services.AddSingleton<DatabaseContextInitializer>();

            // Repositories

            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IShopRepository, ShopRepository>();
            services.AddScoped<ICoffeeRepository, CoffeeRepository>();

            return services;
        }
    }
}
