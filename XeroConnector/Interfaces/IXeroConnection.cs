using System;
using System.Xml.Linq;

namespace XeroConnector.Interfaces
{
    public interface IXeroConnection
    {
        XDocument MakeGetAccountRequest(Guid accountID);
        XDocument MakeGetAccountsRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        XDocument MakeGetContactRequest(string contactIdentifier);
        XDocument MakeGetContactRequest(Guid contactID);
        XDocument MakeGetContactsRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        XDocument MakeGetCreditNoteRequest(string noteIdentifier);
        XDocument MakeGetCreditNoteRequest(Guid creditNoteID);
        XDocument MakeGetCreditNotesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        XDocument MakeGetCurrenciesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        XDocument MakeGetInvoiceRequest(Guid invoiceID);
        XDocument MakeGetInvoicesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null);

        XDocument MakeGetOrganisationRequest();

        XDocument MakeGetTaxRatesRequest(string taxType = null, string whereClause = null, string orderBy = null);

        XDocument MakeGetTrackingCategoryRequest(Guid categoryID);
        XDocument MakeGetTrackingCategoriesRequest(string whereClause = null, string orderBy = null);
    }
}