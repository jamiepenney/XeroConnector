using System.Xml.Serialization;
using XeroConnector.Interfaces;

namespace XeroConnector.Model
{
    public class Currency : ModelBase
    {
        internal Currency() : base(null) { }

        public Currency(IXeroSession session) : base(session)
        {
        }

        [XmlElement]
        public string Code { get; set; }

        [XmlElement]
        public string Description { get; set; }
    }
}