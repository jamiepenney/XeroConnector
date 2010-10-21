using System;
using System.Xml.Serialization;

namespace XeroConnector.Model.Interfaces
{
    public interface IAccount : IModel
    {
        [XmlElement]
        Guid AccountID { get; set; }

        [XmlElement]
        string Code { get; set; }

        [XmlElement]
        string Name { get; set; }

        [XmlElement("Type")]
        AccountTypes AccountType { get; set; }

        [XmlElement]
        string TaxType { get; set; }

        [XmlElement]
        string Description { get; set; }

        [XmlElement]
        SystemAccountTypes? SystemAccount { get; set; }

        [XmlElement]
        bool EnablePaymentsToAccount { get; set; }

        [XmlElement]
        string BankAccountNumber { get; set; }
    }
}