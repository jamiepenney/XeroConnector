using System;
using System.Xml.Serialization;

namespace XeroConnector.Model
{
    [XmlRoot("Payment")]
    public class Payment
    {
        [XmlElement]
        public Guid PaymentID { get; set; }

        [XmlElement]
        public DateTime Date { get; set; }

        [XmlElement]
        public decimal Amount { get; set; }
    }
}