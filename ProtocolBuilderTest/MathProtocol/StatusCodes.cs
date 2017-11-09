

using System.Collections.Generic;

namespace ProtocolBuilderTest.MathProtocol

{
    class StatusCodes
    {
        public static readonly char SEPARATOR = '!';
        public static readonly char MESSAGE_SEPARATOR = '_';
        public static readonly char PARAMETER_SEPARATOR = '$';

        public static readonly StatusCode REQUEST_ADDITION =
           new StatusCode(1, "Request_Addition");

        public static readonly StatusCode REQUEST_SUBTRACTION =
            new StatusCode(2, "Request_Subtraction");

        public static readonly StatusCode REQUEST_MULTIPLICATION =
            new StatusCode(3, "Request_Multiplication");

        public static readonly StatusCode REQUEST_DIVISION =
            new StatusCode(4, "Request_Division");

        public static readonly StatusCode REQUEST_CURRENT_VALUE =
            new StatusCode(5, "Request_Currnet_Value");

        public static readonly StatusCode RESPONSE_OK_CURRENT_VALUE =
            new StatusCode(100, "Response_Ok_Current_Value");

        public static readonly StatusCode RESPONSE_ERROR =
           new StatusCode(222, "Response_Error_Current_Value");


        private readonly Dictionary<int, StatusCode> _statusCodeDict;



        public StatusCodes()
        {
            Dictionary<int, StatusCode> statusCodeDict =
                new Dictionary<int, StatusCode>();

            statusCodeDict.Add(REQUEST_ADDITION.Id, REQUEST_ADDITION);
            statusCodeDict.Add(REQUEST_SUBTRACTION.Id, REQUEST_SUBTRACTION);
            statusCodeDict.Add(REQUEST_MULTIPLICATION.Id, REQUEST_MULTIPLICATION);
            statusCodeDict.Add(REQUEST_DIVISION.Id, REQUEST_DIVISION);

            statusCodeDict.Add(RESPONSE_OK_CURRENT_VALUE.Id, RESPONSE_OK_CURRENT_VALUE);

            _statusCodeDict = statusCodeDict;
        }


        public bool isValidStatusCode(StatusCode statusCode)
        {
            return _statusCodeDict.ContainsKey(statusCode.Id);
        }


        public bool isSameStatusCode(StatusCode sc1, StatusCode sc2)
        {
            return sc1.Id == sc2.Id;
        }
    }
}
