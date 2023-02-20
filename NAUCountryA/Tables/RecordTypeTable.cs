using System.Collections.Specialized;
using NAUCountryA.Models;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Collections;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class RecordTypeTable : IReadOnlyDictionary<string, RecordType>
    {
        private readonly NpgsqlConnection connection;

        public RecordTypeTable(User user)
        {
            connection = user.Connection;
            ConstructTable();
        }

        public int Count
        {
            get
            {
                return 0;
            }
        }

        public RecordType this[string key]
        {
            get
            {
                return null;
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return null;
            }
        }

        public IEnumerable<RecordType> Values
        {
            get
            {
                return null;
            }
        }

        public bool ContainsKey(string key)
        {
            return false;
        }

        public IEnumerator<KeyValuePair<string,RecordType>> GetEnumerator()
        {
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out RecordType value)
        {
            throw new NotImplementedException();
        }

        private ICollection<ICollection<string>> CsvContents
        {
            get
            {
                ICollection<ICollection<string>> contents = new List<ICollection<string>>();
                contents.Add(Service.ToCollection("A23_Commodity"));
                contents.Add(Service.ToCollection("A23_County"));
                contents.Add(Service.ToCollection("A23_Practice"));
                contents.Add(Service.ToCollection("A23_STATE"));
                contents.Add(Service.ToCollection("A23_Type"));
                return contents;
            }
        }

        private void AddEntries()
        {

        }

        private void ConstructTable()
        {
            string sqlCommand = Service.GetCreateTableSQLCommand("record_type");
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, connection);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}