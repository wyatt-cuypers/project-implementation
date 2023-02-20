using Microsoft.VisualBasic.FileIO;
using NAUCountryA.Exceptions;
using Npgsql;
using System.Data;
using System.Data.Common;
namespace NAUCountryA
{
    public class Service
    {
        public static string CreateDatabaseSQLCommand
        {
            get
            {
                string sqlCommand = "";
                string filePath = InitialPathLocation + "\\NAUCountryA\\Resources\\database_construction.sql";
                TextFieldParser sqlParcer = new TextFieldParser(filePath);
                while (!sqlParcer.EndOfData)
                {
                    sqlCommand += sqlParcer.ReadLine();
                }
                return sqlCommand;
            }
        }
        public static string InitialPathLocation
        {
            get
            {
                return GetInitialPathLocation(Path.GetFullPath("."));
            }
        }

        public static object ExpressValue(string value)
        {
            string temp = value.Substring(1, value.Length - 2);
            try
            {
                return Convert.ToInt32(temp);
            }
            catch (FormatException)
            {
                return temp;
            }
        }

        public static string GetCreateTableSQLCommand(string tableName)
        {
            string sqlCommand = "";
            string filePath = InitialPathLocation + "\\NAUCountryA\\Resources\\" + tableName.ToLower() + "_construction.sql";
            TextFieldParser sqlParcer = new TextFieldParser(filePath);
            while (!sqlParcer.EndOfData)
            {
                sqlCommand += sqlParcer.ReadLine();
            }
            return sqlCommand;
        }
        public static DataTable GetDataTable(string sqlCommand, NpgsqlConnection connection)
        {
            DataTable table = new DataTable();
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
            DbDataAdapter adapter = new NpgsqlDataAdapter(cmd);
            string command = sqlCommand.Substring(0, sqlCommand.IndexOf(' ')).ToUpper();
            switch (command)
            {
                case "DELETE":
                    adapter.DeleteCommand = cmd;
                    adapter.DeleteCommand.Connection.Open();
                    adapter.DeleteCommand.ExecuteNonQuery();
                    adapter.Update(table);
                    adapter.DeleteCommand.Connection.Close();
                    break;
                case "INSERT":
                    adapter.InsertCommand = cmd;
                    adapter.InsertCommand.Connection.Open();
                    adapter.InsertCommand.ExecuteNonQuery();
                    adapter.Update(table);
                    adapter.InsertCommand.Connection.Close();
                    break;
                case "SELECT":
                    adapter.SelectCommand = cmd;
                    adapter.SelectCommand.Connection.Open();
                    adapter.SelectCommand.ExecuteNonQuery();
                    adapter.Fill(table);
                    adapter.SelectCommand.Connection.Close();
                    break;
                case "UPDATE":
                    adapter.UpdateCommand = cmd;
                    adapter.UpdateCommand.Connection.Open();
                    adapter.UpdateCommand.ExecuteNonQuery();
                    adapter.Update(table);
                    adapter.UpdateCommand.Connection.Close();
                    break;
                default: throw new UnrecognizedSQLCommandException("The command isn't recognized in PostgreSQL.");
            }
            return table;
        }

        public static ICollection<string> ToCollection(string csvFileName)
        {
            ICollection<string> lines = new List<string>();
            string filePath = InitialPathLocation + "\\NAUCountryA\\Resources\\" + csvFileName + ".csv";
            TextFieldParser csvParcer = new TextFieldParser(filePath);
            csvParcer.TextFieldType = FieldType.Delimited;
            while (!csvParcer.EndOfData)
            {
                lines.Add(csvParcer.ReadLine());
            }
            csvParcer.Close();
            return lines;
        }

        private static string GetInitialPathLocation(string currentLocation)
        {
            DirectoryInfo temp = Directory.GetParent(currentLocation); 
            if (temp.Name.Equals("project-implementation"))
            {
                return temp.FullName;
            }
            return GetInitialPathLocation(temp.FullName);
        }
    }
}
