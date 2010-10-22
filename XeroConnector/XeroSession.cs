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

        public IAccount GetAccount(Guid accountID)
        {
            var doc = _connection.MakeGetAccountRequest(accountID);
            return GetSingleModel<Account>(GetFirstChild(doc, "Accounts"));
        }

        public IEnumerable<IAccount> GetAccounts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetAccountsRequest(modifiedAfter, whereClause, orderBy);
            return GetMultipleModels<Account>(doc, "Accounts");
        }

        public IContact GetContact(string contactIdentifier)
        {
            var doc = _connection.MakeGetContactRequest(contactIdentifier);
            return GetSingleModel<Contact>(GetFirstChild(doc, "Contacts"));
        }

        public IContact GetContact(Guid contactID)
        {
            var doc = _connection.MakeGetContactRequest(contactID);
            return GetSingleModel<Contact>(GetFirstChild(doc, "Contacts"));
        }

        public IEnumerable<IContact> GetContacts(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetContactsRequest(modifiedAfter, whereClause, orderBy);
            return GetMultipleModels<Contact>(doc, "Contacts");
        }

        public ICreditNote GetCreditNote(string creditNoteIdentifier)
        {
            var doc = _connection.MakeGetCreditNoteRequest(creditNoteIdentifier);
            return GetSingleModel<CreditNote>(GetFirstChild(doc, "CreditNotes"));
        }

        public ICreditNote GetCreditNote(Guid creditNoteID)
        {
            var doc = _connection.MakeGetCreditNoteRequest(creditNoteID);
            return GetSingleModel<CreditNote>(GetFirstChild(doc, "CreditNotes"));
        }

        public IEnumerable<ICreditNote> GetCreditNotes(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetCreditNotesRequest(modifiedAfter, whereClause, orderBy);
            return GetMultipleWrappedModels<CreditNote, ICreditNote>(doc, "CreditNotes");
        }

        public IEnumerable<Currency> GetCurrencies(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetCurrenciesRequest(modifiedAfter, whereClause, orderBy);
            return GetMultipleModels<Currency>(doc, "Currencies");
        }

        public IInvoice GetInvoice(Guid invoiceID)
        {
            var doc = _connection.MakeGetInvoiceRequest(invoiceID);
            IInvoice invoice = GetSingleWrappedModel<Invoice, IInvoice>(doc, "Invoices", true);
            return invoice;
        }

        public IEnumerable<IInvoice> GetInvoices(DateTime? modifiedAfter = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetInvoicesRequest(modifiedAfter, whereClause, orderBy);
            return GetMultipleWrappedModels<Invoice, IInvoice>(doc, "Invoices");
        }

        public Organisation GetOrganisation()
        {
            var doc = _connection.MakeGetOrganisationRequest();
            return GetSingleModel<Organisation>(GetFirstChild(doc, "Organisations"));
        }

        public IEnumerable<ITaxRate> GetTaxRates(string taxType = null, string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetTaxRatesRequest(taxType, whereClause, orderBy);
            return GetMultipleModels<TaxRate>(doc, "TaxRates");
        }

        public TrackingCategory GetTrackingCategory(Guid categoryID)
        {
            var doc = _connection.MakeGetTrackingCategoryRequest(categoryID);
            return GetSingleModel<TrackingCategory>(GetFirstChild(doc, "TrackingCategories"));
        }

        public IEnumerable<TrackingCategory> GetTrackingCategories(string whereClause = null, string orderBy = null)
        {
            var doc = _connection.MakeGetTrackingCategoriesRequest(whereClause, orderBy);
            return GetMultipleModels<TrackingCategory>(doc, "TrackingCategories");
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
                return new U[0];

            var element = root.Element(elementToFind);
            if (element == null) return new U[0];

            var elements = element.Elements();

            IEnumerable<T> models = elements.Select(elm => (GetSingleModel<T>(elm)));

            return models.Select(model =>
            {
                model.XeroSession = this;
                return GetWrappedModel<U>(model);
            }).ToList();
        }

        private IEnumerable<T> GetMultipleModels<T>(XDocument doc, string elementToFind) where T : IModel
        {
            var root = doc.Elements().First();
            if (root == null)
                return new T[0];

            var element = root.Element(elementToFind);
            if (element == null) return new T[0];

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
    }
}