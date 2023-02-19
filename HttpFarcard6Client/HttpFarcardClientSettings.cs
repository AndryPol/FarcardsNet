using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FarcardContract;

namespace HttpFarcard6Client
{
    public class HttpFarcardClientSettings:XmlSettings<HttpFarcardClientSettings>
    {
        public int LogLevel { get; set; } = 5;

        public string Address { get; set; } = "http://localhost:8080";

        public bool Json { get; set; } = true;

        public string GetCardInfoEx { get; set; } = "getcardinfoex.php";

        public string TransactionsEx { get; set; } = "transactionsex.php";

        public string FindEmail { get; set; } = "findemail.php";

        public string GetCardImageEx { get; set; } = "getcardimageex.php";

        public Proxy ProxySettings { get; set; }= new Proxy();
    }

    public class Proxy
    {
        public bool UseProxy { get; set; } = false;
        public bool BasicAuthentication { get; set; } = false;
        public string Server { get; set; } = "127.0.0.1";
        public UInt16 Port { get; set; } = 9944;
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";

    }
}
