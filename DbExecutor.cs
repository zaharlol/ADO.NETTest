using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace ADO.NETTest
{
    public class DbExecutor
    {
        private MainConnector connector;
        public DbExecutor(MainConnector connector)
        {
            this.connector = connector;
        }

        public DataTable SelectAll(string table)
        {
            var selectcommandtext = "select * from " + table;
            var adapter = new SqlDataAdapter(
              selectcommandtext,
              connector.GetConnection()
            );

            var ds = new DataSet();
            adapter.Fill(ds);

            return ds.Tables[0];
        }

        public SqlDataReader SelectAllCommandReader(string table)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "select * from " + table,
                Connection = connector.GetConnection(),
            };

            SqlDataReader reader = command.ExecuteReader();

            var columnList = new List<string>();

            for (int i = 0; i < reader.FieldCount; i++)
            {
                var name = reader.GetName(i);
                columnList.Add(name);
            }

            for (int i = 0; i < columnList.Count; i++)
            {
                Console.Write($"{columnList[i]}\t");
            }
            Console.WriteLine();

            while (reader.Read())
            {
                for (int i = 0; i < columnList.Count; i++)
                {
                    var value = reader[columnList[i]];
                    Console.Write($"{value}\t");
                }

                Console.WriteLine();
            }

            if (reader.HasRows)
            {
                return reader;
            }

            return null;
        }

        public int DeleteByColumn(string table, string column, string value)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "delete from " + table + " where " + column + " = '" + value + "';",
                Connection = connector.GetConnection(),
            };

            return command.ExecuteNonQuery();

        }

        public int UpdateByColumn(string table, string columntocheck, string valuecheck, string columntoupdate, string valueupdate)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.Text,
                CommandText = "update   " + table + " set " + columntoupdate + " = '" + valueupdate + "'  where " + columntocheck + " = '" + valuecheck + "';",
                Connection = connector.GetConnection(),
            };

            return command.ExecuteNonQuery();

        }

        public int ExecProcedureAdding(string name, string login)
        {
            var command = new SqlCommand
            {
                CommandType = CommandType.StoredProcedure,
                CommandText = "AddingUserProc",
                Connection = connector.GetConnection(),
            };

            command.Parameters.Add(new SqlParameter("@Name", name));
            command.Parameters.Add(new SqlParameter("@Login", login));

            return command.ExecuteNonQuery();

        }
    }
}

