using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Xml.Serialization;
using Castle.DynamicProxy;
using XeroConnector.Interfaces;
using XeroConnector.Model;
using XeroConnector.Model.Interfaces;
using XeroConnector.Model.Proxy;
using XeroConnector.Util;

namespace XeroConnector
{
    public class XeroSession : IXeroSession
    {
        private readonly IXeroConnection _connection;
        private static readonly ProxyGenerator ProxyGenerator = new ProxyGenerator();

        private static readonly Dictionary<Type, XmlSerializer> Serializers;

        static XeroSession()
        {
            Serializers = new Dictionary<Type, XmlSerializer>();

            Serializers[typeof(Account)] = new XmlSerializer(typeof(Account));
            Serializers[typeof(Contact)] = new XmlSerializer(typeof(Contact));
            Serializers[typeof(CreditNote)] = new XmlSerializer(typeof(CreditNote));
            Serializers[typeof(Currency)] = new XmlSerializer(typeof(Currency));
            Serializers[typeof(Invoice)] = new XmlSerializer(typeof(Invoice));
            Serializers[typeof(Organisation)] = new XmlSerializer(typeof(Organisation));
            Serializers[typeof(Payment)] = new XmlSerializer(typeof(Payment));
            Serializers[typeof(TaxRate)] = new XmlSerializer(typeof(TaxRate));
            Serializers[typeof(TrackingCategory)] = new XmlSerializer(typeof(TrackingCategory));
        }

        public XeroSession(IXeroConnection connection)
        {
            _connection = connection;
        }

        public void LoadContactDetails(IContact contact)
        {
            LazyLoadModel<Contact, IContact>(contact,
                () => _connection.MakeGetContactRequest(contact.ContactID),
                "Contacts");
        }

        public void LoadCreditNoteDetails(ICreditNote creditNote)
        {
            LazyLoadModel<CreditNote, ICreditNote>(creditNote,
                () => _connection.MakeGetCreditNoteRequest(creditNote.CreditNoteID),
                "CreditNotes");
        }

        public void GetInvoiceDetails(IInvoice invoice)
        {
            LazyLoadModel<Invoice, IInvoice>(invoice, 
                () => _connection.MakeGetInvoiceRequest(invoice.InvoiceID),
                "Invoices");
        }

        private void LazyLoadModel<TModel, TIModel>(TIModel existingModel, Func<XDocument> getModel, string elementToFind) where TModel : IModel, TIModel
        {
            var doc = getModel();
            TIModel model = GetSingleModel<TModel>(GetFirstChild(doc, elementToFind));

            PropertyCopier<TIModel, TIModel>.Copy(model, existingModel);
        }

        public Response<IAccount> GetAccount(Guid accountID)
        {
            var doc = _connection.MakeGetAccountRequest(accountID);
            IAccount model = GetSingleModel<Account>(GetFirstChild(doc, "Accounts"));
            return GetResponse(model, doc);
        }

        public Response<IEnumerable<IAccount>> GetAccounts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetAccountsRequest(modifiedAfter, whereClause, orderBy);
            IEnumerable<IAccount> models = GetMultipleModels<Account>(doc, "Accounts");
            return GetResponse(models, doc);
        }

        public Response<IContact> GetContact(string contactIdentifier)
        {
            var doc = _connection.MakeGetContactRequest(contactIdentifier);
            IContact model = GetSingleModel<Contact>(GetFirstChild(doc, "Contacts"));
            return GetResponse(model, doc);
        }

        public Response<IContact> GetContact(Guid contactID)
        {
            var doc = _connection.MakeGetContactRequest(contactID);
            IContact model = GetSingleModel<Contact>(GetFirstChild(doc, "Contacts"));
            return GetResponse(model, doc);
        }

