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
    public class When_Getting_A_Detailed_CreditNote
    {
        [Test]
        public void It_Should_Get_The_Reference_Correctly()
        {
            Assert.That(model.Reference, Is.EqualTo("Yr Ref W08-143"));
        }

        [Test]
        public void It_Should_Get_The_Type_Correctly()
        {
            Assert.That(model.Type, Is.EqualTo(CreditNoteTypes.AccountsPayable));
        }

        [Test]
        public void It_Should_Get_The_Date_Correctly()
        {
            Assert.That(model.Date, Is.EqualTo(new DateTime(2010, 6, 28)));
        }

        [Test]
        public void It_Should_Get_The_Status_Correctly()
        {
            Assert.That(model.Status, Is.EqualTo(CreditNoteStatusCodes.Paid));
        }

        [Test]
        public void It_Should_Get_The_LineAmountTypes_Correctly()
        {
            Assert.That(model.LineAmountTypes, Is.EqualTo(LineAmountTypes.Inclusive));
        }

        [Test]
        public void It_Should_Get_The_LineItems_Correctly()
        {
            Assert.That(model.InvoiceLines, Is.Not.Null.And.Not.Empty);
            Assert.That(model.InvoiceLines, Has.Count.EqualTo(1));
        }

        [Test]
        public void It_Should_Get_The_SubTotal_Correctly()
        {
            Assert.That(model.SubTotal, Is.EqualTo(235.33m));
        }

        [Test]
        public void It_Should_Get_The_TotalTax_Correctly()
        {
            Assert.That(model.TotalTax, Is.EqualTo(35.30m));
        }

        [Test]
        public void It_Should_Get_The_Total_Correctly()
        {
            Assert.That(model.Total, Is.EqualTo(270.63m));
        }

        [Test]
        public void It_Should_Get_The_UpdatedDateUTC_Correctly()
        {
            Assert.That(model.UpdatedDateUTC, Is.EqualTo(new DateTime(2010, 8, 14, 23, 42, 33, 367, DateTimeKind.Utc)));
        }

        [Test]
        public void It_Should_Get_The_CurrencyCode_Correctly()
        {
            Assert.That(model.CurrencyCode, Is.EqualTo("GBP"));
        }

        [Test]
        public void It_Should_Get_The_FullyPaidOnDate_Correctly()
        {
            Assert.That(model.FullyPaidOnDate, Is.EqualTo(new DateTime(2010, 6, 28)));
        }

        [Test]
        public void It_Should_Get_The_CreditNoteID_Correctly()
        {
            Assert.That(model.CreditNoteID, Is.EqualTo(new Guid("84b39b6c-7afa-4b20-88ee-1324049f531f")));
        }

        [Test]
        public void It_Should_Get_The_CreditNoteNumber_Correctly()
        {
            Assert.That(model.CreditNoteNumber, Is.EqualTo("OG laptop"));
        }

        [Test]
        public void It_Should_Set_The_XeroSession_To_The_Session_That_Loaded_It()
        {
            Assert.That(model.XeroSession, Is.SameAs(session));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string xml = File.ReadAllText("Data\\SingleCreditNote.xml");
            var modelDoc = XDocument.Parse(xml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetCreditNoteRequest(Guid.Empty)).Return(modelDoc);

            // Act
            session = new XeroSession(connection);

            model = session.GetCreditNote(Guid.Empty);
        }

        private ICreditNote model;
        private XeroSession session;
    }
}