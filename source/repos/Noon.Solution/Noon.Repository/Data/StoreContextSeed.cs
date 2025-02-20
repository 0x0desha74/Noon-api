using Noon.Core.Entities;
using Noon.Core.Entities.Order_Aggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Noon.Repository.Data
{
    public static class StoreContextSeed
    {
        public static async Task SeedDataAsync(StoreContext dbContext)
        {
            if (!dbContext.ProductBrands.Any())
            {

            var brandsData = File.ReadAllText("../Noon.Repository/Data/DataSeed/brands.json");
            var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
            if(brands is not null && brands.Count() > 0)
            {
                foreach(var brand in brands)
                    await dbContext.ProductBrands.AddAsync(brand);
            }
            await dbContext.SaveChangesAsync();
            }

            if (!dbContext.ProductTypes.Any())
            {

            var typesData = File.ReadAllText("../Noon.Repository/Data/DataSeed/types.json");
            var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
            if(types is not null && types.Count() > 0)
            {
                    foreach (var type in types)
                    await dbContext.ProductTypes.AddAsync(type);
            }
            await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Products.Any())
            {

                var productsData = File.ReadAllText("../Noon.Repository/Data/DataSeed/products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if (products is not null && products.Count() > 0)
                {
                    foreach (var product in products)
                        await dbContext.Products.AddAsync(product);
                }
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.DeliveryMethods.Any())
            {
                var deliveryMethodsData = File.ReadAllText("../Noon.Repository/Data/DataSeed/delivery.json");
                var deliveryMethods = JsonSerializer.Deserialize<List<DeliveryMethod>>(deliveryMethodsData);
                if(deliveryMethods is not null && deliveryMethods.Count() > 0)
                {
                    foreach (var deliveryMethod in deliveryMethods)
                        await dbContext.DeliveryMethods.AddAsync(deliveryMethod);
                }
                await dbContext.SaveChangesAsync();
            }
        }

    }
}
