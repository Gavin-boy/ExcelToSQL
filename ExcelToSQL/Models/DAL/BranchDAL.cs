using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class BranchDAL
    {
        public static List<VM_Branch_Child> GetTreeByBuild(int buildID)
        {
            return DbContext.DefaultDB.Select<VM_Branch_Child>()
                                      .Where(a => a.BuildID == buildID)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToTreeList();
        }

        public static List<VM_Branch_Child> GetTreeByBuildEnergyType(int buildID, int energy_type_code)
        {
            return DbContext.DefaultDB.Select<VM_Branch_Child>()
                                      .Where(a => a.BuildID == buildID)
                                      .Where(a => a.EnergyTypeCode == energy_type_code)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToTreeList();
        }

        public static List<VM_Branch> GetViewListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_Branch>()
                                      .InnerJoin(a => a.BuildID == a.Build.ID)
                                      .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                      .LeftJoin(a => a.EnergyItemCode == a.EnergyItem.Code)
                                      .LeftJoin(a => a.DepartmentID == a.Department.ID)
                                      .LeftJoin(a => a.AreaID == a.Area.ID)
                                      .LeftJoin(a => a.ParentID == a.Parent.ID)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static VM_Branch GetViewByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<VM_Branch>()
                                      .InnerJoin(a => a.BuildID == a.Build.ID)
                                      .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                      .LeftJoin(a => a.EnergyItemCode == a.EnergyItem.Code)
                                      .LeftJoin(a => a.DepartmentID == a.Department.ID)
                                      .LeftJoin(a => a.AreaID == a.Area.ID)
                                      .LeftJoin(a => a.ParentID == a.Parent.ID)
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static Branch GetByID(int id)
        {
            return DbContext.DefaultDB.Select<Branch>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static Branch GetByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<Branch>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static int GetChildCount(int parent_id)
        {
            return (int)DbContext.DefaultDB.Select<Branch>()
                                           .Where(a => a.ParentID == parent_id)
                                           .Where(a => a.State == StateConsts.Normal)
                                           .Count();
        }

        public static int UpdateLeaf(int branch_id, bool leaf)
        {
            return DbContext.DefaultDB.Update<Branch>()
                                      .Set(a => a.Leaf == leaf)
                                      .Where(a => a.ID == branch_id)
                                      .ExecuteAffrows();
        }

        public static int DeleteByID(int id)
        {
            return DbContext.DefaultDB.Update<Branch>()
                                      .Set(a => a.State == StateConsts.Deleted)
                                      .Where(a => a.ID == id)
                                      .ExecuteAffrows();
        }


        public static void DeleteByPID(int pid)
        {
            DbContext.DefaultDB.Delete<Branch>().Where(x => x.PID == pid).ExecuteAffrows();
        }

    }
}