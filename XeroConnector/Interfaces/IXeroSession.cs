using System;
using System.Collections.Generic;
using XeroConnector.Model;
using XeroConnector.Model.Interfaces;

namespace XeroConnector.Interfaces
{
    public interface IXeroSession
    {
        IAccount GetAccount(Guid accountID);
        IEnumerable<IAccount> GetAccounts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        IContact GetContact(string contactIdentifier);
        IContact GetContact(Guid contactID);
        IEnumerable<IContact> GetContacts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        ICreditNote GetCreditNote(string noteIdentifier);
        ICreditNote GetCreditNote(Guid creditNoteID);
        IEnumerable<ICreditNote> GetCreditNotes(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        IEnumerable<Currency> GetCurrencies(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        IInvoice GetInvoice(Guid invoiceID);
        IEnumerable<IInvoice> GetInvoices(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        Organisation GetOrganisation();

        IEnumerable<ITaxRate> GetTaxRates(string taxType = null, string whereClause = null, string orderBy = null);

        TrackingCategory GetTrackingCategory(Guid categoryID);
        IEnumerable<TrackingCategory> GetTrackingCategories(string whereClause = null, string orderBy = null);

        void LoadContactDetails(IContact contact);
        void GetInvoiceDetails(IInvoice invoice);
        void LoadCreditNoteDetails(ICreditNote creditNote);
    }
}