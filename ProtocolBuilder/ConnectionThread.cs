using System;
using System.Threading;
using System.IO;
using System.Net.Sockets;


namespace CustomProtocol
{
    internal class ConnectionThread
    {

        public bool stop = false;
        public bool running = false;
        private TcpClient connection = null;

        private IProtocolWorker worker;
        public ConnectionThread(TcpClient connection, IProtocolWorker worker)
        {
            this.connection = connection;
            this.worker = worker;
            new Thread(new ThreadStart(Run)).Start();
        }

        public void Run()
        {
            this.running = true;

            Stream outStream = this.connection.GetStream();
            StreamReader inSteam = new StreamReader(this.connection.GetStream());

            while (!stop)
            {
                try
                {
                    String request = inSteam.ReadLine();

                    Byte[] resp = this.worker
                        .DigestRequest(request)
                        .GetResponse();

                    outStream.Write(resp, 0, resp.Length);
                }
                catch (Exception)
                {
                    stop = true;
                }
            }

            this.connection.Close();
            this.running = false;
        }

        internal void Abort()
        {
            this.running = false;
            this.stop = true;
        }
    }
}
