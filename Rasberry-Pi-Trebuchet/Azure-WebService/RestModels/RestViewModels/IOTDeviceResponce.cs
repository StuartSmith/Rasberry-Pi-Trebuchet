using System;

namespace Raspberry_Pi_Trebuchet.Azure_WebService.Models.RestViewModels
{
    public class IOTAzureDeviceResponce
    {
        public Uri RequestUri { get; set; }

        public Guid MSGGUID { get; set; } = Guid.NewGuid();

        public string Response { get; set; }

        public int RequestContentLength { get; set; }

        public string RequestContentType { get; set; }

        public byte[] RequestContent { get; set; }

        /// <summary>
        /// Time the Message was finish processing by the Device
        /// </summary>
        public DateTime ProcessedRequestDateTime { get; set; } = DateTime.Now;
    }
}