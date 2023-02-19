using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using FarcardContract;
using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using FarcardContract.Farcard6;
using FarcardContract.HttpData.Farcard6;

namespace HttpFarcard6Client
{
    [Export(typeof(IFarcards6))]
    public class HttpFarcardClient : IFarcards6, IDisposable
    {

        private HttpFarcardClientSettings _settings;

        private Logger<HttpFarcardClient> _logger;

        HttpClient GetNewClient()
        {
            var proxySettings = _settings.ProxySettings;
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;
            handler.SslProtocols = SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
            handler.UseProxy = proxySettings.UseProxy;
            if (proxySettings.UseProxy)
            {
                var proxy = new WebProxy(new Uri(proxySettings.Server));
                proxy.UseDefaultCredentials = proxySettings.BasicAuthentication;
                if (!proxySettings.BasicAuthentication)
                    proxy.Credentials =
                        new NetworkCredential(proxySettings.Username,
                            proxySettings.Password);
            }
            var client = new HttpClient(handler);
            var address = new Uri(_settings.Address);
            client.BaseAddress = address;
            return client;
        }

        public HttpFarcardClient()
        {
            _settings = HttpFarcardClientSettings.GetSettings();
            _logger = new Logger<HttpFarcardClient>(_settings.LogLevel);
        }

        public void Init()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        public void Done()
        {
            try
            {

            }
            catch (Exception ex)
            {

            }
        }

        public int GetCardInfoEx(long card, uint restaurant, uint unitNo, ref CardInfoEx cardInfo, byte[] inpBuf, BuffKind inpKind,
            out byte[] outBuf, out BuffKind outKind)
        {
            outBuf = null;
            outKind = BuffKind.None;
            int res = 1;
            try
            {
                using (var client = GetNewClient())
                {
                    var encoding = Encoding.UTF8;
                    var req = new GetCardInfoExDTORequest(card, restaurant, unitNo, inpBuf, (ushort)inpKind);

                    var reqContent = (!_settings.Json) ? new StringContent(req.ToXml(encoding), encoding, "text/xml") :
                        new StringContent(req.ToJson(), encoding, "text/json");

                    var httpResponse = Task.Run(async () => await client.PostAsync(_settings.GetCardInfoEx, reqContent)
                        .ConfigureAwait(false)).GetAwaiter().GetResult();
                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        if (httpResponse.Content.Headers.ContentLength > 0 &&
                            httpResponse.Content.Headers.ContentType.MediaType.Contains("plain"))
                        {
                            var message = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                                .ConfigureAwait(false)).GetAwaiter().GetResult();
                            throw new Exception(message);
                        }
                        throw new Exception(httpResponse.ReasonPhrase);
                    }

                    var mt = httpResponse.Content.Headers.ContentType.MediaType;
                    if (!mt.Contains("xml") && !mt.Contains("json"))
                        throw new Exception("Error parsing request");

                    var respContent = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                        .ConfigureAwait(false)).GetAwaiter().GetResult();

                    GetCardInfoExDTOResponse response = (mt.Contains("xml")) ?
                        response = GetCardInfoExDTOResponse.GetResponseFromXml(respContent) :
                        response = GetCardInfoExDTOResponse.GetResponseFromJson(respContent);

                    cardInfo = response.CardInfo;
                    outBuf = response.OutBuf;
                    outKind = (BuffKind)response.OutKind;
                    res = response.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                cardInfo.Locked = YesNo.Yes;
                cardInfo.WhyLock = "Processing server connect error";
            }

