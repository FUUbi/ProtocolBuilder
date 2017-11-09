using System;
using System.Collections;
using System.Text;


namespace ProtocolBuilderTest.MathProtocol
{
    class MPEncoder
    {
        public readonly byte[] EncodedObject;

        private readonly MPEventModel Model;
        public MPEncoder(MPEventModel model)
        {
            this.Model = model;
            this.EncodedObject =
               ASCIIEncoding.ASCII.GetBytes(
                   Model.StatusCode.ToString() + StatusCodes.SEPARATOR +
                   this.getParameters() + "\r\n"
                   );

        }


        private String getParameters()
        {
            String str = "";
            int[] p = this.Model.Parameters;
            for (int i = 0; i < p.Length; i++)
            {
                str += p[i];
                if (p.Length - i > 1)
                {
                    str += StatusCodes.PARAMETER_SEPARATOR;
                }
            }



            return str;
        }


    }

}
