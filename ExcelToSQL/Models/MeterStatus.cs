using FreeSql.DataAnnotations;
using Newtonsoft.Json;
using System;

namespace ExcelToSQL.Models
{
    /// <summary>
    /// 仪表
    /// </summary>
    [Table(Name = "BC_MeterStatus")]
    public class MeterStatus
    {
        /// <summary>
        /// 仪表ID
        /// </summary>
        [JsonProperty(Order = 0)]
        [Column(IsPrimary = true)]
        public int ID { get; set; }

        /// <summary>
        /// 表号
        /// </summary>
        [JsonProperty(Order = 1)]
        public string CollectionCode { get; set; }

        /// <summary>
        /// 采集时间
        /// </summary>
        [JsonProperty(Order = 2)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 网关状态
        /// </summary>
        [JsonProperty(Order = 3)]
        public bool GatewayStatus { get; set; }

        /// <summary>
        /// 通信状态(是否在线)
        /// </summary>
        [JsonProperty(Order = 4)]
        public bool IsOnLine { get; set; }

        /// <summary>
        /// 是否断开
        /// </summary>
        [JsonProperty(Order = 5)]
        public bool IsBreak { get; set; }

        /// <summary>
        /// 是否过载
        /// </summary>
        [JsonProperty(Order = 6)]
        public bool IsOverload { get; set; }

        /// <summary>
        /// 是否恶性负载
        /// </summary>
        [JsonProperty(Order = 7)]
        public bool IsMalignantload { get; set; }

        /// <summary>
        /// 所属项目编号
        /// </summary>
        [JsonIgnore]
        [Column(CanUpdate = false)]
        public int PID { get; set; }

    }
}
