using FarcardContract;
using FarcardContract.Farcard6;
using FarcardContract.HttpData.Farcard6;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using Exception = System.Exception;


namespace HttpFarcardService
{
    public class FarcardHttpService
    {
        private readonly Logger<FarcardHttpService> _logger = new Logger<FarcardHttpService>(console: Environment.UserInteractive);

        private HttpFarcardServerSetting _serverSetting;

        private IFarcards6 _farcardProcessor;

        private Encoding _encoding;

        private HttpServer _httpServer;

        public delegate void ActionRequest(RequestContext context, string body);

        public Dictionary<string, ActionRequest> delegateDictionary = new Dictionary<string, ActionRequest>();



        public void Start()
        {
            try
            {

                _serverSetting = HttpFarcardServerSetting.GetSettings();

                _logger.SetLevel(_serverSetting.LogLevel);

                InitDelegateFromSettings();

                _logger.Trace("Starting...");
                _encoding = Encoding.GetEncoding(_serverSetting.AnswerEncoding);

                FileInfo dllinfo = null;
                if (!string.IsNullOrWhiteSpace(_serverSetting.DLL))
                    dllinfo = new FileInfo(_serverSetting.DLL);

                _farcardProcessor = new Farcards6Fabric().GetProcessor(dllinfo);
                _logger.Trace($"Loaded card processor is {_farcardProcessor.GetType().FullName}");

                _httpServer = new HttpServer($"http://*:{_serverSetting.Port}/");
                _httpServer.Subscribe(ProcessRequest);
                _logger.GetLogger().Info($"Started server: {_httpServer.Url}");

                _farcardProcessor?.Init();


            }
            catch (Exception ex)
            {
                _logger.Error($"General server error {ex}");
                DisposeServer();
            }
        }

        void InitDelegateFromSettings()
        {
            delegateDictionary.Clear();
            if (!delegateDictionary.ContainsKey(_serverSetting.GetCardInfoEx))
                delegateDictionary.Add(_serverSetting.GetCardInfoEx, GetCardInfoEx);
            if (!delegateDictionary.ContainsKey(_serverSetting.TransactionsEx))
                delegateDictionary.Add(_serverSetting.TransactionsEx, TransactionsEx);
            if (!delegateDictionary.ContainsKey(_serverSetting.FindEmail))
                delegateDictionary.Add(_serverSetting.FindEmail, FindEmail);
            if (!delegateDictionary.ContainsKey(_serverSetting.GetCardImageEx))
                delegateDictionary.Add(_serverSetting.GetCardImageEx, GetCardImageEx);
            
            _logger.GetLogger().Info("RouteServer");

            foreach (var delegates in delegateDictionary)
            {
                _logger.GetLogger().Info($"route: {delegates.Key} function: {delegates.Value.Method.Name}");
            }
        }




        void ProcessRequest(RequestContext context)
        {
            string body = string.Empty;
            var request = context.Request;
            try
            {
                var enc = request.ContentEncoding;
                _logger.Trace($"Begin request {request.Url}");
                using (var ms = new MemoryStream())
                {
                    request.InputStream.CopyTo(ms);
                    body = request.ContentEncoding.GetString(ms.ToArray());
                }
                _logger.Trace($"\t {body}");

                if ((request.ContentType != null) &&
                ((request.ContentType.Contains("xml")) ||
                request.ContentType.Contains("json")))
                {

                    var localPath = request.Url.LocalPath.Trim('/', '\\');

                    if (delegateDictionary.ContainsKey(localPath))
                    {
                        delegateDictionary[localPath].Invoke(context, body);
                    }
                    else
                    {
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        WriteContent(context, $"route{localPath} not found", "text/plain");
                    }
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    WriteContent(context, $"Unsupport contentType : {request.ContentType}", "text/plain");
                }

            }
            catch (Exception ex)
            {
                _logger.Error($"Error on {request.Url}\r\n{request}\r\n");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                WriteContent(context, ex.Message, "text/plain");
            }
            finally
            {
                context.Response.OutputStream.Close();
                var code = context.Response.StatusCode;
                _logger.Trace($"End request {request.Url} statusCode: {code} {(HttpStatusCode)code}  ");
            }
        }

        private void GetCardInfoEx(RequestContext context, string body)
        {

            GetCardInfoExDTORequest req;
            if (RequestIsXml(context))
                req = GetCardInfoExDTORequest.GetRequestFromXml(body);
            else
                req = GetCardInfoExDTORequest.GetRequestFromJson(body);

            byte[] outBuf = null;
            BuffKind outKind = 0;
            var cardInfo = new CardInfoEx();
            int res = 1;

            try
            {
                res = _farcardProcessor.GetCardInfoEx(req.Card, req.Restaurant, req.UnitNo, ref cardInfo, req.InpBuf,
                (BuffKind)req.InpKind, out outBuf, out outKind);
            }
            catch (Exception ex)
            {
                _logger.GetLogger().Error(ex);
            }

            var resp = new GetCardInfoExDTOResponse(res, cardInfo, outBuf, (UInt16)outKind);
            if (RequestIsXml(context))
            {
                var result = resp.ToXml(_encoding);
                WriteContent(context, result, "text/xml");
            }
            else
            {
                var result = resp.ToJson();
                WriteContent(context, result, "text/json");
            }
        }

