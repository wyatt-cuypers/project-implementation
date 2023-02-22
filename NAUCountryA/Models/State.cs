using NAUCountryA.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace NAUCountryA.Models
{
    public class State : IEquatable<State>
    {

        public State(int stateCode, string stateName, string stateAbbreviation, string recordTypeCode) 
        {
            StateCode = stateCode;
            StateName = stateName;
            StateAbbreviation = stateAbbreviation;
            IReadOnlyDictionary<string, RecordType> recordTypeEntries = new RecordTypeTable();
            RecordType = recordTypeEntries[recordTypeCode];

        }

        public Commodity(DataRow row)
        : this((int)row["STATE_CODE"], (string)row["STATE_NAME"], (string)row["STATE_ABBREVIATION"], (string)row["RECORD_TYPE_CODE"])
        {
        }

        public int StateCode
        {
            get;
            private set;
        }

        public string StateName
        {
            get;
            private set;
        }

        public string StateAbbreviation
        {
            get;
            private set;
        }
        public RecordType RecordType
        {
            get;
            private set;
        }

        public bool Equals(State other)
        {
            return StateCode == other.StateCode && StateName == other.StateName &&
            StateAbbreviation == other.StateAbbreviation && RecordType == other.RecordType;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(Commodity a, Commodity b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Commodity a, Commodity b)
        {
            return !a.Equals(b);
        }
    }
}