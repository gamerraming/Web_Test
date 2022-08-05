using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Shop.Request
{
    public class GetShopRequest
    {
        public string shopId { get; set; }
        public string Keyword { get; set; }
        public string SortBy { get; set; }
        public string SortOrder { get; set; }
    }
}