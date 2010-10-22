using System.Xml.Serialization;
using XeroConnector.Interfaces;

namespace XeroConnector.Model
{
    public class TaxRate : ModelBase, ITaxRate
    {
        internal TaxRate() : base(null) { }

        public TaxRate(IXeroSession session) : base(session)
        {
        }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement]
        public string TaxType { get; set; }

        [XmlElement]
        public bool CanApplyToAssets { get; set; }

        [XmlElement]
        public bool CanApplyToEquity { get; set; }

        [XmlElement]
        public bool CanApplyToExpenses { get; set; }

        [XmlElement]
        public bool CanApplyToLiabilities { get; set; }

        [XmlElement]
        public bool CanApplyToRevenue { get; set; }

        [XmlElement]
        public decimal DisplayTaxRate { get; set; }

        [XmlElement("EffectiveRate")]
        public decimal EffectiveTaxRate { get; set; }
    }
}