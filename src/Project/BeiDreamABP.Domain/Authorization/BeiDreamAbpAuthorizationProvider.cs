using Abp.Authorization;
using Abp.Localization;
using Abp.MultiTenancy;

namespace BeiDreamAbp.Domain.Authorization
{
    /// <summary>
    /// 系统所有权限集合类
    /// </summary>
    public class BeiDreamAbpAuthorizationProvider : AuthorizationProvider
    {
        public override void SetPermissions(IPermissionDefinitionContext context)
        {
            //系统管理模块
            var pages = context.GetPermissionOrNull(PermissionNames.SystemsManagePages) ?? context.CreatePermission(PermissionNames.SystemsManagePages, L("SystemsManagePages"));

            //租赁方的权限集合添加,需配置为 multiTenancySides: MultiTenancySides.Host
            var tenants = pages.CreateChildPermission(PermissionNames.SystemsManagePages_Tenants, L("Tenants"), multiTenancySides: MultiTenancySides.Host);
            //权限管理菜单
            var administration = pages.CreateChildPermission(PermissionNames.SystemsManagePages_Administration, L("Administration"));
            //用户管理
            var users = administration.CreateChildPermission(PermissionNames.SystemsManagePages_Administration_Users, L("Users"));
            //角色管理
            var roles = administration.CreateChildPermission(PermissionNames.SystemsManagePages_Administration_Roles, L("Roles"));
        }

        private static ILocalizableString L(string name)
        {
            return new LocalizableString(name, BeiDreamAbpConsts.LocalizationSourceName);
        }
    }
}