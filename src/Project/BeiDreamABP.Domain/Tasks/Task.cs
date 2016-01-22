using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace BeiDreamAbp.Domain.Tasks
{
    /// <summary>
    /// 用户信息实体
    /// </summary>
    [Table("BeiDreamAbp.Tasks")]  //生成到数据库表的表名称
    public class Task : Entity<long>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        public virtual string Name { get; set; }
    }
}