            return res;
        }

        public int TransactionsEx(List<TransactionInfoEx> transactionInfo, byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
        {
            outBuf = null;
            outKind = BuffKind.None;
            int res = 1;
            try
            {
                using (var client = GetNewClient())
                {
                    var encoding = Encoding.UTF8;
                    var req = new TransactionExDTORequest(transactionInfo, inpBuf, (ushort)inpKind);

                    var reqContent = (!_settings.Json) ? new StringContent(req.ToXml(encoding), encoding, "text/xml") :
                         new StringContent(req.ToJson(), encoding, "text/json");

                    var httpResponse = Task.Run(async () => await client.PostAsync(_settings.TransactionsEx, reqContent)
                    .ConfigureAwait(false)).GetAwaiter().GetResult();

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        if (httpResponse.Content.Headers.ContentLength > 0 &&
                            httpResponse.Content.Headers.ContentType.MediaType.Contains("plain"))
                        {
                            var message = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                                .ConfigureAwait(false)).GetAwaiter().GetResult();
                            throw new Exception(message);
                        }

                        throw new Exception(httpResponse.ReasonPhrase);
                    }

                    var mt = httpResponse.Content.Headers.ContentType.MediaType;

                    if ((!mt.Contains("xml")) && (!mt.Contains("json")))
                        throw new Exception("Error parsing request");
                    var respContent = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                        .ConfigureAwait(false)).GetAwaiter().GetResult();

                    TransactionExDTOResponse response = (mt.Contains("xml"))
                        ? response = TransactionExDTOResponse.GetResponse(respContent)
                        : response = TransactionExDTOResponse.GetResponseFromJson(respContent);

                    outBuf = response.OutBuf;
                    outKind = (BuffKind)response.OutKind;
                    res = response.Result;

                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            finally
            {
                if (res != 0 && (outBuf == null || outBuf.Length < 0))
                {
                    var tResp = new TrResponse();
                    tResp.ErrorCode = -1;
                    tResp.ErrorText = "Processing server connect error";
                    var xml = Serializer.SerializeObject(tResp);
                    if (!string.IsNullOrWhiteSpace(xml))
                    {
                        outBuf = Encoding.UTF8.GetBytes(xml);
                        outKind = BuffKind.Xml;
                    }
                }
            }

            return res;
        }

        public int FindEmail(string email, ref HolderInfo holderInfo)
        {
            int res = 1;
            try
            {
                using (var client = GetNewClient())
                {
                    var encoding = Encoding.UTF8;
                    var req = new FindEmailDTORequest(email);

                    var reqContent = (!_settings.Json) ? new StringContent(req.ToXml(encoding), encoding, "text/xml") :
                        new StringContent(req.ToJson(), encoding, "text/json");

                    var httpResponse = Task.Run(async () => await client.PostAsync(_settings.FindEmail, reqContent)
                        .ConfigureAwait(false)).GetAwaiter().GetResult();

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        if (httpResponse.Content.Headers.ContentLength > 0 &&
                            httpResponse.Content.Headers.ContentType.MediaType.Contains("plain"))
                        {
                            var message = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                                .ConfigureAwait(false)).GetAwaiter().GetResult();
                            throw new Exception(message);
                        }

                        throw new Exception(httpResponse.ReasonPhrase);
                    }

                    var mt = httpResponse.Content.Headers.ContentType.MediaType;

                    if ((!mt.Contains("xml")) && (!mt.Contains("json")))
                        throw new Exception("Error parsing request");
                    var respContent = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                        .ConfigureAwait(false)).GetAwaiter().GetResult();

                    FindEmailDTOResponse response = (mt.Contains("xml"))
                        ? FindEmailDTOResponse.GetResponseFromXml(respContent)
                        : FindEmailDTOResponse.GetResponseFromJson(respContent);

                    holderInfo.ClientId = response.Account;
                    holderInfo.Card = response.CardCode;
                    holderInfo.Owner = response.Name;
                    res = response.Result;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return res;
        }

        public void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public void FindAccountsByKind(FindKind kind, string findText, CBFind cbFind, IntPtr backPtr)
        {
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public void AnyInfo(byte[] inpBuf, out byte[] outBuf)
        {
            outBuf = null;
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public int GetDiscLevelInfoL(uint account, ref DiscLevelInfo info)
        {
            int res = 1;
            try
            {

            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return res;
        }

        public int GetCardImageEx(long card, ref TextInfo info)
        {
            int res = 1;
            try
            {
                using (var client = GetNewClient())
                {
                    var encoding = Encoding.UTF8;
                    var req = new GetCardImageExDTORequest(card);

                    var reqContent = (!_settings.Json)
                        ? new StringContent(req.ToXml(encoding), encoding, "text/xml")
                        : new StringContent(req.ToJson(), encoding, "text/json");

                    var httpResponse = Task.Run(async () => await client.PostAsync(_settings.GetCardImageEx, reqContent)
                        .ConfigureAwait(false)).GetAwaiter().GetResult();

                    if (!httpResponse.IsSuccessStatusCode)
                    {
                        if (httpResponse.Content.Headers.ContentLength > 0 &&
                            httpResponse.Content.Headers.ContentType.MediaType.Contains("plain"))
                        {
                            var message = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                                .ConfigureAwait(false)).GetAwaiter().GetResult();
                            throw new Exception(message);
                        }

                        throw new Exception(httpResponse.ReasonPhrase);
                    }

                    var mt = httpResponse.Content.Headers.ContentType.MediaType;
                    if (!mt.Contains("xml") && !mt.Contains("json") && !mt.Contains("image"))
                        throw new Exception("Error parsing request");

                    if (!mt.Contains("image"))
                    {
                        var respContent = Task.Run(async () => await httpResponse.Content.ReadAsStringAsync()
                            .ConfigureAwait(false)).GetAwaiter().GetResult();

                        GetCardImageExDTOResponse response = (mt.Contains("xml"))
                            ? response = GetCardImageExDTOResponse.GetResponseFromXml(respContent)
                            : response = GetCardImageExDTOResponse.GetResponseFromJson(respContent);
                        throw new Exception(response.ErrorText);
                    }

                    var binaryContent = Task.Run(async () => await httpResponse.Content.ReadAsByteArrayAsync()
                        .ConfigureAwait(false)).GetAwaiter().GetResult();
                    var parseExt = mt.Replace("image/", "");
                    var fi = new FileInfo($"1.{parseExt}");
                    using (var ms = new MemoryStream(binaryContent))
                    {
                        using (var fs = new FileStream(fi.FullName, FileMode.OpenOrCreate, FileAccess.Write))
                        {
                            ms.CopyTo(fs);
                            info.Text = fi.FullName;
                            res = 0;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return res;
        }

        public void Dispose()
        {

        }

    }
}
