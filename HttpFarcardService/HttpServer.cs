using System;
using System.Net;
using System.Reactive.Linq;

namespace HttpFarcardService
{
    public class HttpServer : IObservable<RequestContext>, IDisposable
    {
        readonly HttpListener _listener;
        readonly IObservable<RequestContext> _stream;
        bool _play;
        public string Url { get; }

        public HttpServer(string url)
        {
            Url = url;
            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
            _listener.Start();
            _play = true;
            _stream = ObservableHttpContext();
        }


        public void Dispose()
        {
            _play = false;
            _listener.Stop();
        }

        public IDisposable Subscribe(IObserver<RequestContext> observer)
        {
            return _stream.Subscribe(observer);
        }

        IObservable<RequestContext> ObservableHttpContext()
        {
            return Observable.Create<RequestContext>(obs =>
                    Observable.FromAsyncPattern<HttpListenerContext>(_listener.BeginGetContext,
                            _listener.EndGetContext)()
                        .Select(c => new RequestContext(c.Request, c.Response))
                        .Subscribe(obs))
                .DoWhile(() => _play)
                .Retry()
                .Publish()
                .RefCount();
        }
    }
}
