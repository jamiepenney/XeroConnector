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
    public class When_Getting_A_Detailed_Account
    {
        [Test]
        public void It_Should_Get_The_Id_Correctly()
        {
            Assert.That(account.AccountID, Is.EqualTo(new Guid("46b098a7-57d8-400c-82d9-32cc748f7d8c")));
        }

        [Test]
        public void It_Should_Get_The_Code_Correctly()
        {
            Assert.That(account.Code, Is.EqualTo("090"));
        }

        [Test]
        public void It_Should_Get_The_Name_Correctly()
        {
            Assert.That(account.Name, Is.EqualTo("HSBC Business Current A/C"));
        }

        [Test]
        public void It_Should_Get_The_AccountType_Correctly()
        {
            Assert.That(account.AccountType, Is.EqualTo(AccountTypes.Bank));
        }

        [Test]
        public void It_Should_Get_The_TaxType_Correctly()
        {
            Assert.That(account.TaxType, Is.EqualTo("NONE"));
        }

        [Test]
        public void It_Should_Get_The_EnablePaymentsToAcccount_Property_Correctly()
        {
            Assert.That(account.EnablePaymentsToAccount, Is.True);
        }

        [Test]
        public void It_Should_Get_The_BankAccountNumber_Correctly()
        {
            Assert.That(account.BankAccountNumber, Is.EqualTo("404040123456789"));
        }

        [Test]
        public void It_Should_Set_The_XeroSession_To_The_Session_That_Loaded_It()
        {
            Assert.That(account.XeroSession, Is.SameAs(session));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string accountXml = File.ReadAllText("Data\\SingleAccount.xml");
            var accountDoc = XDocument.Parse(accountXml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetAccountRequest(Guid.Empty)).Return(accountDoc);

            // Act
            session = new XeroSession(connection);
            response = session.GetAccount(Guid.Empty);
            account = response.Result;
        }

        private Response<IAccount> response;
        private IAccount account;
        private XeroSession session;
    }
}