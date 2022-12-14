using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HTechWeb.Models.DTO.User.Request
{
    public class UserUpdateRequest
    {
        public string profilePicture { get; set; }
        public string name { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public int gender { get; set; }
        public DateTime? birthDay { get; set; }
    }
}