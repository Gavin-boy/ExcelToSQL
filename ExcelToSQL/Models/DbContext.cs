using FreeSql;
using FreeSql.Aop;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExcelToSQL.Models
{
    public class DbContext
    {
        IFreeSql _default;
        Dictionary<string, IFreeSql> _dict = new Dictionary<string, IFreeSql>();

        /// <summary>
        /// 注册 IFreeSql
        /// <para>首次注册的 IFreeSql 将被设为默认 IFreeSql，且不可更改</para>
        /// <para>当只有一个 IFreeSql 时，使用默认的就可以了</para>
        /// <para>同名注册会被忽略，不会报错</para>
        /// <para>此方法线程不安全</para>
        /// </summary>
        public static void Register(DataType dbType, string connectionString, bool autoSyncStructure, string key = null)
        {
            DbContext context = Nested._context;

            IFreeSql db = context.Build(dbType, connectionString, autoSyncStructure);

            if (key == null)
            {
                if (context._default == null)
                    context._default = db;
            }
            else
            {
                var dict = context._dict;
                if (!dict.ContainsKey(key))
                {
                    context._dict.Add(key, db);

                    if (context._default == null)
                        context._default = db;
                }
            }
        }

        private IFreeSql Build(DataType dbType, string connectionString, bool autoSyncStructure)
        {
            IFreeSql _db = new FreeSqlBuilder()
                        .UseConnectionString(dbType, connectionString)
                        .UseAutoSyncStructure(autoSyncStructure)
                        .Build();

            _db.Aop.CurdBefore += Aop_CurdBefore;
            _db.Aop.CurdAfter += Aop_CurdAfter;
            _db.Aop.SyncStructureAfter += Aop_SyncStructureAfter;

            return _db;
        }

        ILog _log = LogManager.GetLogger("FSQL");

        private void Aop_CurdBefore(object sender, CurdBeforeEventArgs e)
        {
            if (e.DbParms.Length == 0)
            {
                _log.Debug($"SQL 语句\r\n{e.Sql}\r\n");
            }
            else
            {
                _log.Debug($"SQL 语句\r\n{e.Sql}\r\n参数：\r\n - {string.Join("\r\n - ", e.DbParms.Where(pm => pm != null).Select(pm => $"{pm.ParameterName}: {pm.Value}"))}\r\n");
            }
        }

        private void Aop_CurdAfter(object sender, CurdAfterEventArgs e)
        {
            if (e.Exception != null)
            {
                _log.Error("SQL 语句执行出错。", e.Exception);
            }
        }

        private void Aop_SyncStructureAfter(object sender, SyncStructureAfterEventArgs e)
        {
            if (e.Sql != null)
            {
                _log.Debug($"建表语句\r\n{e.Sql}\r\n");
            }

            if (e.Exception != null)
            {
                _log.Error("建表语句执行出错。", e.Exception);
            }
        }

        public static IFreeSql DefaultDB
        {
            get { return Nested._context._default; }
        }

        public static IFreeSql GetDB(string key)
        {
            DbContext context = Nested._context;
            if (context._dict.ContainsKey(key))
                return context._dict[key];
            else
                return null;
        }

        private class Nested
        {
            static Nested()
            {

            }

            internal static readonly DbContext _context = new DbContext();
        }

        private DbContext()
        {

        }

        public static (bool, string) TryAction(Action<IFreeSql> action)
        {
            if (DefaultDB == null) return (false, "IFreeSql 未注册。");

            return _tryAction(DefaultDB, action);
        }

        public static (bool, string) TryAction(string key, Action<IFreeSql> action)
        {
            IFreeSql db = GetDB(key);
            if (db == null) return (false, $"名为“{key}”的 IFreeSql 未注册。");

            return _tryAction(db, action);
        }

        private static (bool, string) _tryAction(IFreeSql db, Action<IFreeSql> action)
        {
            if (action == null) return (true, string.Empty);

            try
            {
                action(db);

                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        /// <summary>
        /// 对应有返回值无参数的方法
        /// </summary>
        public static (bool, string, TResult) TryFunc<TResult>(Func<IFreeSql, TResult> func)
        {
            if (DefaultDB == null) return (false, "IFreeSql 未注册。", default(TResult));

            return _tryFunc(DefaultDB, func);
        }

        public static (bool, string, TResult) TryFunc<TResult>(string key, Func<IFreeSql, TResult> func)
        {
            IFreeSql db = GetDB(key);
            if (db == null) return (false, $"名为“{key}”的 IFreeSql 未注册。", default(TResult));

            return _tryFunc(db, func);
        }

        private static (bool, string, TResult) _tryFunc<TResult>(IFreeSql db, Func<IFreeSql, TResult> func)
        {
            if (func == null) return (true, string.Empty, default(TResult));

            try
            {
                TResult retval = func(db);

                return (true, string.Empty, retval);
            }
            catch (Exception e)
            {
                return (false, e.Message, default(TResult));
            }
        }

        /// <summary>
        /// 对应有返回值，并且有一个参数的方法
        /// </summary>
        /// <typeparam name="T">参数类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="param">参数</param>
        public static (bool, string, TResult) TryFunc<T, TResult>(Func<IFreeSql, T, TResult> func, T param)
        {
            if (DefaultDB == null) return (false, "IFreeSql 未注册。", default(TResult));

            return _tryFunc(DefaultDB, func, param);
        }

        public static (bool, string, TResult) TryFunc<T, TResult>(string key, Func<IFreeSql, T, TResult> func, T param)
        {
            IFreeSql db = GetDB(key);
            if (db == null) return (false, $"名为“{key}”的 IFreeSql 未注册。", default(TResult));

            return _tryFunc(DefaultDB, func, param);
        }

        private static (bool, string, TResult) _tryFunc<T, TResult>(IFreeSql db, Func<IFreeSql, T, TResult> func, T param)
        {
            if (func == null) return (true, string.Empty, default(TResult));

            try
            {
                TResult retval = func(db, param);

                return (true, string.Empty, retval);
            }
            catch (Exception e)
            {
                return (false, e.Message, default(TResult));
            }
        }

        public static (bool, string) TryActionWithTransaction(params Action<IFreeSql>[] actions)
        {
            if (DefaultDB == null) throw new Exception("IFreeSql 未注册。");

            return _tryActionWithTransaction(DefaultDB, actions);
        }

        public static (bool, string) TryActionWithTransaction(string key, params Action<IFreeSql>[] actions)
        {
            IFreeSql db = GetDB(key);
            if (db == null) return (false, $"名为“{key}”的 IFreeSql 未注册。");

            return _tryActionWithTransaction(db, actions);
        }

        private static (bool, string) _tryActionWithTransaction(IFreeSql db, params Action<IFreeSql>[] actions)
        {
            if (actions == null) return (true, string.Empty);

            try
            {
                db.Transaction(() =>
                {
                    foreach (var action in actions)
                        action(db);
                });

                return (true, string.Empty);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
        }

        public static (bool, string, TResult) TryFuncWithTransaction<TResult>(Func<IFreeSql, TResult> func)
        {
            if (DefaultDB == null) return (false, "IFreeSql 未注册。", default(TResult));

            return _tryFuncWithTransaction(DefaultDB, func);
        }

        public static (bool, string, TResult) TryFuncWithTransaction<TResult>(string key, Func<IFreeSql, TResult> func)
        {
            IFreeSql db = GetDB(key);
            if (db == null) return (false, $"名为“{key}”的 IFreeSql 未注册。", default(TResult));

            return _tryFuncWithTransaction(db, func);
        }

        private static (bool, string, TResult) _tryFuncWithTransaction<TResult>(IFreeSql db, Func<IFreeSql, TResult> func)
        {
            if (func == null) return (true, string.Empty, default(TResult));

            try
            {
                TResult retval = default;
                db.Transaction(() =>
                {
                    retval = func(db);
                });

                return (true, string.Empty, retval);
            }
            catch (Exception e)
            {
                return (false, e.Message, default(TResult));
            }
        }
    }
}
