using Npgsql;
namespace NAUCountryA
{
    public class Service
    {
        public static NpgsqlConnection Connection
        {
            get
            {
                string connectionString = "Server=localhost;Port=2023;User Id=postgres;" 
                    + "Password=naucountrydev;Database=NAUCountryData";
                NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();
                connection.Close();
                return connection;
            }
        }
    }
}
