using System;
using System.Xml.Serialization;
using XeroConnector.Interfaces;
using XeroConnector.Model.Interfaces;

namespace XeroConnector.Model
{
    public class Account : ModelBase, IAccount
    {
        internal Account() : base(null) { }

        public Account(IXeroSession session) : base(session)
        {
        }

        [XmlElement]
        public Guid AccountID { get; set; }

        [XmlElement]
        public string Code { get; set; }

        [XmlElement]
        public string Name { get; set; }

        [XmlElement("Type")]
        public AccountTypes AccountType { get; set; }

        [XmlElement]
        public string TaxType { get; set; }

        [XmlElement]
        public string Description { get; set; }

        [XmlElement]
        public SystemAccountTypes? SystemAccount { get; set; }

        [XmlElement]
        public bool EnablePaymentsToAccount { get; set; }

        [XmlElement]
        public string BankAccountNumber { get; set; }
    }
}