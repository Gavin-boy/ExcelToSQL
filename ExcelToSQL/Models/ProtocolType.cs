using FreeSql.DataAnnotations;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 仪表协议类型
    /// </summary>
    [Table(Name = "D_ProtocolType")]
    public class ProtocolType
    {
        /// <summary>
        /// 仪表协议类型编号
        /// </summary>
        [Column(IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 仪表协议类型名称
        /// </summary>
        [Column(DbType = "varchar", StringLength = 20, IsNullable = false)]
        public string Name { get; set; }
    }
}