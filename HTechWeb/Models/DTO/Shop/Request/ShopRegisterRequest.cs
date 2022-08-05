using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Shop.Request
{
    public class ShopRegisterRequest
    {
        public string shopName { get; set; }
        public string shopPhone { get; set; }
        public string shopAddress { get; set; }
        public string shopEmail { get; set; }
    }
}