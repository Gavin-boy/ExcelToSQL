using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    [Table(Name = "BD_Department")]
    public class Department
    {
        /// <summary>
        /// 部门编号，自增
        /// </summary>
        [JsonProperty(Order = 0)]
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [JsonProperty(Order = 1)]
        [Column(StringLength = 20, IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 所属项目编号
        /// </summary>
        [JsonIgnore]
        [Column(IsNullable = false, CanUpdate = false)]
        public int PID { get; set; }

        /// <summary>
        /// 所属建筑编号
        /// </summary>
        [JsonProperty(Order = 2)]
        [Column(IsNullable = false)]
        public virtual int BuildID { get; set; }

        /// <summary>
        /// 上级部门编号
        /// </summary>
        [JsonProperty(Order = 3)]
        public virtual int? ParentID { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint, IsNullable = false, CanUpdate = false)]
        public int State { get; set; } = StateConsts.Normal;

        /// <summary>
        /// 完整路径
        /// </summary>
        [JsonIgnore]
        [Column(StringLength = 255, IsNullable = false)]
        public string FullPath { get; set; }
    }

    /// <summary>
    /// 部门
    /// </summary>
    [Table(Name = "BD_Department", DisableSyncStructure = true)]
    public class VM_Department : Department
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
        public override int? ParentID { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        [JsonProperty(Order = 3)]
        [Navigate(nameof(ParentID))]
        public Department Parent { get; set; }
    }

    /// <summary>
    /// 部门
    /// </summary>
    [Table(Name = "BD_Department", DisableSyncStructure = true)]
    public class VM_Department_Parent : Department
    {
        [JsonIgnore]
        public override int BuildID { get; set; }

        [JsonIgnore]
        public override int? ParentID { get; set; }

        /// <summary>
        /// 上级部门
        /// </summary>
        [JsonProperty(Order = 3)]
        [Navigate(nameof(ParentID))]
        public VM_Department_Parent Parent { get; set; }
    }

    /// <summary>
    /// 部门
    /// </summary>
    [Table(Name = "BD_Department", DisableSyncStructure = true)]
    public class VM_Department_Child : Department
    {
        [JsonIgnore]
        public override int BuildID { get; set; }

        [JsonProperty(Order = 3)]
        public override int? ParentID { get; set; }

        /// <summary>
        /// 子部门
        /// </summary>
        [JsonProperty(Order = 4)]
        [Navigate(nameof(ParentID))]
        public List<VM_Department_Child> Childs { get; set; }
    }
}