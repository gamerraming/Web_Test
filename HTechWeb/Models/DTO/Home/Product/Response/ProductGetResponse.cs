using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Product.Response
{
    public class ProductGetResponse
    {
        public Result result { get; set; }
        public class Result
        {
            public string productId { get; set; }
            public string shopId { get; set; }
            public string categoryId { get; set; }
            public string categoryName { get; set; }
            public string shopName { get; set; }
            public string productName { get; set; }
            public List<Image> productImages { get; set; }
            public List<Variation> productVariations { get; set; }
            public class Image
            {
                public string productImageId { get; set; }
                public string imagePath { get; set; }
            }
            public class Variation
            {
                public string productVariationId { get; set; }
                public string name { get; set; }
                public string description { get; set; }
                public decimal price { get; set; }
                public int quantity { get; set; }
            }
        }
    }
}