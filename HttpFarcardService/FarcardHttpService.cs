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

                _logger.Info("Starting...");
                _encoding = Encoding.GetEncoding(_serverSetting.AnswerEncoding);

                _logger.Info($"Encoding server {_encoding.EncodingName}");
                _logger.Info($"PathDll: {_serverSetting.DLL}");
                FileInfo dllInfo = null;
                if (!string.IsNullOrWhiteSpace(_serverSetting.DLL))
                {
                    dllInfo = new FileInfo(_serverSetting.DLL);
                    _logger.Info($"FullPathDll: {dllInfo.FullName}");
                }

                _farcardProcessor = new Farcards6Factory().GetProcessor(dllInfo);
                _logger.Info($"Loaded Card Plugin Is {_farcardProcessor.GetType().FullName}");
                _logger.Info("Start HttpServer");
                _httpServer = new HttpServer($"http://*:{_serverSetting.Port}/", _serverSetting.LogLevel);
                _httpServer.Subscribe(ProcessRequest);
                _logger.Info($"Started Server: {_httpServer.Url}");

                _logger.Info("Plugin Init");
                _farcardProcessor?.Init();
                _logger.Info("Plugin Init Complete");

            }
            catch (Exception ex)
            {
                _logger.Error($"General Server Error {ex}");
                DisposeServer();
            }
        }

        void InitDelegateFromSettings()
        {
            _logger.Trace("Init Delegate");

            delegateDictionary.Clear();

            if (!delegateDictionary.ContainsKey(_serverSetting.GetCardInfoEx))
            {
                delegateDictionary.Add(_serverSetting.GetCardInfoEx, GetCardInfoEx);
                _logger.Info($"AddDelegate: {nameof(GetCardInfoEx)} _ {_serverSetting.GetCardInfoEx}");
            }

            if (!delegateDictionary.ContainsKey(_serverSetting.TransactionsEx))
            {
                delegateDictionary.Add(_serverSetting.TransactionsEx, TransactionsEx);
                _logger.Info($"AddDelegate: {nameof(TransactionsEx)} _ {_serverSetting.TransactionsEx}");
            }

            if (!delegateDictionary.ContainsKey(_serverSetting.FindEmail))
            {
                delegateDictionary.Add(_serverSetting.FindEmail, FindEmail);
                _logger.Info($"AddDelegate: {nameof(FindEmail)} _ {_serverSetting.FindEmail}");
            }

            if (!delegateDictionary.ContainsKey(_serverSetting.GetCardImageEx))
            {
                delegateDictionary.Add(_serverSetting.GetCardImageEx, GetCardImageEx);
                _logger.Info($"AddDelegate: {nameof(GetCardImageEx)} _ {_serverSetting.GetCardImageEx}");
            }

            _logger.Info("RouteServer");

            foreach (var delegates in delegateDictionary)
            {
                _logger.Info($"route: {delegates.Key} function: {delegates.Value.Method.Name}");
            }
        }




        void ProcessRequest(RequestContext context)
        {
            string body = string.Empty;
            var request = context.Request;
            try
            {
                var enc = request.ContentEncoding;
                _logger.Info($"Begin request {request.Url}");

                if (request.ContentLength64 <= 0)
                {
                    _logger.Error($"Request content is empty on {request.Url}");
                    context.Response.StatusCode = (int)HttpStatusCode.NoContent;
                    WriteContent(context, $"Request content is empty", "text/plain");
                    return;
                }

                using (var ms = new MemoryStream())
                {
                    request.InputStream.CopyTo(ms);
                    body = request.ContentEncoding.GetString(ms.ToArray());
                }
                _logger.Info($"\t {body}");

                if ((request.ContentType != null) &&
                ((request.ContentType.Contains("xml")) ||
                request.ContentType.Contains("json")))
                {

                    var localPath = request.Url.LocalPath.Trim('/', '\\');

                    if (delegateDictionary.ContainsKey(localPath))
                    {
                        _logger.Info($"Invoke delegate {delegateDictionary[localPath].Method.Name} by {localPath}");
                        delegateDictionary[localPath].Invoke(context, body);
                    }
                    else
                    {
                        _logger.Info($"Route {localPath} not found");
                        context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        WriteContent(context, $"route{localPath} not found", "text/plain");
                    }
                }
                else
                {
                    _logger.Info($"UnSupport ContentType: {request.ContentType}, content {body}");
                    context.Response.StatusCode = (int)HttpStatusCode.UnsupportedMediaType;
                    WriteContent(context, $"UnSupport contentType : {request.ContentType}", "text/plain");
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
            {
                _logger.Info($"GetCardInfoEx RequestXml: {body}");
                req = GetCardInfoExDTORequest.GetRequestFromXml(body);
            }
            else
            {
                _logger.Info($"GetCardInfoEx RequestJson: {body}");
                req = GetCardInfoExDTORequest.GetRequestFromJson(body);
            }

            byte[] outBuf = null;
            BuffKind outKind = 0;
            var cardInfo = new CardInfoEx();
            int res = 1;

            try
            {
                _logger.Info("GetCardInfoEx Start Processing");
                res = _farcardProcessor.GetCardInfoEx(req.Card, req.Restaurant, req.UnitNo, ref cardInfo, req.InpBuf,
                (BuffKind)req.InpKind, out outBuf, out outKind);
                _logger.Info($"GetCardInfoEx Processing Complete, Result:{res}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            var resp = new GetCardInfoExDTOResponse(res, cardInfo, outBuf, (UInt16)outKind);
            if (RequestIsXml(context))
            {
                var result = resp.ToXml(_encoding);
                _logger.Info($"GetCardInfoEx ResponseXml: {result}");
                WriteContent(context, result, "text/xml");
            }
            else
            {
                var result = resp.ToJson();
                _logger.Info($"GetCardInfoEx ResponseJson: {result}");
                WriteContent(context, result, "text/json");
            }
        }

        private void TransactionsEx(RequestContext context, string body)
        {
            TransactionExDtoRequest req;
            if (RequestIsXml(context))
            {
                _logger.Info($"TransactionEx RequestXml: {body}");
                req = TransactionExDtoRequest.GetRequestFromXml(body);
            }
            else
            {
                _logger.Info($"TransactionEx RequestJson: {body}");
                req = TransactionExDtoRequest.GetRequestFromJson(body);
            }

            byte[] outBuf = null;
            BuffKind outKind = 0;
            var res = 1;
            try
            {
                _logger.Info($"TransactionsEx Start Processing");
                res = _farcardProcessor.TransactionsEx(req.Transactions, req.InpBuf, (BuffKind)req.InpKind, out outBuf,
                     out outKind);
                _logger.Info($"TransactionsEx Processing Complete, Result:{res}");
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            var resp = new TransactionExDtoResponse(res, outBuf, (UInt16)outKind);

            if (RequestIsXml(context))
            {
                var result = resp.ToXml(_encoding);
                _logger.Info($"TransactionEx ResponseXml: {result}");
                WriteContent(context, result, "text/xml");
            }
            else
            {
                var result = resp.ToJson();
                _logger.Info($"TransactionEx ResponseJson: {result}");
                WriteContent(context, result, "text/json");
            }

        }

        private void FindEmail(RequestContext context, string body)
        {
            FindEmailDTORequest req;

            if (RequestIsXml(context))
            {
                _logger.Info($"FindEmail RequestXml: {body}");
                req = FindEmailDTORequest.GetRequestFromXml(body);
            }
            else
            {
                _logger.Info($"FindEmail RequestJson: {body}");
                req = FindEmailDTORequest.GetRequestFromJson(body);
            }

            var holderInfo = new HolderInfo();
            int res = 1;

            try
            {
                _logger.Info($"FindEmail Start Processing");
                res = _farcardProcessor.FindEmail(req.Email, ref holderInfo);
                _logger.Info($"FindEmail Processing Complete, Result:{res}");
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }

            var resp = new FindEmailDTOResponse(res, holderInfo.ClientId, holderInfo.Card, holderInfo.Owner);

            if (RequestIsXml(context))
            {
                var result = resp.ToXml(_encoding);
                _logger.Info($"FindEmail ResponseXml: {result}");
                WriteContent(context, result, "text/xml");
            }
            else
            {
                var result = resp.ToJson();
                _logger.Info($"FindEmail ResponseJson: {result}");
                WriteContent(context, result, "text/json");
            }
        }

        private void GetCardImageEx(RequestContext context, string body)
        {
            try
            {
                GetCardImageExDTORequest req;

                if (RequestIsXml(context))
                {
                    _logger.Info($"GetCardImageEx RequestXml: {body}");
                    req = GetCardImageExDTORequest.GetRequestFromXml(body);
                }
                else
                {
                    _logger.Info($"GetCardImageEx RequestJson: {body}");
                    req = GetCardImageExDTORequest.GetRequestFromJson(body);
                }

                var textInfo = new TextInfo();
                _logger.Info("GetCardImageEx Start Processing");
                var res = _farcardProcessor.GetCardImageEx(req.CardCode, ref textInfo);
                _logger.Info($"GetCardImageEx Processing Complete, Result:{res}");
                if (res != 0)
                    throw new Exception("Card or image not found");
                var file = new FileInfo(textInfo.Text);

                _logger.Info($"GetCardImageEx PhotoFile FullPath:{file.FullName}");

                if (!file.Exists)
                {
                    _logger.Info($"GetCardImageEx PhotoFile Path {file.FullName} not Exist ");
                    throw new FileNotFoundException(textInfo.Text);
                }

                using (var ms = new MemoryStream())
                {
                    using (var sr = new FileStream(file.FullName, FileMode.Open, FileAccess.Read))
                        sr.CopyTo(ms);
                    var imageBuffer = ms.ToArray();

                    context.Response.ContentType = "image/jpg";
                    context.Response.ContentLength64 = imageBuffer.Length;
                    context.Response.OutputStream.Write(imageBuffer, 0, imageBuffer.Length);
                    _logger.Info($"GetCardImageEx Response Photo {Convert.ToBase64String(imageBuffer)}");
                }

            }
            catch (Exception ex)
            {
                var resp = new GetCardImageExDTOResponse(ex.Message);

                if (RequestIsXml(context))
                {
                    var result = resp.ToXml(_encoding);
                    _logger.Error($"GetCardImageEx ResponseXml{result}", ex);
                    WriteContent(context, result, "text/xml");
                }
                else
                {
                    var result = resp.ToJson();
                    _logger.Error($"GetCardImageEx ResponseJson{result}", ex);
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

            _logger.Info($"Write Content: \t {message} by {context.Request.Url}");

            response.OutputStream.Write(buf, 0, buf.Length);
        }

        bool RequestIsXml(RequestContext context)
        {
            if (context == null ||
               context.Request == null ||
               context.Request.ContentType == null)
                return false;

            return context.Request.ContentType.ToLower().Contains("xml");
        }

        public void Stop()
        {
            try
            {
                _logger.Info("Stopping...");
                DisposeServer();
                _logger.Info("Stopped");
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
