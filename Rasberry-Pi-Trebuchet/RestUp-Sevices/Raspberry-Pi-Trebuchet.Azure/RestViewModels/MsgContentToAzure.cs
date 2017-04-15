using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.RestViewModels
{
   public  class MsgContentToAzure
    {

        public Guid MSGGUID { get; set; }
        public RestUpHttpServerRequest RestUpMsgRequest { get; set; }

        public DateTime ProcessedRequestDateTime { get; set; } = DateTime.UtcNow;

        public string InernalMessageInfo { get; set;}

        public MsgContentToAzure()
        {            
        }

        protected MsgContentToAzure(MsgContentToAzure other)
        {
            this.MSGGUID = other.MSGGUID;
            this.RestUpMsgRequest = other.RestUpMsgRequest;
            this.ProcessedRequestDateTime = other.ProcessedRequestDateTime;
        }


        public MsgContentToAzure(Guid msgId, RestUpHttpServerRequest restUpMsgRequest)
        {
            MSGGUID = msgId;
            RestUpMsgRequest = restUpMsgRequest;
           
        }   
    }
}
