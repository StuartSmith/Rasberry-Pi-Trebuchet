using System;
using System.Collections.Concurrent;
using Raspberry_Pi_Trebuchet.Azure_WebService.RestViewModel;

namespace Raspberry_Pi_Trebuchet.Azure_WebService.Services
{
    public class AzureIOTRequestResponceService
    {
        private static AzureIOTRequestResponceService _instance;

        private ConcurrentDictionary<Guid, rvm_IOTAzureRequestResponce> requestedConcurrentDictionary;

        private AzureIOTRequestResponceService()
        {
            requestedConcurrentDictionary = new ConcurrentDictionary<Guid, rvm_IOTAzureRequestResponce>();
        }


        public void Add(rvm_IOTAzureRequestResponce msgContentToAndFromAzure)
        {
            requestedConcurrentDictionary.TryAdd(msgContentToAndFromAzure.MSGGUID, msgContentToAndFromAzure);
        }


        public rvm_IOTAzureRequestResponce Get(Guid guid)
        {
            rvm_IOTAzureRequestResponce msgContentToAndFromAzure;

            if (requestedConcurrentDictionary.TryGetValue(guid, out msgContentToAndFromAzure))
                return msgContentToAndFromAzure;

            return null;
        }

        public void Delete(Guid guid)
        {
            rvm_IOTAzureRequestResponce msgContentToAndFromAzure;
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
