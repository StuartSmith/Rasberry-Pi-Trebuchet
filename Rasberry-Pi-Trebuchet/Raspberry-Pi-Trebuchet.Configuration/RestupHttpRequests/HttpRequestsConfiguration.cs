using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.Configuration.RestViewModels;
using Restup.HttpMessage.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Configuration.RestupHttpRequests
{
   public static class HttpRequestsConfiguration
    {
        public static RestUpHttpServerRequest GetRequestPIConfigurationStatus(string name)
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/piconfiguration/{name}?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            return basicGet;
        }

        public static RestUpHttpServerRequest GetRequestPIConfigurationStatuses()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/piconfiguration?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            return basicGet;
        }
        //private RestUpHttpServerRequest SetPIConfigurationStatus_AllowSendingofData_PutRequest()
        //{
        //    RestUpHttpServerRequest basicPut = new RestUpHttpServerRequest()
        //    {
        //        Method = HttpMethod.PUT,
        //        Uri = new Uri($"/piconfiguration/AllowSendingofData", UriKind.RelativeOrAbsolute),
        //        AcceptMediaTypes = new[] { "application/json" },
        //        Content = Encoding.UTF8.GetBytes("\"false\""),
        //        IsComplete = true
        //    };
        //    return basicPut;
        //}

        public static RestUpHttpServerRequest PostReqestPiConfigurationStatus(string name, string  value)
        {
            RestUpHttpServerRequest basicPost = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.PUT,
                Uri = new Uri($"/piconfiguration/{name}", UriKind.RelativeOrAbsolute),
                      AcceptMediaTypes = new[] { "application/json" },
                     Content = Encoding.UTF8.GetBytes(value),
                      IsComplete = true
            };
            return basicPost;
        }


        public static RestUpHttpServerRequest PostReqestPiConfigurationStatuses(List<ViewModelRestNameValuePair> piConfigurations)
        {
            RestUpHttpServerRequest basicPost = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.POST,
                Uri = new Uri($"/piconfiguration?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true,
                Content = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(piConfigurations))
            };
            return basicPost;
        }
    }
}
