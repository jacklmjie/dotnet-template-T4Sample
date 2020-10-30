using System.ComponentModel.DataAnnotations;
using Dapper.Contrib.Extensions;

namespace T4Sample
{
    public class ColumnInfo
    {
        public ColumnInfo()
        {

        }
        public ColumnInfo(DBColumnInfo dBColumnInfo)
        {
            Name = dBColumnInfo.COLUMN_NAME;
            Type = dBColumnInfo.DATA_TYPE;
            IsPrimary = dBColumnInfo.COLUMN_KEY == "PRI";
            NotNull = dBColumnInfo.IS_NULLABLE == "NO";
            Description = dBColumnInfo.COLUMN_COMMENT;
            IsIdentity = dBColumnInfo.EXTRA == "auto_increment";
        }
        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsPrimary { get; set; }

        public bool NotNull { get; set; }

        public string Description { get; set; }

        public bool IsIdentity { get; set; }
    }


    [Table("COLUMNS")]
    public class DBColumnInfo
    {
        public string COLUMN_NAME { get; set; }

        public string DATA_TYPE { get; set; }

        public string COLUMN_KEY { get; set; }

        public string IS_NULLABLE { get; set; }

        public string COLUMN_COMMENT { get; set; }

        public string EXTRA { get; set; }
    }
}
