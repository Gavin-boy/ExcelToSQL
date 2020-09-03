using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class GatewayDAL
    {
        public static List<Gateway> GetListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<Gateway>()
                                     .Where(a => a.PID == pid)
                                     .Where(a => a.State == StateConsts.Normal)
                                     .ToList();
        }

        public static List<VM_Gateway> GetViewListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_Gateway>()
                                     .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                     .Where(a => a.PID == pid)
                                     .Where(a => a.State == StateConsts.Normal)
                                     .ToList();
        }

        public static Gateway GetByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<Gateway>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static VM_Gateway GetViewByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<VM_Gateway>()
                                      .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static bool Exists(int id)
        {
            return DbContext.DefaultDB.Select<Gateway>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static bool Exists(int id, int pid)
        {
            return DbContext.DefaultDB.Select<Gateway>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static int DeleteByID(int id)
        {
            return DbContext.DefaultDB.Update<Gateway>()
                                      .Set(a => a.State == StateConsts.Deleted)
                                      .Where(a => a.ID == id)
                                      .ExecuteAffrows();
        }
    }
}