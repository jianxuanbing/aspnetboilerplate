namespace BeiDreamAbp.Domain.Authorization.Roles
{
    /// <summary>
    /// 默认静态的角色名称集合
    /// </summary>
    public static class StaticRoleNames
    {
        /// <summary>
        /// 租赁方
        /// </summary>
        public static class Host
        {
            /// <summary>
            /// 租赁方Admin角色
            /// </summary>
            public const string Admin = "Admin";
        }
        /// <summary>
        /// 租户方
        /// </summary>
        public static class Tenants
        {
            /// <summary>
            /// 租户方Admin角色
            /// </summary>
            public const string Admin = "Admin";
            /// <summary>
            /// 租户方User角色
            /// </summary>
            public const string User = "User";
        }
    }
}