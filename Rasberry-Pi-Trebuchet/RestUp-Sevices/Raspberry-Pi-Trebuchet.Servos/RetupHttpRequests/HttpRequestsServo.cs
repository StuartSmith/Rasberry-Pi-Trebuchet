using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Servos.RestViewModels;
using Restup.HttpMessage.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Servos.RetupHttpRequests
{
  public static class HttpRequestsServo
    {
        public static RestUpHttpServerRequest GetRequestServoStatus()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/servo/statuses?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicGet;
        }


        public static RestUpHttpServerRequest PostRequestSetServerPosition(ServoRestViewModel content)
        {
            RestUpHttpServerRequest basicPost = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.POST,
                Uri = new Uri($"/servo/statuses", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            basicPost.Content = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(content));


            return basicPost;
        }
    }
}
