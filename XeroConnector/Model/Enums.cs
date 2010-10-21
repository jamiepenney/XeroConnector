using System.Xml.Serialization;

namespace XeroConnector.Model
{
    public enum SystemAccountTypes
    {
        [XmlEnum("ACCOUNTSRECEIVABLE")]
        AccountsReceivable,
        [XmlEnum("ACCOUNTSPAYABLE")]
        AccountsPayable,
        [XmlEnum("BANKREVALUATIONS")]
        BankRevaluations,
        [XmlEnum("GST")]
        GST,
        [XmlEnum("GSTONIMPORTS")]
        GSTOnImports,
        [XmlEnum("HISTORICALADJUSTMENT")]
        HistoricalAdjustment,
        [XmlEnum("REALISEDCURRENCYGAINS")]
        RealisedCurrencyGains,
        [XmlEnum("RETAINEDEARNINGS")]
        RetainedEarnings,
        [XmlEnum("ROUNDING")]
        Rounding,
        [XmlEnum("TRACKINGTRANSFERS")]
        TrackingTransfers,
        [XmlEnum("UNPAIDEXPENSECLAIMS")]
        UnpaidExpenseClaims,
        [XmlEnum("UNREALISEDCURRENCYGAINS")]
        UnrealisedCurrencyGains,
    }

    public enum AccountTypes
    {
        [XmlEnum("BANK")]
        Bank,
        [XmlEnum("CURRENT")]
        CurrentAssets,
        [XmlEnum("CURRLIAB")]
        CurrentLiabilities,
        [XmlEnum("DEPRECIATN")]
        Depreciationn,
        [XmlEnum("DIRECTCOSTS")]
        DirectCosts,
        [XmlEnum("EQUITY")]
        Equity,
        [XmlEnum("EXPENSE")]
        Expense,
        [XmlEnum("FIXED")]
        Fixed,
        [XmlEnum("LIABILITY")]
        Liability,
        [XmlEnum("NONCURRENT")]
        NonCurrentAssets,
        [XmlEnum("OTHERINCOME")]
        OtherIncome,
        [XmlEnum("OVERHEADS")]
        Overheads,
        [XmlEnum("PREPAYMENT")]
        Prepayments,
        [XmlEnum("REVENUE")]
        Revenue,
        [XmlEnum("SALES")]
        Sales,
        [XmlEnum("TERMLIAB")]
        TermLiabilities,
    }

    public enum ContactStatusType
    {
        [XmlEnum("ACTIVE")]
        Active,
        [XmlEnum("DELETED")]
        Deleted
    }

    public enum AddressTypes
    {
        [XmlEnum("POBOX")]
        POBox,
        [XmlEnum("STREET")]
        Street
    }

    public enum PhoneTypes
    {
        [XmlEnum("DEFAULT")]
        Default,
        [XmlEnum("DDI")]
        DDI,
        [XmlEnum("MOBILE")]
        Mobile,
        [XmlEnum("FAX")]
        Fax
    }

    public enum InvoiceStatusCodes
    {
        [XmlEnum("DRAFT")]
        Draft,
        [XmlEnum("SUBMITTED")]
        Submitted,
        [XmlEnum("DELETED")]
        Deleted,
        [XmlEnum("AUTHORISED")]
        Authorised,
        [XmlEnum("PAID")]
        Paid,
        [XmlEnum("VOIDED")]
        Voided,
    }

    public enum CreditNoteStatusCodes
    {
        [XmlEnum("DRAFT")]
        Draft,
        [XmlEnum("SUBMITTED")]
        Submitted,
        [XmlEnum("DELETED")]
        Deleted,
        [XmlEnum("AUTHORISED")]
        Authorised,
        [XmlEnum("PAID")]
        Paid,
        [XmlEnum("VOIDED")]
        Voided,
    }

    public enum LineAmountTypes
    {
        [XmlEnum]
        Exclusive,
        [XmlEnum]
        Inclusive,
        [XmlEnum]
        NoTax
    }

    public enum InvoiceTypes
    {
        [XmlEnum("ACCREC")]
        AccountsReceivable,
        [XmlEnum("ACCPAY")]
        AccountsPayable
    }

    public enum CreditNoteTypes
    {
        [XmlEnum("ACCRECCREDIT")]
        AccountsReceivable,
        [XmlEnum("ACCPAYCREDIT")]
        AccountsPayable
    }
}