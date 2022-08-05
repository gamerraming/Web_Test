using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Order.Response
{
    public class OrderGetResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public string orderId { get; set; }
            public string shopId { get; set; }
            public string shipmentName { get; set; }
            public string shopName { get; set; }
            public string paymentName { get; set; }
            public string voucherName { get; set; }
            public string deliveryName { get; set; }
            public string deliveryAddress { get; set; }
            public string deliveryPhone { get; set; }
            public decimal shipmentPrice { get; set; }
            public string status { get; set; }
            public decimal totalPrice { get; set; }
            public List<OrderDetail> orderDetails { get; set; }
        }

        public class OrderDetail
        {
            public string orderDetailId { get; set; }
            public string productVariationId { get; set; }
            public string productId { get; set; }
            public string productVariationName { get; set; }
            public string productName { get; set; }
            public decimal price { get; set; }
            public int quantity { get; set; }
        }
    }
}