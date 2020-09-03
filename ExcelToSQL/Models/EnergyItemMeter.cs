using FreeSql.DataAnnotations;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 分项-仪表
    /// </summary>
    [Table(Name = "B_EnergyItemMeter")]
    public class EnergyItemMeter
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 建筑编号
        /// </summary>
        [Column(IsNullable = false)]
        public int BuildID { get; set; }

        /// <summary>
        /// 分项代码
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 10, IsNullable = false)]
        public string EnergyItemCode { get; set; }

        /// <summary>
        /// 表号
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 30, IsNullable = false)]
        public string MeterSN { get; set; }

        /// <summary>
        /// 项目ID
        /// </summary>
        [Column(IsNullable = false)]
        public int PID { get; set; }
    }
}