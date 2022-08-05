using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Voucher.Response
{
    public class VoucherResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
            public int totalPages { get; set; }
            public int totalCount { get; set; }
            public List<Voucher> data { get; set; }
        }

        public class Voucher
        {
            public string voucherId { get; set; }
            public string voucherName { get; set; }
            public decimal discount { get; set; }
            public DateTime? dateExpired { get; set; }
            public string status { get; set; }
            public string type { get; set; }
        }
    }
}