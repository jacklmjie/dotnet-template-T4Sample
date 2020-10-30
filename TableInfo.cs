using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace T4Sample
{
    public class TableInfo
    {
        public TableInfo()
        {

        }

        public TableInfo(DBTableInfo dBTableInfo)
        {
            Name = dBTableInfo.TABLE_NAME;
            Description = dBTableInfo.TABLE_COMMENT;
        }

        public string Name { get; set; }

        public IEnumerable<ColumnInfo> Columns { get; set; }

        public string Description { get; set; }
    }

    [Table("TABLES")]
    public class DBTableInfo
    {
        public string TABLE_NAME { get; set; }

        public string TABLE_COMMENT { get; set; }
    }
}
