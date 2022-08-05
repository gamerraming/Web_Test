using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.User.Response
{
    public class UserProfileResponse
    {
        public Result result { get; set; }
        public class Result
        {
            public string profilePicture { get; set; }
            public string username { get; set; }
            public string name { get; set; }
            public string surname { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public int? gender { get; set; }
            public DateTime? birthDay { get; set; }
            public int? status { get; set; }
            public List<string> roles { get; set; }
        }
    }
}