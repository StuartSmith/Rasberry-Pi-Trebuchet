using Raspberry_Pi_Trebuchet.Azure_WebService.Models.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.LoggingService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.LoggingService.RestViewModels
{
    public class IOTLogMSG
    {

        public LoggedMsgType MsgType { get; set; }

        public Guid MSGGUID { get; set; } = Guid.NewGuid();
        public DateTime LoggedTime { get; set; } = DateTime.Now;

        public  OperationResult<ErrorType> OperationMSg { get; set; }

        public MsgSentToDevice MessageSentToDevice { get; set; }

        public IOTAzureDeviceResponce MessageResponseFromDevice { get; set; }

        
       public IOTLogMSG()
        {
        }

       public IOTLogMSG(ErrorType errortype, string InformationalMessage)
        {
            MsgType = LoggedMsgType.LoggedMSg;
            OperationMSg = new OperationResult<ErrorType>(errortype, InformationalMessage);
        }

        public IOTLogMSG(MsgSentToDevice msgResquest )
        {
            MsgType = LoggedMsgType.RequestMsg;
            MessageSentToDevice = msgResquest;
        }

        public IOTLogMSG(IOTAzureDeviceResponce msgResponce)
        {
            MsgType = LoggedMsgType.ResponceMSg;
            MessageResponseFromDevice = msgResponce;
        }
    }
}
