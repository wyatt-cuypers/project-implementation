using NAUCountryA.Models;
using Npgsql;
using System.Collections;
using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace NAUCountryA.Tables
{
    public class OfferTable : IReadOnlyDictionary<int, Offer>
    {
        public OfferTable()
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

        public Offer this[int offerID]
        {
            get
            {
                string sqlCommand = "SELECT * FROM public.\"Offer\" WHERE \"ADM_INSURANCE_OFFER_ID\" = '"
                + offerID + "';";
                DataTable table = Service.GetDataTable(sqlCommand);
                if (table.Rows.Count == 0)
                {
                    throw new KeyNotFoundException("The ADM_INSURANCE_OFFER_ID: " + offerID + " doesn't exist.");
                }
                return new Offer(table.Rows[0]);
            }
        }

        public IEnumerable<int> Keys
        {
            get
            {
                ICollection<int> keys = new HashSet<int>();
                foreach (KeyValuePair<int, Offer> pair in this)
                {
                    keys.Add(pair.Key);
                }
                return keys;
            }
        }

        public IEnumerable<Offer> Values
        {
            get
            {
                ICollection<Offer> values = new List<Offer>();
                foreach (KeyValuePair<int, Offer> pair in this)
                {
                    values.Add(pair.Value);
                }
                return values;
            }
        }

        public bool ContainsKey(int offerID)
        {
            string sqlCommand = "SELECT * FROM public.\"Offer\" WHERE \"ADM_INSURANCE_OFFER_ID\" = '"
                + offerID + "';";
            DataTable table = Service.GetDataTable(sqlCommand);
            return table.Rows.Count >= 1;
        }

        public IEnumerator<KeyValuePair<int, Offer>> GetEnumerator()
        {
            ICollection<KeyValuePair<int, Offer>> pairs = new HashSet<KeyValuePair<int, Offer>>();
            DataTable table = Table;
            foreach (DataRow row in table.Rows)
            {
                Offer offer = new Offer(row);
                pairs.Add(offer.Pair);
            }
            return pairs.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public bool TryGetValue(int offerID, [MaybeNullWhen(false)] out Offer value)
        {
            value = null;
            return ContainsKey(offerID);
        }

        private ICollection<ICollection<string>> CsvContents
        {
            get
            {
                ICollection<ICollection<string>> contents = new List<ICollection<string>>();
                contents.Add(Service.ToCollection("A22_Price"));
                return contents;
            }
        }

        private DataTable Table
        {
            get
            {
                string sqlCommand = "SELECT * FROM public.\"Offer\";";
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
                        int offerID = (int)Service.ExpressValue(values[0]);
                        int practiceCode = (int)Service.ExpressValue(values[2]);
                        int countyCode = (int)Service.ExpressValue(values[3]);
                        int typeCode = (int)Service.ExpressValue(values[4]);
                        int irrigationPracticeCode = (int)Service.ExpressValue(values[5]);
                        if (!ContainsKey(offerID))
                        {
                            string sqlCommand = "INSERT INTO public.\"Offer\" (" +
                                headers[0] + "," + headers[2] + "," + headers[3] + "," + headers[4] + "," + headers[5] ") VALUES " +
                                "('" + offerID + "', " + practiceCode + "," +
                                countyCode + "," + typeCode + "," + irrigationPracticeCode + ");";
                            Service.GetDataTable(sqlCommand);
                        }
                    }
                }
            }
        }

        private void ConstructTable()
        {
            string sqlCommand = Service.GetCreateTableSQLCommand("offer");
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
                    contents.Add(values[0] + "," + values[2] + "," + values[3] + "," + values[4] + "," + values[5]);
                }
            }
            int position = 0;
            while (position < Count)
            {
                Offer offer = new Offer(Table.Rows[position]);
                string lineFromTable = "\"";
                if (offer.OfferID < 10)
                {
                    lineFromTable += "0000000";
                }
                else if (offer.OfferID < 100)
                {
                    lineFromTable += "000000";
                }
                else if (offer.OfferID < 1000)
                {
                    lineFromTable += "00000";
                }
                else if (offer.OfferID < 10000)
                {
                    lineFromTable += "0000";
                }
                else if (offer.OfferID < 100000)
                {
                    lineFromTable += "000";
                }
                else if (offer.OfferID < 1000000)
                {
                    lineFromTable += "00";
                }
                else if (offer.OfferID < 10000000)
                {
                    lineFromTable += "0";
                }
                lineFromTable += offer.OfferID + "\",\"" + offer.PracticeCode + "\",\"" + offer.CountyCode + "\",\"" + offer.TypeCode + "\",\"" + offer.IrrigationPracticeCode "\"";
                if (!contents.Contains(lineFromTable))
                {
                    string sqlCommand = "DELETE FROM public.\"Offer\" WHERE \"ADM_INSURANCE_OFFER_ID\" = '" +
                        offer.OfferID + "';";
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