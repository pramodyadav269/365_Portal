using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal.Code.BO
{
    public class UserProfile
    {
        public class UserProfileRequest
        {

        }

        public class UserProfileResponse
        {
            public string Role { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailID { get; set; }
            public string MobileNum { get; set; }
            public string Position { get; set; }
            public bool EmailNotification { get; set; }
            public bool PushNotification { get; set; }
            public string ProfilePicFile { get; set; }
            public string CompanyProfilePicFile { get; set; }
            public string ThemeColor { get; set; }
            public string ThemeColor2 { get; set; }
            public string ThemeColor3 { get; set; }
            public string ThemeColor4 { get; set; }
            public string GroupName { get; set; }
        }

        public class AdminUserProfileResponse
        {
            public string Role { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string EmailID { get; set; }
            public string MobileNum { get; set; }
            public string Position { get; set; }
            public bool EmailNotification { get; set; }
            public bool PushNotification { get; set; }
            public string ProfilePicFile { get; set; }
            public string CompanyProfilePicFile { get; set; }
            public string ThemeColor { get; set; }
            public string ThemeColor2 { get; set; }
            public string ThemeColor3 { get; set; }
            public string ThemeColor4 { get; set; }
            public string GroupName { get; set; }
        }
    }
}