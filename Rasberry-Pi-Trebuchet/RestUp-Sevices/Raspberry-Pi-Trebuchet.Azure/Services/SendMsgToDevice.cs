using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Azure.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.Services
{
    public class SendMsgToDevice
    {
        private static SendMsgToDevice _instance;
        private static ServiceClient serviceClient;

        private SendMsgToDevice()
        { }


        private async static void ReceiveFeedbackAsync()
        {
            var feedbackReceiver = serviceClient.GetFeedbackReceiver();
            Console.WriteLine("\nReceiving c2d feedback from service");
            while (true)
            {
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();
                if (feedbackBatch == null) continue;              
                await feedbackReceiver.CompleteAsync(feedbackBatch);
            }
        }


        public static SendMsgToDevice Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new SendMsgToDevice();
                }
                return _instance;
            }
        }


        public void SetConnectionString(string connectionstring)
        {
            serviceClient = ServiceClient.CreateFromConnectionString(connectionstring);
        }


        public async Task<MsgContentToAzure> Send(string deviceName, RestUpHttpServerRequest restupRequest)
        {            
            Guid RequestGuid = Guid.NewGuid();
            MsgContentToAzure msgContent = new MsgContentToAzure(RequestGuid, restupRequest);
            string msgContentJSON = JsonConvert.SerializeObject(msgContent);

            var commandMessage = new Message(Encoding.ASCII.GetBytes(msgContentJSON));
            commandMessage.Ack = DeliveryAcknowledgement.Full;
            await serviceClient.SendAsync(deviceName, commandMessage);

            return msgContent;
        }
    }
}
