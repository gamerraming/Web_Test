using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.User.Response
{
    public class UserLoginResponse
    {
        public Result result { get; set; }
        public class Result
        {
            public string userId { get; set; }
            public string accessToken { get; set; }
        }
    }
}