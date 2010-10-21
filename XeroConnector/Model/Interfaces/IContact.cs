using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Model.Proxy;

namespace XeroConnector.Model.Interfaces
{
    public interface IContact : IModel
    {
        [XmlElement]
        Guid ContactID { get; set; }

        [XmlElement]
        string Name { get; set; }

        [LazyLoad]
        [XmlElement]
        ContactStatusType ContactStatus { get; set; }

        [LazyLoad]
        [XmlElement]
        string FirstName { get; set; }

        [LazyLoad]
        [XmlElement]
        string LastName { get; set; }

        [LazyLoad]
        [XmlElement]
        string EmailAddress { get; set; }

        [LazyLoad]
        [XmlElement]
        string SkypeUserName { get; set; }

        [LazyLoad]
        [XmlElement]
        string BankAccountDetails { get; set; }

        [LazyLoad]
        [XmlElement]
        string TaxNumber { get; set; }

        [LazyLoad]
        [XmlElement]
        string AccountsReceivableTaxType { get; set; }

        [LazyLoad]
        [XmlElement]
        string AccountsPayableTaxType { get; set; }

        [LazyLoad]
        [XmlElement]
        string DefaultCurrency { get; set; }

        [LazyLoad]
        [XmlElement]
        bool IsSupplier { get; set; }

        [LazyLoad]
        [XmlElement]
        bool IsCustomer { get; set; }

        [LazyLoad]
        [XmlArray]
        Collection<Address> Addresses { get; set; }

        [LazyLoad]
        [XmlArray("Phones")]
        Collection<Phone> PhoneNumbers { get; set; }

        [LazyLoad]
        [XmlElement("UpdatedDateUTC")]
        DateTime UpdatedDate { get; set; }
    }
}