using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 建筑信息字典
    /// </summary>
    [Table(Name = "D_BuildDictionary")]
    public class BuildDictionary
    {
        /// <summary>
        /// 建筑信息编号
        /// </summary>
        [Column(IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 建筑信息类型
        /// </summary>
        [JsonIgnore]
        public BuildDictionaryType Type { get; set; }

        /// <summary>
        /// 代码
        /// </summary>
        [Column(MapType = typeof(string), DbType = DbTypeConsts.Char)]
        public char Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Column(StringLength = 20, IsNullable = false)]
        public string Name { get; set; }
    }

    /// <summary>
    /// 建筑信息字典类型
    /// </summary>
    public enum BuildDictionaryType
    {
        /// <summary>
        /// 建筑功能
        /// </summary>
        BuildFunction = 1,

        /// <summary>
        /// 建筑结构
        /// </summary>
        BuildStructure = 2,

        /// <summary>
        /// 空调系统形式
        /// </summary>
        AirType = 3,

        /// <summary>
        /// 采暖系统形式
        /// </summary>
        HeatType = 4,

        /// <summary>
        /// 建筑外墙材料
        /// </summary>
        WallMaterialType = 5,

        /// <summary>
        /// 建筑外墙保温形式
        /// </summary>
        WallWarmType = 6,

        /// <summary>
        /// 建筑外窗类型
        /// </summary>
        WallWindowsType = 7,

        /// <summary>
        /// 建筑玻璃类型
        /// </summary>
        GlassType = 8,

        /// <summary>
        /// 窗框材料类型
        /// </summary>
        WinFrameMaterial = 9
    }

    /// <summary>
    /// 建筑信息字典集合
    /// </summary>
    public class VM_BuildDictionaryCollection
    {
        /// <summary>
        /// 建筑功能
        /// </summary>
        public List<BuildDictionary> BuildFunction { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 建筑结构
        /// </summary>
        public List<BuildDictionary> BuildStructure { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 空调系统形式
        /// </summary>
        public List<BuildDictionary> AirType { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 空调采暖形式
        /// </summary>
        public List<BuildDictionary> HeatType { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 建筑外墙材料
        /// </summary>
        public List<BuildDictionary> WallMaterialType { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 建筑外墙保温形式
        /// </summary>
        public List<BuildDictionary> WallWarmType { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 建筑外窗类型
        /// </summary>
        public List<BuildDictionary> WallWindowsType { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 建筑玻璃类型
        /// </summary>
        public List<BuildDictionary> GlassType { get; set; } = new List<BuildDictionary>();

        /// <summary>
        /// 窗框材料类型
        /// </summary>
        public List<BuildDictionary> WindowsFrameMaterial { get; set; } = new List<BuildDictionary>();
    }
}