        public Response<IEnumerable<IContact>> GetContacts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetContactsRequest(modifiedAfter, whereClause, orderBy);
            IEnumerable<IContact> models = GetMultipleModels<Contact>(doc, "Contacts");
            return GetResponse(models, doc);
        }

        public Response<ICreditNote> GetCreditNote(string creditNoteIdentifier)
        {
            var doc = _connection.MakeGetCreditNoteRequest(creditNoteIdentifier);
            ICreditNote model = GetSingleModel<CreditNote>(GetFirstChild(doc, "CreditNotes"));
            return GetResponse(model, doc);
        }

        public Response<ICreditNote> GetCreditNote(Guid creditNoteID)
        {
            var doc = _connection.MakeGetCreditNoteRequest(creditNoteID);
            ICreditNote model = GetSingleModel<CreditNote>(GetFirstChild(doc, "CreditNotes"));
            return GetResponse(model, doc);
        }

        public Response<IEnumerable<ICreditNote>> GetCreditNotes(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetCreditNotesRequest(modifiedAfter, whereClause, orderBy);
            var models = GetMultipleWrappedModels<CreditNote, ICreditNote>(doc, "CreditNotes");
            return GetResponse(models, doc);
        }

        public Response<IEnumerable<Currency>> GetCurrencies(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetCurrenciesRequest(modifiedAfter, whereClause, orderBy);
            var models = GetMultipleModels<Currency>(doc, "Currencies");
            return GetResponse(models, doc);
        }

        public Response<IInvoice> GetInvoice(Guid invoiceID)
        {
            var doc = _connection.MakeGetInvoiceRequest(invoiceID);
            IInvoice invoice = GetSingleWrappedModel<Invoice, IInvoice>(doc, "Invoices", true);
            return GetResponse(invoice, doc);
        }

        public Response<IEnumerable<IInvoice>> GetInvoices(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetInvoicesRequest(modifiedAfter, whereClause, orderBy);
            var models = GetMultipleWrappedModels<Invoice, IInvoice>(doc, "Invoices");
            return GetResponse(models, doc);
        }

        public Response<Organisation> GetOrganisation()
        {
            var doc = _connection.MakeGetOrganisationRequest();
            var model = GetSingleModel<Organisation>(GetFirstChild(doc, "Organisations"));
            return GetResponse(model, doc);
        }

        public Response<IEnumerable<ITaxRate>> GetTaxRates(string taxType = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetTaxRatesRequest(taxType, whereClause, orderBy);
            IEnumerable<ITaxRate> models = GetMultipleModels<TaxRate>(doc, "TaxRates");
            return GetResponse(models, doc);
        }

        public Response<TrackingCategory> GetTrackingCategory(Guid categoryID)
        {
            var doc = _connection.MakeGetTrackingCategoryRequest(categoryID);
            var model = GetSingleModel<TrackingCategory>(GetFirstChild(doc, "TrackingCategories"));
            return GetResponse(model, doc);

        }

        public Response<IEnumerable<TrackingCategory>> GetTrackingCategories(string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetTrackingCategoriesRequest(whereClause, orderBy);
            var models = GetMultipleModels<TrackingCategory>(doc, "TrackingCategories");
            return GetResponse(models, doc);
        }

        private Response<T> GetResponse<T>(T model, XDocument doc)
        {
            var response = new Response<T> {Result = model};

            FillResponse(response, doc);

            return response;
        }

        private static T GetWrappedModel<T>(T model)
        {
            var pgo = new ProxyGenerationOptions(new ModelHook());

            return (T)ProxyGenerator.CreateInterfaceProxyWithTargetInterface(typeof(T), model, pgo, new ModelInterceptor());
        }

        private IEnumerable<U> GetMultipleWrappedModels<T, U>(XContainer doc, string elementToFind) where T : U where U : IModel
        {
            var root = doc.Elements().First();
            if (root == null)
            {
                return new U[0];
            }

            var element = root.Element(elementToFind);
            if (element == null)
            {
                return new U[0];
            }

            var elements = element.Elements();

            IEnumerable<T> models = elements.Select(elm => (GetSingleModel<T>(elm)));

            List<U> list = models.Select(model =>
                                    {
                                        model.XeroSession = this;
                                        return GetWrappedModel<U>(model);
                                    }).ToList();

            return list;
        }

        private IEnumerable<T> GetMultipleModels<T>(XDocument doc, string elementToFind) where T : IModel
        {
            var root = doc.Elements().First();
            if (root == null)
            {
                return new T[0];
            }

            var element = root.Element(elementToFind);
            if (element == null)
            {
                return new T[0];
            }

            var elements = element.Elements();

            IEnumerable<T> models = elements.Select(elm => (GetSingleModel<T>(elm)));
            return models;
        }

        private U GetSingleWrappedModel<T, U>(XContainer doc, string elementToFind, bool isLoaded = false) where T : U where U : IModel
        {
            var model = GetSingleModel<T>(GetFirstChild(doc, elementToFind));
            model.IsLoaded = isLoaded;
            return GetWrappedModel<U>(model);
        }

        private T GetSingleModel<T>(XElement element) where T : IModel
        {
            var model = (T)Serializers[typeof(T)].Deserialize(new StringReader(element.ToString()));
            model.XeroSession = this;
            return model;
        }

        private static XElement GetFirstChild(XContainer doc, string elementToFind)
        {   
            var root = doc.Elements().FirstOrDefault();
            if (root == null) return null;

            var element = root.Element(elementToFind);
            if (element == null) return null;

            return element.Elements().FirstOrDefault();
        }

        private void FillResponse<T>(Response<T> response, XDocument doc)
        {
            var root = doc.Elements().FirstOrDefault();
            if (root == null)
            {
                response.Id = Guid.NewGuid();
                response.ProviderName = _connection.UserAgent;
                response.Status = "InvalidResponse";
                return;
            }
            
            response.Id = GetValue(root.Element("Id")).ToGuid();
            response.Status = GetValue(root.Element("Status"));
            response.ProviderName = GetValue(root.Element("ProviderName"));
            response.DateTimeUTC = GetValue(root.Element("DateTimeUTC")).As<DateTime>();
        }

        private static string GetValue(XElement element)
        {
            if (element != null) return element.Value;
            return "";
        }
    }
}