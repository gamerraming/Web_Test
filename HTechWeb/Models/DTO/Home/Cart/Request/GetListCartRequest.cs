using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Cart.Request
{
    public class GetListCartRequest
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public string Keyword { get; set; }
        public int PageSize { get; set; } = 10;
        public int PageIndex { get; set; } = 1;
        public int Offset { get; set; } = 0;
    }
}