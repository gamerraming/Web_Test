using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Shipment.Response
{
    public class ShipmentResponse
    {
        public Result result { get; set; }

        public class Result
        {
            public int pageIndex { get; set; }
            public int pageSize { get; set; }
            public int totalPages { get; set; }
            public int totalCount { get; set; }
            public List<Shipment> data { get; set; }
        }

        public class Shipment
        {
            public string shipmentId { get; set; }
            public string shipmentName { get; set; }
        }
    }
}