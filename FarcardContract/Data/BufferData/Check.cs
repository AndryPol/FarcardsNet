using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    [Serializable]
    [XmlRoot("CHECK")]
    public class Check
    {
        [XmlAttribute("stationcode")]
        public string StationCode { get; set; }

        [XmlAttribute("restaurantcode")]
        public string RestaurantCode { get; set; }

        [XmlAttribute("cashservername")]
        public string CashServerName { get; set; }

        [XmlAttribute("generateddatetime")]
        public DateTime GeneratedDateTime { get; set; }

        [XmlAttribute("chmode")]
        public CheckModes CheckMode { get; set; }

        [XmlAttribute("locale")]
        public int Locale { get; set; }

        [XmlAttribute("shiftdate")]
        public DateTime ShiftDate { get; set; }

        [XmlAttribute("shiftnum")]
        public int ShiftNum { get; set; }

        [XmlAttribute("protocolversion")]
        public string ProtocolVersion { get; set; }

        [XmlAttribute("clientapp")]
        public string ClientApp { get; set; }

        [XmlAttribute("clientversion")]
        public string ClientVersion { get; set; }

        [XmlElement("CHECKDATA")]
        public CheckData CheckData { get; set; }

    }
}
