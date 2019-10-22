using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal
{

    public class UserClaim
    {
        public const string UserID = "UserID";
        public const string RoleID = "RoleID";
        public const string Role = "Role";
        public const string IsFirstLogin = "IsFirstLogin";
        public const string ProfilePicFileID = "ProfilePicFileID";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string EmailID = "EmailID";
        public const string MobileNum = "MobileNum";
        public const string Position = "Position";
        //public const string PasswordHash = "PasswordHash";
        //public const string PasswordSalt = "PasswordSalt";
    }

    public class UserBO
    {
        public string UserID { get; set; }
        public string CompID { get; set; }
        public string RoleID { get; set; }
        public string Role { get; set; }
        public string IsFirstLogin { get; set; }
        public string ProfilePicFileID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string MobileNum { get; set; }
        public string Position { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string EmailNotification { get; set; }
        public string PushNotification { get; set; }
        public string ThemeColor { get; set; }
        public string Logo { get; set; }
        public string GroupId { get; set; }
        public string DeviceType { get; set; }
        public string DeviceDetails { get; set; }
        public string Token { get; set; }        
        public string IP_Address { get; set; }
        public string CreatedBy { get; set; }
        public string ActivityType { get; set; }        
    }
}