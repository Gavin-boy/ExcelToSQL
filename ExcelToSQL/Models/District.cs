using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 行政区域
    /// </summary>
    [Table(Name = "D_District")]
    public class District
    {
        /// <summary>
        /// 行政区域代码
        /// </summary>
        [Column(IsPrimary = true, DbType = DbTypeConsts.Varchar, StringLength = 6)]
        public string Code { get; set; }

        /// <summary>
        /// 行政区域名称
        /// </summary>
        [Column(StringLength = 20, IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 上级行政区域代码
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 6)]
        public virtual string ParentCode { get; set; }

        /// <summary>
        /// 1：省、直辖市
        /// <para>2：市</para>
        /// <para>3：地级市、区、县</para>
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint, IsNullable = false)]
        public int Level { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        [Column(DbType = DbTypeConsts.Tinyint, IsNullable = false)]
        public int State { get; set; } = StateConsts.Normal;
    }

    /// <summary>
    /// 行政区域
    /// </summary>
    [Table(Name = "D_District", DisableSyncStructure = true)]
    public class VM_District_Parent : District
    {
        [JsonIgnore]
        public override string ParentCode { get; set; }

        /// <summary>
        /// 上级行政区域
        /// </summary>
        [Navigate(nameof(ParentCode))]
        [JsonProperty(Order = 2)]
        public VM_District_Parent Parent { get; set; }

        public override string ToString()
        {
            return $"{Parent?.ToString()}{Name}";
        }
    }

    /// <summary>
    /// 行政区域
    /// </summary>
    [Table(Name = "D_District", DisableSyncStructure = true)]
    public class VM_District_Child : District
    {
        [JsonIgnore]
        public override string ParentCode { get; set; }

        /// <summary>
        /// 下级行政区域
        /// </summary>
        [JsonProperty(Order = 2)]
        [Navigate(nameof(ParentCode))]
        public List<VM_District_Child> Childs { get; set; }
    }

}