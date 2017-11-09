using CustomProtocol;
using System;
using System.Text;

namespace ProtocolBuilderTest
{
    class HelloWorldProtocol : IProtocolWorker
    {
        private Byte[] _response = null;

        public IProtocolWorker DigestRequest(string request)
        {
            Console.WriteLine("Greeter::Digest with parameter: " + request);
            _response = Encoding.ASCII.GetBytes(request + " World\r\n");
            return this;
        }

        public byte[] GetResponse()
        {
            return _response;
        }
    }
}
