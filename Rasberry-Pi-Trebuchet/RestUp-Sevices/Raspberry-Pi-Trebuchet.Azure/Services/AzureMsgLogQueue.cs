using Raspberry_Pi_Trebuchet.RestUp.Azure.RestViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.Services
{
    public class AzureMsgLogQueue
    {
        private static AzureMsgLogQueue _instance;
        private int _maxQueueCount;

        private const int MAXIMUM_NUMBER_OF_ITEMS_IN_QUEUE = 5;

        private ConcurrentQueue<MsgContentToAndFromAzure> AzureMsgQueue;
        private AzureMsgLogQueue() {
            AzureMsgQueue = new ConcurrentQueue<MsgContentToAndFromAzure>();
        }

        public static AzureMsgLogQueue Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AzureMsgLogQueue();
                }
                return _instance;
            }
        }


        public void addMsgToQueue(MsgContentToAndFromAzure msgContent)
        {
            AzureMsgQueue.Enqueue(msgContent);
            MsgContentToAndFromAzure dequedItem;

            if (MAXIMUM_NUMBER_OF_ITEMS_IN_QUEUE > _maxQueueCount)
                Interlocked.Increment(ref _maxQueueCount);
            else
            {
                if (AzureMsgQueue.TryDequeue(out dequedItem))
                    Interlocked.Decrement(ref _maxQueueCount);
            }
        }


        public async Task<List<MsgContentToAndFromAzure>> RetrieveAllQueuedMessages()
        {
            var RetrievedMessages = await Task<List<MsgContentToAndFromAzure>>.Factory.StartNew(() =>
            {               
                return AzureMsgQueue.ToList<MsgContentToAndFromAzure>();
            });

            return RetrievedMessages;


        }
    }
}
