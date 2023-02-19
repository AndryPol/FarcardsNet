using FarcardContract;

namespace HttpFarcardService
{
    public class HttpFarcardServerSetting : XmlSettings<HttpFarcardServerSetting>
    {
        public int LogLevel { get; set; } = 5;

        public string DLL { get; set; }

        public uint Port { get; set; } = 8080;

        public int AnswerEncoding { get; set; } = 65001;

        public string GetCardInfoEx { get; set; } = "getcardinfoex.php";

        public string TransactionsEx { get; set; } = "transactionsex.php";

        public string FindEmail { get; set; } = "findemail.php";

        public string GetCardImageEx { get; set; } = "getcardimageex.php";
    }
}
