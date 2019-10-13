using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal.Models
{   
    public class Login 
    {
        public class LoginRequest //: RequestBase
        {
            public string EmailId { get; set; }
            public string UserPwd { get; set; }
        }

        public class LoginResponse : ResponseBase
        {
            public string IsFirstLogin { get; set; }
            public string ProfilePicPath { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailId { get; set; }
            public string MobileNo { get; set; }
            public string Position { get; set; }
        }
    }


}