using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Model.Proxy;

namespace XeroConnector.Model.Interfaces
{
    public interface IInvoice : IModel
    {
        [XmlElement]
        Guid InvoiceID { get; set; }

        [XmlElement]
        string InvoiceNumber { get; set; }

        [XmlElement]
        string Reference { get; set; }

        [XmlElement]
        InvoiceTypes InvoiceType { get; set; }

        [XmlElement]
        Contact Contact { get; set; }

        [XmlElement]
        DateTime? Date { get; set; }

        [XmlElement]
        DateTime? DueDate { get; set; }

        [XmlElement]
        InvoiceStatusCodes Status { get; set; }

        [XmlElement(ElementName = "LineAmountTypes")]
        LineAmountTypes LineAmountType { get; set; }

        [XmlElement]
        decimal? SubTotal { get; set; }

        [XmlElement]
        decimal? Total { get; set; }

        [XmlElement]
        decimal? TotalTax { get; set; }

        [XmlElement("UpdatedDateUTC")]
        DateTime? UpdatedDate { get; set; }

        [XmlElement]
        string CurrencyCode { get; set; }

        [XmlElement]
        decimal? AmountDue { get; set; }

        [XmlElement]
        decimal? AmountPaid { get; set; }

        [XmlElement]
        decimal? AmountCredited { get; set; }

        [LazyLoad]
        [XmlArray("LineItems")]
        [XmlArrayItem("LineItem", typeof(LineItem))]
        Collection<LineItem> LineItems { get; set; }

        [LazyLoad]
        [XmlArray]
        Collection<Payment> Payments { get; set; }
    }
}