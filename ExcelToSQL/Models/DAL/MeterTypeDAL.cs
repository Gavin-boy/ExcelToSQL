using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class MeterTypeDAL
    {
        public static List<MeterType> GetListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<MeterType>()
                                     .Where(a => a.PID == pid)
                                     .Where(a => a.State == StateConsts.Normal)
                                     .ToList();
        }

        public static List<VM_MeterType> GetViewListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_MeterType>()
                                     .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                     .InnerJoin(a => a.ProtocolTypeID == a.ProtocolType.ID)
                                     .Where(a => a.PID == pid)
                                     .Where(a => a.State == StateConsts.Normal)
                                     .ToList();
        }

        public static VM_MeterType GetViewByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<VM_MeterType>()
                                      .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                      .InnerJoin(a => a.ProtocolTypeID == a.ProtocolType.ID)
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static MeterType GetByID(int id, int pid)
        {
            return DbContext.DefaultDB.Select<MeterType>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static bool Exists(int id, int pid)
        {
            return DbContext.DefaultDB.Select<MeterType>()
                                      .Where(a => a.ID == id)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static int DeleteByID(int id)
        {
            return DbContext.DefaultDB.Update<MeterType>()
                                      .Set(a => a.State == StateConsts.Deleted)
                                      .Where(a => a.ID == id)
                                      .ExecuteAffrows();
        }
    }
}