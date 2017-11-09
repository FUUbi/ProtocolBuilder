using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CustomProtocol;
using System.Text;
using ProtocolBuilderTest.MathProtocol;

namespace ProtocolBuilderTest
{
    [TestClass]
    public class ProtocolBuilderTest
    {

        [TestMethod]
        public void ShouldBuildHelloWorldProtocol()
        {
            Protocol protocol = new ProtocolBuilder()
                    .SetProtocolWorker(new HelloWorldProtocol())
                    .SetIPEndPoint("127.0.0.1", 4001)
                    .Build();

            protocol.Start();
            TestClient testClient = new TestClient("127.0.0.1", 4001);

            Byte[] sendBytes = Encoding.ASCII.GetBytes("Hello\r\n");
            testClient.OutStream.Write(sendBytes, 0, sendBytes.Length);

            String response = testClient.InStream.ReadLine();

            protocol.Stop();
            testClient.Close();

            Assert.AreEqual("Hello World", response);

        }


        [TestMethod]
        public void ShouldBuildMathProtocol()
        {

            Protocol protocol = new ProtocolBuilder()
                    .SetProtocolWorker(new MPWorker())
                    .SetIPEndPoint("127.0.0.1", 4001)
                    .Build();

            protocol.Start();
            TestClient testClient = new TestClient("127.0.0.1", 4001);

            Byte[] addition = new MPEncoder(
                new MPEventModel(
                    StatusCodes.REQUEST_ADDITION,
                    new int[] { 1, 2 })
                    ).EncodedObject;

            testClient.OutStream.Write(addition, 0, addition.Length);

            String response = testClient.InStream.ReadLine();

            MPEventModel additionResponse =
                new MPDecoder(response).Model;

            MPEventModel expectedAdditionResponse =
                new MPEventModel(
                   StatusCodes.RESPONSE_OK_CURRENT_VALUE,
                   new int[] { 3 });

            Assert.AreEqual(expectedAdditionResponse, additionResponse);






            Byte[] subtraction = new MPEncoder(
              new MPEventModel(
                  StatusCodes.REQUEST_SUBTRACTION,
                  new int[] { 8 })
                  ).EncodedObject;

            testClient.OutStream.Write(subtraction, 0, subtraction.Length);

            response = testClient.InStream.ReadLine();

            MPEventModel subtractionResponse =
                new MPDecoder(response).Model;

            MPEventModel expectedSubtractionResponse =
                new MPEventModel(
                   StatusCodes.RESPONSE_OK_CURRENT_VALUE,
                   new int[] { -5 });

            Assert.AreEqual(expectedSubtractionResponse, subtractionResponse);

            protocol.Stop();
            testClient.Close();


        }



    }





}
