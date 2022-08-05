using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Shop.Response
{
    public class ShopGetResponse
    {
        public Result result { get; set; }
        public class Result
        {
            public string shopId { get; set; }
            public string userId { get; set; }
            public string shopName { get; set; }
            public string userName { get; set; }
            public string status { get; set; }
            public string shopPhone { get; set; }
            public string shopAddress { get; set; }
            public string shopEmail { get; set; }
            public List<Product> products { get; set; }
            public class Product
            {
                public string productId { get; set; }
                public string categoryName { get; set; }
                public string productName { get; set; }
                public string image { get; set; }
                public decimal price { get; set; }
            }

        }
    }
}