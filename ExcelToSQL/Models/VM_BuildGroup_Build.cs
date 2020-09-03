using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 建筑群-建筑
    /// </summary>
    [Table(Name = "BD_BuildGroup", DisableSyncStructure = true)]
    public class VM_BuildGroup_Build
    {
        /// <summary>
        /// 建筑群编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 建筑群名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        [JsonIgnore]
        public int PID { get; set; }

        /// <summary>
        /// 建筑
        /// </summary>
        [Navigate(nameof(VM_Build_ID_Name.BuildGroupID))]
        public List<VM_Build_ID_Name> Builds { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        public int State { get; set; }
    }

    /// <summary>
    /// 建筑，仅包含 ID 和名称
    /// </summary>
    [Table(Name = "BD_Build", DisableSyncStructure = true)]
    public class VM_Build_ID_Name
    {
        /// <summary>
        /// 建筑编号
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 所属建筑群编号
        /// </summary>
        [JsonIgnore]
        public int BuildGroupID { get; set; }

        /// <summary>
        /// 状态
        /// <para>1 启用，0 未启用</para>
        /// </summary>
        [JsonIgnore]
        public int State { get; set; }
    }
}