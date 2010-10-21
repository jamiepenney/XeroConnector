using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using XeroConnector;
using XeroConnector.Interfaces;
using XeroConnector.Model;
using XeroConnector.Model.Interfaces;

namespace Specs_For_XeroSession
{
    public class When_Getting_A_Compact_Invoice
    {
        [Test]
        public void It_Should_Get_The_Id_Correctly()
        {
            Assert.That(invoice.InvoiceID, Is.EqualTo(new Guid("03778e72-d541-404a-ab9b-2757aeda76a3")));
        }

        [Test]
        public void It_Should_Get_The_Date_Correctly()
        {
            Assert.That(invoice.Date.Value.Date, Is.EqualTo(new DateTime(2009, 4, 26)));
        }

        [Test]
        public void It_Should_Get_The_DueDate_Correctly()
        {
            Assert.That(invoice.DueDate.Value.Date, Is.EqualTo(new DateTime(2009, 4, 26)));
        }

        [Test]
        public void It_Should_Get_The_Status_Correctly()
        {
            Assert.That(invoice.Status, Is.EqualTo(InvoiceStatusCodes.Submitted));
        }

        [Test]
        public void It_Should_Get_The_LineAmountTypes_Correctly()
        {
            Assert.That(invoice.LineAmountType, Is.EqualTo(LineAmountTypes.NoTax));
        }

        [Test]
        public void It_Should_Get_The_SubTotal_Correctly()
        {
            Assert.That(invoice.SubTotal, Is.EqualTo(28.50m));
        }

        [Test]
        public void It_Should_Get_The_TotalTax_Correctly()
        {
            Assert.That(invoice.TotalTax, Is.EqualTo(3.56m));
        }

        [Test]
        public void It_Should_Get_The_Total_Correctly()
        {
            Assert.That(invoice.Total, Is.EqualTo(32.06m));
        }

        [Test]
        public void It_Should_Get_The_UpdatedDate_Correctly()
        {
            Assert.That(invoice.UpdatedDate.Value.Date, Is.EqualTo(new DateTime(2009, 8, 14)));
        }

        [Test]
        public void It_Should_Get_The_CurrencyCode_Correctly()
        {
            Assert.That(invoice.CurrencyCode, Is.EqualTo("NZD"));
        }

        [Test]
        public void It_Should_Get_The_InvoiceNumber_Correctly()
        {
            Assert.That(invoice.InvoiceNumber, Is.EqualTo("OIT00542"));
        }

        [Test]
        public void It_Should_Get_The_AmountDue_Correctly()
        {
            Assert.That(invoice.AmountDue, Is.EqualTo(32.06m));
        }

        [Test]
        public void It_Should_Get_The_AmountPaid_Correctly()
        {
            Assert.That(invoice.AmountPaid, Is.EqualTo(0m));
        }

        [Test]
        public void It_Should_Get_The_AmountCredited_Correctly()
        {
            Assert.That(invoice.AmountCredited, Is.EqualTo(0m));
        }

        [Test]
        public void It_Should_Fill_The_Contact_Property()
        {
            Assert.That(invoice.Contact, Is.Not.Null);
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string invoiceXml = File.ReadAllText("Data\\CompactInvoice.xml");
            var invoiceDoc = XDocument.Parse(invoiceXml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetInvoiceRequest(Guid.Empty)).Return(invoiceDoc);

            // Act
            var session = new XeroSession(connection);

            invoice = session.GetInvoice(Guid.Empty);
        }

        private IInvoice invoice;
    }

    [TestFixture]
    public class When_Acccessing_A_LazyLoaded_Property_On_An_Invoice
    {
        [Test]
        public void It_Should_Call_Out_To_The_Web_Service()
        {
            connection.VerifyAllExpectations();
        }

        [Test]
        public void It_Should_Fill_The_Payments_Collection()
        {
            Assert.That(payments, Is.Not.Null.And.Not.Empty);
            Assert.That(payments.Count, Is.EqualTo(2));
        }

        [Test]
        public void It_Should_Fill_The_LineItems_Collection()
        {
            Assert.That(lineItems, Is.Not.Null.And.Not.Empty);
            Assert.That(lineItems.Count, Is.EqualTo(2));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string compactInvoiceXml = File.ReadAllText("Data\\CompactInvoice.xml");
            var compactInvoiceDoc = XDocument.Parse(compactInvoiceXml);

            string fullInvoiceXml = File.ReadAllText("Data\\SingleInvoice.xml");
            var fullInvoiceDoc = XDocument.Parse(fullInvoiceXml);

            connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetInvoicesRequest()).Return(compactInvoiceDoc);
            connection.Expect(c => c.MakeGetInvoiceRequest(new Guid("03778e72-d541-404a-ab9b-2757aeda76a3")))
                .Return(fullInvoiceDoc);

            var session = new XeroSession(connection);
            invoice = session.GetInvoices().First();

            // Act
            payments = invoice.Payments;
            lineItems = invoice.LineItems;
        }

        private IInvoice invoice;
        private IXeroConnection connection;
        private ICollection<Payment> payments;
        private Collection<LineItem> lineItems;
    }
}