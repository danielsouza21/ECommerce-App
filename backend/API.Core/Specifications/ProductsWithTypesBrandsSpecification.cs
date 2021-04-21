using System;
using System.Linq.Expressions;
using API.Core.Entities;

namespace API.Core.Specifications
{
    public class ProductsWithTypesBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesBrandsSpecification()
        {
            //Add new entities in base Includes prop
            //Method to do like :
            //_context.Product.Include(x => x.ProductType).Include(x => x.ProductBrand).FirstOrDefault();

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

        public ProductsWithTypesBrandsSpecification(int id) : base(x => x.Id == id)
        {
            //Criteria = "x => x.Id == id"

            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }
    }
}
