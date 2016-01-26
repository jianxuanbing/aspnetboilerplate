using System;

namespace Abp.MultiTenancy
{
    /// <summary>
    /// 多租户系统中的租方和赁方的标志
    /// </summary>
    [Flags]
    public enum MultiTenancySides
    {
        /// <summary>
        /// 代表租户方
        /// </summary>
        Tenant = 1,
        
        /// <summary>
        /// 代表赁方(所有租户的拥有者，也就是超级管理员方)
        /// </summary>
        Host = 2
    }
}