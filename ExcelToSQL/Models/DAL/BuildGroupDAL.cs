using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class BuildGroupDAL
    {
        public static List<BuildGroup> GetListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<BuildGroup>()
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static List<VM_BuildGroup> GetViewListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_BuildGroup>()
                                      .InnerJoin(a => a.ProvinceCode == a.Province.Code)
                                      .InnerJoin(a => a.CityCode == a.City.Code)
                                      .InnerJoin(a => a.DistrictCode == a.District.Code)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static VM_BuildGroup GetViewByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<VM_BuildGroup>()
                                      .InnerJoin(a => a.ProvinceCode == a.Province.Code)
                                      .InnerJoin(a => a.CityCode == a.City.Code)
                                      .InnerJoin(a => a.DistrictCode == a.District.Code)
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static bool Exists(int id, int pid)
        {
            return DbContext.DefaultDB.Select<BuildGroup>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static bool BuildUsed(int id)
        {
            return DbContext.DefaultDB.Select<Build>()
                                      .Where(a => a.BuildGroupID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static int DeleteByID(int id)
        {
            return DbContext.DefaultDB.Update<BuildGroup>()
                                      .Set(a => a.State == StateConsts.Deleted)
                                      .Where(a => a.ID == id)
                                      .ExecuteAffrows();
        }
    }
}