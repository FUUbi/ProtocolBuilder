
namespace CustomProtocol
{
    public class Protocol
    {
        private readonly Server _server;

        internal Protocol(Server server)
        {
            _server = server;
        }

        public void Start() => _server.Start();

        public void Stop() => _server.Stop();

    }
}
