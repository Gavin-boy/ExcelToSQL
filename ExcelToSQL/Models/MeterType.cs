using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 仪表类型
    /// </summary>
    [Table(Name = "BD_MeterType")]
    public class MeterType
    {
        /// <summary>
        /// 仪表类型编号，自增
        /// </summary>
        [JsonProperty(Order = 0)]
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 仪表型号
        /// </summary>
        [JsonProperty(Order = 1)]
        [Column(DbType = "varchar", StringLength = 20, IsNullable = false)]
        public string Model { get; set; }

        /// <summary>
        /// 仪表类型名称
        /// </summary>
        [JsonProperty(Order = 2)]
        [Column(StringLength = 20)]
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
        /// 仪表协议类型编号
        /// </summary>
        [JsonProperty(Order = 5)]
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 20, IsNullable = false)]
        public virtual int ProtocolTypeID { get; set; }

        /// <summary>
        /// 具体的 modbus 协议名称
        /// </summary>
        [JsonProperty(Order = 6)]
        [Column(StringLength = 30)]
        public string ModbusProtocolName { get; set; }

        /// <summary>
        /// 读取参数
        /// </summary>
        [JsonProperty(Order = 7)]
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 255)]
        public string Parameters { get; set; }

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

    [Table(Name = "BD_MeterType", DisableSyncStructure = true)]
    public class VM_MeterType : MeterType
    {
        [JsonIgnore]
        public override int EnergyTypeCode { get; set; }

        /// <summary>
        /// 能源类型
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(EnergyTypeCode))]
        public EnergyType EnergyType { get; set; }

        [JsonIgnore]
        public override int ProtocolTypeID { get; set; }

        /// <summary>
        /// 仪表协议类型
        /// </summary>
        [JsonProperty(Order = 5)]
        [Navigate(nameof(ProtocolTypeID))]
        public ProtocolType ProtocolType { get; set; }
    }
}