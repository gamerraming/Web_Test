﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Product.Response
{
    public class ProductResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
            public int totalPages { get; set; }
            public int totalCount { get; set; }
            public List<Product> data { get; set; }
        }

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