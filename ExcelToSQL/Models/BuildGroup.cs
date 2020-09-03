using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 建筑群
    /// </summary>
    [Table(Name = "BD_BuildGroup")]
    public class BuildGroup
    {
        /// <summary>
        /// 建筑群编号
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 建筑群代码
        /// <para>第 1-6  位编码为建筑所在地的行政区划代码</para>
        /// <para>第 7-10 位为流水号</para>
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 10, IsNullable = false)]
        public string Code { get; set; }

        /// <summary>
        /// 建筑群名称
        /// </summary>
        [Column(StringLength = 24, IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 所属项目编号
        /// </summary>
        [JsonIgnore]
        [Column(IsNullable = false, CanUpdate = false)]
        public int PID { get; set; }

        /// <summary>
        /// 省代码
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 6, IsNullable = false)]
        public virtual string ProvinceCode { get; set; }

        /// <summary>
        /// 市代码
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 6, IsNullable = false)]
        public virtual string CityCode { get; set; }

        /// <summary>
        /// 区代码
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 6, IsNullable = false)]
        public virtual string DistrictCode { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint, IsNullable = false, CanUpdate = false)]
        public int State { get; set; } = StateConsts.Normal;
    }

    /// <summary>
    /// 建筑群
    /// </summary>
    [Table(Name = "BD_BuildGroup", DisableSyncStructure = true)]
    public class VM_BuildGroup : BuildGroup
    {
        [JsonIgnore]
        public override string ProvinceCode { get; set; }

        [JsonIgnore]
        public override string CityCode { get; set; }

        [JsonIgnore]
        public override string DistrictCode { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(ProvinceCode))]
        public District Province { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        [JsonProperty(Order = 5)]
        [Navigate(nameof(CityCode))]
        public District City { get; set; }

        /// <summary>
        /// 区
        /// </summary>
        [JsonProperty(Order = 6)]
        [Navigate(nameof(DistrictCode))]
        public District District { get; set; }
    }
}