using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Interfaces;

namespace XeroConnector.Model
{
    public class LineItem : ModelBase
    {
        internal LineItem() :this(null) { }

        public LineItem(IXeroSession session) : base(session)
        {
        }

        [XmlElement]
        public string Description { get; set; }

        [XmlElement]
        public decimal? Quantity { get; set; }

        [XmlElement]
        public decimal? UnitAmount { get; set; }

        [XmlElement]
        public string TaxType { get; set; }

        [XmlElement]
        public decimal? TaxAmount { get; set; }

        [XmlElement]
        public decimal? LineAmount { get; set; }

        [XmlElement]
        public string AccountCode { get; set; }

        [XmlArray("Tracking")]
        [XmlArrayItem("TrackingCategory", typeof(TrackingCategory))]
        public Collection<TrackingCategory> TrackingCategories { get; set; }
    }
}