using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public static class ShopListDbContextSeed
    {
        public static async Task SeedAsync(ShopListDbContext context)
        {
            if (!context.Products.Any() || !context.Categories.Any())
            {
                var c1 = new Category() { Name = "Daily Product" }; 
                var c2 = new Category() { Name = "Fruits" };
                var c3 = new Category() { Name = "Vegetables" };

                var p1 = new Product()
                {
                    Name = "Banana",
                    PictureUri = "banana.png",
                    Category = c2
                };
                var p2 = new Product()
                {
                    Name = "Orange",
                    PictureUri = "orange.png",
                    Category = c2
                };
                var p3 = new Product()
                {
                    Name = "Kiwi",
                    PictureUri = "kiwi.png",
                    Category = c2
                };
                var p4 = new Product()
                {
                    Name = "Tomato",
                    PictureUri = "tomato.png",
                    Category = c3
                };
                var p5 = new Product()
                {
                    Name = "Potato",
                    PictureUri = "potato.png",
                    Category = c3
                };
                var p6 = new Product()
                {
                    Name = "Egg",
                    PictureUri = "eggs.png",
                    Category = c1
                };
                var p7 = new Product()
                {
                    Name = "Bread",
                    PictureUri = "bread.png",
                    Category = c1
                };
                var p8 = new Product()
                {
                    Name = "Meat",
                    PictureUri = "meat.png",
                    Category = c1
                };

                context.AddRange(p1, p2, p3, p4, p5, p6, p7, p8);
                await context.SaveChangesAsync();
            }
        }
    }
}
