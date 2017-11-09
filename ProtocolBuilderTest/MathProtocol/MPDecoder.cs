using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilderTest.MathProtocol
{
    class MPDecoder
    {
        private readonly String EncodedObject;

        public readonly MPEventModel Model;
        private readonly StatusCodes STATUS_CODES = new StatusCodes();

        public MPDecoder(String encodedObject)
        {

            this.EncodedObject = encodedObject;


            this.Model = new MPEventModel(
                this.getStatusCode(),
                this.getParameters()
                );

        }


        private StatusCode getStatusCode()
        {
            StatusCode statusCode = StatusCodes.RESPONSE_ERROR;
            try
            {

                string[] request = this.EncodedObject
                    .Split(StatusCodes.SEPARATOR);

                int code = int.Parse(request.First());
                String msg = request[1];

                StatusCode currentStatusCode = new StatusCode(code, msg);

                bool isValidStatusCode =
                    new StatusCodes()
                    .isValidStatusCode(currentStatusCode);

                if (isValidStatusCode)
                {
                    statusCode = currentStatusCode;
                }
            }
            catch (Exception)
            {
                statusCode = StatusCodes.RESPONSE_ERROR;
            }

            return statusCode;
        }

        private int[] getParameters()

        {
            if (STATUS_CODES.isSameStatusCode(getStatusCode(), StatusCodes.RESPONSE_ERROR))
            {
                return new int[] { };
            }
            else
            {
                string[] encoded_parameters = this.EncodedObject.Split(StatusCodes.SEPARATOR).Last().Split(StatusCodes.PARAMETER_SEPARATOR);

                return Array.ConvertAll(
                    encoded_parameters,
                    delegate (string s)
                    {
                        return int.Parse(s);
                    }
                    );
            }

        }


    }
}

