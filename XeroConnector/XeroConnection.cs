using System;
using System.Collections.Generic;
using System.Xml.Linq;
using DevDefined.OAuth.Consumer;
using DevDefined.OAuth.Framework;
using DevDefined.OAuth.Utility;
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

        public string UserAgent
        {
            get { return _session.ConsumerContext.UserAgent; }
        }

        public XDocument MakeGetAccountRequest(Guid accountID)
        {
            return ParseResponse(GetRequest("Accounts/" + accountID));
        }

        public XDocument MakeGetAccountsRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return ParseResponse(GetRequest("Accounts/", whereClause, orderBy, modifiedAfter));
        }

        public XDocument MakeGetContactRequest(string contactIdentifier)
        {
            return ParseResponse(GetRequest("Contacts/" + contactIdentifier));
        }

        public XDocument MakeGetContactRequest(Guid contactID)
        {
            return ParseResponse(GetRequest("Contacts/" + contactID));
        }

        public XDocument MakeGetContactsRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return ParseResponse(GetRequest("Contacts/", whereClause, orderBy, modifiedAfter));
        }

        public XDocument MakeGetCreditNoteRequest(string noteIdentifier)
        {
            return ParseResponse(GetRequest("CreditNotes/" + noteIdentifier));
        }

        public XDocument MakeGetCreditNoteRequest(Guid creditNoteID)
        {
            return ParseResponse(GetRequest("CreditNotes/" + creditNoteID));
        }

        public XDocument MakeGetCreditNotesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return ParseResponse(GetRequest("CreditNotes/", whereClause, orderBy, modifiedAfter));
        }

        public XDocument MakeGetCurrenciesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return ParseResponse(GetRequest("Currencies/", whereClause, orderBy, modifiedAfter));
        }

        public XDocument MakeGetInvoicesRequest(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            return ParseResponse(GetRequest("Invoices/", whereClause, orderBy, modifiedAfter));
        }

        public XDocument MakeGetInvoiceRequest(Guid invoiceID)
        {
            return ParseResponse(GetRequest("Invoices/" + invoiceID));
        }

        public XDocument MakeGetOrganisationRequest()
        {
            return ParseResponse(GetRequest("Organisation/"));
        }

        public XDocument MakeGetTaxRatesRequest(string taxType = null, string whereClause = null, string orderBy = null)
        {
            var request = GetRequest("TaxRates/", whereClause, orderBy)
                .AddFormParameter("TaxType", taxType);
            
            return ParseResponse(request);
        }

        public XDocument MakeGetTrackingCategoriesRequest(string whereClause = null, string orderBy = null)
        {
            return ParseResponse(GetRequest("TrackingCategories/", whereClause, orderBy));
        }

        public XDocument MakeGetTrackingCategoryRequest(Guid categoryID)
        {
            return ParseResponse(GetRequest("TrackingCategories/" + categoryID));
        }

        private XDocument ParseResponse(IConsumerRequest request)
        {
            var response = request.ToWebResponse();
            if(response.StatusCode == System.Net.HttpStatusCode.OK
                || response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return XDocument.Parse(response.ReadToEnd());
            }

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                string responseXml = string.Format(NotFoundResponseTemplate,
                                                   Guid.NewGuid(), _session.ConsumerContext.UserAgent, DateTime.UtcNow);

                return XDocument.Parse(responseXml);
            }

            throw new Exception("Unknown response status code: " + response.StatusCode);
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

        private const string NotFoundResponseTemplate =
@"<Response xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
    <Id>{0}</Id>
    <Status>NotFound</Status>
    <ProviderName>{1}</ProviderName>
    <DateTimeUTC>{2}</DateTimeUTC>
</Response>";
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