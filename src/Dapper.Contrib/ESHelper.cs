using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


using Microsoft.CSharp.RuntimeBinder;

using static ESAbstractions.Dapper.Contrib.DbAttributes;

namespace ESAbstractions.Dapper.Contrib
{



    public class ESHelper
    {

        public enum Dialect
        {
            SQLServer,
            PostgreSQL,
            SQLite,
            MySQL,
            Oracle,
            DB2
        }
        public static string GetDialect()
        {
            return _dialect.ToString();
        }
        public interface ITableNameResolver
        {
            string ResolveTableName(Type type);
        }

        public interface IColumnNameResolver
        {
            string ResolveColumnName(PropertyInfo propertyInfo);
        }
        private static string Encapsulate(string databaseword)
        {
            return string.Format(_encapsulation, databaseword);
        }
        private static string _encapsulation;

        public class TableNameResolver : ITableNameResolver
        {
            public virtual string ResolveTableName(Type type)
            {
                string result = ((!(GetDialect() == Dialect.DB2.ToString())) ? Encapsulate(type.Name) : type.Name);
                dynamic val = type.GetCustomAttributes(inherit: true).SingleOrDefault((object attr) => attr.GetType().Name == typeof(TableAttribute).Name);
                if (val != null)
                {
                    result = ESHelper.Encapsulate(val.Name);
                    try
                    {
                        if ((!string.IsNullOrEmpty(val.Schema)))
                        {
                            string arg = ESHelper.Encapsulate(val.Schema);
                            result = $"{arg}.{result}";
                            return result;
                        }

                        return result;
                    }
                    catch (RuntimeBinderException)
                    {
                        return result;
                    }
                }

                return result;
            }
        }

        public class ColumnNameResolver : IColumnNameResolver
        {
            public virtual string ResolveColumnName(PropertyInfo propertyInfo)
            {
                string result = ((!(GetDialect() == Dialect.DB2.ToString())) ? Encapsulate(propertyInfo.Name) : propertyInfo.Name);
                dynamic val = propertyInfo.GetCustomAttributes(inherit: true).SingleOrDefault((object attr) => attr.GetType().Name == typeof(ColumnAttribute).Name);
                if (val != null)
                {
                    result = ESHelper.Encapsulate(val.Name);
                    _ = Debugger.IsAttached;
                }

                return result;
            }
        }

        private static Dialect _dialect;
        private static ITableNameResolver _tableNameResolver = new TableNameResolver();

        public static void SetTableNameResolver(ITableNameResolver resolver)
        {
            _tableNameResolver = resolver;
        }
    }
}
