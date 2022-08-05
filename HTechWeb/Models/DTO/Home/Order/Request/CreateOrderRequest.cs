using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Order.Request
{
    public class CreateOrderRequest
    {
        public string shipmentId { get; set; }
        public string paymentId { get; set; }
        public string voucherId { get; set; }
        public string deliveryName { get; set; }
        public string deliveryAddress { get; set; }
        public string deliveryPhone { get; set; }
        public int shipmentPrice { get; set; }
        public int status { get; set; }
        public List<Variant> productVariants { get; set; }
        public class Variant
        {
            public string productVariationId { get; set; }
            public int quantity { get; set; }
        }
    }
}