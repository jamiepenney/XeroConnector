using System;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using XeroConnector.Interfaces;
using XeroConnector.Model.Interfaces;
using XeroConnector.Model.Proxy;

namespace XeroConnector.Model
{
    public class Contact : ModelBase, IContact
    {
        internal Contact() : base(null) {}

        public Contact(IXeroSession session, Guid contactID) : base(session)
        {
            ContactID = contactID;
        }

        [XmlElement]
        public Guid ContactID { get; set; }

        [LazyLoad]
        [XmlElement]
        public ContactStatusType ContactStatus { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [LazyLoad]
        [XmlElement]
        public string FirstName { get; set; }

        [LazyLoad]
        [XmlElement]
        public string LastName { get; set; }

        [LazyLoad]
        [XmlElement]
        public string EmailAddress { get; set; }

        [LazyLoad]
        [XmlElement]
        public string SkypeUserName { get; set; }

        [LazyLoad]
        [XmlElement]
        public string BankAccountDetails { get; set; }

        [LazyLoad]
        [XmlElement]
        public string TaxNumber { get; set; }

        [LazyLoad]
        [XmlElement]
        public string AccountsReceivableTaxType { get; set; }

        [LazyLoad]
        [XmlElement]
        public string AccountsPayableTaxType { get; set; }

        [LazyLoad]
        [XmlElement("UpdatedDateUTC")]
        public DateTime UpdatedDate { get; set; }

        [LazyLoad]
        [XmlElement]
        public bool IsSupplier { get; set; }

        [LazyLoad]
        [XmlElement]
        public bool IsCustomer { get; set; }

        [LazyLoad]
        [XmlElement]
        public string DefaultCurrency { get; set; }

        [LazyLoad]
        [XmlArray]
        public Collection<Address> Addresses { get; set; }

        [LazyLoad]
        [XmlArray("Phones")]
        public Collection<Phone> PhoneNumbers { get; set; }

        public override void LoadDetailedInformation()
        {
            XeroSession.LoadContactDetails(this);
        }
    }
}