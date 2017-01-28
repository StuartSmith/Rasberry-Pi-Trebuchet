using Restup.HttpMessage;
using Restup.HttpMessage.Models.Contracts;
using Restup.HttpMessage.Models.Schemas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace Rasberry_Pi_Trebuchet.Common.RestViewModels
{
   

   public class RestUpHttpServerRequest : IHttpServerRequest
    {
        private readonly List<IHttpRequestHeader> _headers;

        public RestUpHttpServerRequest()
        {
            _headers = new List<IHttpRequestHeader>();

            AcceptCharsets = Enumerable.Empty<string>();
            AcceptMediaTypes = Enumerable.Empty<string>();
            AcceptEncodings = Enumerable.Empty<string>();
            AccessControlRequestHeaders = Enumerable.Empty<string>();
        }

        public IEnumerable<IHttpRequestHeader> Headers => _headers;
        public HttpMethod? Method { get; set; }
        public Uri Uri { get; set; }
        public string HttpVersion { get; set; }
        public string ContentTypeCharset { get; set; }
        public IEnumerable<string> AcceptCharsets { get; set; }
        public int ContentLength { get; set; }
        public string ContentType { get; set; }
        public IEnumerable<string> AcceptEncodings { get; set; }
        public IEnumerable<string> AcceptMediaTypes { get; set; }
        public byte[] Content { get; set; }
        public bool IsComplete { get; set; }
        public HttpMethod? AccessControlRequestMethod { get; set; }
        public IEnumerable<string> AccessControlRequestHeaders { get; set; }
        public string Origin { get; set; }
        public static Task<RestUpHttpServerRequest> HttpRequestParser { get; private set; }

        internal void AddHeader(IHttpRequestHeader header)
        {
            if (IsComplete)
            {
                throw new InvalidOperationException("Can't add header after processing request is finished!");
            }
            // header.Visit(HttpRequestHandleHeaderData.Default, this);

            _headers.Add(header);
        }

        public async static Task<MutableHttpServerRequest> Parse(IInputStream requestStream)
        {
            return await MutableHttpServerRequest.Parse(requestStream);

        }
    }
}
