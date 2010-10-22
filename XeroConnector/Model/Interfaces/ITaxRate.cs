using System.Xml.Serialization;

namespace XeroConnector.Model
{
    public interface ITaxRate : IModel
    {
        [XmlElement]
        string Name { get; set; }

        [XmlElement]
        string TaxType { get; set; }

        [XmlElement]
        bool CanApplyToAssets { get; set; }

        [XmlElement]
        bool CanApplyToEquity { get; set; }

        [XmlElement]
        bool CanApplyToExpenses { get; set; }

        [XmlElement]
        bool CanApplyToLiabilities { get; set; }

        [XmlElement]
        bool CanApplyToRevenue { get; set; }

        [XmlElement]
        decimal DisplayTaxRate { get; set; }

        [XmlElement("EffectiveRate")]
        decimal EffectiveTaxRate { get; set; }
    }
}