using System;
using System.Linq.Expressions;
using API.Core.Entities;
using API.Core.Specifications;

namespace API.Infrastructure.Data.EfCore.Specifications
{
    public class ProductWithFiltersForCountSpecification : BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productParams)
            : base(DefineBrandTypeIdAndSearchCriteria(productParams))
        {
        }

        #region Private methods
        private static Expression<Func<Product, bool>> DefineBrandTypeIdAndSearchCriteria(ProductSpecParams productParams)
        {
            // This method defines the Criteria in such a way as to define the Query with type or brand Id, if they have a value.
            // Or define the search value in the query, if it have a value.

            var brandId = productParams.BrandId;
            var typeId = productParams.TypeId;
            var search = productParams.Search;

            return x =>
            (
                ((!brandId.HasValue || x.ProductBrandId == brandId) &&
                (!typeId.HasValue || x.ProductTypeId == typeId)) &&
                (string.IsNullOrEmpty(search) || x.Name.ToLower().Contains(search))
            );
        }
        #endregion
    }
}
