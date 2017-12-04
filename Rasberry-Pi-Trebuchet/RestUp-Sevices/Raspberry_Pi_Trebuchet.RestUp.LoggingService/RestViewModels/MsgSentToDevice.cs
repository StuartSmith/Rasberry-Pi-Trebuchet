using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.LoggingService.RestViewModels
{
    public class MsgSentToDevice
    {

        public Guid MSGGUID { get; set; } = Guid.NewGuid();
        public RestUpHttpServerRequest RestUpMsgRequest { get; set; }

        public DateTime ProcessedRequestDateTime { get; set; } = DateTime.UtcNow;

        public MsgSentToDevice()
        {
        }

        public MsgSentToDevice(RestUpHttpServerRequest restUpMsgRequest)
        {
            RestUpMsgRequest = restUpMsgRequest;
        }

        public MsgSentToDevice(Guid msgId, RestUpHttpServerRequest restUpMsgRequest)
        {
            MSGGUID = msgId;
            RestUpMsgRequest = restUpMsgRequest;

        }


    }
}
