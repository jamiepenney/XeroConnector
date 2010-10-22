using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Model.Proxy;

namespace XeroConnector.Model.Interfaces
{
    public interface ICreditNote : IModel
    {
        [XmlElement]
        Guid CreditNoteID { get; set; }

        [XmlElement]
        string Reference { get; set; }

        [XmlElement]
        CreditNoteTypes Type { get; set; }

        [XmlElement]
        Contact Contact { get; set; }

        [XmlElement]
        DateTime Date { get; set; }

        [XmlElement]
        CreditNoteStatusCodes Status { get; set; }

        [XmlElement]
        LineAmountTypes LineAmountTypes { get; set; }

        [XmlArray("LineItems")]
        [LazyLoad]
        Collection<LineItem> InvoiceLines { get; set; }

        [XmlElement]
        decimal? SubTotal { get; set; }

        [XmlElement]
        decimal? Total { get; set; }

        [XmlElement]
        decimal? TotalTax { get; set; }

        [XmlElement]
        DateTime? UpdatedDateUTC { get; set; }

        [XmlElement]
        string CurrencyCode { get; set; }

        [XmlElement]
        DateTime FullyPaidOnDate { get; set; }

        [XmlElement]
        string CreditNoteNumber { get; set; }
    }
}