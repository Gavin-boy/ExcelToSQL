using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 建筑基础信息
    /// </summary>
    [Table(Name = "BD_Build", DisableSyncStructure = true)]
    public class BaseBuild
    {
        /// <summary>
        /// 建筑编号
        /// </summary>
        [JsonProperty(Order = 0)]
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 建筑代码
        /// <para>第 1-6  位数编码为 建筑所在地的行政区划代码</para>
        /// <para>第 7    位数编码为 建筑类别编码</para>
        /// <para>第 8-10 位数编码为 建筑识别编码</para>
        /// </summary>
        [JsonProperty(Order = 1)]
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 10, IsNullable = false)]
        public string Code { get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        [JsonProperty(Order = 2)]
        [Column(StringLength = 24, IsNullable = false)]
        public string Name { get; set; }
    }

    /// <summary>
    /// 建筑
    /// </summary>
    [Table(Name = "BD_Build", DisableSyncStructure = true)]
    public class VM_Build_CU : BaseBuild
    {
        /// <summary>
        /// 所属项目编号
        /// <para>因为建筑群里已经包含了 PID，所以这里是个冗余的</para>
        /// </summary>
        [JsonIgnore]
        [Column(IsNullable = false, CanUpdate = false)]
        public int PID { get; set; }

        /// <summary>
        /// 所属建筑群编号
        /// </summary>
        [JsonProperty(Order = 3)]
        [Column(IsNullable = false)]
        public virtual int BuildGroupID { get; set; }

        /// <summary>
        /// 数据中心编号
        /// </summary>
        [JsonProperty(Order = 4)]
        [Column(IsNullable = false)]
        public virtual int DataCenterID { get; set; }

        /// <summary>
        /// 建筑别名
        /// <para>一般用建筑名称的声母，可降低建筑敏感性</para>
        /// </summary>
        [JsonProperty(Order = 5)]
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 24, IsNullable = false)]
        public string AliasName { get; set; }

        /// <summary>
        /// 建筑业主
        /// </summary>
        [JsonProperty(Order = 6)]
        [Column(StringLength = 40, IsNullable = false)]
        public string Owner { get; set; }

        /// <summary>
        /// 建筑地址
        /// </summary>
        [JsonProperty(Order = 7)]
        [Column(StringLength = 40, IsNullable = false)]
        public string Address { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        [JsonProperty(Order = 8)]
        [Column(DbType = "decimal(7, 4)")]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        [JsonProperty(Order = 9)]
        [Column(DbType = "decimal(7, 4)")]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// 建筑年份
        /// </summary>
        [JsonProperty(Order = 10)]
        [Column(IsNullable = false)]
        public int BuildYear { get; set; }

        /// <summary>
        /// 地上建筑层数
        /// </summary>
        [JsonProperty(Order = 11)]
        [Column(IsNullable = false)]
        public int UpFloor { get; set; }

        /// <summary>
        /// 地下建筑层数
        /// </summary>
        [JsonProperty(Order = 12)]
        [Column(IsNullable = false)]
        public int DownFloor { get; set; }

        /// <summary>
        /// 总面积
        /// </summary>
        [JsonProperty(Order = 13)]
        [Column(DbType = "decimal(12, 4)", IsNullable = false)]
        public decimal TotalArea { get; set; }

        /// <summary>
        /// 空调面积
        /// </summary>
        [JsonProperty(Order = 14)]
        [Column(DbType = "decimal(12, 4)", IsNullable = false)]
        public decimal AirArea { get; set; }

        /// <summary>
        /// 采暖面积
        /// </summary>
        [JsonProperty(Order = 15)]
        [Column(DbType = "decimal(12, 4)", IsNullable = false)]
        public decimal HeatArea { get; set; }

        /// <summary>
        /// 体积系数
        /// </summary>
        [JsonProperty(Order = 16)]
        [Column(DbType = "decimal(12, 4)")]
        public decimal? BodyCoef { get; set; }

        /// <summary>
        /// 建筑功能编号
        /// </summary>
        [JsonProperty(Order = 17)]
        [Column(IsNullable = false)]
        public virtual int BuildFunctionID { get; set; }

        /// <summary>
        /// 建筑结构形式编号
        /// </summary>
        [JsonProperty(Order = 18)]
        [Column(IsNullable = false)]
        public virtual int BuildStructureID { get; set; }

        /// <summary>
        /// 空调系统形式编号
        /// </summary>
        [JsonProperty(Order = 19)]
        [Column(IsNullable = false)]
        public virtual int AirTypeID { get; set; }

        /// <summary>
        /// 采暖形式编号
        /// </summary>
        [JsonProperty(Order = 20)]
        [Column(IsNullable = false)]
        public virtual int HeatTypeID { get; set; }

        /// <summary>
        /// 建筑外墙材料编号
        /// </summary>
        [JsonProperty(Order = 21)]
        [Column(IsNullable = false)]
        public virtual int WallMaterialTypeID { get; set; }

        /// <summary>
        /// 建筑外墙保温形式编号
        /// </summary>
        [JsonProperty(Order = 22)]
        [Column(IsNullable = false)]
        public virtual int WallWarmTypeID { get; set; }

        /// <summary>
        /// 建筑外窗类型编号
        /// </summary>
        [JsonProperty(Order = 23)]
        [Column(IsNullable = false)]
        public virtual int WallWindowsTypeID { get; set; }

        /// <summary>
        /// 建筑玻璃类型编号
        /// </summary>
        [JsonProperty(Order = 24)]
        [Column(IsNullable = false)]
        public virtual int GlassTypeID { get; set; }

        /// <summary>
        /// 窗框材料类型编号
        /// </summary>
        [JsonProperty(Order = 25)]
        [Column(IsNullable = false)]
        public virtual int WinFrameMaterialID { get; set; }

        /// <summary>
        /// 建筑检测状态
        /// </summary>
        [JsonProperty(Order = 26)]
        [Column(IsNullable = false)]
        public bool MonitorState { get; set; }

        /// <summary>
        /// 是否标杆建筑
        /// </summary>
        [JsonProperty(Order = 27)]
        [Column(IsNullable = false)]
        public bool IsStandard { get; set; }

        /// <summary>
        /// 监测方案设计单位
        /// </summary>
        [JsonProperty(Order = 28)]
        [Column(StringLength = 32, IsNullable = false)]
        public string DesignCompany { get; set; }

        /// <summary>
        /// 监测工程实施单位
        /// </summary>
        [JsonProperty(Order = 29)]
        [Column(StringLength = 32, IsNullable = false)]
        public string WorkCompany { get; set; }

        /// <summary>
        /// 监测工程验收日期
        /// </summary>
        [JsonProperty(Order = 30)]
        public DateTime AcceptDate { get; set; }

        /// <summary>
        /// 开始监测日期
        /// </summary>
        [JsonProperty(Order = 31)]
        public DateTime MonitorDate { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint, IsNullable = false, CanUpdate = false)]
        public int State { get; set; } = StateConsts.Normal;
    }

    /// <summary>
    /// 建筑
    /// </summary>
    [Table(Name = "BD_Build")]
    public class Build : VM_Build_CU
    {
        /// <summary>
        /// 操作员
        /// </summary>
        [JsonProperty(Order = 32)]
        [Column(Name = "Operator", DbType = DbTypeConsts.Varchar, StringLength = 20, IsNullable = false)]
        public string OperatorUserName { get; set; }

        /// <summary>
        /// 录入时间
        /// </summary>
        [JsonProperty(Order = 33)]
        [Column(ServerTime = DateTimeKind.Local, CanUpdate = false)]
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 建筑
    /// </summary>
    [Table(Name = "BD_Build", DisableSyncStructure = true)]
    public class VM_Build : Build
    {
        /// <summary>
        /// 所属建筑群编号
        /// </summary>
        [JsonIgnore]
        public override int BuildGroupID { get; set; }

        /// <summary>
        /// 所属建筑群
        /// </summary>
        [JsonProperty(Order = 3)]
        [Navigate(nameof(BuildGroupID))]
        public BuildGroup Group { get; set; }

        /// <summary>
        /// 数据中心编号
        /// </summary>
        [JsonIgnore]
        public override int DataCenterID { get; set; }

        /// <summary>
        /// 数据中心
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(DataCenterID))]
        public DataCenter DataCenter { get; set; }

        /// <summary>
        /// 建筑功能代码
        /// </summary>
        [JsonIgnore]
        public override int BuildFunctionID { get; set; }

        /// <summary>
        /// 建筑功能
        /// </summary>
        [JsonProperty(Order = 17)]
        [Navigate(nameof(BuildFunctionID))]
        public BuildDictionary BuildFunction { get; set; }

        /// <summary>
        /// 建筑结构形式代码
        /// </summary>
        [JsonIgnore]
        public override int BuildStructureID { get; set; }

        /// <summary>
        /// 建筑结构形式
        /// </summary>
        [JsonProperty(Order = 18)]
        [Navigate(nameof(BuildStructureID))]
        public BuildDictionary BuildStructure { get; set; }

        /// <summary>
        /// 空调系统形式代码
        /// </summary>
        [JsonIgnore]
        public override int AirTypeID { get; set; }

        /// <summary>
        /// 空调系统形式
        /// </summary>
        [JsonProperty(Order = 19)]
        [Navigate(nameof(AirTypeID))]
        public BuildDictionary AirType { get; set; }

        /// <summary>
        /// 采暖形式代码
        /// </summary>
        [JsonIgnore]
        public override int HeatTypeID { get; set; }

        /// <summary>
        /// 采暖形式
        /// </summary>
        [JsonProperty(Order = 20)]
        [Navigate(nameof(HeatTypeID))]
        public BuildDictionary HeatType { get; set; }

        /// <summary>
        /// 建筑外墙材料代码
        /// </summary>
        [JsonIgnore]
        public override int WallMaterialTypeID { get; set; }

        /// <summary>
        /// 建筑外墙材料
        /// </summary>
        [JsonProperty(Order = 21)]
        [Navigate(nameof(WallMaterialTypeID))]
        public BuildDictionary WallMaterialType { get; set; }

        /// <summary>
        /// 建筑外墙保温形式代码
        /// </summary>
        [JsonIgnore]
        public override int WallWarmTypeID { get; set; }

        /// <summary>
        /// 建筑外墙保温形式
        /// </summary>
        [JsonProperty(Order = 22)]
        [Navigate(nameof(WallWarmTypeID))]
        public BuildDictionary WallWarmType { get; set; }

        /// <summary>
        /// 建筑外窗类型代码
        /// </summary>
        [JsonIgnore]
        public override int WallWindowsTypeID { get; set; }

        /// <summary>
        /// 建筑外窗类型
        /// </summary>
        [JsonProperty(Order = 23)]
        [Navigate(nameof(WallWindowsTypeID))]
        public BuildDictionary WallWindowsType { get; set; }

        /// <summary>
        /// 建筑玻璃类型代码
        /// </summary>
        public override int GlassTypeID { get; set; }

        /// <summary>
        /// 建筑玻璃类型
        /// </summary>
        [JsonProperty(Order = 24)]
        [Navigate(nameof(GlassTypeID))]
        public BuildDictionary GlassType { get; set; }

        /// <summary>
        /// 窗框材料类型代码
        /// </summary>
        public override int WinFrameMaterialID { get; set; }

        /// <summary>
        /// 窗框材料类型
        /// </summary>
        [JsonProperty(Order = 25)]
        [Navigate(nameof(WinFrameMaterialID))]
        public BuildDictionary WinFrameMaterial { get; set; }
    }

}