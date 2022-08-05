using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Product.Response
{
    public class CategoryResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
            public int totalPages { get; set; }
            public int totalCount { get; set; }
            public List<Category> data { get; set; }
        }

        public class Category
        {
            public string categoryId { get; set; }
            public string categoryName { get; set; }
        }
    }
}