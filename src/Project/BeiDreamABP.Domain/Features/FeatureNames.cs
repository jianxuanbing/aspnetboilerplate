namespace BeiDreamAbp.Domain.Features
{
    /// <summary>
    /// 套餐的特征名称集合
    /// </summary>
    public class FeatureNames
    {
        /// <summary>
        /// 创建用户数量标签名称
        /// </summary>
        public const string CreateUsersNumericFeature = "CreateUsersNumericFeature";
        /// <summary>
        /// 允许是否使用导入Tasks的Excel数据
        /// </summary>
        public const string ExcelInputTasksBooleanFeature = "ExcelInputTasksBooleanFeature";
        /// <summary>
        /// 允许导入Tasks的Excel数据数量值
        /// </summary>
        public const string ExcelInputTasksNumericFeature = "ExcelInputTasksNumericFeature";
    }
}