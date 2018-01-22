
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using System.Threading;
using Raspberry_Pi_Trebuchet.RestUp.LoggingService.RestViewModels;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.Services
{
    public class IOTMsgLogQueue
    {
        private static IOTMsgLogQueue _instance;
        private int _maxQueueCount;

        private const int MAXIMUM_NUMBER_OF_ITEMS_IN_QUEUE = 5;

        private ConcurrentQueue<IOTLogMSG> AzureMsgQueue;
        private IOTMsgLogQueue() {
            AzureMsgQueue = new ConcurrentQueue<IOTLogMSG>();
        }

        public static IOTMsgLogQueue Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new IOTMsgLogQueue();
                }
                return _instance;
            }
        }


        public void addMsgToLog(IOTLogMSG msgContent)
        {
            AzureMsgQueue.Enqueue(msgContent);
            IOTLogMSG dequedItem;

            if (MAXIMUM_NUMBER_OF_ITEMS_IN_QUEUE > _maxQueueCount)
                Interlocked.Increment(ref _maxQueueCount);
            else
            {
                if (AzureMsgQueue.TryDequeue(out dequedItem))
                    Interlocked.Decrement(ref _maxQueueCount);
            }
        }


        public async Task<List<IOTLogMSG>> RetrieveAllQueuedMessages()
        {
            var RetrievedMessages = await Task<List<IOTLogMSG>>.Factory.StartNew(() =>
            {               
                return AzureMsgQueue.ToList<IOTLogMSG>();
            });

            return RetrievedMessages;
        }
    }
}
