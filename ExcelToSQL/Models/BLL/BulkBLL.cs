using ExcelToSQL.Models.BLL;
using ExcelToSQL.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelToSQL.Models
{

    public class BulkBLL
    {
        //所有建筑群 id + name
        private List<(int, string)> BuildGroupMap;
        //所有建筑 id + 建筑群ID + name
        private List<(int, int, string)> BuildMap;
        //所有分项代码
        private List<(string, string)> EnergyItemMap;
        //所有能源类型 code + name
        private List<(int, string)> EnergyTypeMap;
        //部门id + 建筑id + 全路径
        private List<(int, int, string)> DepartmentMap;
        //区域id+ 建筑id + 全路径
        private List<(int, int, string)> AreaMap;
        //仪表类型 id+model+name(4	REM203-EY 三相导轨电能表)
        private List<(int, string, string)> MeterTypeMap;
        //网关id+name
        private List<(int, string)> GatewayMap;

        //项目ID
        private int ProjectId;

        public BulkBLL(int pid)
        {
            BuildGroupMap = BuildGroupDAL.GetListByPID(pid).Select(x => (x.ID, x.Name)).ToList();
            BuildMap = BuildDAL.GetAllBuild(pid);
            EnergyItemMap = CommonDAL.GetList<EnergyItem>().Select(x => (x.Code, x.Name)).ToList();
            EnergyTypeMap = ProjectEnergyTypeDAL.GetListByPID(pid).Select(x => (x.Code, x.Name)).ToList();
            DepartmentMap = DepartmentDAL.GetAllDepartment(pid);
            AreaMap = AreaDAL.GetAllArea(pid);
            MeterTypeMap = MeterTypeDAL.GetListByPID(pid).Select(x => (x.ID, x.Model, x.Name)).ToList();
            GatewayMap = GatewayDAL.GetListByPID(pid).Select(x => (x.ID, x.Name)).ToList();
            //
            ProjectId = pid;
        }

        /// <summary>
        /// 根据建筑群读取ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int? FindBuildGroupByName(string name)
        {
            return FindItemByName(name, BuildGroupMap);
        }

        /// <summary>
        /// 根据建筑读取ID
        /// </summary>
        /// <param name="BuildGroupID"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private int? FindBuildByName(int BuildGroupID, string name)
        {
            var e = BuildMap.Where(x => x.Item2 == BuildGroupID && x.Item3 == name).FirstOrDefault();
            return e == (0, 0, null) ? (int?)null : e.Item1;
        }

        /// <summary>
        /// 该分项代码是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private string FindEnergyItemExist(string name)
        {
            var e = EnergyItemMap.Where(x => x.Item2 == name).FirstOrDefault();
            return e == (null, null) ? null : e.Item1;
        }

        /// <summary>
        /// 根据能源类型读取ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int? FindEnergyTypeByName(string name)
        {
            return FindItemByName(name, EnergyTypeMap);
        }

        /// <summary>
        /// 根据部门全路径读取ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private (int, int) FindDepartmentByName(string name)
        {
            return FindItemByName(name, DepartmentMap);
        }

        /// <summary>
        /// 根据区域全路径读取ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private (int, int) FindAreaByName(string name)
        {
            return FindItemByName(name, AreaMap);
        }

        /// <summary>
        /// 根据仪表类型名读取ID
        /// </summary>
        /// <param name="model"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        private int? FindMeterTypeByName(string model, string name)
        {
            return FindItemByName(model, name, MeterTypeMap);
        }

        /// <summary>
        /// 根据网关名读取ID
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private int? FindGatewayMapByName(string name)
        {
            return FindItemByName(name, GatewayMap);
        }

        private int? FindItemByName(string name, List<(int, string)> Map)
        {
            var e = Map.Where(x => x.Item2 == name).FirstOrDefault();
            return e == (0, null) ? (int?)null : e.Item1;
        }

        private (int, int) FindItemByName(string name, List<(int, int, string)> Map)
        {
            var e = Map.Where(x => x.Item3 == name).FirstOrDefault();
            return e == (0, 0, null) ? (0, 0) : (e.Item1, e.Item2);
        }

        private int? FindItemByName(string model, string name, List<(int, string, string)> Map)
        {
            var e = Map.Where(x => x.Item2 == model && x.Item3 == name).FirstOrDefault();
            return e == (0, null, null) ? (int?)null : e.Item1;
        }

        public (bool, string) BulkImport(List<BulkInfo> bulkInfos)
        {
            string errMsg = "第{0}行，{1}不能为空";
            string errMsg2 = "第{0}行，没有找到对应的{1}:{2}";
            string errMsg3 = "第{0}行，{1}所属建筑必须和支路所属建筑一致";

            // 1、循环检查验证
            foreach (var e in bulkInfos)
            {
                //检查建筑群名称是否为空
                if (string.IsNullOrWhiteSpace(e.BuildGroupName))
                {
                    return (false, string.Format(errMsg, e.key, "建筑群名称"));
                }
                //判断建筑群是否存在，并提取建筑群ID
                var BuildGroupID = FindBuildGroupByName(e.BuildGroupName);
                if (BuildGroupID == null || BuildGroupID == 0)
                {
                    return (false, string.Format(errMsg2, e.key, "建筑群名称", e.BuildGroupName));
                }

                //检查建筑名称是否为空
                if (string.IsNullOrWhiteSpace(e.BuildName))
                {
                    return (false, string.Format(errMsg, e.key, "建筑名称"));
                }
                //判断建筑是否存在，并提取建筑ID
                var BuildID = FindBuildByName(BuildGroupID.Value, e.BuildName);
                if (BuildID == null || BuildID == 0)
                {
                    return (false, string.Format(errMsg2, e.key, "建筑名称", e.BuildName));
                }
                //为建筑ID赋值
                e.BuildID = BuildID.Value;

                //检查分项名称是否为空
                if (string.IsNullOrWhiteSpace(e.EnergyItemCode))
                {
                    return (false, string.Format(errMsg, e.key, "分项"));
                }
                //判断分项名称是否存在,并提取分项代码
                var EnergyItemCode = FindEnergyItemExist(e.EnergyItemCode);
                if (EnergyItemCode == null)
                {
                    return (false, string.Format(errMsg2, e.key, "分项", e.EnergyItemCode));
                }
                e.EnergyItemCode = EnergyItemCode;

                //检查能源类型是否为空
                if (string.IsNullOrWhiteSpace(e.EnergyTypeName))
                {
                    return (false, string.Format(errMsg, e.key, "能源类型"));
                }
                //判断能源类型是否存在并提取ID
                var EnergyTypeCode = FindEnergyTypeByName(e.EnergyTypeName);
                if (EnergyTypeCode == null || EnergyTypeCode == 0)
                {
                    return (false, string.Format(errMsg2, e.key, "能源类型", e.EnergyTypeName));
                }
                e.EnergyTypeCode = EnergyTypeCode.Value;

                //检查一级支路名称是否为空
                if (string.IsNullOrWhiteSpace(e.BranchName1))
                {
                    return (false, string.Format(errMsg, e.key, "一级支路名称"));
                }
                if (!string.IsNullOrWhiteSpace(e.DepartmentName1))
                {
                    //判断部门是否存在并提取ID
                    var DepartmentID1 = FindDepartmentByName(e.DepartmentName1);
                    if (DepartmentID1 == (0, 0))
                    {
                        return (false, string.Format(errMsg2, e.key, "一级支路所属部门", e.DepartmentName1));
                    }
                    e.DepartmentID1 = DepartmentID1.Item1;
                    if (e.BuildID != DepartmentID1.Item2)
                    {
                        return (false, string.Format(errMsg3, e.key, "一级支路部门"));
                    }
                }

                if (!string.IsNullOrWhiteSpace(e.AreaName1))
                {
                    //判断区域是否存在并提取ID
                    var AreaID1 = FindAreaByName(e.AreaName1);
                    if (AreaID1 == (0, 0))
                    {
                        return (false, string.Format(errMsg2, e.key, "一级支路所属区域", e.AreaName1));
                    }
                    e.AreaID1 = AreaID1.Item1;
                    if (e.BuildID != AreaID1.Item2)
                    {
                        return (false, string.Format(errMsg3, e.key, "一级支路区域"));
                    }
                }

                //若二级支路名称不为空
                if (!string.IsNullOrWhiteSpace(e.BranchName2))
                {
                    if (!string.IsNullOrWhiteSpace(e.DepartmentName2))
                    {
                        //判断部门是否存在并提取ID
                        var DepartmentID2 = FindDepartmentByName(e.DepartmentName2);
                        if (DepartmentID2 == (0, 0))
                        {
                            return (false, string.Format(errMsg2, e.key, "二级支路所属部门", e.DepartmentName2));
                        }
                        e.DepartmentID2 = DepartmentID2.Item1;
                        if (e.BuildID != DepartmentID2.Item2)
                        {
                            return (false, string.Format(errMsg3, e.key, "二级支路部门"));
                        }
                    }

                    if (!string.IsNullOrWhiteSpace(e.AreaName2))
                    {
                        //判断区域是否存在并提取ID
                        var AreaID2 = FindAreaByName(e.AreaName2);
                        if (AreaID2 == (0, 0))
                        {
                            return (false, string.Format(errMsg, e.key, "二级支路所属区域"));
                        }
                        e.AreaID2 = AreaID2.Item1;
                        if (e.BuildID != AreaID2.Item2)
                        {
                            return (false, string.Format(errMsg3, e.key, "二级支路区域"));
                        }
                    }
                }

                //若三级支路名称不为空
                if (!string.IsNullOrWhiteSpace(e.BranchName3))
                {
                    if (!string.IsNullOrWhiteSpace(e.DepartmentName3))
                    {
                        //判断部门是否存在并提取ID
                        var DepartmentID3 = FindDepartmentByName(e.DepartmentName3);
                        if (DepartmentID3 == (0, 0))
                        {
                            return (false, string.Format(errMsg2, e.key, "三级支路所属部门", e.DepartmentName3));
                        }
                        e.DepartmentID3 = DepartmentID3.Item1;
                        if (e.BuildID != DepartmentID3.Item2)
                        {
                            return (false, string.Format(errMsg3, e.key, "三级支路部门"));
                        }
                    }

                    //判断区域是否存在并提取ID
                    var AreaID3 = FindAreaByName(e.AreaName3);
                    if (AreaID3 == (0, 0))
                    {
                        return (false, string.Format(errMsg, e.key, "三级支路所属区域"));
                    }
                    e.AreaID3 = AreaID3.Item1;
                    if (e.BuildID != AreaID3.Item2)
                    {
                        return (false, string.Format(errMsg3, e.key, "三级支路区域"));
                    }
                }

                //若仪表不为空
                if (!string.IsNullOrWhiteSpace(e.MeterAddress))
                {
                    //检查仪表名称是否为空
                    if (string.IsNullOrWhiteSpace(e.MeterName))
                    {
                        return (false, string.Format(errMsg, e.key, "仪表名称"));
                    }

                    //检查仪表型号是否为空
                    if (string.IsNullOrWhiteSpace(e.MeterTypeName))
                    {
                        return (false, string.Format(errMsg, e.key, "仪表型号"));
                    }

                    //判断仪表型号是否存在并提取IDs
                    var MeterTypeID = FindMeterTypeByName(e.MeterTypeName, e.MeterName);
                    if (MeterTypeID == null || MeterTypeID == 0)
                    {
                        return (false, $"第{e.key}行，没有找到对应的仪表型号:{e.MeterTypeName}或没有找到对应的仪表名称:{e.MeterName}");
                    }
                    e.MeterTypeID = MeterTypeID.Value;

                    //若网关名称不为空
                    if (!string.IsNullOrWhiteSpace(e.GatewayName))
                    {
                        //判断网关名称是否存在并提取ID
                        var GatewayID = FindGatewayMapByName(e.GatewayName);
                        if (GatewayID == null || GatewayID == 0)
                        {
                            return (false, string.Format(errMsg2, e.key, "网关名称", e.GatewayName));
                        }
                        e.GatewayID = GatewayID;
                    }

                    //检查IP是否为空
                    if (string.IsNullOrWhiteSpace(e.IP))
                    {
                        return (false, string.Format(errMsg, e.key, "IP"));
                    }

                    //检查端口是否为空
                    if (e.Port <= 0)
                    {
                        return (false, string.Format(errMsg, e.key, "端口"));
                    }
                }

            }

            //2、检查支路重复
            var names = bulkInfos
            .Select(x =>
            {
                string bname = x.BranchName1;
                if (!string.IsNullOrWhiteSpace(x.BranchName2))
                {
                    bname += "/" + x.BranchName2;
                    if (!string.IsNullOrWhiteSpace(x.BranchName3))
                    {
                        bname += "/" + x.BranchName3;
                    }
                }
                return bname;
            })
            .GroupBy(x => x)
            .Where(x => x.Count() > 1).Select(x => x.Key);

            if (names.Count() > 0)
            {
                var eMsg = "以下支路出现重复：" + string.Join(",", names);
                return (true, eMsg);
            }

            //3、检查表重复
            var Mnames = bulkInfos
                .Where(x => !string.IsNullOrWhiteSpace(x.MeterAddress))
                .Select(x => "【仪表地址：" + x.MeterAddress + ",IP：" + x.IP + ",端口：" + x.Port + "】")
                .GroupBy(x => x)
                .Where(x => x.Count() > 1)
                .Select(x => x.Key);
            if (Mnames.Count() > 0)
            {
                var eMsg = "以下仪表出现重复：" + string.Join(",", Mnames);
                return (true, eMsg);
            }

            //4、批量导入到数据库
            //储存BranchID
            var dicBranch = new Dictionary<string, int>();
            DbContext.DefaultDB.Transaction(() =>
            {
                //先清空三张表
                BranchDAL.DeleteByPID(ProjectId);
                MeterDAL.DeleteByPID(ProjectId);
                MeterStatusDAL.DeleteByPID(ProjectId);

                foreach (var e in bulkInfos)
                {
                    int BranchID1;
                    //先处理一级支路
                    if (dicBranch.ContainsKey(e.BranchName1))
                    {
                        //以一级支路为根支路
                        BranchID1 = dicBranch[e.BranchName1];
                    }
                    else
                    {
                        //插入数据库，并提取ID
                        var branch = new Branch()
                        {
                            Name = e.BranchName1,
                            BuildID = e.BuildID,
                            EnergyTypeCode = e.EnergyTypeCode,
                            EnergyItemCode = e.EnergyItemCode,
                            DepartmentID = e.DepartmentID1,
                            AreaID = e.AreaID1,
                            ParentID = null,
                            Level = 1,
                            Leaf = true,
                            State = StateConsts.Normal,
                            PID = ProjectId
                        };
                        //以一级支路为根支路
                        BranchID1 = CommonDAL.CreateIdentity(branch);
                        //加入到缓存
                        dicBranch[e.BranchName1] = BranchID1;
                    }
                    //把根支路id覆盖掉
                    e.BranchID = BranchID1;

                    //处理二级支路
                    if (!string.IsNullOrWhiteSpace(e.BranchName2))
                    {
                        int BranchID2;
                        //二级支路全名
                        var bName2 = e.BranchName1 + "|" + e.BranchName2;
                        if (dicBranch.ContainsKey(bName2))
                        {
                            //把根支路id覆盖掉
                            BranchID2 = dicBranch[bName2];
                        }
                        else
                        {
                            //插入数据库，并提取ID
                            var branch = new Branch()
                            {
                                Name = e.BranchName2,
                                BuildID = e.BuildID,
                                EnergyTypeCode = e.EnergyTypeCode,
                                EnergyItemCode = e.EnergyItemCode,
                                DepartmentID = e.DepartmentID2,
                                AreaID = e.AreaID2,
                                ParentID = BranchID1,
                                Level = 2,
                                Leaf = true,
                                State = StateConsts.Normal,
                                PID = ProjectId
                            };
                            BranchDAL.UpdateLeaf(BranchID1, false);
                            BranchID2 = CommonDAL.CreateIdentity(branch);
                            //加入到缓存
                            dicBranch[bName2] = BranchID2;
                        }
                        //把根支路id覆盖掉
                        e.BranchID = BranchID2;

                        //处理三级支路
                        if (!string.IsNullOrWhiteSpace(e.BranchName3))
                        {
                            int BranchID3;
                            //三级支路全名
                            var bName3 = e.BranchName1 + "|" + e.BranchName2 + "|" + e.BranchName3;
                            if (dicBranch.ContainsKey(bName3))
                            {
                                BranchID3 = dicBranch[bName3];
                            }
                            else
                            {
                                //插入数据库，并提取ID
                                var branch = new Branch()
                                {
                                    Name = e.BranchName3,
                                    BuildID = e.BuildID,
                                    EnergyTypeCode = e.EnergyTypeCode,
                                    EnergyItemCode = e.EnergyItemCode,
                                    DepartmentID = e.DepartmentID3,
                                    AreaID = e.AreaID3,
                                    ParentID = BranchID2,
                                    Level = 3,
                                    Leaf = true,
                                    State = StateConsts.Normal,
                                    PID = ProjectId
                                };
                                BranchDAL.UpdateLeaf(BranchID2, false);
                                BranchID3 = CommonDAL.CreateIdentity(branch);
                                //加入到缓存
                                dicBranch[bName3] = BranchID3;
                            }
                            //把根支路id覆盖掉
                            e.BranchID = BranchID3;
                        }
                    }

                    if (string.IsNullOrWhiteSpace(e.MeterAddress))
                        continue;

                    //唯一标识
                    string sn = e.IP.Replace(".", "") + e.Port + "_" + e.MeterAddress;

                    //添加仪表
                    var meter = new Meter()
                    {
                        SN = sn,
                        SN2 = sn,
                        MeterTypeID = e.MeterTypeID,
                        EnergyTypeCode = e.EnergyTypeCode,
                        GatewayID = e.GatewayID,
                        BranchID = e.BranchID,
                        IP = e.IP,
                        Port = e.Port,
                        PID = ProjectId,
                        State = StateConsts.Normal
                    };
                    int meterid = CommonDAL.CreateIdentity(meter);

                    //添加仪表状态
                    var meterstatus = new MeterStatus()
                    {
                        ID = meterid,
                        CollectionCode = sn,
                        CreateTime = DateTime.Now,
                        GatewayStatus = false,
                        IsOnLine = false,
                        IsBreak = true,
                        IsOverload = false,
                        IsMalignantload = false,
                        PID = ProjectId
                    };
                    CommonDAL.CreateIdentity(meterstatus);

                }

                //更新关系表
                InitMeterBLL.Create(ProjectId);
            });



            return (true, string.Empty);

        }

    }

}