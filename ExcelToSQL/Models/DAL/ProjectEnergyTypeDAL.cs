using System.Collections.Generic;

namespace ExcelToSQL.Models.DAL
{
    class ProjectEnergyTypeDAL
    {
        public static List<EnergyType> GetListByPID(int pid)
        {
            return DbContext.DefaultDB.Select<EnergyType, ProjectEnergyType>()
                                      .InnerJoin((a, b) => a.Code == b.EnergyTypeCode)
                                      .Where((a, b) => b.PID == pid)
                                      .ToList((a, b) => new EnergyType { Code = a.Code, Name = a.Name });
        }

        public static int DeleteByPID(int pid)
        {
            return DbContext.DefaultDB.Delete<ProjectEnergyType>()
                                      .Where(a => a.PID == pid)
                                      .ExecuteAffrows();
        }

        public static int CreateMultiple(IEnumerable<ProjectEnergyType> project_energy_types)
        {
            return DbContext.DefaultDB.Insert(project_energy_types).ExecuteAffrows();
        }
    }
}