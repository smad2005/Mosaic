using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace laba3
{
    public static class BD
    {
        public static DataSet ds;
        public static string Path="Players.xml";
        static BD()
        {
            ds = new DataSet("Smadstat");
            DataTable table = new DataTable();
            ds.Tables.Add(table);
            DataColumn col = new DataColumn("name");
            col.DataType = System.Type.GetType("System.String");
            table.Columns.Add(col);
            col = new DataColumn("step");
            col.DataType = System.Type.GetType("System.Int32");
            table.Columns.Add(col);
            col = new DataColumn("Time");
            col.DataType = System.Type.GetType("System.TimeSpan");
            table.Columns.Add(col);
            Load();
          
        }

        public static void Load()
        {
            try
            {
                ds.ReadXml(Path);
            }
            catch { }
        }

        private static void Save()
        {
            ds.WriteXml(Path);
        }

        public static void addNewPlayer(string name, int step, TimeSpan time)
        {
            DataRow row = ds.Tables[0].NewRow();
            row[0] = name;
            row[1] = step;
            row[2] = time;
            ds.Tables[0].Rows.Add(row);
            ds.AcceptChanges();
            Save();
        }


    }
}
