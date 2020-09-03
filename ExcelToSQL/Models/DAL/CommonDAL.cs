using System.Collections.Generic;
using System.Data;

namespace ExcelToSQL.Models.DAL
{
    public class CommonDAL
    {
        public static void CreateTable<T>() where T : class
            => DbContext.DefaultDB.CodeFirst.SyncStructure<T>();

        public static List<T> GetList<T>() where T : class
            => DbContext.DefaultDB.Select<T>().ToList();

        public static int CreateIdentity<T>(T source) where T : class
            => (int)DbContext.DefaultDB.Insert(source).ExecuteIdentity();

        public static int CreateMultiple<T>(IEnumerable<T> source) where T : class
            => DbContext.DefaultDB.Insert(source).ExecuteAffrows();

        public static int Update<T>(T source) where T : class
            => DbContext.DefaultDB.Update<T>().SetSource(source).ExecuteAffrows();

        public static int Clear<T>() where T : class
            => DbContext.DefaultDB.Delete<T>().Where(a => 1 == 1).ExecuteAffrows();

        public static void DropTable<T>() where T : class
        {
            string tablename = GetTableName<T>();

            DbContext.DefaultDB.Ado.ExecuteNonQuery(CommandType.Text, $"if exists(select 1 from sysObjects where Id=OBJECT_ID(N'{tablename}') and xtype='U') DROP TABLE {tablename}");
        }

        public static bool TableExists<T>() where T : class
        {
            string tablename = GetTableName<T>();

            object obj = DbContext.DefaultDB.Ado.ExecuteScalar(CommandType.Text,
                $"select 1 from sysObjects where Id=OBJECT_ID(N'{tablename}') and xtype='U'");

            return obj != null;
        }

        /// <summary>
        /// 获取类对应的数据库表名称
        /// </summary>
        public static string GetTableName<T>() where T : class
        {
            return DbContext.DefaultDB.CodeFirst.GetTableByEntity(typeof(T)).DbName;
        }
    }
}