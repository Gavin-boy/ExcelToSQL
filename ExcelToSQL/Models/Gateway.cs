using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 网关
    /// </summary>
    [Table(Name = "BD_Gateway")]
    public class Gateway
    {
        /// <summary>
        /// 网关编号
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 网关名称
        /// </summary>
        [JsonProperty(Order = 2)]
        [Column(StringLength = 20, IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 生产厂商
        /// </summary>
        [JsonProperty(Order = 3)]
        [Column(StringLength = 30)]
        public string Manufacturer { get; set; }

        /// <summary>
        /// 能源类型代码
        /// </summary>
        [JsonProperty(Order = 4)]
        [Column(IsNullable = false)]
        public virtual int EnergyTypeCode { get; set; }

        /// <summary>
        /// 接入协议
        /// </summary>
        [Column(StringLength = 30)]
        public string AccessProtocol { get; set; }

        /// <summary>
        /// IP 地址
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 20)]
        public string IP { get; set; }

        /// <summary>
        /// 端口号（随意填，可以是 8001~8004）
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 30)]
        public string Port { get; set; }

        /// <summary>
        /// 所属项目编号
        /// </summary>
        [JsonIgnore]
        [Column(IsNullable = false, CanUpdate = false)]
        public int PID { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint, IsNullable = false, CanUpdate = false)]
        public int State { get; set; } = StateConsts.Normal;
    }

    /// <summary>
    /// 网关
    /// </summary>
    [Table(Name = "BD_Gateway", DisableSyncStructure = true)]
    public class VM_Gateway : Gateway
    {
        [JsonIgnore]
        public override int EnergyTypeCode { get; set; }

        /// <summary>
        /// 能源类型
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(EnergyTypeCode))]
        public EnergyType EnergyType { get; set; }
    }
}