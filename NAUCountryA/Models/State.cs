using NAUCountryA.Tables;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Threading.Tasks;

namespace NAUCountryA.Models
{
    public class State : IEquatable<State>
    {
<<<<<<< HEAD
        //is this part needed?
        public State(IReadOnlyDictionary<string, State> stateEntries, string stateCode)
        {
            StateCode = stateEntries[stateCode].StateCode;
            StateName = stateEntries[stateCode].StateName;
            StateAbbreviation = stateEntries[stateCode].StateAbbreviation;
        }
=======
>>>>>>> 288574a849aa5f2f00ad8013daefce9f582fb904

        public State(int stateCode, string stateName, string stateAbbreviation, string recordTypeCode) 
        {
            StateCode = stateCode;
            StateName = stateName;
            StateAbbreviation = stateAbbreviation;
            IReadOnlyDictionary<string, RecordType> recordTypeEntries = new RecordTypeTable();
            RecordType = recordTypeEntries[recordTypeCode];

        }

        public State(DataRow row)
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

        public KeyValuePair<int, State> Pair
        {
            get
            {
                return new KeyValuePair<int, State>(StateCode, this);
            }
        }

        public bool Equals(State other)
        {
            return StateCode == other.StateCode && StateName == other.StateName &&
            StateAbbreviation == other.StateAbbreviation && RecordType == other.RecordType;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is State))
            {
                return false;
            }
            return Equals((State)obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public static bool operator ==(State a, State b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(State a, State b)
        {
            return !a.Equals(b);
        }
    }
}