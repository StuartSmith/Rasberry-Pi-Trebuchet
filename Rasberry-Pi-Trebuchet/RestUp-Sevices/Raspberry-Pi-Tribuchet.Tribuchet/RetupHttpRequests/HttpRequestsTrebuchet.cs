using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Restup.HttpMessage.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.RestUp.Trebuchet.RetupHttpRequests
{
    public class HttpRequestsTrebuchet
    {
       public  static RestUpHttpServerRequest PostRequest_TrebuchetFire()
        {
            RestUpHttpServerRequest basicPost = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.POST,
                Uri = new Uri($"/trebuchet/fire", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            
            return basicPost;
        }

       public static RestUpHttpServerRequest PostRequest_TrebuchetReset()
        {
            RestUpHttpServerRequest basicPost = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.POST,
                Uri = new Uri($"/trebuchet/reset", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };

            return basicPost;
        }


    }
}
