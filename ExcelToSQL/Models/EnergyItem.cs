using FreeSql.DataAnnotations;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 分项
    /// </summary>
    [Table(Name = "D_EnergyItem")]
    public class EnergyItem
    {
        /// <summary>
        /// 能源类型代码
        /// </summary>
        [Column(IsNullable = false)]
        public int EnergyTypeCode { get; set; }

        /// <summary>
        /// 能源类型
        /// </summary>
        [Navigate(nameof(EnergyTypeCode))]
        public EnergyType EnergyType { get; set; }

        /// <summary>
        /// 分项代码
        /// </summary>
        [Column(IsPrimary = true, DbType = DbTypeConsts.Varchar, StringLength = 10)]
        public string Code { get; set; }

        /// <summary>
        /// 分项名称
        /// </summary>
        [Column(StringLength = 10, IsNullable = false)]
        public string Name { get; set; }
    }
}