        private void TransactionsEx(RequestContext context, string body)
        {
            TransactionExDTORequest req;
            if (RequestIsXml(context))
                req = TransactionExDTORequest.GetRequestFromXml(body);
            else
                req = TransactionExDTORequest.GetRequestFromJson(body);

            byte[] outBuf = null;
            BuffKind outKind = 0;
            var res = 1;
            try
            {
                res = _farcardProcessor.TransactionsEx(req.Transactions, req.InpBuf, (BuffKind)req.InpKind, out outBuf,
                     out outKind);

            }
            catch (Exception ex)
            {
                _logger.GetLogger().Error(ex);
            }

            var resp = new TransactionExDTOResponse(res, outBuf, (UInt16)outKind);

            if (RequestIsXml(context))
            {
                var result = resp.ToXml(_encoding);
                WriteContent(context, result, "text/xml");
            }
            else
            {
                var result = resp.ToJson();
                WriteContent(context, result, "text/json");
            }

        }

        private void FindEmail(RequestContext context, string body)
        {
            FindEmailDTORequest req;
            if (RequestIsXml(context))
                req = FindEmailDTORequest.GetRequestFromXml(body);
            else
                req = FindEmailDTORequest.GetRequestFromJson(body);
            var holderInfo = new HolderInfo();
            int res = 1;
            try
            {
                res = _farcardProcessor.FindEmail(req.Email, ref holderInfo);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            var resp = new FindEmailDTOResponse(res, holderInfo.ClientId, holderInfo.Card, holderInfo.Owner);

            if (RequestIsXml(context))
            {
                var result = resp.ToXml(_encoding);
                WriteContent(context, result, "text/xml");
            }
            else
            {
                var result = resp.ToJson();
                WriteContent(context, result, "text/json");
            }
        }

        private void GetCardImageEx(RequestContext context, string body)
        {
            try
            {
                GetCardImageExDTORequest req;
                if (RequestIsXml(context))
                    req = GetCardImageExDTORequest.GetRequestFromXml(body);
                else
                    req = GetCardImageExDTORequest.GetRequestFromJson(body);

                var textInfo = new TextInfo();
                var res = _farcardProcessor.GetCardImageEx(req.CardCode, ref textInfo);
                if (res != 0)
                    throw new Exception("Card or image not found");
                var file = new FileInfo(textInfo.Text);
                if (!file.Exists)
                    throw new FileNotFoundException(textInfo.Text);

                using (var ms = new MemoryStream())
                {
                    using (var sr = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                        sr.CopyTo(ms);
                    var buf = ms.ToArray();

                    context.Response.ContentType = "image/jpg";
                    context.Response.ContentLength64 = buf.Length;
                    context.Response.OutputStream.Write(buf, 0, buf.Length);
                }

            }
            catch (Exception ex)
            {
                var resp = new GetCardImageExDTOResponse(ex.Message);

                if (RequestIsXml(context))
                {
                    var result = resp.ToXml(_encoding);
                    WriteContent(context, result, "text/xml");
                }
                else
                {
                    var result = resp.ToJson();
                    WriteContent(context, result, "text/json");
                }
            }
        }


        private void WriteContent(RequestContext context, string message, string contentType)
        {
            var response = context.Response;
            response.ContentEncoding = _encoding;
            var buf = _encoding.GetBytes(message);
            response.ContentType = contentType + "; charset=" + _encoding.WebName;
            response.ContentLength64 = buf.Length;

            _logger.Trace($"\t {message}");

            response.OutputStream.Write(buf, 0, buf.Length);
        }

        bool RequestIsXml(RequestContext context)
        {
            if (context == null ||
               context.Request == null ||
               context.Request.ContentType == null)
                return false;

            return context.Request.ContentType.Contains("xml");
        }

        public void Stop()
        {
            try
            {
                _logger.Trace("Stopping...");
                DisposeServer();
                _logger.Trace("Stoped");
                _farcardProcessor?.Done();
            }
            catch (Exception ex)
            {
                _logger.Error($"General server error {ex}");
            }
        }

        private void DisposeServer()
        {
            if (_httpServer != null)
            {
                _httpServer.Dispose();
                _httpServer = null;
            }
        }
    }
}
