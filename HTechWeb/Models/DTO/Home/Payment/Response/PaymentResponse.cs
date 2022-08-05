using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Payment.Response
{
    public class PaymentResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
            public int totalPages { get; set; }
            public int totalCount { get; set; }
            public List<Payment> data { get; set; }
        }

        public class Payment
        {
            public string paymentId { get; set; }
            public string paymentName { get; set; }
        }
    }
}