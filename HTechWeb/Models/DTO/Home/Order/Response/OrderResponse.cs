using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Order.Response
{
    public class OrderResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
            public int totalPages { get; set; }
            public int totalCount { get; set; }
            public List<Order> data { get; set; }
        }

        public class Order
        {
            public string orderId { get; set; }
            public string status { get; set; }
            public decimal totalPrice { get; set; }
            public DateTime creationTime  { get; set; }
            public string shopName { get; set; }
        }
    }
}