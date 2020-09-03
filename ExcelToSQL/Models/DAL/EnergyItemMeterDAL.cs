namespace ExcelToSQL.Models.DAL
{
    public class EnergyItemMeterDAL
    {
        public static void DeleteByPID(int pid)
        {
            DbContext.DefaultDB.Delete<EnergyItemMeter>().Where(a => a.PID == pid).ExecuteAffrows();
        }
    }
}