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
        public const string Name = "Name";
        public const string UserName = "UserName";
        public const string UserPwd = "UserPwd";
        public const string PwdNo = "PwdNo";
        public const string CreatedBy = "CreatedBy";
        public const string CreatedOn = "CreatedOn";
        public const string ModifyBy = "ModifyBy";
        public const string ModifyOn = "ModifyOn";
        public const string DeletedBy = "DeletedBy";
        public const string DeletedOn = "DeletedOn";
        public const string IsDeleted = "IsDeleted";
        public const string RoleId = "RoleId";
        public const string LastName = "LastName";
        public const string RoleName = "RoleName";
        public const string EmailId = "RoleName";
        public const string MobileNo = "RoleName";
    }

    public class UserBO
    {
        public Int64 Id { get; set; }
        public Int64 UserId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string UserPwd { get; set; }
        public string PwdNo { get; set; }
        public Int64 CreatedBy { get; set; }
        public string CreatedOn { get; set; }
        public Int64 ModifyBy { get; set; }
        public string ModifyOn { get; set; }
        public Int64 DeletedBy { get; set; }
        public string DeletedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string UserEmpId { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; }

    }
}