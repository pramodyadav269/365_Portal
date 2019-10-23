using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal
{

    public class UserClaim
    {
        public const string Id = "Id";
        public const string UserId = "UserId";
        public const string ProfilePicPath = "ProfilePicPath";
        public const string FirstName = "FirstName";
        public const string LastName = "LastName";
        public const string EmailId = "EmailId";
        public const string UserPwd = "UserPwd";
        public const string MobileNo = "MobileNo";
        public const string Position = "Position";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedOn = "CreatedOn";
        public const string ModifiedBy = "ModifiedBy";
        public const string ModifiedOn = "ModifiedOn";
        public const string IsDeleted = "IsDeleted";
        public const string DeletedBy = "DeletedBy";
        public const string DeletedOn = "DeletedOn";        
    }

    public class UserBO
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserKey { get; set; }
        public string ProfilePicPath { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string UserPwd { get; set; }
        public string MobileNo { get; set; }
        public string Position { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public int ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int DeletedBy { get; set; }
        public string DeletedOn { get; set; }
        public string DeviceType { get; set; }
        public string DeviceDetails { get; set; }
        public string IP_Addess { get; set; }
        public string Token { get; set; }
        public string ActivityType { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordSalt { get; set; } 
        public bool EmailNotification { get; set; }
        public bool PushNotification { get; set; }
        public string ThemeColor { get; set; }
        public string Logo { get; set; }
        public string ProfilePic { get; set; }
        public string GroupId { get; set; }
        public int CompId { get; set; }
        public string RoleId { get; set; }
        public string ErrorCode  { get; set; }
        public string ErrorMessage { get; set; }
    }
}