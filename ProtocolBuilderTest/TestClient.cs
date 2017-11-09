using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilderTest
{
    class TestClient
    {
        public StreamReader InStream;
        public Stream OutStream;
        private TcpClient _c;

        public TestClient(String ip, int port)
        {
            _c = new TcpClient(ip, port);
            InStream = new StreamReader(_c.GetStream());
            OutStream = _c.GetStream();

        }
        public void Close()
        {
            _c.Close();
        }
    }
}
