using System.Xml.Serialization;

namespace XeroConnector.Model
{
    public class Phone
    {
        [XmlElement]
        public PhoneTypes PhoneType { get; set; }

        [XmlElement("PhoneCountryCode")]
        public string CountryCode { get; set; }

        [XmlElement("PhoneAreaCode")]
        public string AreaCode { get; set; }

        [XmlElement("PhoneNumber")]
        public string PhoneNumber { get; set; }
    }
}