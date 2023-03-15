using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace com.mobiquity.packer
{
    [Serializable()]
    public class APIException : Exception
    {
        public APIException() { }

        public APIException(string message)
            : base(String.Format("Packer Exception Encountered: {0}", message))
        {
        }

        public APIException(string message, Exception innerException) 
            : base(String.Format("Packer Exception Encountered: {0}", message), innerException)
        {
        }

        protected APIException(SerializationInfo info, StreamingContext context) 
            : base(info, context)
        {
        }
    }
}
