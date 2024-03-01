using System;
using System.Net;
using System.Reactive.Linq;
using FarcardContract;

namespace HttpFarcardService
{
    public class HttpServer : IObservable<RequestContext>, IDisposable
    {
        private readonly HttpListener _listener;
        private readonly IObservable<RequestContext> _stream;
        private bool _play;
        private readonly Logger<HttpServer> _logger = new Logger<HttpServer>();
        public string Url { get; }

        public HttpServer(string url, int loggerLevel = 5)
        {
            _logger.SetLevel(loggerLevel);
            _logger.Info($"Server start: {url}");
            Url = url;
            _listener = new HttpListener();
            _listener.Prefixes.Add(url);
            _listener.Start();
            _play = true;
            _stream = ObservableHttpContext();
            _logger.Info($"Server Started");

        }


        public void Dispose()
        {
            _logger.Info("Stop Server");
            _play = false;
            _listener.Stop();
            _logger.Info("Server Stopped");
        }

        public IDisposable Subscribe(IObserver<RequestContext> observer)
        {
            _logger.Info($"Subscribe {observer?.ToString()}");
            return _stream.Subscribe(observer);
        }

        IObservable<RequestContext> ObservableHttpContext()
        {

            return Observable.Create<RequestContext>(obs =>
                    Observable.FromAsync(() => _listener.GetContextAsync())
                        //Observable.FromAsyncPattern<HttpListenerContext>(_listener.BeginGetContext,
                        //      _listener.EndGetContext)()
                        .Select(c =>
                        {
                            _logger.Info($"Create context {c.Request.Url}");
                            return new RequestContext(c.Request, c.Response);
                        })
                        .Subscribe(obs))
                .DoWhile(() => _play)
                .Retry()
                .Publish()
                .RefCount();
        }
    }
}
