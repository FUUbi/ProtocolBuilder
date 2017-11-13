using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;


namespace CustomProtocol
{
    internal class Server
    {
        private static TcpListener _listener = null;
        private static Thread _serverThread = null;

        private static ArrayList _tcpConnectionThreads = new ArrayList();
        private static IProtocolWorker _tcpWorker;


        internal Server(IPEndPoint localEndPoint, IProtocolWorker tcpWorker)
        {
            _listener = new TcpListener(localEndPoint);
            _tcpWorker = tcpWorker;

        }

        public void Start()
        {
            _listener.Start();
            _serverThread = new Thread(new ThreadStart(Run));
            _serverThread.Start();
        }

        public void Stop()
        {

            StopAllTcpConnection();
            _serverThread.Abort();
            _listener.Stop();
        }

        private void StopAllTcpConnection()
        {
            for (IEnumerator connThread = _tcpConnectionThreads.GetEnumerator();
                connThread.MoveNext();)
            {
                ConnectionThread ct = (ConnectionThread)connThread.Current;
                ct.Abort();

            }
            _listener.Stop();
        }

        public static void Run()
        {
            while (true)
            {
                TcpClient c = _listener.AcceptTcpClient();
                _tcpConnectionThreads.Add(new ConnectionThread(c, _tcpWorker));

            }

        }


    }
}
