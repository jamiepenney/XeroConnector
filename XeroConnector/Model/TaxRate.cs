using XeroConnector.Interfaces;

namespace XeroConnector.Model
{
    public class TaxRate : ModelBase
    {
        internal TaxRate() : base(null) { }

        public TaxRate(IXeroSession session) : base(session)
        {
        }

        public string Name { get; set; }

        public string TaxType { get; set; }

        public bool CanApplyToAssets { get; set; }

        public bool CanApplyToEquity { get; set; }

        public bool CanApplyToExpenses { get; set; }

        public bool CanApplyToLiabilities { get; set; }

        public bool CanApplyToRevenue { get; set; }

        public decimal DisplayTaxRate { get; set; }
        
        public decimal EffectiveTaxRate { get; set; }
    }
}