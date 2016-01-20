using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace BeiDreamAbp.Domain.User
{
    /// <summary>
    /// 用户信息实体
    /// </summary>
    [Table("BeiDreamAbp.Users")]  //生成到数据库表的表名称
    public class User : Entity<long>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string Name { get; set; }
    }
}