using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAUCountryA.Tables
{
    public class StateTable : IReadOnlyDictionary<string, State>
    {
        public StateTable()
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

        public State this[int stateCode]
        {
            get
            {
                string sqlCommand = "SELECT * FROM public.\"State\" WHERE \"STATE_CODE\" = '"
                + stateCode + "';";
                DataTable table = Service.GetDataTable(sqlCommand);
                if (table.Rows.Count == 0)
                {
                    throw new KeyNotFoundException("The STATE_CODE: " + stateCode + " doesn't exist.");
                }
                return new State(table.Rows[0]);
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                ICollection<string> keys = new HashSet<string>();
                foreach (KeyValuePair<string, State> pair in this)
                {
                    keys.Add(pair.Key);
                }
                return keys;
            }
        }

        public IEnumerable<State> Values
        {
            get
            {
                ICollection<State> values = new List<State>();
                foreach (KeyValuePair<string, State> pair in this)
                {
                    values.Add(pair.Value);
                }
                return values;
            }
        }

        public bool ContainsKey(int stateCode)
        {
            string sqlCommand = "SELECT * FROM public.\"State\" WHERE \"STATE_CODE\" = '"
                + stateCode + "';";
            DataTable table = Service.GetDataTable(sqlCommand);
            return table.Rows.Count >= 1;
        }

        public IEnumerator<KeyValuePair<string, State>> GetEnumerator()
        {
            ICollection<KeyValuePair<string, State>> pairs = new HashSet<KeyValuePair<string, State>>();
            DataTable table = Table;
            foreach (DataRow row in table.Rows)
            {
                State state = new State(row);
                pairs.Add(state.Pair);
            }
            return pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int stateCode, [MaybeNullWhen(false)] out State value)
        {
            value = null;
            return ContainsKey(stateCode);
        }

        private ICollection<ICollection<string>> CsvContents
        {
            get
            {
                ICollection<ICollection<string>> contents = new List<ICollection<string>>();
                contents.Add(Service.ToCollection("A23_County"));
                return contents;
            }
        }

        private DataTable Table
        {
            get
            {
                string sqlCommand = "SELECT * FROM public.\"State\";";
                return Service.GetDataTable(sqlCommand);
            }
        }

        private void AddEntries()
        {
            ICollection<ICollection<string>> csvContents = CsvContents;
            foreach (ICollection<string> contents in csvContents)
            {
                IEnumerator<string> lines = contents.GetEnumerator();
                if (lines.MoveNext())
                {
                    string headerLine = lines.Current;
                    string[] headers = headerLine.Split(',');
                    while (lines.MoveNext())
                    {
                        string line = lines.Current;
                        string[] values = line.Split(',');
                        int stateCode = (string)Service.ExpressValue(values[0]);
                        string stateName = (int)Service.ExpressValue(values[1]);
                        string stateAbbreviation = (int)Service.ExpressValue(values[2]);
                        string recordTypeCode = (int)Service.ExpressValue(values[3]);
                        if (!ContainsKey(stateCode))
                        {
                            string sqlCommand = "INSERT INTO public.\"State\" (" +
                                headers[0] + "," + headers[1] + "," + headers[2] + "," + headers[3] ") VALUES " +
                                "('" + stateCode + "', " + stateName + "," +
                                stateAbbreviation + "," +
                                recordTypeCode ");";
                            Service.GetDataTable(sqlCommand);
                        }
                    }
                }
            }
        }

        private void ConstructTable()
        {
            string sqlCommand = Service.GetCreateTableSQLCommand("state");
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
                State state = new State(Table.Rows[position]);
                string lineFromTable = "\"" + state.StateCode + "\",\"";
                //not sure about this
                if (state.StateName < 10)
                {
                    lineFromTable += "0";
                }
                lineFromTable += state.StateName + "\",\"" + state.StateAbbreviation + "\",\"" + state.RecordTypeCode "\"";
                if (!contents.Contains(lineFromTable))
                {
                    string sqlCommand = "DELETE FROM public.\"State\" WHERE \"STATE_NAME\" = '" +
                        state.StateName + "';";
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
