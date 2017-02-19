using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Lights.RestViewModels;
using Restup.HttpMessage.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Lights.RetupHttpRequests
{
    public static class HttpRequestsLight
    {
        public static  RestUpHttpServerRequest GetRequestLightStatuses()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/lights/statuses?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicGet;
        }


        public static RestUpHttpServerRequest PostRequestSetLightStatus(LightRestViewModel lightRestViewModel)
        {
            RestUpHttpServerRequest postRequest = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.POST,
                Uri = new Uri($"/lights/statuses", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            postRequest.Content = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(lightRestViewModel));

            return postRequest;
        }
       
    }
}
