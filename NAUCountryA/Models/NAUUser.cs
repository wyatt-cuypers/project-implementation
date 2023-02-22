using Npgsql;
using System.Data;


namespace NAUCountryA.Models
{
    public class NAUUser
    {
        private string connectionString;
        public NAUUser(string serverName, int portNumber, string userId, string password)
        {
            connectionString = "Server=" + serverName + ";Port=" + 
                portNumber + ";User Id=" + userId +";Password=" + password;
            CreateDatabase();
        }

        public NpgsqlConnection Connection
        {
            get;
            private set;
        }

        private void CreateDatabase()
        {
            try
            {
                Connection = new NpgsqlConnection(connectionString);
                Connection.Open();
                NpgsqlCommand createDatabaseCmd = new NpgsqlCommand(Service.CreateDatabaseSQLCommand, Connection);
                createDatabaseCmd.ExecuteNonQuery();
                if (createDatabaseCmd.Connection.State == ConnectionState.Open)
                {
                    createDatabaseCmd.Connection.Close();
                }
            }
            catch (PostgresException)
            {
                if (Connection.State == ConnectionState.Open)
                {
                    Connection.Close();
                }
            }
            finally
            {
                Connection.Open();
                Connection.ChangeDatabase("NAUCountryData");
                Connection.Close();
            }
        }
    }
}