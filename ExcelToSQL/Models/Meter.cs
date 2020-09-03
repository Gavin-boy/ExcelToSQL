using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 仪表
    /// </summary>
    [Table(Name = "BD_Meter")]
    public class Meter
    {
        /// <summary>
        /// 仪表编号
        /// </summary>
        [JsonProperty(Order = 0)]
        [Column(IsIdentity = true)]
        public virtual int ID { get; set; }

        /// <summary>
        /// 表号（用作主键关联，第一次添加后就不可更改）
        /// </summary>
        [JsonProperty(Order = 1)]
        [Column(IsPrimary = true, DbType = DbTypeConsts.Varchar, StringLength = 30)]
        public string SN { get; set; }

        /// <summary>
        /// 表号（真实表号，可更改，展示时展示此值，如果未发生换表之类的情况，跟 SN 保持一致）
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 30)]
        public string SN2 { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [JsonProperty(Order = 2)]
        [Column(StringLength = 30)]
        public string Name { get; set; }

        /// <summary>
        /// 仪表类型编号
        /// </summary>
        [JsonProperty(Order = 3)]
        [Column(IsNullable = false)]
        public virtual int MeterTypeID { get; set; }

        /// <summary>
        /// 所属能源类型代码
        /// <para>插入时从仪表类型获取，无需传入</para>
        /// </summary>
        [JsonIgnore]
        [Column(IsNullable = false)]
        public virtual int EnergyTypeCode { get; set; }

        /// <summary>
        /// 所属网关编号
        /// </summary>
        [JsonProperty(Order = 5)]
        public virtual int? GatewayID { get; set; }

        /// <summary>
        /// 所属支路编号
        /// </summary>
        [JsonProperty(Order = 6)]
        public virtual int BranchID { get; set; }

        /// <summary>
        /// IP
        /// <para>当有网关时，应和网关 IP 保持一致</para>
        /// <para>但网关是可以没有的，所以这里必填</para>
        /// </summary>
        [JsonProperty(Order = 7)]
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 15, IsNullable = false)]
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        [JsonProperty(Order = 8)]
        [Column(IsNullable = false)]
        public int Port { get; set; }

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
    /// 仪表
    /// </summary>
    [Table(Name = "BD_Meter", DisableSyncStructure = true)]
    public class VM_Meter : Meter
    {
        /// <summary>
        /// 这个属性仅在插入测试数据时用
        /// </summary>
        [JsonIgnore]
        [Column(IsIgnore = true)]
        public int Magnification { get; set; } = 1;

        /// <summary>
        /// 仪表类型编号
        /// </summary>
        [JsonIgnore]
        public override int MeterTypeID { get; set; }

        /// <summary>
        /// 仪表类型
        /// </summary>
        [JsonProperty(Order = 3)]
        [Navigate(nameof(MeterTypeID))]
        public MeterType MeterType { get; set; }

        [JsonIgnore]
        public override int EnergyTypeCode { get; set; }

        /// <summary>
        /// 能源类型
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(EnergyTypeCode))]
        public EnergyType EnergyType { get; set; }

        [JsonIgnore]
        public override int? GatewayID { get; set; }

        /// <summary>
        /// 所属网关
        /// </summary>
        [JsonProperty(Order = 5)]
        [Navigate(nameof(GatewayID))]
        public Gateway Gateway { get; set; }

        [JsonIgnore]
        public override int BranchID { get; set; }

        /// <summary>
        /// 所属支路
        /// </summary>
        [JsonProperty(Order = 6)]
        [Navigate(nameof(BranchID))]
        public Branch Branch { get; set; }

        [JsonIgnore]
        public override int ID { get; set; }

        /// <summary>
        /// 仪表状态
        /// </summary>
        [JsonProperty(Order = 7)]
        [Navigate(nameof(ID))]
        public MeterStatus Meterstatus { get; set; }
    }
}