using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESAbstractions.Dapper.Contrib
{
    /// <summary>
    /// Thesse attributes are related to database 
    /// e.g. table, column, required (not null) , keys etc
    /// </summary>
    public class DbAttributes
    {
        /// <summary>
        /// Marks attribute is required
        /// In database it is specified as not null
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class RequiredAttribute : Attribute
        {
        }

        /// <summary>
        /// Attribute which marks property as DB column in table
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class ColumnAttribute : Attribute
        {
            //
            // Summary:
            //     Name of the column
            public string Name { get; private set; }

            //
            // Summary:
            //     Optional Column attribute.
            //
            // Parameters:
            //   columnName:
            public ColumnAttribute(string columnName)
            {
                Name = columnName;
            }
        }

        /// <summary>
        /// This attribute denotes that this class is represented as database table
        /// </summary>
        [AttributeUsage(AttributeTargets.Class)]
        public class TableAttribute : Attribute
        {
            /// <summary>
            /// Optional Table attribute.
            /// </summary>
            /// <param name="tableName"></param>
            public TableAttribute(string tableName)
            {
                Name = tableName;
            }
            /// <summary>
            /// Name of the table
            /// </summary>
            public string Name { get; private set; }
            /// <summary>
            /// Name of the schema
            /// </summary>
            public string Schema { get; set; }
        }

        /// <summary>
        /// Specifies field as Primary key
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class KeyAttribute : Attribute
        {
        }
    }
}
