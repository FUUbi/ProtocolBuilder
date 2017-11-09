using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilderTest.MathProtocol

{
    class StatusCode
    {
        public readonly int Code;
        public readonly String Message;
        public readonly int Id;


        public StatusCode(int code, String message)
        {
            Code = code;
            Message = message;
            Id = (Code.ToString() + Message.ToString()).GetHashCode();
        }

        override
        public String ToString()
        {
            return Code.ToString() + StatusCodes.SEPARATOR + Message.ToString();
        }

        public override bool Equals(object obj)
        {
            StatusCode other = obj as StatusCode;
            if (other == null)
            {
                return false;
            }
            return Id == other.Id;
        }
    }
}
