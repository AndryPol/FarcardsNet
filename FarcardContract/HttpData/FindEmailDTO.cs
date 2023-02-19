using Newtonsoft.Json;
using System;
using System.Text;
using System.Xml;

namespace FarcardContract.HttpData.Farcard6
{
    public class FindEmailDTORequest
    {
        public string Email { get; private set; }

        public FindEmailDTORequest(string email)
        {
            Email = email;
        }

        public static FindEmailDTORequest GetRequestFromXml(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var qry = document.Node("qry");
            var email = qry.Attribute(nameof(Email));
            return new FindEmailDTORequest(email);
        }

        public string ToXml(Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.AppendDeclaration(encoding);
            var root = doc.AppendElement("ROOT");
            var qry = root.AppendElement("QRY");
            qry.SetAttribute(nameof(Email), Email);
            return doc.OuterXml;
        }

        public static FindEmailDTORequest GetRequestFromJson(string Body)
        {
            return JsonConvert.DeserializeObject<FindEmailDTORequest>(Body);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

    public class FindEmailDTOResponse
    {
        public int Result { get; private set; }
        public UInt32 Account { get; private set; }

        public long CardCode { get; private set; }

        public string Name { get; private set; }

        public FindEmailDTOResponse(int result, uint account, long cardCode, string name)
        {
            Result = result;
            Account = account;
            CardCode = cardCode;
            Name = name;
        }

        public static FindEmailDTOResponse GetResponseFromXml(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var findEmail = document.Node(nameof(FindEmail));
            var result = findEmail.Attribute<int>(nameof(Result));
            UInt32 account = 0;
            long card = 0;
            string name = null;
            if (result == 0)
            {
                account = findEmail.Attribute<UInt32>(nameof(Account));
                card = findEmail.Attribute<long>(nameof(CardCode));
                name = findEmail.Attribute(nameof(Name));
            }

            return new FindEmailDTOResponse(result, account, card, name);
        }

        public string ToXml(Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.AppendDeclaration(encoding);
            var root = doc.AppendElement("ROOT");
            var findEmail = root.AppendElement(nameof(FindEmail));
            if (Result == 0)
                findEmail.AppendAttributesFromObject(this);
            return doc.OuterXml;
        }

        public static FindEmailDTOResponse GetResponseFromJson(string body)
        {
            return JsonConvert.DeserializeObject<FindEmailDTOResponse>(body);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
