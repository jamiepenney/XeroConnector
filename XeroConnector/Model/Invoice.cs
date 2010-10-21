using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Interfaces;
using XeroConnector.Model.Interfaces;
using XeroConnector.Model.Proxy;

namespace XeroConnector.Model
{
    public class Invoice : ModelBase, IInvoice
    {
        internal Invoice() : this(null)
        {
        }

        public Invoice(IXeroSession xeroSession)
            : base(xeroSession)
        {
            Payments = new Collection<Payment>();
            LineItems = new Collection<LineItem>();
        }

        [XmlElement]
        public Guid InvoiceID { get; set; }

        [XmlElement]
        public string InvoiceNumber { get; set; }

        [XmlElement]
        public string Reference { get; set; }

        [XmlElement]
        public InvoiceTypes InvoiceType { get; set; }

        [XmlElement]
        public Contact Contact { get; set; }

        [XmlElement]
        public DateTime? Date { get; set; }

        [XmlElement]
        public DateTime? DueDate { get; set; }

        [XmlElement]
        public InvoiceStatusCodes Status { get; set; }

        [XmlElement("LineAmountTypes")]
        public LineAmountTypes LineAmountType { get; set; }

        [XmlElement]
        public decimal? SubTotal { get; set; }

        [XmlElement]
        public decimal? Total { get; set; }

        [XmlElement]
        public decimal? TotalTax { get; set; }

        [XmlElement("UpdatedDateUTC")]
        public DateTime? UpdatedDate { get; set; }

        [XmlElement]
        public string CurrencyCode { get; set; }

        [XmlElement]
        public decimal? AmountDue { get; set; }

        [XmlElement]
        public decimal? AmountPaid { get; set; }

        [XmlElement]
        public decimal? AmountCredited { get; set; }

        [LazyLoad]
        [XmlArray("LineItems")]
        [XmlArrayItem("LineItem", typeof(LineItem))]
        public Collection<LineItem> LineItems { get; set; }

        [LazyLoad]
        [XmlArray]
        public Collection<Payment> Payments { get; set; }

        public override void LoadDetailedInformation()
        {
            XeroSession.GetInvoiceDetails(this);
        }
    }
}