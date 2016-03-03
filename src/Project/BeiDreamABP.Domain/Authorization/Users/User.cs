using System;
using Abp.Authorization.Users;
using Abp.Extensions;
using BeiDreamAbp.Domain.MultiTenancy;
using Microsoft.AspNet.Identity;

namespace BeiDreamAbp.Domain.Authorization.Users
{
    public class User : AbpUser<Tenant, User>
    {
        //public const string DefaultPassword = "123qwe";

        public const int MinPlainPasswordLength = 6;
        /// <summary>
        /// 用户头像ID
        /// </summary>
        public virtual Guid? ProfilePictureId { get; set; }
        /// <summary>
        /// 下次登录是否修改密码
        /// </summary>
        public virtual bool ShouldChangePasswordOnNextLogin { get; set; }

        public static string CreateRandomPassword()
        {
            return Guid.NewGuid().ToString("N").Truncate(16);
        }

        public static User CreateTenantAdminUser(int tenantId, string emailAddress, string password)
        {
            return new User
            {
                TenantId = tenantId,
                UserName = AdminUserName,
                Name = AdminUserName,
                Surname = AdminUserName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };
        }
    }
}