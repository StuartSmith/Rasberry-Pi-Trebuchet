using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Azure.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.Client
{
    public class SendMsgToDeviceThroughAzure
    {

        private ServiceClient serviceClient;

        public SendMsgToDeviceThroughAzure(string connectionstring)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionstring);
        }


        //private async static void ReceiveFeedbackAsync()
        //{
        //    var feedbackReceiver = serviceClient.GetFeedbackReceiver();
        //    Console.WriteLine("\nReceiving c2d feedback from service");
        //    while (true)
        //    {
        //        var feedbackBatch = await feedbackReceiver.ReceiveAsync();
        //        if (feedbackBatch == null) continue;              
        //        await feedbackReceiver.CompleteAsync(feedbackBatch);
        //    }
        //}


        //public static SendMsgToDevice Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //        {
        //            _instance = new SendMsgToDevice();
        //        }
        //        return _instance;
        //    }
        //}


        //public void SetConnectionString(string connectionstring)
        //{
        //    serviceClient = ServiceClient.CreateFromConnectionString(connectionstring);
        //}


        public async Task<MsgContentToAzure> Send(string deviceName, RestUpHttpServerRequest restupRequestMSG)
        {            
            Guid RequestGuid = Guid.NewGuid();
            MsgContentToAzure msgContent = new MsgContentToAzure(RequestGuid, restupRequestMSG);
            string msgContentJSON = JsonConvert.SerializeObject(msgContent);

            var commandMessage = new Message(Encoding.ASCII.GetBytes(msgContentJSON));
            commandMessage.Ack = DeliveryAcknowledgement.Full;
            await serviceClient.SendAsync(deviceName, commandMessage);

            return msgContent;
        }
    }
}
