﻿using Newtonsoft.Json;
using Rasberry_Pi_Trebuchet.Common.RestViewModels;
using Raspberry_Pi_Trebuchet.RestUp.Sonic.Models;
using Restup.HttpMessage.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Tribuchet.Sonic.RetupHttpRequests
{
    public static class HttpRequestsSonic
    {
        public static RestUpHttpServerRequest DeleteUltraSonicRuns()
        {
            RestUpHttpServerRequest basicDelete = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.DELETE,
                Uri = new Uri($"ultrasonic/removeultrasonicruns", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicDelete;

        }

        public static RestUpHttpServerRequest GetRequestIsSonicRunning()
        {
            RestUpHttpServerRequest basicGet = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.GET,
                Uri = new Uri($"/ultrasonic/isultrasonicrunning?={DateTime.Now}", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            return basicGet;
        }

        public static RestUpHttpServerRequest PostRequestStartRun(UltraSonicRunRequest runrequest)
        {
            RestUpHttpServerRequest basicPost = new RestUpHttpServerRequest()
            {
                Method = HttpMethod.POST,
                Uri = new Uri($"/ultrasonic/startrun", UriKind.RelativeOrAbsolute),
                AcceptMediaTypes = new[] { "application/json" },
                IsComplete = true
            };
            basicPost.Content = System.Text.Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(runrequest));
            return basicPost;
        }

       


    }
}