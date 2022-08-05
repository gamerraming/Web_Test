using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Payment.Response
{
    public class PaymentGetResponse
    {
        public Result result { get; set; }
        public class Result
        {
            public string paymentId { get; set; }
            public string paymentName { get; set; }
            public string description { get; set; }

        }
    }
}