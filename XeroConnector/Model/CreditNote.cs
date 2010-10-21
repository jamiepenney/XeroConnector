using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Interfaces;
using XeroConnector.Model.Interfaces;
using XeroConnector.Model.Proxy;

namespace XeroConnector.Model
{
    public class CreditNote : ModelBase, ICreditNote
    {
        internal CreditNote() : base(null) { }

        public CreditNote(IXeroSession session) : base(session)
        {
        }

        [XmlElement]
        public Guid CreditNoteID { get; set; }

        [XmlElement]
        public string Reference { get; set; }

        [XmlElement]
        public CreditNoteTypes Type { get; set; }

        [XmlElement]
        public Contact Contact { get; set; }

        [XmlElement]
        public DateTime Date { get; set; }

        [XmlElement]
        public CreditNoteStatusCodes Status { get; set; }

        [XmlElement]
        public LineAmountTypes LineAmountTypes { get; set; }

        [LazyLoad]
        [XmlElement("LineItems")]
        public Collection<LineItem> InvoiceLines { get; set; }

        [XmlElement]
        public decimal? SubTotal { get; set; }

        [XmlElement]
        public decimal? Total { get; set; }

        [XmlElement]
        public decimal? TotalTax { get; set; }

        [XmlElement]
        public DateTime? UpdatedDateUTC { get; set; }

        [XmlElement]
        public string CurrencyCode { get; set; }

        [XmlElement]
        public DateTime FullyPaidOnDate { get; set; }

        [XmlElement]
        public string CreditNoteNumber { get; set; }

        public override void LoadDetailedInformation()
        {
            XeroSession.LoadCreditNoteDetails(this);
        }
    }
}