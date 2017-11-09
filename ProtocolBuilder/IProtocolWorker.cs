using System;

namespace CustomProtocol
{
    public interface IProtocolWorker
    {
        IProtocolWorker DigestRequest(String request);
        Byte[] GetResponse();
    }
}