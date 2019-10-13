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
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public string ProfilePicPath { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string UserPwd { get; set; }
        public string MobileNo { get; set; }
        public string Position { get; set; }
        public Int64 CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public Int64 ModifiedBy { get; set; }
        public string ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        public Int64 DeletedBy { get; set; }
        public string DeletedOn { get; set; }   
    }
}