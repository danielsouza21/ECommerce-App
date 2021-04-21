using System;
using API.Core.Entities;
using API.WebUI.DTOs;
using AutoMapper;
using Microsoft.Extensions.Configuration;

namespace API.WebUI.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductToReturnDto, string>
    {
        private const string API_URL_FIELD = "ApiUrl";
        private const string BAR_STRING = "/";
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            var ApiUrl = _configuration[API_URL_FIELD] ?? String.Empty;

            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return String.Concat(ApiUrl, BAR_STRING, source.PictureUrl);
            }

            return null;
        }
    }
}
