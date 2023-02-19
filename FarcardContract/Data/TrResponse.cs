using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FarcardContract.Data
{
    [Serializable]
    [XmlRoot("TRRESPONSE")]
    public class TrResponse
    {
        public bool ShouldSerializeErrorCode() { return ErrorCode!=0; }
        public bool ShouldSerializeErrorText() { return ErrorText != null; }
        public bool ShouldSerializePlaceId() { return PlaceId != null; }

        [XmlAttribute("error_code")]
        public int ErrorCode { get; set; }

        [XmlAttribute("err_text")]
        public string ErrorText { get; set; }

        [XmlAttribute("placeid")]
        public string PlaceId { get; set; }
    }
}
