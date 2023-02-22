using NAUCountryA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA.Tables
{
    public class TypeTable : IReadOnlyDictionary<string, Type>
    {
        public TypeTable()
        {
            ConstructTable();
            TrimEntries();
            AddEntries();
        }

        public int Count
        {
            get
            {
                return Table.Rows.Count;
            }
        }

        public Type this[string typeCode]
        {
            get
            {
                string sqlCommand = "SELECT * FROM public.\"Type\" WHERE \"TYPE_CODE\" = '"
                    + typeCode + "';";
                DataTable table = Service.GetDataTable(sqlCommand);
                if (table.Rows.Count == 0)
                {
                    throw new KeyNotFoundException("The TYPE_CODE: " + stateCode + " doesn't exist.");
                }
                return new Type(table.Rows[0]);

            }

        public IEnumerable<string> Keys
        {
            get
            {
                ICollection<string> keys = new HashSet<string>();
                foreach (KeyValuePair<string, Type> pair in this)
                {
                    keys.Add(pair.Key);
                }
                return keys;
            }
        }

        public IEnumerable<Type> Values
        {
            get
            {
                ICollection<Type> values = new List<Type>();
                foreach (KeyValuePair<string, Type> pair in this)
                {
                    values.Add(pair.Value);
                }
                return values;
            }
        }

        public bool ContainsKey(int typeCode)
        {
            string sqlCommand = "SELECT * FROM public.\"Type\" WHERE \"TYPE_CODE\" = '"
                + TypeCode + "';";
            DataTable table = Service.GetDataTable(sqlCommand);
            return table.Rows.Count >= 1;
        }

        public IEnumerator<KeyValuePair<string, Type>> GetEnumerator()
        {
            ICollection<KeyValuePair<string, Type>> pairs = new HashSet<KeyValuePair<string, Type>>();
            DataTable table = Table;
            foreach (DataRow row in table.Rows)
            {
                Type type = new Type(row);
                pairs.Add(type.Pair);
            }
            return pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int typeCode, [MaybeNullWhen(false)] out Type value)
        {
            value = null;
            return ContainsKey(typeCode);
        }

        private ICollection<ICollection<string>> CsvContents
        {
            get
            {
                ICollection<ICollection<string>> contents = new List<ICollection<string>>();
                contents.Add(Service.ToCollection("A23_INSURANCE_OFFER"));
                return contents;
            }
        }

        private DataTable Table
        {
            get
            {
                string sqlCommand = "SELECT * FROM public.\"Type\";";
                return Service.GetDataTable(sqlCommand);
            }
        }

        private void AddEntries()
        {
            ICollection<ICollection<string>> csvContents = CsvContents;
            foreach (ICollection<string> contents in csvContents)
            {
                IEnumerator<string> lines = contents.GetEnumerator();
                if(lines.MoveNext())
                {
                    string headerLine = lines.Current;
                    string[] headers = headerLine.Split(',');
                    while (lines.MoveNext())
                    {
                        string line = lines.Current;
                        string[] values = line.Split(",");
                        int typeCode = (int)Service.ExpressValue(values[0]);
                        string typeName = (string)Service.ExpressValue(values[1]);
                        string typeAbbreviation = (string)Service.ExpressValue(values[2]);
                        Commodity commodity = (Commodity)Service.ExpressValue(values[3]); 
                        DateTime releasedDate = (DateTime)Service.ExpressValue(values[4]);
                        string recordType = (string)Service.ExpressValue(values[5]);
                        if(!ContainsKey(typeCode))
                        {
                            string sqlCommand = "INSERT INTO public.\"State\" (" +
                                 headers[0] + "," + headers[1] + "," + headers[2] + "," + headers[3] + "," + headers[4] + "," + headers[5] ") VALUES " +
                                "('" + typeCode + "', " + typeName + "," +
                                typeAbbreviation + "," + commodity + "," + releasedDate +
                                "," + recordType ");";
                            Service.GetDataTable(sqlCommand);
                        }
                    }
                }
            }
        }

        private void ConstructTable()
        {
            string sqlCommand = Service.GetCreateTableSQLCommand("type");
            NpgsqlCommand cmd = new NpgsqlCommand(sqlCommand, Service.User.Connection);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }

        private void TrimEntries()
        {
            ICollection<string> contents = new HashSet<string>();
            foreach (ICollection<string> contents1 in CsvContents)
            {
                foreach (string line in contents1)
                {
                    string[] values = line.Split(',');
                    contents.Add(values[0] + "," + values[1] + "," + values[2] + "," + values[3]);
                }
            }
            int position = 0;
            while (position < Count)
            {
                Type type = new Models.Type(Table.Rows[position]);
                string lineFromTable = "\"" + type.TypeCode + "\"";
                if(type.TypeName < 10)
                {
                    lineFromTable += "0";
                }
                lineFromTable += type.TypeName + "\",\"" + type.TypeAbbreviation + 
                    "\",\"" + type.Commodity + "\",\"" + type.ReleasedDate 
                    + "\",\"" + type.RecordType "\"";
                if(!contents.Contains(lineFromTable))
                {
                    string sqlCommand = "DELETE FROM public.\"Type\" WHERE \"TYPE_NAME\" = '" +
                        type.TypeName + "';";
                    Service.GetDataTable(sqlCommand);
                }
                else
                {
                    position++;
                }
            }

        }

    }
}
