using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace FarcardContract.HttpData.Farcard6
{
    public class GetCardImageExDTORequest
    {
        public long CardCode { get; private set; }

        public GetCardImageExDTORequest(long cardCode)
        {
            CardCode = cardCode;
        }

        public static GetCardImageExDTORequest GetRequestFromXml(string request)
        {
            var document = new XmlDocument();
            document.LoadXml(request);
            var qryNode = document.Node("qry");
            var cardCode = qryNode.Attribute<long>(nameof(CardCode));
            return new GetCardImageExDTORequest(cardCode);
        }

        public string ToXml(Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.AppendDeclaration(encoding);
            var root = doc.AppendElement("ROOT");
            var qry = root.AppendElement("QRY");
            qry.AppendAttribute(nameof(CardCode), CardCode);
            return doc.OuterXml;
        }

        public static GetCardImageExDTORequest GetRequestFromJson(string StrBody)
        {
            return JsonConvert.DeserializeObject<GetCardImageExDTORequest>(StrBody);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class GetCardImageExDTOResponse
    {
        public string ErrorText { get; private set; }

        public GetCardImageExDTOResponse(string errorText)
        {
            ErrorText = errorText;
        }

        public static GetCardImageExDTOResponse GetResponseFromXml(string response)
        {
            var document = new XmlDocument();
            document.LoadXml(response);
            var getCardImageEx = document.Node(nameof(GetCardImageEx));
            var errorText = getCardImageEx.Attribute(nameof(ErrorText));
            return new GetCardImageExDTOResponse(errorText);
        }

        public string ToXml(Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.AppendDeclaration(encoding);
            var root = doc.AppendElement("ROOT");
            var getCardImageEx = root.AppendElement(nameof(GetCardImageEx));
            getCardImageEx.SetAttribute(nameof(ErrorText), ErrorText);
            return doc.OuterXml;
        }

        public static GetCardImageExDTOResponse GetResponseFromJson(string response)
        {
            return JsonConvert.DeserializeObject<GetCardImageExDTOResponse>(response);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
