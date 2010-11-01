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
    public class When_Getting_A_Compact_TaxRate
    {
        [Test]
        public void It_Should_Get_The_Name_Correctly()
        {
            Assert.That(model.Name, Is.EqualTo("15% (VAT on Capital Purchases)"));
        }

        [Test]
        public void It_Should_Get_The_TaxType_Correctly()
        {
            Assert.That(model.TaxType, Is.EqualTo("CAPEXSRINPUT"));
        }

        [Test]
        public void It_Should_Get_The_CanApplyToAssets_Correctly()
        {
            Assert.That(model.CanApplyToAssets, Is.True);
        }

        [Test]
        public void It_Should_Get_The_CanApplyToEquity_Correctly()
        {
            Assert.That(model.CanApplyToEquity, Is.True);
        }

        [Test]
        public void It_Should_Get_The_CanApplyToExpenses_Correctly()
        {
            Assert.That(model.CanApplyToExpenses, Is.True);
        }

        [Test]
        public void It_Should_Get_The_CanApplyToLiabilities_Correctly()
        {
            Assert.That(model.CanApplyToLiabilities, Is.True);
        }

        [Test]
        public void It_Should_Get_The_CanApplyToRevenue_Correctly()
        {
            Assert.That(model.CanApplyToRevenue, Is.True);
        }

        [Test]
        public void It_Should_Get_The_DisplayTaxRate_Correctly()
        {
            Assert.That(model.DisplayTaxRate, Is.EqualTo(15m));
        }

        [Test]
        public void It_Should_Get_The_EffectiveTaxRate_Correctly()
        {
            Assert.That(model.EffectiveTaxRate, Is.EqualTo(15m));
        }

        [TestFixtureSetUp]
        public void SetUp()
        {
            // Arrange
            string xml = File.ReadAllText("Data\\CompactTaxRate.xml");
            var modelDoc = XDocument.Parse(xml);

            var connection = MockRepository.GenerateMock<IXeroConnection>();
            connection.Stub(c => c.MakeGetTaxRatesRequest()).Return(modelDoc);

            // Act
            var session = new XeroSession(connection);

            response = session.GetTaxRates();
            model = response.Result.First();
        }

        private Response<IEnumerable<ITaxRate>> response;
        private ITaxRate model;
    }
}