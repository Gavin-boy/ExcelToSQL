using FreeSql.DataAnnotations;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 项目-能源类型
    /// </summary>
    [Table(Name = "DL_ProjectEnergyType")]
    public class ProjectEnergyType
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [Column(IsNullable = false)]
        public int PID { get; set; }

        /// <summary>
        /// 能源类型代码
        /// </summary>
        [Column(IsNullable = false)]
        public int EnergyTypeCode { get; set; }
    }
}