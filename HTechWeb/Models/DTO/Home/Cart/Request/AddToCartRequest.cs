using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Cart.Request
{
    public class AddToCartRequest
    {
        public string productVariationId { get; set; }
        public int quantity { get; set; }
    }
}