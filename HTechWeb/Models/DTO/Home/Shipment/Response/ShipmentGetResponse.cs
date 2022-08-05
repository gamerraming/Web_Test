using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Shipment.Response
{
    public class ShipmentGetResponse
    {
        public Result result { get; set; }
        public class Result
        {
            public string shipmentId { get; set; }
            public string shipmentName { get; set; }
            public string description { get; set; }

        }
    }
}