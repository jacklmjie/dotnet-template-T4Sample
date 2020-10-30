using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using MySql.Data.MySqlClient;

namespace T4Sample
{
    class Program
    {
        static void Main()
        {
            // DB Type => .NET Type
            var typeDef = new Dictionary<string, string>()
            {
                { "integer", "int" },
                { "varchar", "string" },
                { "date", "DateTime" },
            };
            var output = new Output
            {
                Path = @"D:\\Tools",
                Mode = CreateMode.Full,
                Extension = ".cs"
            };

            var connstr = "server=172.28.66.48;port=5306;database=yallaactivity;uid=yalla_d_dzs;pwd={-i(a1.1Qezs^tG;";
            using (IDbConnection conn = new MySqlConnection(connstr))
            {
                conn.Open();
                // Select Database Table
                var sql = @"SELECT TABLE_NAME,TABLE_COMMENT
                            from `information_schema`.`TABLES`
                            where TABLE_SCHEMA=@Database;";
                var listTable = conn.Query<DBTableInfo>(sql, new { conn.Database });

                foreach (var t in listTable)
                {
                    //Select Database Columns
                    sql = @"SELECT COLUMN_NAME,DATA_TYPE,COLUMN_KEY,IS_NULLABLE,COLUMN_COMMENT,EXTRA
                            from `information_schema`.`COLUMNS`
                            where TABLE_NAME=@TableName
                            ORDER BY ORDINAL_POSITION;";
                    var listColumns = conn.Query<DBColumnInfo>(sql, new { TableName = t.TABLE_NAME });
                    var table = new TableInfo(t)
                    {
                        Columns = listColumns.Select(x => new ColumnInfo(x))
                    };

                    ITextTemplate template = new TableEntityTemplate(typeDef, "MyNameSpace", table);
                    var txt = template.TransformText();
                    Console.WriteLine(txt);
                    new FileOutput().Output(txt, t.TABLE_NAME.ToPascalCase() + "M", output);
                }
            }
        }
    }
}
