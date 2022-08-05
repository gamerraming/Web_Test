using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Order.Request
{
    public class GetListOrderRequest
    {
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
        public string Keyword { get; set; }
        public string FilterByStatus { get; set; }
        public int PageSize { get; set; } = 5;
        public int PageIndex { get; set; } = 1;
        public int Offset { get; set; } = 0;
    }
}