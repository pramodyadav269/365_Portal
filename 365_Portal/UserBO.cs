using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace _365_Portal
{

    public class UserClaim
    {
        public const string CompId = "CompId";
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
        public const string IsFirstPasswordNotChanged = "IsFirstPasswordNotChanged";
        //public const string PasswordHash = "PasswordHash";
        //public const string PasswordSalt = "PasswordSalt";
    }

    public class UserBO
    {
        public string UserID { get; set; }
        public string UserRole { get; set; }
        public string UserKey { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailID { get; set; }
        public string MobileNum { get; set; }
        public string Position { get; set; }
        public Int32 CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public Int32 ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Int32 DeletedBy { get; set; }
        public string DeletedOn { get; set; }
        public string DeviceType { get; set; }
        public string DeviceDetails { get; set; }
        public string IP_Address { get; set; }
        public string Token { get; set; }
        public string ActivityType { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; } 
        public bool EmailNotification { get; set; }
        public bool PushNotification { get; set; }
        public string ThemeColor { get; set; }
        public string ThemeColor2 { get; set; }
        public string ThemeColor3 { get; set; }
        public string ThemeColor4 { get; set; }
        public string Logo { get; set; }
        public string GroupId { get; set; }
        public string GroupName { get; set; }
        public Int32 CompId { get; set; }
        public string RoleID { get; set; }
        public string Role { get; set; }                
        public string ProfilePicFileID { get; set; }
        public string CompanyProfilePicFileID { get; set; }
        public string ProfilePicFile { get; set; }
        public string CompanyProfilePicFile { get; set; }
        public string IsFirstLogin { get; set; }
        public string IsFirstPasswordNotChanged { get; set; }
        public string IsChangingPassword { get; set; }
        public string ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        public string CompName { get; set; }
        public string NoOfEmployees { get; set; }
        public string Country { get; set; }
        public string IsActive { get; set; }
    }
}