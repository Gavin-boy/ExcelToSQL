namespace ExcelToSQL.Models
{
    public class BulkIBranchsBasic
    {
        /// <summary>
        /// 建筑ID
        /// </summary>
        public int BuildID { get; set; }

        /// <summary>
        /// 支路ID
        /// </summary>
        public int BranchID { get; set; }

        /// <summary>
        /// 能源类型代码
        /// </summary>
        public int EnergyTypeCode { get; set; }

        /// <summary>
        /// 一级支路所属部门ID
        /// </summary>
        public int? DepartmentID1 { get; set; }

        /// <summary>
        /// 一级支路所属区域ID
        /// </summary>
        public int? AreaID1 { get; set; }

        /// <summary>
        /// 二级支路所属部门ID
        /// </summary>
        public int? DepartmentID2 { get; set; }

        /// <summary>
        /// 二级支路所属区域ID
        /// </summary>
        public int? AreaID2 { get; set; }

        /// <summary>
        /// 三级支路所属部门ID
        /// </summary>
        public int? DepartmentID3 { get; set; }

        /// <summary>
        /// 三级支路所属区域ID
        /// </summary>
        public int? AreaID3 { get; set; }
    }

    public class BulkBranchs : BulkIBranchsBasic
    {
        /// <summary>
        /// 建筑群名称
        /// </summary>
        public string BuildGroupName { get; set; }

        /// <summary>
        /// 建筑名称
        /// </summary>
        public string BuildName { get; set; }

        /// <summary>
        /// 分项代码
        /// </summary>
        public string EnergyItemCode { get; set; }

        /// <summary>
        /// 能源类型
        /// </summary>
        public string EnergyTypeName { get; set; }

        /// <summary>
        /// 一级支路名称
        /// </summary>
        public string BranchName1 { get; set; }

        /// <summary>
        /// 一级支路所属部门(全路径)
        /// </summary>
        public string DepartmentName1 { get; set; }

        /// <summary>
        /// 一级支路所属区域(全路径)
        /// </summary>
        public string AreaName1 { get; set; }

        /// <summary>
        /// 二级支路名称
        /// </summary>
        public string BranchName2 { get; set; }

        /// <summary>
        /// 二级支路所属部门(全路径)
        /// </summary>
        public string DepartmentName2 { get; set; }

        /// <summary>
        /// 二级支路所属区域(全路径)
        /// </summary>
        public string AreaName2 { get; set; }

        /// <summary>
        /// 三级支路名称
        /// </summary>
        public string BranchName3 { get; set; }

        /// <summary>
        /// 三级支路所属部门(全路径)
        /// </summary>
        public string DepartmentName3 { get; set; }

        /// <summary>
        /// 三级支路所属区域(全路径)
        /// </summary>
        public string AreaName3 { get; set; }
    }

    public class BulkInfo : BulkBranchs
    {
        /// <summary>
        /// 
        /// </summary>
        public int key { get; set; }

        /// <summary>
        /// 仪表型号ID
        /// </summary>
        public int MeterTypeID { get; set; }

        /// <summary>
        /// 仪表型号
        /// </summary>
        public string MeterTypeName { get; set; }

        /// <summary>
        /// MeterAddress
        /// </summary>
        public string MeterAddress { get; set; }

        /// <summary>
        /// 仪表名称
        /// </summary>
        public string MeterName { get; set; }

        /// <summary>
        /// 网关ID
        /// </summary>
        public int? GatewayID { get; set; }

        /// <summary>
        /// 网关名称
        /// </summary>
        public string GatewayName { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string IP { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }



    }
}
