using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class BuildDAL
    {
        public static List<VM_Build> GetViewListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_Build>()
                                      .InnerJoin(a => a.BuildGroupID == a.Group.ID)
                                      .InnerJoin(a => a.DataCenterID == a.DataCenter.ID)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static List<Build> GetListByGroup(int build_group_id)
        {
            return DbContext.DefaultDB.Select<Build>()
                                      .Where(a => a.BuildGroupID == build_group_id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static VM_Build GetViewByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<VM_Build>()
                                      .InnerJoin(a => a.BuildGroupID == a.Group.ID)
                                      .InnerJoin(a => a.DataCenterID == a.DataCenter.ID)
                                      .InnerJoin(a => a.BuildFunctionID == a.BuildFunction.ID)
                                      .InnerJoin(a => a.BuildStructureID == a.BuildStructure.ID)
                                      .InnerJoin(a => a.AirTypeID == a.AirType.ID)
                                      .InnerJoin(a => a.HeatTypeID == a.HeatType.ID)
                                      .InnerJoin(a => a.WallMaterialTypeID == a.WallMaterialType.ID)
                                      .InnerJoin(a => a.WallWarmTypeID == a.WallWarmType.ID)
                                      .InnerJoin(a => a.WallWindowsTypeID == a.WallWindowsType.ID)
                                      .InnerJoin(a => a.GlassTypeID == a.GlassType.ID)
                                      .InnerJoin(a => a.WinFrameMaterialID == a.WinFrameMaterial.ID)
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static Build GetByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<Build>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static bool Exists(int id, int pid)
        {
            return DbContext.DefaultDB.Select<Build>()
                                     .Where(a => a.ID == id)
                                     .Where(a => a.PID == pid)
                                     .Where(a => a.State == StateConsts.Normal)
                                     .Any();
        }

        public static List<VM_BuildGroup_Build> GetViewByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_BuildGroup_Build>()
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .IncludeMany(a => a.Builds, then => then.Where(b => b.State == StateConsts.Normal))
                                      .ToList();
        }

        public static List<VM_Build_CU> GetList(int pid)
        {
            return DbContext.DefaultDB.Select<VM_Build_CU>()
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static bool AreaUsed(int id)
        {
            return DbContext.DefaultDB.Select<Area>()
                                      .Where(a => a.BuildID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static bool DepartmentUsed(int id)
        {
            return DbContext.DefaultDB.Select<Department>()
                                      .Where(a => a.BuildID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static bool BranchUsed(int id)
        {
            return DbContext.DefaultDB.Select<Branch>()
                                      .Where(a => a.BuildID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static int DeleteByID(int id)
        {
            return DbContext.DefaultDB.Update<Build>()
                                      .Set(a => a.State == StateConsts.Deleted)
                                      .Where(a => a.ID == id)
                                      .ExecuteAffrows();
        }

        public static List<(int, int, string)> GetAllBuild(int pid)
        {
            return DbContext.DefaultDB.Select<Build>()
                                     .Where(a => a.PID == pid)
                                     .Where(a => a.State == StateConsts.Normal)
                                     .ToList<(int, int, string)>("ID,BuildGroupID,Name");
        }
    }
}