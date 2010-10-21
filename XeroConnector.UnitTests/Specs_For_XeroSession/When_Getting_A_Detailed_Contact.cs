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
    public class When_Getting_A_Detailed_Contact
    {
        [Test]
        public void It_Should_Get_The_Id_Correctly()
        {
            Assert.That(contact.ContactID, Is.EqualTo(new Guid("bd2270c3-8706-4c11-9cfb-000b551c3f51")));
        }

        [Test]
        public void It_Should_Get_The_Status_Correctly()
        {
            Assert.That(contact.ContactStatus, Is.EqualTo(ContactStatusType.Active));
        }

        [Test]
        public void It_Should_Get_The_Name_Correctly()
        {
            Assert.That(contact.Name, Is.EqualTo("ABC Limited"));
        }

        [Test]
        public void It_Should_Get_The_FirstName_Correctly()
        {
            Assert.That(contact.FirstName, Is.EqualTo("Andrea"));
        }

        [Test]
        public void It_Should_Get_The_LastName_Correctly()
        {
            Assert.That(contact.LastName, Is.EqualTo("Dutchess"));
        }

        [Test]
        public void It_Should_Get_The_EmailAddress_Correctly()
        {
            Assert.That(contact.EmailAddress, Is.EqualTo("a.dutchess@abclimited.com"));
        }

        [Test]
        public void It_Should_Get_The_SkypeUserName_Correctly()
        {
            Assert.That(contact.SkypeUserName, Is.EqualTo("24locks.demopurposesonly"));
        }

        [Test]
        public void It_Should_Get_The_BankAccountDetails_Correctly()
        {
            Assert.That(contact.BankAccountDetails, Is.EqualTo("12346"));
        }

        [Test]
        public void It_Should_Get_The_TaxNumber_Correctly()
        {
            Assert.That(contact.TaxNumber, Is.EqualTo("1234567"));
        }

        [Test]
        public void It_Should_Get_The_AccountsReceivableTaxType_Correctly()
        {
            Assert.That(contact.AccountsReceivableTaxType, Is.EqualTo("SROUTPUT"));
        }

        [Test]
        public void It_Should_Get_The_AccountsPayableTaxType_Correctly()
        {
            Assert.That(contact.AccountsPayableTaxType, Is.EqualTo("SRINPUT"));
        }

        [Test]
        public void It_Should_Get_The_UpdatedDate_Correctly()
        {
            Assert.That(contact.UpdatedDate, Is.EqualTo(new DateTime(2009, 05, 14, 01, 44, 26, 747, DateTimeKind.Utc)));
        }

        [Test]
        public void It_Should_Get_The_IsSupplier_Correctly()
        {
            Assert.That(contact.IsSupplier, Is.EqualTo(false));
        }

        [Test]
        public void It_Should_Get_The_IsCustomer_Correctly()
        {
            Assert.That(contact.IsCustomer, Is.EqualTo(true));
        }

        [Test]
        public void It_Should_Get_The_DefaultCurrency_Correctly()
        {
            Assert.That(contact.DefaultCurrency, Is.EqualTo("GBP"));
        }

        [Test]
        public void It_Should_Get_Both_Addresses()
        {
            // Don't test the addresses themselves - that will be covered
            // in another test.
            Assert.That(contact.Addresses.Count, Is.EqualTo(2));
        }

        [Test]
        public void It_Should_Get_The_PhoneNumbers_Correctly()
        {
            Assert.That(contact.PhoneNumbers.Count, Is.EqualTo(4));
        }

        [Test]
        public void It_Should_Set_The_XeroSession_To_The_Session_That_Loaded_It()
        {
            Assert.That(contact.XeroSession, Is.SameAs(session));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string contactXml = File.ReadAllText("Data\\SingleContact.xml");
            var contactDoc = XDocument.Parse(contactXml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetContactRequest(Guid.Empty)).Return(contactDoc);

            // Act
            session = new XeroSession(connection);
            contact = session.GetContact(Guid.Empty);
        }

        private IContact contact;
        private XeroSession session;
    }
}