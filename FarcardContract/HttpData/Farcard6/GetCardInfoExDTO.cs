using FarcardContract.Data.Farcard6;
using System;
using System.Text;
using System.Xml;
using Newtonsoft.Json;

namespace FarcardContract.HttpData.Farcard6
{
    public class GetCardInfoExDTORequest
    {
        public long Card { get; private set; }

        public UInt32 Restaurant { get; private set; }

        public UInt32 UnitNo { get; private set; }

        public byte[] InpBuf { get; private set; }

        public UInt16 InpKind { get; private set; }

        public GetCardInfoExDTORequest(long card, UInt32 restaurant, UInt32 unitNo, byte[] inpBuf, UInt16 inpKind)
        {
            Card = card;
            Restaurant = restaurant;
            UnitNo = unitNo;
            InpBuf = inpBuf;
            InpKind = inpKind;
        }

        public static GetCardInfoExDTORequest GetRequestFromXml(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var encoding = document.GetEncoding();
            var qry = document.Node("qry");
            long card = qry.Attribute<long>(nameof(Card));
            ushort restaurant = qry.Attribute<ushort>(nameof(Restaurant));
            ushort unitNo = qry.Attribute<ushort>(nameof(UnitNo));
            byte[] inpBuf = null;
            ushort inpKind = 0;
            var inpBufNode = document.Node(nameof(InpBuf), false);

            if (inpBufNode != null)
            {
                var inpDoc = new XmlDocument();
                var dec = inpDoc.CreateXmlDeclaration("1.0", encoding.WebName, null);
                inpDoc.AppendChild(dec);
                var importNode = inpDoc.ImportNode(inpBufNode.FirstChild, true);
                inpDoc.AppendChild(importNode);
                inpBuf = encoding.GetBytes(inpDoc.OuterXml);
                var attrkind = inpBufNode.Attribute(nameof(InpKind), false);
                ushort kind = 0;
                inpKind = (ushort)((attrkind != null && UInt16.TryParse(attrkind, out kind)) ? kind : 1);
            }

            return new GetCardInfoExDTORequest(card, restaurant, unitNo, inpBuf, inpKind);
        }

        public string ToXml(Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.AppendDeclaration(encoding);
            var root = doc.AppendElement("ROOT");
            var qry = root.AppendElement("QRY");
            qry.AppendAttribute(nameof(Card), Card);
            qry.AppendAttribute(nameof(Restaurant), Restaurant);
            qry.AppendAttribute(nameof(UnitNo), UnitNo);

            if (InpBuf != null && InpBuf.Length > 0)
            {
                var inpBuf = qry.AppendXmlBuffer(nameof(InpBuf), InpBuf);

                if (inpBuf != null && InpKind != 0)
                {
                    inpBuf.AppendAttribute(nameof(InpKind), InpKind);
                }
            }

            root.AppendChild(qry);
            return doc.OuterXml;
        }

        public static GetCardInfoExDTORequest GetRequestFromJson(string body)
        {
            return JsonConvert.DeserializeObject<GetCardInfoExDTORequest>(body);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }

    public class GetCardInfoExDTOResponse
    {
        public int Result { get; private set; }

        public CardInfoEx CardInfo { get; private set; }

        public byte[] OutBuf { get; private set; }

        public ushort OutKind { get; private set; }

        public GetCardInfoExDTOResponse(int result, CardInfoEx cardInfo, byte[] outBuf, ushort outKind)
        {
            Result = result;
            CardInfo = cardInfo;
            OutBuf = outBuf;
            OutKind = outKind;
        }



        public static GetCardInfoExDTOResponse GetResponseFromXml(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var encoding = document.GetEncoding();
            var getCardInfoEx = document.Node(nameof(GetCardInfoEx));
            int result = getCardInfoEx.Attribute<int>(nameof(Result));
            var info = getCardInfoEx.ParseAttributeToObject<CardInfoEx>();
            byte[] outBuf = null;
            ushort outKind = 0;

            document.GetBufferXmlFromNode(nameof(outBuf), nameof(outKind), out outBuf, out outKind);

            return new GetCardInfoExDTOResponse(result, info, outBuf, outKind);
        }

        public string ToXml(Encoding encoding)
        {
            var document = new XmlDocument();
            document.AppendDeclaration(encoding);
            var root = document.AppendElement("ROOT");
            var getCardInfoEx = root.AppendElement(nameof(GetCardInfoEx));
            getCardInfoEx.AppendAttribute(nameof(Result), Result);

            if (Result == 0)
            {
                if (CardInfo != null)
                    getCardInfoEx.AppendAttributesFromObject(CardInfo);

                if (OutBuf != null && OutBuf.Length > 0)
                {
                    var outBufNode = getCardInfoEx.AppendXmlBuffer(nameof(OutBuf), OutBuf);
                    if (OutKind != 0)
                        outBufNode.AppendAttribute(nameof(OutKind), OutKind);
                }
            }

            return document.OuterXml;
        }

        public static GetCardInfoExDTOResponse GetResponseFromJson(string body)
        {
            return JsonConvert.DeserializeObject<GetCardInfoExDTOResponse>(body);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
