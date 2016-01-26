using Abp.MultiTenancy;
using Abp.Zero.Configuration;

namespace BeiDreamAbp.Domain.Authorization.Roles
{
    /// <summary>
    /// 角色管理配置
    /// </summary>
    public class AppRoleConfig
    {
        /// <summary>
        /// 给系统角色管理器添加租赁方(超级管理员)超级角色，以及添加租户方管理员角色
        /// </summary>
        /// <param name="roleManagementConfig">角色管理器配置类</param>
        public static void Configure(IRoleManagementConfig roleManagementConfig)
        {
            //Static host roles(默认的租赁方角色)

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Host.Admin,
                    MultiTenancySides.Host)
                );

            //Static tenant roles(默认的租户方角色)

            roleManagementConfig.StaticRoles.Add(
                new StaticRoleDefinition(
                    StaticRoleNames.Tenants.Admin,
                    MultiTenancySides.Tenant)
                );
        }
    }
}