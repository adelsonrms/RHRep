using System;
using System.Runtime.Serialization;

namespace RH.Infra.Data.Exceptions
{
    [Serializable]
    internal class DALExceptionGetDataTable : Exception
    {
        public DALExceptionGetDataTable()
        {
        }

        public DALExceptionGetDataTable(string message) : base(message)
        {
        }

        public DALExceptionGetDataTable(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected DALExceptionGetDataTable(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}