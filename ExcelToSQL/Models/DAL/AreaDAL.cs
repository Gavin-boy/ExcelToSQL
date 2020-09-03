using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class AreaDAL
    {
        public static List<VM_Area> GetViewListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_Area>()
                                      .InnerJoin(a => a.BuildID == a.Build.ID)
                                      .LeftJoin(a => a.ParentID == a.Parent.ID)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static List<VM_Area_Parent> GetViewListByBuild(int build_id)
        {
            return DbContext.DefaultDB.Select<VM_Area_Parent>()
                                      .LeftJoin(a => a.ParentID == a.Parent.ID)
                                      .Where(a => a.BuildID == build_id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static List<VM_Area_Child> GetTreeByBuild(int build_id)
        {
            return DbContext.DefaultDB.Select<VM_Area_Child>()
                                      .Where(a => a.BuildID == build_id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToTreeList();
        }

        public static VM_Area GetViewByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<VM_Area>()
                                      .InnerJoin(a => a.BuildID == a.Build.ID)
                                      .LeftJoin(a => a.ParentID == a.Parent.ID)
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static Area GetByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<Area>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static int GetChildCount(int parent_id)
        {
            return (int)DbContext.DefaultDB.Select<Area>()
                                           .Where(a => a.ParentID == parent_id)
                                           .Where(a => a.State == StateConsts.Normal)
                                           .Count();
        }

        public static bool ParentUsed(int id)
        {
            return DbContext.DefaultDB.Select<Area>()
                                      .Where(a => a.ParentID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static bool BranchUsed(int id)
        {
            return DbContext.DefaultDB.Select<Branch>()
                                      .Where(a => a.AreaID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static int DeleteByID(int id)
        {
            return DbContext.DefaultDB.Update<Area>()
                                      .Set(a => a.State == StateConsts.Deleted)
                                      .Where(a => a.ID == id)
                                      .ExecuteAffrows();
        }

        public static List<(int, int, string)> GetAllArea(int pid)
        {
            return DbContext.DefaultDB.Select<Area>()
                                     .Where(a => a.PID == pid)
                                     .Where(a => a.State == StateConsts.Normal)
                                     .ToList<(int, int, string)>("ID,BuildID,FullPath");
        }

    }
}