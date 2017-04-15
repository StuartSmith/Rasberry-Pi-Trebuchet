using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public DateTime ProcessedRequestDateTime { get; set; } = DateTime.Now;

    }
}
