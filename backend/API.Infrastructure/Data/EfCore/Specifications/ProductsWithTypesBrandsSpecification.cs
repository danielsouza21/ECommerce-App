using System;
using System.Linq.Expressions;
using API.Core.Entities;
using API.Core.Specifications;
using API.WebUI.Constants;

namespace API.Infrastructure.Data.EfCore.Specifications
{
    public class ProductsWithTypesBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesBrandsSpecification(ProductSpecParams productParams) 
            : base(DefineBrandTypeIdAndSearchCriteria(productParams))
        {
            //AddInclude: add new entities in base Includes prop
            //Method to do like this data access operation:
            //_context.Product.Include(x => x.ProductType).Include(x => x.ProductBrand).FirstOrDefault();

            var sort = productParams.Sort;
            var skip = productParams.PageSize * (productParams.PageIndex - 1);
            var take = productParams.PageSize;

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);

            ApplyPaging(skip, take);

            if (!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case DataConstants.ORDERBY_ASC_STRING_QUERY:
                        AddOrderBy(p => p.Price);
                        break;

                    case DataConstants.ORDERBY_DESC_STRING_QUERY:
                        AddOrderByDescending(p => p.Price);
                        break;

                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }

        public ProductsWithTypesBrandsSpecification(int id) : base(DefineIdCriteria(id))
        {
            //Criteria = "x => x.Id == id"

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
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

        private static Expression<Func<Product, bool>> DefineIdCriteria(int id)
        {
            return x => x.Id == id;
        }
        #endregion
    }
}
