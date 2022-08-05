using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.User.Request
{
    public class UserRegisterRequest
    {
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string retypePassword { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string phoneNumber { get; set; }
    }
}