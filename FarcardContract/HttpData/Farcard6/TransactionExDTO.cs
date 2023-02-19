using FarcardContract.Data.Farcard6;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FarcardContract.HttpData.Farcard6
{
    public class TransactionExDTORequest
    {
        public List<TransactionInfoEx> Transactions { get; private set; }

        
        public byte[] InpBuf { get; private set; }

        public UInt16 InpKind { get; private set; }

        public TransactionExDTORequest(List<TransactionInfoEx> transactions, byte[] inpBuf, ushort inpKind)
        {
            Transactions = transactions;
            InpBuf = inpBuf;
            InpKind = inpKind;
        }

        public static TransactionExDTORequest GetRequestFromXml(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var encoding = document.GetEncoding();
            XmlNodeList transactNodes = document.Nodes(nameof(TransactionsEx));
            if (transactNodes.Count == 0)
                throw new ArgumentException("Transactions Nodes Not Found");
            var list = new List<TransactionInfoEx>();
            foreach (XmlNode node in transactNodes)
            {
                var tr = node.ParseAttributeToObject<TransactionInfoEx>();
                if (tr != null)
                    list.Add(tr);
            }

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
                var attrKind = inpBufNode.Attribute(nameof(InpKind), false);
                ushort kind = 0;
                inpKind = (ushort)((attrKind != null && UInt16.TryParse(attrKind, out kind)) ? kind : 1);
            }

            return new TransactionExDTORequest(list, inpBuf, inpKind);
        }

        public static TransactionExDTORequest GetRequestFromJson(string json)
        {
            return JsonConvert.DeserializeObject<TransactionExDTORequest>(json);
        }

        public string ToJson()
        {
            var res = JsonConvert.SerializeObject(this);
            return res;
        }

        public string ToXml(Encoding encoding)
        {
            var doc = new XmlDocument();
            doc.AppendDeclaration(encoding);
            var root = doc.AppendElement("ROOT");
            var transactions = root.AppendElement(nameof(Transactions));
            foreach (var transaction in Transactions)
            {
                var tranct = transactions.AppendElement(nameof(TransactionsEx));
                tranct.AppendAttributesFromObject(transaction);
            }

            if (InpBuf != null && InpBuf.Length > 0 && InpKind > 0)
            {
                var inpBuf = root.AppendXmlBuffer(nameof(InpBuf), InpBuf);

                if (inpBuf != null && InpKind != 0)
                {
                    inpBuf.AppendAttribute(nameof(InpKind), InpKind);
                }
            }

            return doc.OuterXml;
        }
    }

    public class TransactionExDTOResponse
    {

        public int Result { get; private set; }

        public byte[] OutBuf { get; private set; }

        public UInt16 OutKind { get; private set; }

        public TransactionExDTOResponse(int result, byte[] outBuf, ushort outKind)
        {
            Result = result;
            OutBuf = outBuf;
            OutKind = outKind;
        }

        public static TransactionExDTOResponse GetResponse(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var transNode = document.Node(nameof(TransactionsEx));
            var res = transNode.Attribute<int>(nameof(Result));

            byte[] outBuf = null;
            ushort outKind = 0;

            document.GetBufferXmlFromNode(nameof(outBuf), nameof(outKind), out outBuf, out outKind);

            return new TransactionExDTOResponse(res, outBuf, outKind);
        }

        public static TransactionExDTOResponse GetResponseFromJson(string json)
        {
            return JsonConvert.DeserializeObject<TransactionExDTOResponse>(json);
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public string ToXml(Encoding encoding)
        {
            var document = new XmlDocument();
            document.AppendDeclaration(encoding);
            var root = document.AppendElement("ROOT");
            var getCardInfoEx = root.AppendElement(nameof(TransactionsEx));
            getCardInfoEx.AppendAttribute(nameof(Result), Result);


            if (OutBuf != null && OutBuf.Length > 0)
            {
                var outBufNode = getCardInfoEx.AppendXmlBuffer(nameof(OutBuf), OutBuf);
                if (OutKind != 0)
                    outBufNode.AppendAttribute(nameof(OutKind), OutKind);
            }

            return document.OuterXml;
        }
    }
}
