﻿using Infrastructure.Entities;

namespace Infrastructure.Persistance
{
    public class DatabaseContextInitializer
    {
        private readonly DatabaseContext _context;

        public DatabaseContextInitializer(DatabaseContext context)
        {
            _context = context;
        }

        public void Initialise()
        {
            Seed();
        }

        private void Seed()
        {
            if (!_context.Categories.Any())
            {
                var categories = new List<CategoryEntity>
                {
                    new CategoryEntity { Name = "Corporate" },
                    new CategoryEntity { Name = "Drive-Thru" },
                    new CategoryEntity { Name = "Hipster" },
                    new CategoryEntity { Name = "Modern" },
                    new CategoryEntity { Name = "Urban" },
                    new CategoryEntity { Name = "Upscale" }
                };

                _context.Categories.AddRange(categories);

                var categoryDict = categories.ToDictionary(c => c.Name);

                var shops = new List<ShopEntity>
                {
                    new ShopEntity
                    {
                        Name = "Ovation Coffee",
                        Rating = 4.5,
                        Categories = new List<CategoryEntity>
                        {
                            categoryDict["Modern"],
                            categoryDict["Corporate"],
                            categoryDict["Urban"],
                            categoryDict["Upscale"]
                        }
                    },
                    new ShopEntity
                    {
                        Name = "Murray Hill’s",
                        Rating = 3.9,
                        Categories = new List<CategoryEntity>
                        {
                            categoryDict["Urban"],
                            categoryDict["Corporate"],
                            categoryDict["Drive-Thru"],
                            categoryDict["Hipster"]
                        }
                    }
                };

                _context.Shops.AddRange(shops);
                _context.SaveChanges();
            }
        }
    }
}