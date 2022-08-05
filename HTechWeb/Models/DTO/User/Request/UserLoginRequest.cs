using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.User.Request
{
    public class UserLoginRequest
    {
        public string UserNameOrEmailAddress { get; set; }
        public string Password { get; set; }
    }
}