using FreeSql.DataAnnotations;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 能源类型
    /// </summary>
    [Table(Name = "D_EnergyType")]
    public class EnergyType
    {
        /// <summary>
        /// 能源类型代码
        /// </summary>
        [Column(IsPrimary = true)]
        public int Code { get; set; }

        /// <summary>
        /// 能源类型名称
        /// </summary>
        [Column(StringLength = 10, IsNullable = false)]
        public string Name { get; set; }

        public static bool operator ==(EnergyType left, EnergyType right)
            => left.Code == right.Code;

        public static bool operator !=(EnergyType left, EnergyType right)
            => left.Code != right.Code;
    }
}