using System;

namespace ExcelToSQL.Models
{
    public class HeasAuth
    {
        public string username { get; set; }

        /// <summary>
        /// 项目编号
        /// </summary>
        public int? pid { get; set; }

        /// <summary>
        /// 角色编号
        /// </summary>
        public int rid { get; set; }

        public DateTime getdatetime { get; set; }

        public int expires_in { get; set; }
    }
}