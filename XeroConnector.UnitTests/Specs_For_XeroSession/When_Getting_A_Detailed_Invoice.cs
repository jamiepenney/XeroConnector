using System;
using System.IO;
using System.Xml.Linq;
using NUnit.Framework;
using Rhino.Mocks;
using XeroConnector;
using XeroConnector.Interfaces;
using XeroConnector.Model;
using XeroConnector.Model.Interfaces;

namespace Specs_For_XeroSession
{
    [TestFixture]
    public class When_Getting_A_Detailed_Invoice
    {
        [Test]
        public void It_Should_Get_The_Id_Correctly()
        {
            Assert.That(invoice.InvoiceID, Is.EqualTo(new Guid("243216c5-369e-4056-ac67-05388f86dc81")));
        }

        [Test]
        public void It_Should_Get_The_Reference_Correctly()
        {
            Assert.That(invoice.Reference, Is.EqualTo("City01"));
        }

        [Test]
        public void It_Should_Get_The_Date_Correctly()
        {
            Assert.That(invoice.Date.Value.Date, Is.EqualTo(new DateTime(2009, 5, 27)));
        }

        [Test]
        public void It_Should_Get_The_DueDate_Correctly()
        {
            Assert.That(invoice.DueDate.Value.Date, Is.EqualTo(new DateTime(2009, 6, 6)));
        }

        [Test]
        public void It_Should_Get_The_Status_Correctly()
        {
            Assert.That(invoice.Status, Is.EqualTo(InvoiceStatusCodes.Voided));
        }

        [Test]
        public void It_Should_Get_The_LineAmountTypes_Correctly()
        {
            Assert.That(invoice.LineAmountType, Is.EqualTo(LineAmountTypes.Inclusive));
        }

        [Test]
        public void It_Should_Get_The_SubTotal_Correctly()
        {
            Assert.That(invoice.SubTotal, Is.EqualTo(1800m));
        }

        [Test]
        public void It_Should_Get_The_TotalTax_Correctly()
        {
            Assert.That(invoice.TotalTax, Is.EqualTo(225m));
        }

        [Test]
        public void It_Should_Get_The_Total_Correctly()
        {
            Assert.That(invoice.Total, Is.EqualTo(2025m));
        }

        [Test]
        public void It_Should_Get_The_UpdatedDate_Correctly()
        {
            Assert.That(invoice.UpdatedDate.Value.Date, Is.EqualTo(new DateTime(2009, 8, 15)));
        }

        [Test]
        public void It_Should_Get_The_CurrencyCode_Correctly()
        {
            Assert.That(invoice.CurrencyCode, Is.EqualTo("NZD"));
        }

        [Test]
        public void It_Should_Get_The_InvoiceNumber_Correctly()
        {
            Assert.That(invoice.InvoiceNumber, Is.EqualTo("OIT00546"));
        }

        [Test]
        public void It_Should_Get_The_AmountDue_Correctly()
        {
            Assert.That(invoice.AmountDue, Is.EqualTo(975m));
        }

        [Test]
        public void It_Should_Get_The_AmountPaid_Correctly()
        {
            Assert.That(invoice.AmountPaid, Is.EqualTo(1050m));
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

        [Test]
        public void It_Should_Fill_The_Payments_Collection()
        {
            Assert.That(invoice.Payments.Count, Is.EqualTo(2));
        }

        [Test]
        public void It_Should_Fill_The_InvoiceLines_Collection()
        {
            Assert.That(invoice.InvoiceLines.Count, Is.EqualTo(2));
        }

        [Test]
        public void It_Should_Set_The_XeroSession_To_The_Session_That_Loaded_It()
        {
            Assert.That(invoice.XeroSession, Is.SameAs(session));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string invoiceXml = File.ReadAllText("Data\\SingleInvoice.xml");
            var invoiceDoc = XDocument.Parse(invoiceXml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetInvoiceRequest(Guid.Empty)).Return(invoiceDoc);

            // Act
            session = new XeroSession(connection);

            invoice = session.GetInvoice(Guid.Empty);
        }

        private IInvoice invoice;
        private XeroSession session;
    }
}