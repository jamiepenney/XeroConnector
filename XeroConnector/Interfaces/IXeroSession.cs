using System;
using System.Collections.Generic;
using XeroConnector.Model;
using XeroConnector.Model.Interfaces;

namespace XeroConnector.Interfaces
{
    public interface IXeroSession
    {
        Response<IAccount> GetAccount(Guid accountID);
        Response<IEnumerable<IAccount>> GetAccounts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        Response<IContact> GetContact(string contactIdentifier);
        Response<IContact> GetContact(Guid contactID);
        Response<IEnumerable<IContact>> GetContacts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        Response<ICreditNote> GetCreditNote(string noteIdentifier);
        Response<ICreditNote> GetCreditNote(Guid creditNoteID);
        Response<IEnumerable<ICreditNote>> GetCreditNotes(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        Response<IEnumerable<Currency>> GetCurrencies(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        Response<IInvoice> GetInvoice(Guid invoiceID);
        Response<IEnumerable<IInvoice>> GetInvoices(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        Response<Organisation> GetOrganisation();

        Response<IEnumerable<ITaxRate>> GetTaxRates(string taxType = null, string whereClause = null, string orderBy = null);

        Response<TrackingCategory> GetTrackingCategory(Guid categoryID);
        Response<IEnumerable<TrackingCategory>> GetTrackingCategories(string whereClause = null, string orderBy = null);

        void LoadContactDetails(IContact contact);
        void GetInvoiceDetails(IInvoice invoice);
        void LoadCreditNoteDetails(ICreditNote creditNote);
    }
}