using System.Xml.Serialization;
using XeroConnector.Interfaces;

namespace XeroConnector.Model
{
    public class Organisation : ModelBase
    {
        internal Organisation() : base(null) {}

        public Organisation(IXeroSession session) : base(session)
        {
        }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string LegalName { get; set; }

        [XmlElement]
        public string Version { get; set; }

        [XmlElement]
        public bool PaysTax { get; set; }

        [XmlElement]
        public string BaseCurrency { get; set; }
    }
}