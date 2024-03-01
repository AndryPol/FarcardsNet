using FarcardContract.Data.Farcard6;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace FarcardContract.HttpData.Farcard6
{
    public class TransactionExDtoRequest
    {
        public List<TransactionInfoEx> Transactions { get; private set; }

        
        public byte[] InpBuf { get; private set; }

        public UInt16 InpKind { get; private set; }

        public TransactionExDtoRequest(List<TransactionInfoEx> transactions, byte[] inpBuf, ushort inpKind)
        {
            Transactions = transactions;
            InpBuf = inpBuf;
            InpKind = inpKind;
        }

        public static TransactionExDtoRequest GetRequestFromXml(string body)
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
                inpKind = (ushort)((attrKind != null && ushort.TryParse(attrKind, out var kind)) ? kind : 1);
            }

            return new TransactionExDtoRequest(list, inpBuf, inpKind);
        }

        public static TransactionExDtoRequest GetRequestFromJson(string json)
        {
            return JsonConvert.DeserializeObject<TransactionExDtoRequest>(json);
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
                var transact = transactions.AppendElement(nameof(TransactionsEx));
                transact.AppendAttributesFromObject(transaction);
            }

            if (InpBuf != null && InpBuf.Length > 0)
            {
                var inpBuf = root.AppendXmlBuffer("INPBUF", InpBuf);

                if (inpBuf != null && InpKind != 0)
                {
                    inpBuf.AppendAttribute(nameof(InpKind), InpKind);
                }
            }

            return doc.OuterXml;
        }
    }

    public class TransactionExDtoResponse
    {

        public int Result { get; private set; }

        public byte[] OutBuf { get; private set; }

        public UInt16 OutKind { get; private set; }

        public TransactionExDtoResponse(int result, byte[] outBuf, ushort outKind)
        {
            Result = result;
            OutBuf = outBuf;
            OutKind = outKind;
        }

        public static TransactionExDtoResponse GetResponse(string body)
        {
            var document = new XmlDocument();
            document.LoadXml(body);
            var transNode = document.Node(nameof(TransactionsEx));
            var res = transNode.Attribute<int>(nameof(Result));

            byte[] outBuf = null;
            ushort outKind = 0;

            document.GetBufferXmlFromNode(nameof(outBuf), nameof(outKind), out outBuf, out outKind);

            return new TransactionExDtoResponse(res, outBuf, outKind);
        }

        public static TransactionExDtoResponse GetResponseFromJson(string json)
        {
            return JsonConvert.DeserializeObject<TransactionExDtoResponse>(json);
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
