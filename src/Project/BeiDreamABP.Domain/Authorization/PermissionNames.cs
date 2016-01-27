namespace BeiDreamAbp.Domain.Authorization
{
    /// <summary>
    /// 权限名称(权限标识)集合类
    /// </summary>
    public static class PermissionNames
    {
        //后台管理板块
        public const string SystemsManagePages = "SystemsManagePages";

        //租赁方独有的权限列表
        public const string SystemsManagePages_Tenants = "SystemsManagePages.Tenants";

        //租赁方和租户方通用的权限列表
        public const string SystemsManagePages_Administration = "SystemsManagePages.Administration";

        public const string SystemsManagePages_Administration_Users = "SystemsManagePages.Administration.Users";
        public const string SystemsManagePages_Administration_Roles = "SystemsManagePages.Administration.Roles";

        //系统管理页说明.登录即可访问
        //public const string SystemsManagePages_About = "SystemsManagePages.About";


        
    }
}