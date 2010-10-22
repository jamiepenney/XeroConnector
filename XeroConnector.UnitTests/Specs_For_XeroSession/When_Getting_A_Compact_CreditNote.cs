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
    public class When_Getting_A_Compact_CreditNote
    {
        [Test]
        public void It_Should_Get_The_Id_Correctly()
        {
            Assert.That(model.CreditNoteID, Is.EqualTo(new Guid("201fe941-fa47-4d82-bc82-ba47ebdb1bb0")));
        }

        [Test]
        public void It_Should_Get_The_Reference_Correctly()
        {
            Assert.That(model.Reference, Is.EqualTo("Yr Ref W08-143"));
        }

        [Test]
        public void It_Should_Get_The_Type_Correctly()
        {
            Assert.That(model.Type, Is.EqualTo(CreditNoteTypes.AccountsReceivable));
        }

        [Test]
        public void It_Should_Get_The_Date_Correctly()
        {
            Assert.That(model.Date, Is.EqualTo(new DateTime(2010, 07, 24)));
        }

        [Test]
        public void It_Should_Get_The_Status_Correctly()
        {
            Assert.That(model.Status, Is.EqualTo(CreditNoteStatusCodes.Paid));
        }

        [Test]
        public void It_Should_Get_The_LineAmountTypes_Correctly()
        {
            Assert.That(model.LineAmountTypes, Is.EqualTo(LineAmountTypes.Exclusive));
        }

        [Test]
        public void It_Should_Get_The_SubTotal_Correctly()
        {
            Assert.That(model.SubTotal, Is.EqualTo(19.95m));
        }

        [Test]
        public void It_Should_Get_The_TotalTax_Correctly()
        {
            Assert.That(model.TotalTax, Is.EqualTo(2.99m));
        }

        [Test]
        public void It_Should_Get_The_Total_Correctly()
        {
            Assert.That(model.Total, Is.EqualTo(22.94m));
        }

        [Test]
        public void It_Should_Get_The_UpdatedDateUTC_Correctly()
        {
            Assert.That(model.UpdatedDateUTC, Is.EqualTo(new DateTime(2010, 08, 15, 0, 40, 38, 323, DateTimeKind.Utc)));
        }

        [Test]
        public void It_Should_Get_The_CurrencyCode_Correctly()
        {
            Assert.That(model.CurrencyCode, Is.EqualTo("GBP"));
        }

        [Test]
        public void It_Should_Get_The_FullyPaidOnDate_Correctly()
        {
            Assert.That(model.FullyPaidOnDate, Is.EqualTo(new DateTime(2010, 07, 24)));
        }

        [Test]
        public void It_Should_Get_The_CreditNoteNumber_Correctly()
        {
            Assert.That(model.CreditNoteNumber, Is.EqualTo("CN-0023"));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string creditNoteXml = File.ReadAllText("Data\\CompactCreditNote.xml");
            var creditNoteDoc = XDocument.Parse(creditNoteXml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetCreditNotesRequest()).Return(creditNoteDoc);

            // Act
            var session = new XeroSession(connection);

            model = session.GetCreditNotes().First();
        }

        private ICreditNote model;
    }

    [TestFixture]
    public class When_Acccessing_A_LazyLoaded_Property_On_CreditNote
    {
        [Test]
        public void It_Should_Call_Out_To_The_Web_Service()
        {
            connection.VerifyAllExpectations();
        }

        [Test]
        public void It_Should_Fill_The_InvoiceLines_Collection()
        {
            Assert.That(invoiceLines, Is.Not.Null.And.Not.Empty);
            Assert.That(invoiceLines.Count, Is.EqualTo(2));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string compactCreditNoteXml = File.ReadAllText("Data\\CompactCreditNote.xml");
            var compactCreditNoteDoc = XDocument.Parse(compactCreditNoteXml);

            string fullCreditNoteXml = File.ReadAllText("Data\\SingleCreditNote.xml");
            var fullCreditNoteDoc = XDocument.Parse(fullCreditNoteXml);

            connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetCreditNotesRequest()).Return(compactCreditNoteDoc);
            connection.Expect(c => c.MakeGetCreditNoteRequest(new Guid("201fe941-fa47-4d82-bc82-ba47ebdb1bb0")))
                .Return(fullCreditNoteDoc);

            var session = new XeroSession(connection);
            model = session.GetCreditNotes().First();

            // Act
            invoiceLines = model.InvoiceLines;
        }

        private ICreditNote model;
        private IXeroConnection connection;
        private Collection<LineItem> invoiceLines;
    }
}