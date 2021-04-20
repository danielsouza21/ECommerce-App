using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using API.Domain.Entities;
using API.Infrastructure.Data.EfCore;
using Microsoft.Extensions.Logging;

namespace API.Infrastructure.Data.SeedData
{
    public class StoreContextSeed
    {
        private const string FILE_LOCATION_DUMMY_DATA = "../API.Infrastructure/Data/SeedData/DummyData";
        private const string FILE_NAME_BRANDS_DATA = "/brands.json";
        private const string FILE_NAME_TYPES_DATA = "/types.json";
        private const string FILE_NAME_PRODUCTS_DATA = "/products.json";

        public static async Task SeedAsync(StoreContext context, ILoggerFactory loggerFactory)
        {
            try
            {
                await SeedProductBrandsTable(context);
                await SeedProductTypesTable(context);
                await SeedProductsTable(context);
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<StoreContext>();
                logger.LogError(ex.Message);
            }
        }

        #region privateMethods

        private static async Task SeedProductBrandsTable(StoreContext context)
        {
            if (!context.ProductBrands.Any())
            {
                var brandsFileLocation = FILE_LOCATION_DUMMY_DATA + FILE_NAME_BRANDS_DATA;
                var brandsData = File.ReadAllText(brandsFileLocation);

                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                foreach (var item in brands)
                {
                    context.ProductBrands.Add(item);
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductTypesTable(StoreContext context)
        {
            if (!context.ProductTypes.Any())
            {
                var typesFileLocation = FILE_LOCATION_DUMMY_DATA + FILE_NAME_TYPES_DATA;
                var typesData = File.ReadAllText(typesFileLocation);

                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                foreach (var item in types)
                {
                    context.ProductTypes.Add(item);
                }

                await context.SaveChangesAsync();
            }
        }

        private static async Task SeedProductsTable(StoreContext context)
        {
            if (!context.Products.Any())
            {
                var productsFileLocation = FILE_LOCATION_DUMMY_DATA + FILE_NAME_PRODUCTS_DATA;
                var productsData = File.ReadAllText(productsFileLocation);

                var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                foreach (var item in products)
                {
                    context.Products.Add(item);
                }

                await context.SaveChangesAsync();
            }
        }

        #endregion privateMethods
    }
}
