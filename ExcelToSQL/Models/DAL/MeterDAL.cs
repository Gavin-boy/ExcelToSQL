using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class MeterDAL
    {
        public static List<VM_Meter> GetViewListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<VM_Meter>()
                                      .InnerJoin(a => a.MeterTypeID == a.MeterType.ID)
                                      .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                      .LeftJoin(a => a.GatewayID == a.Gateway.ID)
                                      .InnerJoin(a => a.BranchID == a.Branch.ID)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static List<VM_Meter> GetViewListByBranches(List<int> branch_ids, string metertype, string ip, int state, int pid)
        {
            //直接在表达式中写state == 1freesql会不认
            bool isOnline = state == 1;
            return DbContext.DefaultDB.Select<VM_Meter>()
                                      .InnerJoin(a => a.MeterTypeID == a.MeterType.ID)
                                      .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                      .LeftJoin(a => a.GatewayID == a.Gateway.ID)
                                      .InnerJoin(a => a.BranchID == a.Branch.ID)
                                      .InnerJoin(a => a.ID == a.Meterstatus.ID)
                                      .Where(a => branch_ids.Contains(a.BranchID))
                                      .Where(a => string.IsNullOrEmpty(metertype) || a.MeterType.Name.Contains(metertype))
                                      .Where(a => string.IsNullOrEmpty(ip) || a.IP.Contains(ip))
                                      .Where(a => state < 0 || a.Meterstatus.IsOnLine == isOnline)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToList();
        }

        public static VM_Meter GetViewBySN2(string sn2, int pid)
        {
            return DbContext.DefaultDB.Select<VM_Meter>()
                                      .InnerJoin(a => a.MeterTypeID == a.MeterType.ID)
                                      .InnerJoin(a => a.EnergyTypeCode == a.EnergyType.Code)
                                      .LeftJoin(a => a.GatewayID == a.Gateway.ID)
                                      .InnerJoin(a => a.BranchID == a.Branch.ID)
                                      .Where(a => a.SN2 == sn2)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static Meter GetBySN2(string sn2, int pid)
        {
            return DbContext.DefaultDB.Select<Meter>()
                                      .Where(a => a.SN2 == sn2)
                                      .Where(a => a.PID == pid)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .ToOne();
        }

        public static bool ExistsBranchLink(int branch_id)
        {
            return DbContext.DefaultDB.Select<Meter>()
                                      .Where(a => a.BranchID == branch_id)
                                      .Where(a => a.State == StateConsts.Normal)
                                      .Any();
        }

        public static int DeleteBySN2(string sn2)
        {
            return DbContext.DefaultDB.Update<Meter>()
                                      .Set(a => a.State == StateConsts.Deleted)
                                      .Where(a => a.SN2 == sn2)
                                      .ExecuteAffrows();
        }

        public static void DeleteByPID(int pid)
        {
            DbContext.DefaultDB.Delete<Meter>().Where(x => x.PID == pid).ExecuteAffrows();
        }

    }
}