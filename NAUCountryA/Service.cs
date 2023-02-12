using Microsoft.VisualBasic.FileIO;
using Npgsql;
using System.Collections.Generic;
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

        public static ICollection<string> ToCollection(string csvFileName)
        {
            ICollection<string> lines = new List<string>();
            TextFieldParser csvParcer = new TextFieldParser(csvFileName);
            csvParcer.TextFieldType = FieldType.Delimited;
            while (!csvParcer.EndOfData)
            {
                lines.Add(csvParcer.ReadLine());
            }
            csvParcer.Close();
            return lines;
        }
    }
}
