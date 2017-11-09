using CustomProtocol;

namespace ProtocolBuilderTest.MathProtocol
{
    class MPWorker : IProtocolWorker

    {
        private static byte[] Response;
        private MPEventSource _eventSource = new MPEventSource();


        public IProtocolWorker DigestRequest(string request)
        {
            MPEventModel requestModel = new MPDecoder(request).Model;

            _eventSource.addMathEventModel(requestModel);
            MPEventModel responseModel = _eventSource.CurrentEvnetModel();

            Response = new MPEncoder(responseModel).EncodedObject;

            return this;

        }

        public byte[] GetResponse()
        {
            return Response;
        }


    }
}
