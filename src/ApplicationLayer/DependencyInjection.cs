﻿using ApplicationLayer.Services.CartService;
using ApplicationLayer.Services.CategoryService;
using ApplicationLayer.Services.CoffeeService;
using ApplicationLayer.Services.PromotionService;
using ApplicationLayer.Services.ShopService;
using Microsoft.Extensions.DependencyInjection;

namespace ApplicationLayer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Services

            services.AddScoped<CategoryService>();
            services.AddScoped<ShopService>();
            services.AddScoped<CoffeeService>();
            services.AddScoped<CartService>();
            services.AddScoped<PromotionService>();

            return services;
        }
    }
}
