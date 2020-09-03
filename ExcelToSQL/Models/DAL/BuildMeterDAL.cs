namespace ExcelToSQL.Models.DAL
{
    public class BuildMeterDAL
    {
        public static void DeleteByPID(int pid)
        {
            DbContext.DefaultDB.Delete<BuildMeter>().Where(a => a.PID == pid).ExecuteAffrows();
        }
    }
}