using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Restup.HttpMessage.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Azure.RestupHttpRequests
{
    public class HttpRequestsAzureMsgListener
    {
        public static RestUpHttpServerRequest GetRequest_AzureMsgListenerStatus()
        {

            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/azuremsglistener/status?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicGet;
        }

        public static RestUpHttpServerRequest PutRequest_AzureMsgListenerRegister()
        {

            RestUpHttpServerRequest basicPut = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.PUT,
                Uri = new Uri($"/azuremsglistener/registerdevice", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicPut;
        }

        public static RestUpHttpServerRequest PutRequest_AzureMsgListenerStart()
        {

            RestUpHttpServerRequest basicPut = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.PUT,
                Uri = new Uri($"/azuremsglistener/start", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicPut;
        }

        public static RestUpHttpServerRequest PutRequest_AzureMsgListenerStop()
        {

            RestUpHttpServerRequest basicPut = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.PUT,
                Uri = new Uri($"/azuremsglistener/stop", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicPut;
        }

    }
}
