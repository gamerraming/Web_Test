using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.Home.Cart.Request
{
    public class UpdateCartRequest
    {
        public string cartDetailId { get; set; }
        public int quantity { get; set; }
    }
}