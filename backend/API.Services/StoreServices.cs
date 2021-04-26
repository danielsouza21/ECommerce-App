using System.Collections.Generic;
using System.Threading.Tasks;
using API.Core.Entities;
using API.Core.Interfaces;
using API.Core.Specifications;
using API.Infrastructure.Data.EfCore.Specifications;

namespace API.Services
{
    public class StoreServices : IStoreServices
    {
        private readonly IGenericRepository<Product> _repoProducts;
        private readonly IGenericRepository<ProductBrand> _repoBrands;
        private readonly IGenericRepository<ProductType> _repoTypes;

        public StoreServices(IGenericRepository<Product> repoProducts, IGenericRepository<ProductBrand> repoBrands, IGenericRepository<ProductType> repoTypes)
        {
            _repoProducts = repoProducts;
            _repoBrands = repoBrands;
            _repoTypes = repoTypes;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetAllProductsBrands()
        {
            return await _repoBrands.GetAllAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetAllProductsTypes()
        {
            return await _repoTypes.GetAllAsync();
        }

        public async Task<Product> GetProductById(int id)
        {
            var spec = new ProductsWithTypesBrandsSpecification(id);
            return await _repoProducts.GetEntityWithSpec(spec);
        }

        public async Task<IReadOnlyList<Product>> GetProductsAsync(ProductSpecParams productParams)
        {
            var spec = new ProductsWithTypesBrandsSpecification(productParams);
            return await _repoProducts.GetWithSpecAsync(spec);
        }
    }
}
