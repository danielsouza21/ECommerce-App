using System;
using System.Linq.Expressions;
using API.Core.Entities;
using API.Core.Specifications;

namespace API.Infrastructure.Data.EfCore.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(DefineBrandAndTypeIdCriteria(productParams))
        {
        }

        private static Expression<Func<Product, bool>> DefineBrandAndTypeIdCriteria(ProductSpecParams productParams)
        {
            // This method defines the Criteria in such a way as to define the type or brand Id, if they have a value.

            var brandId = productParams.BrandId;
            var typeId = productParams.TypeId;

            return x =>
            (
                ((!brandId.HasValue || x.ProductBrandId == brandId) && (!typeId.HasValue || x.ProductTypeId == typeId))
            );
        }
    }
}
