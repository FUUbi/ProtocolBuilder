
using System;
using System.Net;

namespace CustomProtocol
{
    public class ProtocolBuilder
    {
        private IProtocolWorker _worker;
        private IPEndPoint _ipEndPoint = null;

        public ProtocolBuilder()
        {

        }

        public ProtocolBuilder SetProtocolWorker(IProtocolWorker worker)
        {
            _worker = worker;
            return this;
        }

        public ProtocolBuilder SetIPEndPoint(String ip, int port)
        {
            _ipEndPoint = new IPEndPoint(
                IPAddress.Parse(ip),
                port
                );
            return this;
        }

        public Protocol Build()
        {

            return new Protocol(
                new Server(_ipEndPoint, _worker)
                );
        }
    }

}
