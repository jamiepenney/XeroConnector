using System.Xml.Serialization;

namespace XeroConnector.Model
{
    public class Address
    {
        [XmlElement]
        public AddressTypes AddressType { get; set; }

        [XmlElement("AddressLine1")]
        public string AddressLine1 { get; set; }

        [XmlElement("AddressLine2")]
        public string AddressLine2 { get; set; }

        [XmlElement("AddressLine3")]
        public string AddressLine3 { get; set; }

        [XmlElement("AddressLine4")]
        public string AddressLine4 { get; set; }

        [XmlElement("City")]
        public string City { get; set; }
        
        [XmlElement("Region")]
        public string Region { get; set; }
        
        [XmlElement("PostalCode")]
        public string PostalCode { get; set; }

        [XmlElement("Country")]
        public string Country { get; set; }
    }
}