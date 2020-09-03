namespace ExcelToSQL.Models.DAL
{
    class MeterStatusDAL
    {
        public static void DeleteByPID(int pid)
        {
            DbContext.DefaultDB.Delete<MeterStatus>().Where(x => x.PID == pid).ExecuteAffrows();
        }
    }
}