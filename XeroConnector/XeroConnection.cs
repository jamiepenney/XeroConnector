using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using XeroConnector.Interfaces;

namespace XeroConnector
{
    public class XeroConnection : IXeroConnection
    {
        private readonly IOAuthSession _session;
        private readonly IToken _token;

        public XeroConnection(IOAuthSession session, IToken token)
        {
            _session = session;
            _token = token;
        }

        public XDocument MakeGetAccountRequest(Guid accountID)
        {
            return GetRequest("Accounts/" + accountID).ToDocument();
        }

        public XDocument MakeGetAccountsRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return GetRequest("Accounts/", whereClause, orderBy, modifiedAfter).ToDocument();
        }

        public XDocument MakeGetContactRequest(string contactIdentifier)
        {
            return GetRequest("Contacts/" + contactIdentifier).ToDocument();
        }

        public XDocument MakeGetContactRequest(Guid contactID)
        {
            return GetRequest("Contacts/" + contactID).ToDocument();
        }

        public XDocument MakeGetContactsRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return GetRequest("Contacts/", whereClause, orderBy, modifiedAfter).ToDocument();
        }

        public XDocument MakeGetCreditNoteRequest(string noteIdentifier)
        {
            return GetRequest("CreditNotes/" + noteIdentifier).ToDocument();
        }

        public XDocument MakeGetCreditNoteRequest(Guid creditNoteID)
        {
            return GetRequest("CreditNotes/" + creditNoteID).ToDocument();
        }

        public XDocument MakeGetCreditNotesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return GetRequest("CreditNotes/", whereClause, orderBy, modifiedAfter).ToDocument();
        }

        public XDocument MakeGetCurrenciesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return GetRequest("Currencies/", whereClause, orderBy, modifiedAfter).ToDocument();
        }

        public XDocument MakeGetInvoicesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return GetRequest("Invoices/", whereClause, orderBy, modifiedAfter).ToDocument();
        }

        public XDocument MakeGetInvoiceRequest(Guid invoiceID)
        {
            return GetRequest("Invoices/" + invoiceID).ToDocument();
        }

        public XDocument MakeGetOrganisationRequest()
        {
            return GetRequest("Organisation/").ToDocument();
        }

        public XDocument MakeGetTaxRatesRequest(string taxType = null, string whereClause = null, string orderBy = null)
        {
            var request = GetRequest("TaxRates/", whereClause, orderBy)
                .AddFormParameter("TaxType", taxType);
            
            return request.ToDocument();
        }

        public XDocument MakeGetTrackingCategoriesRequest(string whereClause = null, string orderBy = null)
        {
            return GetRequest("TrackingCategories/", whereClause, orderBy).ToDocument();
        }

        public XDocument MakeGetTrackingCategoryRequest(Guid categoryID)
        {
            return GetRequest("TrackingCategories/" + categoryID).ToDocument();
        }

        private IConsumerRequest GetRequest(string endpointName, string whereClause = null, string orderBy = null, DateTime? modifiedAfter = null)
        {
            return _session
                .Request()
                .ForMethod("GET")
                .ForUri(new Uri("https://api.xero.com/api.xro/2.0/" + endpointName))
                .SignWithToken(_token)
                .AddFormParameter("Where", whereClause)
                .AddFormParameter("Order", orderBy)
                .AddModifierAfterHeader(modifiedAfter);
        }
    }

    public static class CustomerRequestExtension
    {
        public static IConsumerRequest AddModifierAfterHeader(this IConsumerRequest request, DateTime? modifiedAfter)
        {
            if (modifiedAfter != null)
            {
                request = request.WithHeaders(new Dictionary<string, string> { { "If-Modified-Since", modifiedAfter.Value.ToUniversalTime().ToString("s") } });
            }

            return request;
        }

        public static IConsumerRequest AddFormParameter(this IConsumerRequest request, string name, object value)
        {
            if (value != null)
                request.Context.FormEncodedParameters[name] = Convert.ToString(value);

            return request;
        }
    }
}