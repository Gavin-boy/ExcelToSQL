namespace ExcelToSQL.Models.DAL
{
    public class BranchMeterDAL
    {
        public static void DeleteByPID(int pid)
        {
            DbContext.DefaultDB.Delete<BranchMeter>().Where(a => a.PID == pid).ExecuteAffrows();
        }
    }
}