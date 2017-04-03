using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Raspberry_Pi_Trebuchet.Azure_WebService.RestViewModel
{
    public class rvm_IOTAzureRequestResponce
    {
        public Uri RequestUri { get; set; }

        public Guid MSGGUID { get; set; }

        public string Response { get; set; }

        public int ContentLength { get; set; }

        public string ContentType { get; set; }

        public byte[] RequestContent { get; set; }
    }
}
