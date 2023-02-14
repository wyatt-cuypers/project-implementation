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

        public static string InitialPathLocation
        {
            get
            {
                return GetInitialPathLocation(Path.GetFullPath("."));
            }
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
