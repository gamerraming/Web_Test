using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Cart.Response
{
    public class CartResponse
    {
        public List<Cart> result { get; set; }

        public class Cart
        {
            public string cartDetailId { get; set; }
            public string productVariantId { get; set; }
            public string productId { get; set; }
            public string productImage { get; set; }
            public string productName { get; set; }
            public string productVariantName { get; set; }
            public string shopName { get; set; }
            public decimal price { get; set; }
            public int quantity { get; set; }
        }
    }
}