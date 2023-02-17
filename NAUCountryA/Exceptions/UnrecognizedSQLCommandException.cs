using System;
using System.Data;
using System.Runtime.Serialization;
namespace NAUCountryA.Exceptions
{
    public class UnrecognizedSQLCommandException : DataException
    {
        public UnrecognizedSQLCommandException()
        :base()
        {

        }

        public UnrecognizedSQLCommandException(string message)
        :base(message)
        {

        }

        public UnrecognizedSQLCommandException(SerializationInfo info, StreamingContext context)
        :base(info, context)
        {

        }

        public UnrecognizedSQLCommandException(string message, Exception innerException)
        :base(message, innerException)
        {

        }
    }
}