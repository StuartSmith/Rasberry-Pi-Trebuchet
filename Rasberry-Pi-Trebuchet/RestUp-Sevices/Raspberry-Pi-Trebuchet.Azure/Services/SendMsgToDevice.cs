using Microsoft.Azure.Devices;
using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
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

        private async static void ReceiveFeedbackAsync()
        {
            var feedbackReceiver = serviceClient.GetFeedbackReceiver();

            Console.WriteLine("\nReceiving c2d feedback from service");
            while (true)
            {
                var feedbackBatch = await feedbackReceiver.ReceiveAsync();
                if (feedbackBatch == null) continue;

               //Console.ForegroundColor = ConsoleColor.Yellow;
               Console.WriteLine("Received feedback: {0}", string.Join(", ", feedbackBatch.Records.Select(f => f.StatusCode)));
               // Console.ResetColor();

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

        public async Task Send(string deviceName, RestUpHttpServerRequest restupRequest)
        {
            string restupJSON = JsonConvert.SerializeObject(restupRequest);
            var commandMessage = new Message(Encoding.ASCII.GetBytes(restupJSON));
            commandMessage.Ack = DeliveryAcknowledgement.Full;
            await serviceClient.SendAsync(deviceName, commandMessage);
        }
    }
}
