using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 支路
    /// </summary>
    [Table(Name = "BD_Branch")]
    public class Branch
    {
        /// <summary>
        /// 支路编号
        /// </summary>
        [JsonProperty(Order = 0)]
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 支路名称
        /// </summary>
        [JsonProperty(Order = 1)]
        [Column(StringLength = 30, IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 所属建筑编号
        /// </summary>
        [JsonProperty(Order = 2)]
        [Column(IsNullable = false)]
        public virtual int BuildID { get; set; }

        /// <summary>
        /// 所属能源类型编号
        /// </summary>
        [JsonProperty(Order = 3)]
        [Column(IsNullable = false)]
        public virtual int EnergyTypeCode { get; set; }

        /// <summary>
        /// 所属分项编号，可空
        /// <para>下级支路分项必须与上级支路相同</para>
        /// </summary>
        [JsonProperty(Order = 4)]
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 10)]
        public virtual string EnergyItemCode { get; set; }

        // 关于部门和区域应该放在表下面还是区域下面
        // 表没有上下级的概念，如果直接将表跟部门和区域关联，则依然还是要关联到支路来获取上下级关系
        // 所以部门和区域应直接与支路关联

        /// <summary>
        /// 所属部门编号，可空
        /// </summary>
        [JsonProperty(Order = 5)]
        public virtual int? DepartmentID { get; set; }

        /// <summary>
        /// 所属区域编号
        /// <para>对于一栋建筑的总支路，区域是可以为空的</para>
        /// </summary>
        [JsonProperty(Order = 6)]
        public virtual int? AreaID { get; set; }

        /// <summary>
        /// 上级支路编号
        /// </summary>
        [JsonProperty(Order = 7)]
        public virtual int? ParentID { get; set; }

        /// <summary>
        /// 层级
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint)]
        public int Level { get; set; }

        /// <summary>
        /// 所属项目编号
        /// </summary>
        [JsonIgnore]
        [Column(IsNullable = false, CanUpdate = false)]
        public int PID { get; set; }

        /// <summary>
        /// 是否叶子节点
        /// </summary>
        [JsonIgnore]
        [Column(IsNullable = false, CanUpdate = false)]
        public bool Leaf { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint, IsNullable = false, CanUpdate = false)]
        public int State { get; set; } = StateConsts.Normal;
    }

    /// <summary>
    /// 支路
    /// </summary>
    [Table(Name = "BD_Branch", DisableSyncStructure = true)]
    public class VM_Branch : Branch
    {
        [JsonIgnore]
        public override int BuildID { get; set; }

        /// <summary>
        /// 所属建筑
        /// </summary>
        [JsonProperty(Order = 2)]
        [Navigate(nameof(BuildID))]
        public BaseBuild Build { get; set; }

        [JsonIgnore]
        public override int EnergyTypeCode { get; set; }

        /// <summary>
        /// 所属能源类型
        /// </summary>
        [JsonProperty(Order = 3)]
        [Navigate(nameof(EnergyTypeCode))]
        public EnergyType EnergyType { get; set; }

        [JsonIgnore]
        public override string EnergyItemCode { get; set; }

        /// <summary>
        /// 所属分项
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(EnergyItemCode))]
        public EnergyItem EnergyItem { get; set; }

        [JsonIgnore]
        public override int? DepartmentID { get; set; }

        /// <summary>
        /// 部门
        /// </summary>
        [JsonProperty(Order = 5)]
        [Navigate(nameof(DepartmentID))]
        public Department Department { get; set; }

        [JsonIgnore]
        public override int? AreaID { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [JsonProperty(Order = 6)]
        [Navigate(nameof(AreaID))]
        public Area Area { get; set; }

        [JsonIgnore]
        public override int? ParentID { get; set; }

        /// <summary>
        /// 上级支路
        /// </summary>
        [JsonProperty(Order = 7)]
        [Navigate(nameof(ParentID))]
        public Branch Parent { get; set; }
    }

    /// <summary>
    /// 支路
    /// </summary>
    [Table(Name = "BD_Branch", DisableSyncStructure = true)]
    public class VM_Branch_Parent : Branch
    {
        [JsonIgnore]
        public override int EnergyTypeCode { get; set; }

        [JsonIgnore]
        public override string EnergyItemCode { get; set; }

        [JsonIgnore]
        public override int? DepartmentID { get; set; }

        [JsonIgnore]
        public override int? AreaID { get; set; }

        [JsonIgnore]
        public override int? ParentID { get; set; }

        /// <summary>
        /// 上级支路
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(ParentID))]
        public VM_Branch_Parent Parent { get; set; }
    }

    /// <summary>
    /// 支路
    /// </summary>
    [Table(Name = "BD_Branch", DisableSyncStructure = true)]
    public class VM_Branch_Child : Branch
    {
        [JsonIgnore]
        public override int EnergyTypeCode { get; set; }

        [JsonIgnore]
        public override string EnergyItemCode { get; set; }

        [JsonIgnore]
        public override int? DepartmentID { get; set; }

        [JsonIgnore]
        public override int? AreaID { get; set; }

        public override int? ParentID { get; set; }

        /// <summary>
        /// 下级支路
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(ParentID))]
        public List<VM_Branch_Child> Childs { get; set; }
    }
}