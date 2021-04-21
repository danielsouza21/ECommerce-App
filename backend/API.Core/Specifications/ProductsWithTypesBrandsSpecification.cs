using API.Core.Entities;

namespace API.Core.Specifications
{
    public class ProductsWithTypesBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesBrandsSpecification()
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
