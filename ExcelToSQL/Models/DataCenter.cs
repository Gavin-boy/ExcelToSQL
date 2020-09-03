using FreeSql.DataAnnotations;
using Newtonsoft.Json;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 数据中心
    /// </summary>
    [Table(Name = "BD_DataCenter")]
    public class DataCenter
    {
        /// <summary>
        /// 数据中心编号
        /// </summary>
        [Column(IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        /// <summary>
        /// 数据中心代码
        /// <para>数据中心代码由部里统一分配</para>
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 10, IsNullable = false)]
        public string Code { get; set; }

        /// <summary>
        /// 数据中心名称
        /// </summary>
        [Column(StringLength = 24, IsNullable = false)]
        public string Name { get; set; }

        /// <summary>
        /// 数据中心 IP
        /// </summary>
        [Column(DbType = DbTypeConsts.Varchar, StringLength = 128)]    // 长度要考虑到不是 IP 而是域名
        public string IP { get; set; }

        /// <summary>
        /// 数据中心端口
        /// </summary>
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

}