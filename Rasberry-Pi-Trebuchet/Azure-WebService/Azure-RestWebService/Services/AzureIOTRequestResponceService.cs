using Raspberry_Pi_Trebuchet.Azure_WebService.Models.RestViewModels;
using System;
using System.Collections.Concurrent;



namespace Raspberry_Pi_Trebuchet.RestUp.LoggingService.Services
{
    public class AzureIOTRequestResponceService
    {
        private static AzureIOTRequestResponceService _instance;

        private ConcurrentDictionary<Guid, IOTDeviceResponce> requestedConcurrentDictionary;

        private AzureIOTRequestResponceService()
        {
            requestedConcurrentDictionary = new ConcurrentDictionary<Guid, IOTDeviceResponce>();
        }


        public void Add(IOTDeviceResponce msgContentToAndFromAzure)
        {
            requestedConcurrentDictionary.TryAdd(msgContentToAndFromAzure.MSGGUID, msgContentToAndFromAzure);
        }


        public IOTDeviceResponce Get(Guid guid)
        {
            IOTDeviceResponce msgContentToAndFromAzure;

            if (requestedConcurrentDictionary.TryGetValue(guid, out msgContentToAndFromAzure))
                return msgContentToAndFromAzure;

            return null;
        }

        public void Delete(Guid guid)
        {
            IOTDeviceResponce msgContentToAndFromAzure;
            requestedConcurrentDictionary.TryRemove(guid,out msgContentToAndFromAzure);
        }

        public static AzureIOTRequestResponceService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new AzureIOTRequestResponceService();
                }
                return _instance;
            }
        }
       
    }
}
