using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProtocolBuilderTest.MathProtocol
{
    class MPEventSource
    {
        private List<MPEventModel> EventsSource;
        private readonly StatusCodes STATUS_CODES = new StatusCodes();
        public MPEventSource()
        {
            this.EventsSource = new List<MPEventModel>();
        }


        public void addMathEventModel(MPEventModel model)
        {

            this.EventsSource.Add(model);
        }

        public MPEventModel CurrentEvnetModel()
        {
            return this.AggregateEventSource();
        }

        private MPEventModel AggregateEventSource()
        {
            int currentValue = 0;
            StatusCode responseStatus = StatusCodes.RESPONSE_ERROR;
            foreach (MPEventModel m in this.EventsSource)
            {
                if (STATUS_CODES.isSameStatusCode(m.StatusCode, StatusCodes.RESPONSE_ERROR))
                {
                    responseStatus = StatusCodes.RESPONSE_ERROR;
                }
                else if (STATUS_CODES.isSameStatusCode(m.StatusCode, StatusCodes.REQUEST_CURRENT_VALUE))
                {
                    responseStatus = StatusCodes.RESPONSE_OK_CURRENT_VALUE;
                }
                else if (this.EventsSource.IndexOf(m) == 0)
                {
                    currentValue = m.Parameters.First();
                    currentValue = this.Calculate(
                        currentValue,
                        m.Parameters.Skip(1).ToArray(),
                        m.StatusCode);
                    responseStatus = StatusCodes.RESPONSE_OK_CURRENT_VALUE;
                }
                else
                {
                    currentValue = this.Calculate(
                        currentValue,
                        m.Parameters.ToArray(),
                        m.StatusCode
                        );
                    responseStatus = StatusCodes.RESPONSE_OK_CURRENT_VALUE;
                }
            }
            return new MPEventModel(responseStatus, new int[] { currentValue });
        }


        private int Calculate(int currentValue, int[] parameters, StatusCode status)
        {
            foreach (int x in parameters)
            {
                if (STATUS_CODES.isSameStatusCode(
                     status, StatusCodes.REQUEST_ADDITION)
                   )
                {
                    currentValue += x;
                }

                else if (STATUS_CODES.isSameStatusCode(
                    status, StatusCodes.REQUEST_SUBTRACTION)
                    )
                {
                    currentValue -= x;
                }
                else if (STATUS_CODES.isSameStatusCode(
                          status, StatusCodes.REQUEST_MULTIPLICATION)
                )
                {
                    currentValue *= x;
                }
                else if (STATUS_CODES.isSameStatusCode(
                          status, StatusCodes.REQUEST_DIVISION)
                )
                {
                    currentValue /= x;
                }


            }

            return currentValue;
        }
    }
}
