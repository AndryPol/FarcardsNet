using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FarcardContract.Data.BufferData
{
    [Serializable]
    public class ItemList<T> : List<T>
    {
        [XmlAttribute("count")]
        public int GetCount => Count;
    }
}
