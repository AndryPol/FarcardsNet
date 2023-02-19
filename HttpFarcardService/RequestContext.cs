using System.Net;

namespace HttpFarcardService
{
    public class RequestContext
    {
        public RequestContext(HttpListenerRequest request, HttpListenerResponse response)
        {
            Request = request;
            Response = response;
        }

        public HttpListenerRequest Request { get; private set; }
        public HttpListenerResponse Response { get; private set; }
    }
}
