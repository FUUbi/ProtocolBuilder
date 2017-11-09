using System.Collections.Generic;

namespace ProtocolBuilderTest.MathProtocol
{
    class MPEventModel
    {
        public readonly StatusCode StatusCode;
        public readonly int[] Parameters;

        public MPEventModel(
            StatusCode statusCode,
            int[] parameters
            )
        {
            StatusCode = statusCode;
            Parameters = parameters;
        }


        public override bool Equals(object obj)
        {
            MPEventModel other = obj as MPEventModel;
            if (other == null)
            {
                return false;
            }
            bool areSAAAAME = false;

            if (other.Parameters.Length == Parameters.Length)
            {
                for (int i = 0; i < other.Parameters.Length; i++)
                {
                    areSAAAAME = (Parameters[i] == other.Parameters[i]);
                }
            }

            bool b = StatusCode.Equals(other.StatusCode);
            return b && areSAAAAME;
        }
    }




}
