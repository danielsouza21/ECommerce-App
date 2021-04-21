using API.Core.Entities;

namespace API.WebUI.DTOs
{
    public class ProductToReturnDto
    {
        //DTO: Data Transfer Object

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string ProductType { get; set; }
        public string ProductBrand { get; set; }

        public ProductToReturnDto()
        {
        }

        public ProductToReturnDto(Product product)
        {
            this.Id = product.Id;
            this.Name = product.Name;
            this.Description = product.Description;
            this.PictureUrl = product.PictureUrl;
            this.Price = product.Price;
            this.ProductBrand = product.ProductBrand.Name;
            this.ProductType = product.ProductType.Name;
        }
    }
}
